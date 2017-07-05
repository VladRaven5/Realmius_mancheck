using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realmius_mancheck.Interfaces
{
    public interface IItem
    {
        int Id { get; set; }

        string Title { get; set; }

        DateTimeOffset PostTime { get; }
    }
}
