using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realmius_mancheck.Model
{
    public class User
    {
        public string Name { get; }

        public readonly string Password;

        public User()
        {
            Name = "empty";
            Password = "empty";
        }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
