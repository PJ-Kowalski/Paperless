using CommonDatabase.Data;
using CommonDatabase.Libs;
using CommonDatabase.Tools;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CommonDatabase.Data.Operator;

namespace CommonDatabase
{
    public class CommonDbAccess
    {
        protected static int APP_ID = 91;
        protected static string connectionString;
        public static DateTime? AddTemporaryBadge(Operator op, DateTime timestamp, Operator creator)
        {
            DateTime? ret = null;
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"insert into temporary_badge (operator, creation_date, generated_by, expiration_date) values (:acpno, :timestamp, :creator, :expiration_date)";
                cmd.BindByName = true;

                cmd.Parameters.Add("acpno", op.ACPno);
                cmd.Parameters.Add("creator", creator.ACPno);
                cmd.Parameters.Add("timestamp", timestamp);
                cmd.Parameters.Add("expiration_date", timestamp.AddHours(12));

                var sth = cmd.ExecuteNonQuery();
                if (sth == 1)
                {
                    ret = timestamp.AddHours(12);
                }
                con.Close();
            }
            return ret;
        }
        public static DateTime? CheckTemporaryBadge(string acpno, DateTime timestamp)
        {
            DateTime? ret = null;
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"select max(expiration_date) from TEMPORARY_BADGE where operator = :acpno and expiration_date > sysdate"; //@"select max(expiration_date) from TEMPORARY_BADGE where operator = :acpno and creation_date = :timestamp and expiration_date > sysdate";
                cmd.BindByName = true;

                cmd.Parameters.Add("acpno", acpno);
                cmd.Parameters.Add("timestamp", timestamp);

                var sth = cmd.ExecuteScalar();
                if (sth is DateTime)
                {
                    return new DateTime?((DateTime)sth);
                }
                con.Close();
            }
            
            return ret;
        }

        public static bool SaveTabletBarcode(Tablet tablet, string barcode)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"update tablets set barcode = :barcode where id = :id";
                cmd.BindByName = true;

                cmd.Parameters.Add("barcode", tablet.Barcode);
                cmd.Parameters.Add("id", tablet.Id);

                var ret = cmd.ExecuteNonQuery();
                if (ret == 1)
                {
                    tablet.Barcode = barcode;
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
        }

        public static void Setup(string connectionString)
        {
            CommonDbAccess.connectionString = connectionString;
        }
        public static List<Alert> GetAlertsForLocation(string location, DateTime from, decimal? id = null)
        {
            List<Alert> ret = new List<Alert>();
            string fromDate = from.ToString("dd/MM/yyyy hh:mm:ss");
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"
                    select 
                        a.id aid, a.creation_date, a.alert_type, a.content, a.created_by, a.ack_by, a.ack_date,
                        a.source_name,
                        t.id tid,
                        lau.firstname created_by_firstname, lau.lastname created_by_lastname, la.roleid created_by_roleid, lr.rolename created_by_rolename,
                        lau2.firstname ack_by_firstname, lau2.lastname ack_by_lastname, la2.roleid ack_by_roleid, lr2.rolename ack_by_rolename,
                        a.location
                    from
                        alerts a
                    left join
                        tablets t on a.tablet_id = t.id
                    
                    left join
                        login_all_users lau on a.created_by = lau.capacno
                    left join
                        login_access la on la.programid = :appId and la.capacno = lau.capacno
                    left join
                        login_roles lr on la.roleid = lr.roleid
                    
                    left join
                        login_all_users lau2 on a.ack_by = lau2.capacno
                    left join
                        login_access la2 on la2.programid = :appId and la2.capacno = lau2.capacno
                    left join
                        login_roles lr2 on la2.roleid = lr2.roleid

                    where
                        a.location = :location
                        and (a.creation_date >= TO_DATE(:fromCreationTime,'DD-MM-YY HH12:MI:SS') or a.ack_by is null)
                        
                    order by
                        a.ack_date desc nulls first,
                        a.creation_date desc
                    ";
                cmd.BindByName = true;

                cmd.Parameters.Add("appId", APP_ID);
                cmd.Parameters.Add("fromCreationTime", fromDate);
                cmd.Parameters.Add("location", location);
                cmd.Parameters.Add("id", id);

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var tmp = new Alert();

                        tmp.Id = (decimal)r["aid"];
                        tmp.CreationDate = (DateTime)r["creation_date"];
                        if (!r.IsDBNull(r.GetOrdinal("created_by")))
                        {
                            tmp.CreatedBy = r["created_by"].ToString();
                                
                            //    new Operator()
                            //{
                            //    ACPno = r["created_by"].ToString(),
                            //    FirstName = r["created_by_firstname"].ToString(),
                            //    LastName = r["created_by_lastname"].ToString(),

                            //    PositionName = r["created_by_rolename"].ToString()
                            //};
                            //tmp.CreatedBy.SetPositionEnumFromRoleId((decimal)r["created_by_roleid"]);
                            //tmp.CreatedBy.Photo = PhotoDownloader.GetPhoto(tmp.CreatedBy.Name); //was GetPhotoCashe
                        }
                        tmp.AckDate = null;
                        if (!r.IsDBNull(r.GetOrdinal("ack_date")))
                        {
                            tmp.AckDate = (DateTime)r["ack_date"];
                        }
                        if (!r.IsDBNull(r.GetOrdinal("ack_by")))
                        {
                            tmp.AckBy = r["ack_by"].ToString();
                            //    new Operator()
                            //{
                            //    ACPno = r["ack_by"].ToString(),
                            //    FirstName = r["ack_by_firstname"].ToString(),
                            //    LastName = r["ack_by_lastname"].ToString(),

                            //    PositionName = r["ack_by_rolename"].ToString()
                            //};
                            //tmp.AckBy.SetPositionEnumFromRoleId((decimal)r["ack_by_roleid"]);
                            //tmp.AckBy.Photo = PhotoDownloader.GetPhoto(tmp.AckBy.Name); //was GetPhotoCashe
                        }
                        tmp.AlertType = r["alert_type"].ToString();
                        tmp.Content = r["content"].ToString();
                        tmp.TabletId = r.IsDBNull(r.GetOrdinal("tid")) ? null : new decimal?((decimal)r["tid"]);
                        tmp.SourceName = r["source_name"].ToString();
                        tmp.Location = r["location"].ToString();
                        ret.Add(tmp);
                    }
                con.Close();
                }
                return ret;
            }
        }

        public static Alert AckAlert(Alert payload, Operator who)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"update alerts set ack_by = :ack_by, ack_date = sysdate where id = :id and ack_by is null and ack_date is null";
                cmd.BindByName = true;

                cmd.Parameters.Add("ack_by", who.ACPno);
                cmd.Parameters.Add("id", payload.Id);

                var ret = cmd.ExecuteNonQuery();
                con.Close();
                return GetAlertsForLocation(payload.Location, payload.CreationDate, new decimal?(payload.Id)).FirstOrDefault();
            }
        }

        public static void AddAlert(int tabletId, string alertType, string alertText)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"PAPERLESS.AddAlert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                cmd.Parameters.Add("tablet_id_par", tabletId);
                cmd.Parameters.Add("alert_name_par", alertType);
                cmd.Parameters.Add("alert_text_par", alertText);

                var ret = cmd.ExecuteNonQuery();
                con.Close();
                
            }
        }

        public static List<TabletEvent> GetHistoryForLocation(string location, DateTime from, DateTime to)
        {
            List<TabletEvent> ret = new List<TabletEvent>();
            if (from >= to)
            {
                return ret;
            }
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();
                //to have all events in it region
                string locationQuery;
                if (location == "IT")
                {
                    locationQuery = null;
                }
                else
                {
                    locationQuery = "e.location = :location and";
                }

                cmd.CommandText = $@"
                    select 
                        e.creation_date, e.event_type, e.content, e.created_by,
                        e.tablet_name,
                        t.id,
                        lau.firstname created_by_firstname, lau.lastname created_by_lastname, la.roleid created_by_roleid, lr.rolename created_by_rolename,
                        e.location
                    from
                        events e
                    left join
                        tablets t on e.tablet_id = t.id
                    left join
                        login_all_users lau on e.created_by = lau.capacno
                    left join
                        login_access la on la.programid = :appId and la.capacno = lau.capacno
                    left join
                        login_roles lr on la.roleid = lr.roleid
                    where
                        {locationQuery}
                        e.creation_date >= :fromCreationTime
                        and e.creation_date <= :toCreationTime

                    order by
                        e.creation_date desc
                    ";

                cmd.BindByName = true;
                
                cmd.Parameters.Add("appId", APP_ID);
                cmd.Parameters.Add("fromCreationTime", from);
                cmd.Parameters.Add("toCreationTime", to);
                cmd.Parameters.Add("location", location);

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var tmp = new TabletEvent();

                        tmp.CreationDate = (DateTime)r["creation_date"];
                        tmp.CreatedBy = r["created_by"].ToString();
                        //    new Operator()
                        //{
                        //    ACPno = r["created_by"].ToString(),
                        //    FirstName = r["created_by_firstname"].ToString(),
                        //    LastName = r["created_by_lastname"].ToString(),

                        //    PositionName = r["created_by_rolename"].ToString()
                        //};
                        //tmp.CreatedBy.SetPositionEnumFromRoleId((decimal)r["created_by_roleid"]);
                        //tmp.CreatedBy.Photo = PhotoDownloader.GetPhotoCashe(tmp.CreatedBy.Name);

                        tmp.EventType = r["event_type"].ToString();
                        tmp.Content = r["content"].ToString();
                        tmp.TabletId = r.IsDBNull(r.GetOrdinal("id")) ? null : new decimal?((decimal)r["id"]);
                        tmp.SourceName = r["tablet_name"].ToString();
                        tmp.Location = r["location"].ToString();
                        ret.Add(tmp);
                    }
                con.Close();
                }
                return ret;
            }
        }
        public static List<TabletEvent> GetHistoryForTablets(IEnumerable<Tablet> tablet_list, DateTime from, DateTime to)
        {
            List<TabletEvent> ret = new List<TabletEvent>();
            if (!tablet_list.Any())
            {
                return ret;
            }
            if (from >= to)
            {
                return ret;
            }
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"
                    select 
                        e.creation_date, e.event_type, e.content, e.created_by,
                        e.tablet_name,
                        t.id,
                        lau.firstname created_by_firstname, lau.lastname created_by_lastname, la.roleid created_by_roleid, lr.rolename created_by_rolename,
                        e.location
                    from
                        events e
                    left join
                        tablets t on e.tablet_id = t.id
                    left join
                        login_all_users lau on e.created_by = lau.capacno
                    left join
                        login_access la on la.programid = :appId and la.capacno = lau.capacno
                    left join
                        login_roles lr on la.roleid = lr.roleid
                    where
                        t.id = :tablet_ids
                        and e.creation_date >= :fromCreationTime
                        and e.creation_date <= :toCreationTime
                    order by
                        e.creation_date
                    ";
                cmd.BindByName = true;
                
                cmd.Parameters.Add("appId", APP_ID);
                cmd.Parameters.Add("fromCreationTime", from);
                cmd.Parameters.Add("toCreationTime", to);
                cmd.Parameters.Add(new OracleParameter("tablet_ids", OracleDbType.Decimal));//no value yet, will be provided later, in loop

                /*
                 ORA-01484: 
                  var ids = x.Select(y => y.Id).ToArray();

                OracleParameter parameter = new OracleParameter
                {
                    ParameterName = "tablet_ids",
                    OracleDbType = OracleDbType.Int32,
                    CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                    Size = ids.Count(), 
                    Value = ids,
                    Direction = System.Data.ParameterDirection.Output
                };
                cmd.Parameters.Add(parameter);*/
                //decimal xxx = 0;
                //cmd.Parameters.Add("tablet_ids", xxx);

                foreach (var id in tablet_list.Select(x => x.Id))
                {
                    cmd.Parameters["tablet_ids"].Value = id;

                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            var tmp = new TabletEvent();

                            tmp.CreationDate = (DateTime)r["creation_date"];
                            tmp.CreatedBy = r["created_by"].ToString();
                            //    new Operator()
                            //{
                            //    ACPno = r["created_by"].ToString(),
                            //    FirstName = r["created_by_firstname"].ToString(),
                            //    LastName = r["created_by_lastname"].ToString(),

                            //    PositionName = r["created_by_rolename"].ToString()
                            //};
                            //tmp.CreatedBy.SetPositionEnumFromRoleId((decimal)r["created_by_roleid"]);
                            //tmp.CreatedBy.Photo = PhotoDownloader.GetPhoto(tmp.CreatedBy.Name);

                            tmp.EventType = r["event_type"].ToString();
                            tmp.Content = r["content"].ToString();
                            tmp.TabletId = r.IsDBNull(r.GetOrdinal("id")) ? null : new decimal?((decimal)r["id"]);
                            tmp.SourceName = r["tablet_name"].ToString();
                            tmp.Location = r["location"].ToString();
                            ret.Add(tmp);
                        }
                    //con.Close();
                    }
                }
                return ret;
            }
        }

        public static bool RegisterEvent(Tablet myself, string user, string event_type, string reason, OracleTransaction trans)
        {
            var cmd = new OracleCommand(@"
                insert into events
                    (creation_date, created_by, event_type, content, tablet_id, tablet_name,location ) 
                values
                    (sysdate, :username, :event_type, :content, :tablet_id, :tablet_name, :location)
                ", trans.Connection);
            cmd.BindByName = true;

            cmd.Parameters.Add("username", user);
            cmd.Parameters.Add("event_type", event_type);
            cmd.Parameters.Add("content", reason);
            cmd.Parameters.Add("tablet_id", myself?.Id);//?
            cmd.Parameters.Add("tablet_name", myself == null ? Dns.GetHostName() : myself.WindowsName);//?
            cmd.Parameters.Add("location", myself.Location);

            return cmd.ExecuteNonQuery() == 1;
        }
        public static bool RegisterEvent(Tablet myself, string user, string event_type, string reason)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();
                OracleTransaction trans = con.BeginTransaction();
                if (RegisterEvent(myself, user, event_type, reason, trans))
                {
                    trans.Commit();
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
        }
        public static bool LockT(OracleTransaction trans, Tablet myself, LockType lockType, string who, string why, DateTime when)
        {
            if (!RegisterEvent(myself, who, "LOCK", why, trans))
            {
                return false;
            }
            var cmd = new OracleCommand(@"
                update tablets 
                set
                    locked_by = :username, locked_on = :when, lock_type = :lock_type, locked_reason = :why, last_change = sysdate
                where
                    id = :tablet_id
                ", trans.Connection);
            cmd.BindByName = true;

            cmd.Parameters.Add("username", who);
            cmd.Parameters.Add("lock_type", lockType.Name);
            cmd.Parameters.Add("tablet_id", myself.Id);
            cmd.Parameters.Add("when", when);
            cmd.Parameters.Add("why", why);

            var ret = cmd.ExecuteNonQuery();
            if (ret != 1)
            {
                return false;
            }

            return true;
        }
        public static bool Lock(Tablet myself, LockType lockType, string who, string why, DateTime when)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();
                OracleTransaction trans = con.BeginTransaction();

                if (!LockT(trans, myself, lockType, who, why, when))
                {
                    trans.Rollback();
                    con.Close();
                    return false;
                }

                trans.Commit();
                con.Close();
                return true;
            }
        }
        public static bool PassTheDevice(Tablet myself, Operator to, bool shouldlock, string lockReason, string lockType)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();
                OracleTransaction trans = con.BeginTransaction();

                if (!RegisterEvent(myself, to.ACPno, "TABLET_RECEIVED", null, trans))
                {
                    trans.Rollback();
                    return false;
                }
                var cmd = new OracleCommand(@"
                    update tablets 
                    set
                        responsible = :username, last_change = sysdate,
                        lock_type = :lockType
                    where
                        id = :tablet_id
                    ", con);
                cmd.BindByName = true;

                cmd.Parameters.Add("username", to.ACPno);
                cmd.Parameters.Add("tablet_id", myself.Id);
                cmd.Parameters.Add("lockType", lockType);

                var ret = cmd.ExecuteNonQuery();
                if (ret != 1)
                {
                    trans.Rollback();
                    return false;
                }
                if (shouldlock)
                {
                    if (!LockT(trans, myself, LockType.DAMAGE_LOCK, to.ACPno, lockReason, DateTime.Now))
                    {
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                con.Close();
                return true;
            }
        }
        static public void RefreshOnPremiseInfo(Operator op)
        {
            if (DateTime.Now - op.OnPremiseLastRefresh > TimeSpan.FromMinutes(5))
            {
                using (OracleConnection con = new OracleConnection(connectionString))
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    
                    cmd.CommandText = @"
                    select
                        ou.certainity, ou.alliance_id, ou.gate, ou.region, ou.gatetime
                    from
                        onpremise_users ou
                    where
                        ou.capacno = :acpNo
                    order by
                        certainity desc
                    ";

                    cmd.BindByName = true;

                    cmd.Parameters.Add("acpNo", op.ACPno);

                    using (var r = cmd.ExecuteReader())
                    {
                        op.OnPremise.Clear();
                        while (r.Read())
                        {
                            if (!r.IsDBNull(r.GetOrdinal("certainity")))
                            {
                                OnPremiseInfo info = new OnPremiseInfo();

                                info.AllianceId = (long)r["alliance_id"];
                                info.Certainity = (decimal)r["certainity"];
                                info.Gate = r["gate"].ToString();
                                info.Region = r["region"].ToString();
                                info.GateTime = (DateTime)r["gatetime"];

                                op.OnPremise.Add(info);
                                op.OnPremiseLastRefresh = DateTime.Now;
                            }
                        }
                    }
                    //con.Close();
                }
            }
        }
        static public Operator GetOperator(string acpNo, string location, string pass = null)
        {
            
            Operator ret = null;
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"
                    select
                        la.capacno, la.location, lau.firstname, lau.lastname, lau.orgcode, la.roleid, rolename, 
                        ou.certainity, ou.alliance_id, ou.gate, ou.region, ou.gatetime,
                        lp.pass_hash
                    from
                        login_access la
                    join
                        login_all_users lau on lau.capacno = la.capacno
                    join
                        login_roles lr on lr.roleid = la.roleid
                    left join
                        onpremise_users ou on la.capacno = ou.capacno
                    left join
                        login_password lp on lp.capacno = la.capacno
                    where
                        la.programid = :appId
                        and la.capacno = :acpNo
                        and (
                            la.location = :location
                            or :location is null
                        )
                    order by
                        certainity desc
                    ";

                cmd.BindByName = true;

                cmd.Parameters.Add("acpNo", acpNo);
                cmd.Parameters.Add("appId", APP_ID);
                cmd.Parameters.Add("location", location);

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        if (ret == null)
                        {
                            ret = new Operator();
                            ret.ACPno = acpNo;
                            ret.FirstName = r["firstname"].ToString();
                            ret.LastName = r["lastname"].ToString();
                            ret.SetPositionEnumFromRoleId((decimal)r["roleid"]);
                            ret.PositionName = r["rolename"].ToString();
                            ret.Location = r["location"].ToString();
                            ret.OrgCode = r["orgcode"].ToString();

                            var hash = r["pass_hash"].ToString();
                            if (hash != "")
                            {
                                ret.PasswordVerified = SecurePasswordHasher.Verify(pass, hash) ? PasswordVerificationResult.OK : PasswordVerificationResult.WrongPassword;
                            }
                            else
                            {
                                ret.PasswordVerified = PasswordVerificationResult.NoPasswordSet;
                            }
                        }
                        if (!r.IsDBNull(r.GetOrdinal("certainity")))
                        {
                            OnPremiseInfo info = new OnPremiseInfo();

                            info.AllianceId = (long)r["alliance_id"];
                            info.Certainity = (decimal)r["certainity"]; 
                            info.Gate = r["gate"].ToString();
                            info.Region = r["region"].ToString();
                            info.GateTime = (DateTime)r["gatetime"];

                            ret.OnPremise.Add(info);
                            ret.OnPremiseLastRefresh = DateTime.Now;
                        }
                    }
                con.Close();
                }
                if (ret != null)
                {
                    ret.Photo = PhotoDownloader.GetPhoto(ret.Name);
                }
                
                return ret;
            }
        }

        public static string GetACPLocation(string acp)
        {
            string location = null;
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"
                    select
                        LOACTION
                    from
                        LOGIN_ACCESS
                    where
                        capacno = :acpNo
                    ";

                cmd.BindByName = true;

                cmd.Parameters.Add("acpNo", acp);

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        location = r["LOCATION"].ToString();

                    }
                }
                return location;
            }
        }

        public static bool DeleteLocation(Location loc)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();
                var trans = con.BeginTransaction();
                cmd.BindByName = true;

                

                cmd.CommandText = @"
                    delete from locations where name = :name and position = :position
                ";

                cmd.Parameters.Add("name", loc.Name);
                cmd.Parameters.Add("position", loc.Position);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    trans.Commit();
                    con.Close();
                    return true;
                }
                else
                {
                    trans.Rollback();
                    con.Close();
                    return false;
                }
            }
        }

        public static bool UpdateLocation(Location loc, string new_name, int new_position)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();
                var trans = con.BeginTransaction();

                cmd.BindByName = true;

                

                cmd.CommandText = @"
                    update locations set name = :new_name, position = :new_position where name = :name and position = :position
                ";

                cmd.Parameters.Add("name", loc.Name);
                cmd.Parameters.Add("position", loc.Position);
                cmd.Parameters.Add("new_name", new_name);
                cmd.Parameters.Add("new_position", (decimal)new_position);

                var sth = cmd.ExecuteNonQuery();
                if (sth == 1)
                {
                    trans.Commit();
                    con.Close();
                    return true;
                }
                else
                {
                    trans.Rollback();
                    con.Close();
                    return false;
                }
            }
        }

        public static bool AddLocation(string name, int position)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                cmd.BindByName = true;

                con.Open();

                cmd.CommandText = @"
                    insert into locations (name, position) values (:name, :position)
                ";

                cmd.Parameters.Add("name", name);
                cmd.Parameters.Add("position", position);

                con.Close();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public static bool SetOperatorPassword(string acpno, string pass)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                cmd.BindByName = true;

                con.Open();

                cmd.CommandText = @"
                    MERGE INTO login_password lp
                    USING(SELECT :capacno AS capacno, :hash AS pass_hash FROM DUAL) src
                    ON (lp.capacno = src.capacno)
                    WHEN MATCHED THEN
                        UPDATE SET pass_hash = src.pass_hash
                    WHEN NOT MATCHED THEN
                        INSERT(capacno, pass_hash)
                        VALUES(src.capacno, src.pass_hash)
                ";

                var pass_hash = SecurePasswordHasher.Hash(pass);

                cmd.Parameters.Add("capacno", acpno);
                cmd.Parameters.Add("hash", pass_hash);

                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public static List<PositionEnum> GetPositions()
        {
            List<PositionEnum> ret = new List<PositionEnum>();
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                try
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = @"
                    select
                        roleid, rolename
                    from
                        login_roles
                    order by
                        roleid desc
                ";

                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            ret.Add((PositionEnum)(decimal)r["roleid"]);
                            //TODO: refactor by PositionEnum przestal istniec.
                        }
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                }
                return ret;
            }
        }

        public static List<Operator> GetOperators(string location, int minimumPosition)
        {
            List<Operator> ret = new List<Operator>();
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"
                   select
                        lau.capacno, lau.firstname, lau.lastname, lau.orgcode,
                        la.roleid, la.location, 
                        lr.rolename
                    from
                        login_all_users lau
                    join
                        login_access la on la.programid = :programid and la.capacno = lau.capacno
                    join
                        login_roles lr on la.roleid = lr.roleid
                    where
                        la.location = :location
                        and lr.roleid >= :position
                    order by
                        lau.capacno
                    ";

                cmd.BindByName = true;
                
                cmd.Parameters.Add("programid", APP_ID);
                cmd.Parameters.Add("location", location);
                cmd.Parameters.Add("position", minimumPosition);

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var tmp = new Operator();
                        tmp.ACPno = r["capacno"].ToString();
                        tmp.FirstName = r["firstname"].ToString();
                        tmp.LastName = r["lastname"].ToString();
                        tmp.OrgCode = r["orgcode"].ToString();
                        tmp.SetPositionEnumFromRoleId((decimal)r["roleid"]);
                        tmp.PositionName = r["rolename"].ToString();
                        tmp.Location = r["location"].ToString();
                        ret.Add(tmp);
                    }
                }
                /*Parallel.ForEach(ret, new Action<Operator>((op)=> {
                    op.Photo = PhotoDownloader.GetPhoto(op.Name);
                }));*/
                con.Close();
                return ret;
            }
        }
        public static bool AddOperatorToLocation(string location, string acpno, PositionEnum position)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                cmd.BindByName = true;

                con.Open();

                cmd.CommandText = @"
                insert into
                    login_access (programid, roleid, capacno, location)
                values (
                    :programid,
                    :roleid,
                    :capacno,
                    :location
                )";

                cmd.Parameters.Add("programid", APP_ID);
                cmd.Parameters.Add("roleid", (int)position);
                cmd.Parameters.Add("capacno", acpno);
                cmd.Parameters.Add("location", location);

                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
        }
        public static bool RemoveOperatorFromLocation(string location, Operator x)
        {
            
                using (OracleConnection con = new OracleConnection(connectionString))
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = @"delete from login_access where capacno = :capacno and location = :location";

                    cmd.BindByName = true;

                    cmd.Parameters.Add("capacno", x.ACPno);
                    cmd.Parameters.Add("location", location);

                    return cmd.ExecuteNonQuery() == 1;
                } 
            
        }

        public static List<string> CheckIfOperatorIsResponsibleForTablets(Operator x)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                List<string> tabletsList =new List<string>();
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = $@"SELECT  ID, WINDOWS_NAME FROM PAPERLESS.TABLETS WHERE RESPONSIBLE='{x.ACPno}' OR LOCKED_BY='{x.ACPno}'";

                cmd.BindByName = true;

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                       tabletsList.Add(r["windows_name"].ToString());  
                    }
                con.Close();
                }
                return tabletsList;

            }
        }
        static public Operator GetSomeone(string acpNo)
        {
            Operator ret = null;
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"
                   select
                        lau.capacno, lau.firstname, lau.lastname, lau.orgcode,
                        ou.certainity, ou.alliance_id, ou.gate, ou.region, ou.gatetime
                    from
                        login_all_users lau
                    left join
                        onpremise_users ou on lau.capacno = ou.capacno
                    where
                        lau.capacno = :acpNo
                    order by
                        certainity desc
                    ";

                cmd.BindByName = true;

                cmd.Parameters.Add("acpNo", acpNo);

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        if (ret == null)
                        {
                            ret = new Operator();
                            ret.ACPno = acpNo;
                            ret.FirstName = r["firstname"].ToString();
                            ret.LastName = r["lastname"].ToString();
                            ret.OrgCode = r["orgcode"].ToString();
                        }
                        if (!r.IsDBNull(r.GetOrdinal("certainity")))
                        {
                            OnPremiseInfo info = new OnPremiseInfo();

                            info.AllianceId = (long)r["alliance_id"];
                            info.Certainity = (decimal)r["certainity"];
                            info.Gate = r["gate"].ToString();
                            info.Region = r["region"].ToString();
                            info.GateTime = (DateTime)r["gatetime"];

                            ret.OnPremise.Add(info);
                            ret.OnPremiseLastRefresh = DateTime.Now;
                        }
                    }
                }
                if (ret != null)
                {
                    ret.Photo = PhotoDownloader.GetPhoto(ret.Name);
                }
                con.Close();
                return ret;
            }
        }
        static public Location[] GetLocationsForSomeone(Operator op)
        {
            if (op.IsAllowed(PositionEnum.Service))
            {
                return GetAllLocations();
            }
            else
            {
                List<Location> ret = new List<Location>();
                using (OracleConnection con = new OracleConnection(connectionString))
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = @"
                    select distinct 
                        l.name,
                        l.position
                    from
                        locations l
                    join
                        login_access la on l.name = la.location
                    where
                        la.capacno = :capacno
                    order by position
                    ";
                    cmd.BindByName = true;

                    cmd.Parameters.Add("capacno", op.ACPno);

                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            ret.Add(new Location()
                            {
                                Name = r["name"].ToString(),
                                Position = (int)(decimal)r["position"]
                            });
                        }
                    }
                    con.Close();
                    return ret.ToArray();
                }
            }
        }
        static public Location[] GetAllLocations()
        {
            List<Location> ret = new List<Location>();
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"
                    select distinct name, position from locations order by position
                    ";
                cmd.BindByName = true;

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        ret.Add(new Location() { 
                            Name = r["name"].ToString(),
                            Position = (int)(decimal)r["position"]
                        });
                    }
                }
                con.Close();
                return ret.ToArray();
            }
        }
        static public List<Tablet> GetTablets(string location = null, Operator op = null)
        {
            List<Tablet> ret = new List<Tablet>();
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"
                    select 
                        t.id, t.windows_name, t.creation_date, t.last_change, t.responsible, t.location, t.last_change, t.locked_by, t.locked_on, t.locked_reason, t.barcode, t.lock_type,
                        lau.firstname responsible_firstname, lau.lastname responsible_lastname, la.roleid responsible_roleid, lr.rolename responsible_rolename,
                        lau2.firstname locked_by_firstname, lau2.lastname locked_by_lastname, la2.roleid locked_by_roleid, lr2.rolename locked_by_rolename,
                        lt.lock_type, lt.description, lt.any_sl_can_unlock
                    from
                        tablets t
                    left join
                        login_all_users lau on t.responsible = lau.capacno
                    left join
                        login_access la on la.programid = :appId and la.capacno = lau.capacno
                    left join
                        login_roles lr on la.roleid = lr.roleid
                    left join
                        login_all_users lau2 on t.locked_by = lau2.capacno
                    left join
                        login_access la2 on la2.programid = :appId and la2.capacno = lau2.capacno
                    left join
                        login_roles lr2 on la2.roleid = lr2.roleid
                    left join
                        lock_types lt on lt.lock_type = t.lock_type
                    where
                        (
                            :location is null or
                            t.location = :location
                        ) and (
                            :responsible is null or
                            t.responsible = :responsible
                        )
                    ";
                cmd.BindByName = true;

                cmd.Parameters.Add("location", location);
                cmd.Parameters.Add("responsible", op?.ACPno);
                cmd.Parameters.Add("appId", APP_ID);

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var tmp = new Tablet();
                        tmp.Id = (decimal)r["id"];
                        tmp.WindowsName = r["windows_name"].ToString();
                        tmp.CreationDate = (DateTime)r["creation_date"];
                        tmp.LastChange = (DateTime)r["last_change"];
                        tmp.Responsible = new Operator()
                        {
                            ACPno = r["responsible"].ToString(),
                            FirstName = r["responsible_firstname"].ToString(),
                            LastName = r["responsible_lastname"].ToString(),

                            PositionName = r["responsible_rolename"].ToString()
                        };
                        tmp.Responsible.SetPositionEnumFromRoleId((decimal)r["responsible_roleid"]);
                        tmp.Responsible.Photo = PhotoDownloader.GetPhoto(tmp.Responsible.Name); //was GetPhotoCashe
                        tmp.Location = r["location"].ToString();

                        if (!r.IsDBNull(r.GetOrdinal("locked_on")))
                        {
                            tmp.LockedOn = (DateTime)r["locked_on"];
                            tmp.LockedBy = new Operator()
                            {
                                ACPno = r["locked_by"].ToString(),
                                FirstName = r["locked_by_firstname"].ToString(),
                                LastName = r["locked_by_lastname"].ToString(),
                                PositionName = r["locked_by_rolename"].ToString()
                            };
                            tmp.LockedBy.SetPositionEnumFromRoleId((decimal)r["locked_by_roleid"]);
                            tmp.LockedBy.Photo = PhotoDownloader.GetPhoto(tmp.LockedBy.Name); //was GetPhotoCashe
                            tmp.LockedReason = r["locked_reason"].ToString();
                            tmp.LockType = new LockType()
                            {
                                Name = r["lock_type"].ToString(),
                                Description = r["description"].ToString(),
                                AnySlCanUnlock = !r.IsDBNull(r.GetOrdinal("any_sl_can_unlock")) && (decimal)r["any_sl_can_unlock"] != 0
                            };
                        }
                        tmp.Barcode = r["barcode"].ToString();
                        
                        ret.Add(tmp);
                    }
                }
                con.Close();
                return ret;
            }
        }

        static public List<ShortTablet> GetAllTabletName()
        {
            List<ShortTablet> ret = new List<ShortTablet>();
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"
                    select ID, WINDOWS_NAME from TABLETS
                    ";
                cmd.BindByName = true;

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var tmp = new ShortTablet();
                        tmp.WindowsName = r["WINDOWS_NAME"].ToString();
                        tmp.IdNumber = (decimal)r["id"]; 
                        ret.Add(tmp);
                    }
                }
                con.Close();
                return ret;
            }
        }
        static public void UpdateTabletLocation(string windowsName, string responsible, string location)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"
                    UPDATE tablets SET RESPONSIBLE= :responsible,LOCATION= :location, LAST_CHANGE=sysdate
                    WHERE WINDOWS_NAME =:windowsName";
                cmd.BindByName = true;

                cmd.Parameters.Add("windowsName", windowsName);
                cmd.Parameters.Add("responsible", responsible);
                cmd.Parameters.Add("location", location);

                var ret = cmd.ExecuteNonQuery();
                if (ret != 1)
                {
                    throw new Exception("Problem when adding tablet.");
                }
            }
        }

        static public List<string> GetUserNameListForLocation(string location)
        {
             //public string Name { get { return $"{FirstName} {LastName}"; } }
            List<string> ret = new List<string>();
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"
                    SELECT firstname, lastname
                    FROM
                        login_all_users 
                    WHERE
                       CAPACNO=ANY(SELECT CAPACNO FROM LOGIN_ACCESS WHERE LOCATION =:location)";
                cmd.BindByName = true;
                cmd.Parameters.Add("location", location);
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        ret.Add(r["firstname"].ToString()+" "+ r["lastname"].ToString());
                    }
                }
                con.Close();
                return ret;
            }
        }

        static public  void RemoveUserSeting(UserSeting seting)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"delete from SETINGS where CATEGORY = :category and SETING_1 = :seting1 and SETING_2=:seting2";

                cmd.BindByName = true;

                cmd.Parameters.Add("CATEGORY", seting.Category);
                cmd.Parameters.Add("seting1", seting.Seting1);
                cmd.Parameters.Add("seting2", seting.Seting2);

                cmd.ExecuteNonQuery();
            }
        }

        static public void AddUserSeting(UserSeting seting)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                cmd.BindByName = true;

                con.Open();

                cmd.CommandText = @"
                    insert into SETINGS (CATEGORY, SETING_1, SETING_2) values (:category, :seting1, :seting2)
                ";

                cmd.Parameters.Add("category", seting.Category);
                cmd.Parameters.Add("seting1", seting.Seting1);
                cmd.Parameters.Add("seting2", seting.Seting2);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public static List<UserSeting> GetUserSetings(string location)
        {
            List<UserSeting> ret = new List<UserSeting>();
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"
                    SELECT *
                    FROM
                        SETINGS 
                    WHERE
                       SETING_2=:location";
                cmd.BindByName = true;
                cmd.Parameters.Add("location", location);
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var tmp = new UserSeting();
                        tmp.Category = r["CATEGORY"].ToString();
                        tmp.Seting1 = r["SETING_1"].ToString();
                        tmp.Seting2 = r["SETING_2"].ToString();
                        ret.Add(tmp);
                    }
                }
                con.Close();
                return ret;

            }
        }

        public static void DeadManSwitch(decimal tabletId)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                cmd.BindByName = true;

                con.Open();

                cmd.CommandText = @"
                        update deadman_switch set last_seen = sysdate where tablet_id = :tabletId
                    ";

                cmd.Parameters.Add("tabletId", (int)tabletId);

                if (cmd.ExecuteNonQuery() != 1)
                {
                    cmd.CommandText = @"
                            insert into deadman_switch (tablet_id, last_seen) values (:tabletId, sysdate)
                        ";

                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        static public void AddComputer(string windowsName, string responsible, string location)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"
                    insert into tablets (windows_name, creation_date, responsible, location, last_change, locked_by, locked_on, lock_type)
                    values (:windowsName, sysdate, :responsible, :location, sysdate, :responsible, sysdate, 'IN USE')
                    ";
                cmd.BindByName = true;

                cmd.Parameters.Add("windowsName", windowsName);
                cmd.Parameters.Add("responsible", responsible);
                cmd.Parameters.Add("location", location);

                var ret = cmd.ExecuteNonQuery();
                if (ret != 1)
                {
                    throw new Exception("Problem when adding tablet.");
                }
            }

        }

        public static List<DocumentRevisionData> GetDocumentRevision()
        {
            List<DocumentRevisionData> ret = new List<DocumentRevisionData>();
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = @"
                    select * from QSI_DOCUMENTS
                    ";
                cmd.BindByName = true;

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var tmp = new DocumentRevisionData();
                        tmp.Location = r["LOCATION"].ToString();
                        tmp.DocNum = r["DOCUMENT"].ToString();
                        tmp.revisionValue = r["REVISION"].ToString();
                        ret.Add(tmp);
                    }
                }
                con.Close();
                return ret;
            }
        }

        public static void UpdateDocumentRevision(DocumentRevisionData document)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = con;
                cmd.BindByName = true;

                con.Open();

                cmd.CommandText = @"
                        update QSI_DOCUMENTS set REVISION = :revision, LAST_UPDATE= sysdate where DOCUMENT = :document and LOCATION=:location
                    ";

                cmd.Parameters.Add("location", document.Location);
                cmd.Parameters.Add("document", document.DocNum);
                cmd.Parameters.Add("revision", document.revisionValue);

                if (cmd.ExecuteNonQuery() != 1)
                {
                    cmd.CommandText = @"
                            insert into QSI_DOCUMENTS (LOCATION, DOCUMENT, REVISION,  LAST_UPDATE) values (:location, :document,:revision, sysdate)
                        ";

                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

    }
}

