using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Security.Provider;
using Realmius.Server;
using Realmius.Server.Configurations;
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

        public override IList<Type> TypesToSync => Startup.TypesForSync; // { get { return new List<Type>() {typeof(NoteRealm), typeof(PhotoRealm), typeof(ChatMessageRealm)};} }

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
                var newPhoto = args.Entity as PhotoRealm;
                return true;
            }
            if (args.Entity is ChatMessageRealm)
            {
                var newMessage = args.Entity as ChatMessageRealm;
                return true;
            }
            return false;
        }

        public override IList<string> GetTagsForObject(ChangeTrackingDbContext db, IRealmiusObjectServer obj)
        {

            if (obj is NoteRealm)
            {
                var currentNote = obj as NoteRealm;
                if (currentNote.UserRole == null)
                {
                    currentNote.UserRole = forAll;
                }
                if (ObjectsTagsHierarchy.ContainsKey(currentNote.UserRole))
                {
                    return ObjectsTagsHierarchy[currentNote.UserRole];
                }
                return null;
            }

            if (obj is PhotoRealm)
            {
                var currentObject = obj as PhotoRealm;
                return new List<string>() { "all" };
            }

            if (obj is ChatMessageRealm)
            {
                return new List<string>() { "all" };
            }

            return null;

            //return new List<string>(){"all"};
        }

        public override IList<string> GetTagsForUser(User user, ChangeTrackingDbContext db)
        {
            if (user.Role == null)
            {
                user.Role = unknwnRole;
            }

            if (UsersTagsHierarchy.ContainsKey(user.Role))
            {
                return UsersTagsHierarchy[user.Role];
            }

            return null;
            //return new List<string>() { "all" };
        }

#region - USER'S CREDS -

        private static string adminRole = "admin";
        private static string devRole = "dev";
        private static string userRole = "user";
        private static string unknwnRole = "";
        private static string forAll = "all";

        //какие роли юзеров имеют доступ к объектам, выложенным юзерами с ролями, равными ключу 
        private Dictionary<string, List<string>> ObjectsTagsHierarchy = new Dictionary<string, List<string>>()
        {
            { unknwnRole, new List<string>() {unknwnRole, userRole, devRole, adminRole}},

            { forAll, new List<string>() {unknwnRole, userRole, devRole, adminRole}},

            { userRole, new List<string>() {userRole, devRole, adminRole} },

            { devRole, new List<string>() {devRole, adminRole} },

            { adminRole, new List<string>() { adminRole } }
        };

        //к какому контенту имею доступ юзеры определенных ролей(ключи)
        private Dictionary<string, List<string>> UsersTagsHierarchy = new Dictionary<string, List<string>>()
        {
            { unknwnRole, new List<string>() {unknwnRole, forAll}},

            { forAll, new List<string>() {unknwnRole, userRole, forAll}},

            { userRole, new List<string>() { unknwnRole, userRole, forAll } },

            { devRole, new List<string>() { unknwnRole, userRole, devRole, forAll } },

            { adminRole, new List<string>() { unknwnRole, userRole, devRole, adminRole, forAll } }
        };

#endregion // - USER'S CREDS -
    }
}