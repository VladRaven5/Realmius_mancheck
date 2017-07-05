using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realmius_mancheck.Model
{
    public static class UsersCredentialsDict
    {
        private static Dictionary<string, string> _usersCredentials = new Dictionary<string, string>()
        {
            {"empty", "empty" },
            { "vlad","123"},
            { "homer", "321"},
            {"odmen", "odmen" },
            
        };

        public static bool CheckUser(string name, string password)
        {
            if (_usersCredentials.ContainsKey(name) && _usersCredentials[name] == password.Trim())
                return true;

            return false;
        }

        public static KeyValuePair<string, string> GetUser(int number)
        {
            if (number < _usersCredentials.Count)
            {
                return _usersCredentials.ToList()[number];
            }

            return new KeyValuePair<string, string>("error","error");
        }
    }
}
