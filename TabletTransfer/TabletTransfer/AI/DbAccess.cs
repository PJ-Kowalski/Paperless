using CommonDatabase;
using CommonDatabase.Data;
using CommonDatabase.Tools;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TabletTransfer.AI
{
    public partial class StateSingleton
    {
        class DbAccess: CommonDbAccess
        {
            private static new void Setup(string connectionString)
            {
                CommonDbAccess.connectionString = connectionString;
            }
            static DbAccess()
            {
                Setup(Properties.Settings.Default.ProgAuthConnectionString);
            }

            static public Tablet CreateTablet(string windowsName, string responsible, string location)
            {
                using (OracleConnection con = new OracleConnection(Properties.Settings.Default.ProgAuthConnectionString))
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = @"
                    insert into tablets (windows_name, creation_date, responsible, location, last_change)
                    values (:windowsName, sysdate, :responsible, :location, sysdate)
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
                return GetTablet(0, windowsName);
            }
            static public Tablet GetTablet(decimal tabletId, string windowsName)
            {
                Tablet ret = null;
                using (OracleConnection con = new OracleConnection(Properties.Settings.Default.ProgAuthConnectionString))
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
                        (:tabletId = 0 or id = :tabletId)
                        and windows_name = :windowsName
                    ";
                    cmd.BindByName = true;

                    cmd.Parameters.Add("tabletId", tabletId);
                    cmd.Parameters.Add("windowsName", windowsName);
                    cmd.Parameters.Add("appId", APP_ID);

                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            ret = new Tablet();
                            ret.Id = (decimal)r["id"];
                            ret.WindowsName = r["windows_name"].ToString();
                            ret.CreationDate = (DateTime)r["creation_date"];
                            ret.LastChange = (DateTime)r["last_change"];
                            ret.Responsible = new Operator()
                            {
                                ACPno = r["responsible"].ToString(),
                                FirstName = r["responsible_firstname"].ToString(),
                                LastName = r["responsible_lastname"].ToString(),
                                PositionName = r["responsible_rolename"].ToString()
                            };
                            ret.Responsible.SetPositionEnumFromRoleId((decimal)r["responsible_roleid"]);
                            ret.Responsible.Photo = PhotoDownloader.GetPhoto(ret.Responsible.Name);
                            ret.Location = r["location"].ToString();

                            if (!r.IsDBNull(r.GetOrdinal("locked_on")))
                            {
                                ret.LockedOn = (DateTime)r["locked_on"];
                                ret.LockedBy = new Operator()
                                {
                                    ACPno = r["locked_by"].ToString(),
                                    FirstName = r["locked_by_firstname"].ToString(),
                                    LastName = r["locked_by_lastname"].ToString(),
                                    PositionName = r["locked_by_rolename"].ToString()
                                };
                                ret.LockedBy.SetPositionEnumFromRoleId((decimal)r["locked_by_roleid"]);
                                ret.LockedReason = r["locked_reason"].ToString();
                                ret.LockType = new LockType()
                                {
                                    Name = r["lock_type"].ToString(),
                                    Description = r["description"].ToString(),
                                    AnySlCanUnlock = !r.IsDBNull(r.GetOrdinal("any_sl_can_unlock")) && (decimal)r["any_sl_can_unlock"] != 0
                                };
                            }
                            ret.Barcode = r["barcode"].ToString();
                            break;
                        }
                    }

                    return ret;
                }
            }


            internal static bool RegisterEvent(Tablet myself, string user, string event_type, string reason)
            {
                using (OracleConnection con = new OracleConnection(Properties.Settings.Default.ProgAuthConnectionString))
                {
                    con.Open();
                    OracleTransaction trans = con.BeginTransaction();

                    if (!RegisterEvent(myself, user, event_type, reason, trans))
                    {
                        trans.Rollback();
                        return false;
                    }

                    trans.Commit();
                    return true;
                }
            }
            internal static bool PutInStorage(Tablet myself, Operator sl, string reason)
            {
                using (OracleConnection con = new OracleConnection(Properties.Settings.Default.ProgAuthConnectionString))
                {
                    con.Open();
                    OracleTransaction trans = con.BeginTransaction();

                    if (!RegisterEvent(myself, sl.ACPno, "APP_STARTUP", reason, trans))
                    {
                        trans.Rollback();
                        return false;
                    }
                    var cmd = new OracleCommand(@"
                    update tablets 
                    set
                        locked_by = :username, locked_on = sysdate, lock_type = :lock_type, locked_reason = ''
                    where
                        id = :tablet_id
                    ", con);
                    cmd.BindByName = true;

                    cmd.Parameters.Add("username", sl.ACPno);
                    cmd.Parameters.Add("lock_type", "MAGAZYN");
                    cmd.Parameters.Add("tablet_id", myself.Id);

                    var ret = cmd.ExecuteNonQuery();
                    if (ret != 1)
                    {
                        trans.Rollback();
                        return false;
                    }

                    trans.Commit();
                    return true;
                }
            }

           

            internal static void DeadManSwitch(decimal tabletId)
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
        }
    }
}
