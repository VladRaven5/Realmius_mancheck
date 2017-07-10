using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realmius.SyncService;
using Realms;

namespace Realmius_mancheck.RealmEntities
{
    public class ChatMessageRealm : RealmObject, IRealmiusObjectClient
    {
        public string MobilePrimaryKey => Id;

        public string Id { get; set; }

        public string Text { get; set; }

        public string AuthorName { get; set; }

        public DateTimeOffset CreatingDateTime { get; set;  }

        public int MessageStatusCode { get; set; }

        [Ignored]
        public MessageStatus MessageStatus => (MessageStatus)MessageStatusCode;

        public ChatMessageRealm(string text)
        {
            Id = Guid.NewGuid().ToString();
            Text = text;
            AuthorName = App.CurrenUser.Name;
            CreatingDateTime = DateTimeOffset.Now;
        }

        public ChatMessageRealm()
        {
        }
    }

    public enum MessageStatus
    {
        Sended = 1,
        Received = 2,
        Readed = 3,
        Error = -1
    }
}
