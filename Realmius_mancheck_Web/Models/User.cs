﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Realmius_mancheck_Web.Interfaces;

namespace Realmius_mancheck_Web.Models
{
    public class User : IUser
    {
        public IList<string> Tags { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}