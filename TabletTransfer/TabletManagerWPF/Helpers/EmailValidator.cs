using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TabletManagerWPF.Helpers
{
    public static class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            bool isValid = false;
            const string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            isValid = email != "" && Regex.IsMatch(email, pattern);

            return isValid;
        }
    }
}
