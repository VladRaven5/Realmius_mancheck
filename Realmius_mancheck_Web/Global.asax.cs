using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Cors;
using Owin;
using Realmius.Server.Models;
using Realmius.Server.QuickStart;
using Realmius_mancheck_Web.DAL;
using Realmius_mancheck_Web.Models;

namespace Realmius_mancheck_Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //var dbContext = new RealmiusServerContext();
            //RealmiusServer.SetupShareEverythingSignalRServer(
            //    "/Realmius",
            //    Startup., 
            //    () => dbContext,
            //    new[]
            //    {
            //        typeof(NoteRealm),
            //        typeof(PhotoRealm)
            //    });
            
        }

        //private void InitRealmData()
        //{
        //    var realm = Realm.GetInstance();
        //    realm.Write(() =>
        //    {
        //        realm.Add(new NoteRealm() { Id = 1001, Title = "Film", Description = "Fight club" });

        //        realm.Add(new NoteRealm() { Id = 1002, Title = "Cleaning", Description = "Clean the room" });

        //        realm.Add(new NoteRealm() { Id = 1003, Title = "Pet", Description = "Feed the dog" });

        //        realm.Add(new PhotoRealm()
        //        {
        //            Id = 1001,
        //            Title = "Bike",
        //            PhotoUri = "https://auto.ndtvimg.com/bike-images/gallery/honda/cbr-150r/exterior/bike-img.png"
        //        });

        //        realm.Add(new PhotoRealm()
        //        {
        //            Id = 1002,
        //            Title = "Plain",
        //            PhotoUri = "http://az616578.vo.msecnd.net/files/2016/06/11/636012615152249351-1424983048_cover4.jpg"
        //        });

        //        realm.Add(new PhotoRealm()
        //        {
        //            Id = 1003,
        //            Title = "Helicopter",
        //            PhotoUri = "https://i.ytimg.com/vi/_rLTPRGpA60/maxresdefault.jpg"
        //        });
        //    });
        //}
    }

    //public class Startup
    //{
    //    public void Configuration(IAppBuilder app)
    //    {
    //        app.UseCors(CorsOptions.AllowAll);

    //        RealmiusServer.SetupShareEverythingSignalRServer(
    //            "/Realmius",
    //            app,
    //            () => new RealmiusServerContext(),
    //            typeof(Note),
    //            typeof(Photo));
           
    //    }
    //}
}
