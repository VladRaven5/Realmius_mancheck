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
            {"admin", new User()
            {
                Id = "0", Name = "Admin", Password = "admin", Tags = new List<string>(){"all", "admin"}, Role = "admin"
            }},
            { "vlad",new User()
            {
                Id = "1", Name = "Vlad", Password = "123", Tags = new List<string>(){"all", "dev"}, Role= "dev"
            }},
            { "homer", new User()
            {
                Id = "2", Name = "Homer", Password = "simpson", Tags = new List<string>(){"all", "user"}, Role = "user"
            }},
            { "anonymous", new User()
            {
                Id = "3", Name = "Anonymous", Password = "Anonymous", Tags = new List<string>(){"all"}, Role = ""
            }},
        };

        public static User GetUser(string name, string password)
        {
            if (_usersCredentials.ContainsKey(name) && _usersCredentials[name].Password == password.Trim())
                return _usersCredentials[name];

            return null;
        }

        public static User GetDefaultUser()
        {
            return _usersCredentials.Count > 0
                ? _usersCredentials.Last().Value
                : new User
                {
                    Id = "3",
                    Name = "Anonymous",
                    Password = "Anonymous",
                    Tags = new List<string>() { "all" },
                    Role = ""
                };
        }
    }
}