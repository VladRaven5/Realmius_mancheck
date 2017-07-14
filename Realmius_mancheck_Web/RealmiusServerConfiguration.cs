using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Security.Provider;
using Realmius.Server;
using Realmius.Server.Configurations;
using Realmius.Server.Infrastructure;
using Realmius.Server.Models;
using Realmius_mancheck_Web.DAL;
using Realmius_mancheck_Web.Interfaces;
using Realmius_mancheck_Web.Models;

namespace Realmius_mancheck_Web
{
    public class RealmiusServerAuthConfiguration : RealmiusConfigurationBase<User>
    {
        public RealmiusServerAuthConfiguration() : base(() => new RealmiusServerContext())
        {
        }

        public IList<Type> TypesToSyncList { get; set; }

        public override IList<Type> TypesToSync => Startup.TypesForSync;

        public override ILogger Logger { get; set; } = new Logger();

        public override User AuthenticateUser(IRequest request)
        {
            var userName = request.QueryString["userLogin"];
            var password = request.QueryString["pass"];
            var user = UsersCredentialsDict.GetUser(userName, password);
            return user;
        }

        public override bool CheckAndProcess(CheckAndProcessArgs<User> args)
        {
            var db = args.Database as RealmiusServerContext;

            if (args.Entity is NoteRealm)
            {
                var newNote = args.Entity as NoteRealm;
                newNote.UserRole = args.User.Role;
                return true;
            }
            if (args.Entity is PhotoRealm)
            {
                return true;
            }
            if (args.Entity is ChatMessageRealm)
            {
                return true;
            }
            return false;
        }

        public override IList<string> GetTagsForObject(ChangeTrackingDbContext db, IRealmiusObjectServer obj)
        {

            if (obj is NoteRealm)
            {
                var currentNote = obj as NoteRealm;
                var tagsList = Enum.GetValues(typeof(UserRole)).Cast<int>().Where(v => v >= currentNote.UserRole).Select(x => x.ToString()).ToList();
                return tagsList;
            }

            if (obj is PhotoRealm)
            {
                return new List<string>() { ((int)UserRole.Anonymous).ToString() };
            }

            if (obj is ChatMessageRealm)
            {
                return new List<string>() { ((int)UserRole.Anonymous).ToString() };
            }

            return null;
        }

        public override IList<string> GetTagsForUser(User user, ChangeTrackingDbContext db)
        {
            return Enum.GetValues(typeof(UserRole)).Cast<int>().Where(v => v <= user.Role).Select(x => x.ToString()).ToList();
        }
    }
}