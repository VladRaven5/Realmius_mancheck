using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using Realmius.Server.Configurations;
using Realmius.Server.QuickStart;
using Realmius_mancheck_Web.DAL;
using Realmius_mancheck_Web.Models;

[assembly: OwinStartup(typeof(Realmius_mancheck_Web.Startup))]
namespace Realmius_mancheck_Web
{
    public class Startup
    {
        private readonly bool needAuthorisation = true;

        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            app.MapSignalR("/signalr", new HubConfiguration()
            {
                EnableDetailedErrors = true,

            });

            if (needAuthorisation)
            {
                RealmiusServer.SetupSignalRServer(
                    "/Realmius",
                    app,
                    new RealmiusServerAuthConfiguration()
                    {
                        TypesToSyncList = new List<Type>()
                        {
                            typeof(NoteRealm), typeof(PhotoRealm)
                        }
                    });
            }
            else
            {
                RealmiusServer.SetupShareEverythingSignalRServer(
                "/Realmius",
                app,
                () => new RealmiusServerContext(),
                new[]
                {
                    typeof(NoteRealm),
                    typeof(PhotoRealm)
                });
            }
        }
    }
}
