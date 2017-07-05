using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Realmius_mancheck_Web.Models
{
    public class BaseItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset PostTime { get; set; }
    }
}