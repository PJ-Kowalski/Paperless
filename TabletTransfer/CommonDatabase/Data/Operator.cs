using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabase.Data
{
    public class Operator
    {
        public string ACPno { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get { return $"{FirstName} {LastName}"; } }
         
        public override string ToString()
        {
            return $"{Name} ({ACPno})";
        }
        public enum PositionEnum {
            Service = 3,
            TL = 4,
            SL = 5,
            Normal = 8
        }
        public static string ToString(PositionEnum value)
        {
            //na tym etapie serio zaluje ze nie zaimplementowalem tego jako osobnej klasy....
            switch (value)
            {
                case PositionEnum.Service:
                    return "SERWIS";
                case PositionEnum.TL:
                    return "TL";
                case PositionEnum.SL:
                    return "SL";
                case PositionEnum.Normal:
                    return "Operator";
                default:
                    return value.ToString();
            }
        }
        public void SetPositionEnumFromRoleId(decimal roleid)
        {
            _position = (PositionEnum)roleid;//yep, sygnatura dluzsza niz rzutowanie :-D
        }
        public bool IsAllowed/*ToActAsUserWithGivenPosition*/(PositionEnum position)
        {
            return _position <= position;
        }
        public int GetUnderlings()
        {
            if (_position == PositionEnum.Service)
            {
                return (int)PositionEnum.Service;//service user can do harm to each other.
            }
            return (int)_position + 1;
        }
        private PositionEnum _position;
        public PositionEnum Position { get { return _position; } }//let it stay like that. no setter!
        public string PositionName { get; set; }
        public string Location { get; set; }
        public Image Photo { get; set; }
        public List<OnPremiseInfo> OnPremise { get; set; } = new List<OnPremiseInfo>();
        public DateTime OnPremiseLastRefresh { get; set; }
        public enum PasswordVerificationResult
        { 
            NoPasswordSet,
            OK,
            WrongPassword
        }
        public PasswordVerificationResult PasswordVerified { get; set; }
        public string OrgCode { get; set; }
        public enum OnPremiseEnum { 
            Yes,
            Maybe,
            No
        }
        public string IsOnPremiseStr
        {
            get {
                switch (IsOnPremise)
                {
                    case OnPremiseEnum.Yes:
                        return "Tak";
                    case OnPremiseEnum.Maybe:
                        return "Możliwe";
                    case OnPremiseEnum.No:
                        return "Nie";
                    default:
                        return "Nie wiem";
                }
            }
        }
        public OnPremiseEnum IsOnPremise 
        {
            get
            {
                try
                {
                    CommonDatabase.CommonDbAccess.RefreshOnPremiseInfo(this);
                }
                catch (Exception)
                {

                }
                if (OnPremise.Count > 0)
                {
                    if (OnPremise.Max(x => x.Certainity) >= 90)
                    {
                        return OnPremiseEnum.Yes;
                    }
                    else
                    {
                        return OnPremiseEnum.Maybe;
                    }
                }
                else
                {
                    return OnPremiseEnum.No;
                }
            }
        }
        /*public bool CanWork
        {
            get
            {
#if DEBUG
                return ACPno == "ACP173001"|| ACPno == "ACP121974" || IsOnPremise != OnPremiseEnum.No;
#else
                return IsOnPremise != OnPremiseEnum.No;
#endif
            }
        }*/
        public bool CanWork(Tablet tablet, string reason)
        {
            bool ok = false;
#if DEBUG
                ok = ACPno == "ACP215818" || ACPno == "ACP121974" || IsOnPremise != OnPremiseEnum.No;
#else
                ok =  IsOnPremise != OnPremiseEnum.No;
#endif
            if(!ok)
            {
                CommonDatabase.CommonDbAccess.RegisterEvent(tablet, ACPno, "ON_PREMISE_FAILURE", reason); //ToString()
            }
            return ok;
        }

        public object[] GetDefaultRow()
        {
            return new object[] { ACPno, PositionName, FirstName, LastName, OrgCode, IsOnPremise };
        }
    }
}
