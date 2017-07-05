﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realmius.SyncService;
using Realmius_mancheck.Interfaces;
using Realms;

namespace Realmius_mancheck.RealmEntities
{
    public class NoteRealm : RealmObject, IRealmiusObjectClient
    {
        public string MobilePrimaryKey => Id.ToString();

        public string Description { get; set; }

        [PrimaryKey]
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset PostTime { get; set; }
    }
}
