using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Auth_Nanda
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public bool IsPasswordValid()
        {
            if (Password.Length < 8)
            {
                return false;
            }

            if (!Regex.IsMatch(Password, @"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)"))
            {
                return false;
            }

            return true;
        }
    }

}
