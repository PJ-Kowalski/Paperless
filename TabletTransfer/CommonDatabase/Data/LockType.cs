using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabase.Data
{
    public class LockType
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AnySlCanUnlock { get; set; }

        public override string ToString()
        {
            return Name;
        }
        public static LockType REMOTE_LOCK = new LockType()
        {
            Name = "REMOTE",
            Description = @"Tablet został zablokowany zdalnie.

Urządzenie może zostać odblokowane
jedynie przez blokującego lub IT.

",
            AnySlCanUnlock = false
        };
        public static LockType SYSTEM_LOCK = new LockType()
        {
            Name = "SYSTEM",
            Description = @"Tablet został zablokowany systemowo.

Urządzenie może zostać odblokowane
jedynie przez serwis.
Skontaktuj się z IT.

",
            AnySlCanUnlock = false
        };
        public static LockType DAMAGE_LOCK = new LockType()
        {
            Name = "DAMAGE",
            Description = @"Tablet został zablokowany z powodu uszkodzenia.

Urządzenie może zostać odblokowane
jedynie przez SL lub serwis.

",
            AnySlCanUnlock = false
        };
        //service lock nie wyskakuje w alarmach... taki tablet nie musi sie zglaszac zeby nie budzic alarmu.
        public static LockType SERVICE_LOCK = new LockType()
        {
            Name = "SERVICE",
            Description = @"Tablet został zablokowany przez SERVIS.

Urządzenie może zostać odblokowane
jedynie przez serwis.
Skontaktuj się z IT.

",
            AnySlCanUnlock = false
        };
        public static LockType SCRAP = new LockType()
        {
            Name = "SCRAP",
            Description = @"JUŻ NIE ISTNIEJE, REKORDY ZACHOWANE DLA POTOMNOŚCI",
            AnySlCanUnlock = false
        };
    }
}
