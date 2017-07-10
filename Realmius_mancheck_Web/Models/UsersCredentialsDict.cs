using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Realmius_mancheck_Web.Models
{
    public static class UsersCredentialsDict
    {
        private static Dictionary<string, User> _usersCredentials = new Dictionary<string, User>()
        {
            {"empty", new User()
            {
                Id = "0", Name = "empty", Password = "empty", Tags = new List<string>(){"all"}, Role = ""
            }},
            { "vlad",new User()
            {
                Id = "1", Name = "Vlad", Password = "123", Tags = new List<string>(){"all", "dev"}, Role= "dev"
            }},
            { "homer", new User()
            {
                Id = "2", Name = "Homer", Password = "321", Tags = new List<string>(){"all", "user"}, Role = "user"
            }},
            {"odmen", new User()
            {
                Id = "0", Name = "Odmen", Password = "odmen", Tags = new List<string>(){"all", "admin"}, Role = "admin"
            }}
        };

        public static User GetUser(string name, string password)
        {
            if (_usersCredentials.ContainsKey(name) && _usersCredentials[name].Password == password.Trim())
                return _usersCredentials[name];

            return null;
        }
    }
}