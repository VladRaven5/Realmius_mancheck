﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using DLToolkit.Forms.Controls;
using Realmius;
using Realmius.SyncService;
using Realmius_mancheck.Model;
using Realmius_mancheck.RealmEntities;
using Realmius_mancheck.ViewModel;
using Realms;
using Xamarin.Forms;

namespace Realmius_mancheck
{
    public partial class App : Application
    {
        public static User CurrenUser { get; set; }

        public static bool UserAuthorised { get; set; }

        private readonly bool needAuthorisation = true;

        private static IRealmiusSyncService syncService;

        public static App Instance { get; set; }

        private readonly string hostUrl = "http://192.168.10.109:9930";

        private static RealmConfiguration RealmConfiguration
        {
            get
            {
                var config = new RealmConfiguration(realmPath)
                {
                    MigrationCallback = (s, e) =>
                    {
                    },
                    ShouldDeleteIfMigrationNeeded = false,
                    SchemaVersion = 1,
                };
                return config;
            }
        }

        public static Realm GetRealm()
        {
            var instance = Realm.GetInstance(RealmConfiguration);
            return instance;
        }

        public App()
        {
            Instance = this;

            FlowListView.Init();

            GetUserCredentials();
            
            InitializeComponent();
            
            var tabbedPageViewModel = new TabbedPageViewModel();
            RefreshViews += (o, e) =>
            {
                tabbedPageViewModel.RefreshViewModels();
            };

            MainPage = new NavigationPage(new Realmius_mancheck.MainTabbedPage() {BindingContext = tabbedPageViewModel});

            if (!UserAuthorised)
            {
                InitAuthorisation();
            }
        }

        private void SetRealmConnection()
        {
            syncService = SyncServiceFactory.CreateUsingSignalR(
                GetRealm,
                new Uri(hostUrl + "/Realmius" + (needAuthorisation? "?userLogin=" + CurrenUser.Name + "&pass=" + CurrenUser.Password : null)),
                new[]
                {
                    typeof(NoteRealm),
                    typeof(PhotoRealm)
                });

            //InitRealmData();
        }

        public void ReinitializeDatabases()
        {
            try
            {
                syncService.Dispose();
                syncService.DeleteDatabase();
            }
            catch (Exception e)
            {
            }

            ResetDatabasePaths();
            InitializeDatabasePaths();
            SetRealmConnection();
            RefreshViews?.Invoke(new object(), EventArgs.Empty);
        }

        private static string realmPath = Guid.NewGuid().ToString();

        public static void InitializeDatabasePaths()
        {
            if (string.IsNullOrEmpty(realmPath))
            {
                ResetDatabasePaths();
            }
            RealmiusSyncService.RealmiusDbPath = realmPath + "_sync";
        }

        public static void ResetDatabasePaths()
        {
            realmPath = Guid.NewGuid().ToString();
        }

        #region ---USER PROCCESSING---

        public void InitAuthorisation()
        {
            if (UserAuthorised) return;

            var authorisationPageViewModel = new AuthorisationPageViewModel();
            var authorisationPage = new AuthorisationPage() { BindingContext = authorisationPageViewModel  };
            
            authorisationPageViewModel.OnAuthorisePageClosed += (o, e) =>
            {
                authorisationPage.Navigation.PopModalAsync();
                ReinitializeDatabases();
                SaveUserCredentials();
            };

            MainPage.Navigation.PushModalAsync(authorisationPage);
        }

        public EventHandler RefreshViews;

        private void GetUserCredentials()
        {
            IDictionary<string, object> properties = Application.Current.Properties;

            string name = string.Empty;
            string password = string.Empty;

            if (properties.ContainsKey(nameof(User.Name)))
            {
                name = properties[nameof(User.Name)] as string;
            }

            if (properties.ContainsKey(nameof(User.Password)))
            {
                password = properties[nameof(User.Password)] as string;
            }

            if (!String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(password))
            {
                CurrenUser = new User(name, password);
                UserAuthorised = true;
            }
            else
            {
                UserAuthorised = false;
                CurrenUser = new User();
            }
            OnPropertyChanged(nameof(CurrenUser));
            OnPropertyChanged(nameof(UserAuthorised));
        }

        private void SaveUserCredentials()
        {
            IDictionary<string, object> properties = Application.Current.Properties;
            foreach (PropertyInfo prop in CurrenUser.GetType().GetRuntimeProperties())
            {
                if (!properties.ContainsKey(prop.Name))
                {
                    if (prop.GetValue(CurrenUser, null) != null)
                    {
                        properties.Add(prop.Name, prop.GetValue(CurrenUser, null).ToString());
                    }
                }
                else
                {
                    if (prop.GetValue(CurrenUser, null) != null)
                    {
                        properties[prop.Name] = prop.GetValue(CurrenUser, null).ToString();
                    }
                    else
                    {
                        properties.Remove(prop.Name);
                    }
                }
            }
            Application.Current.SavePropertiesAsync();
        }

        private void InitRealmData()
        {
            //var realm = Realm.GetInstance();
            var realm = App.GetRealm();
            realm.Write(() =>
            {
                realm.Add(new NoteRealm() { Id = "1001", Title = "Film", Description = "Fight club", PostTime = DateTimeOffset.Now });
                
                realm.Add(new NoteRealm() {Id = "1002", Title = "Cleaning", Description = "Clean the room", PostTime = DateTimeOffset.Now });

                realm.Add(new NoteRealm() {Id = "1003", Title = "Pet", Description = "Feed the dog", PostTime = DateTimeOffset.Now});

                realm.Add(new PhotoRealm()
                {
                    Id = "1004",
                    Title = "Bike",
                    PhotoUri = "https://auto.ndtvimg.com/bike-images/gallery/honda/cbr-150r/exterior/bike-img.png",
                    PostTime = DateTimeOffset.Now
                });

                realm.Add(new PhotoRealm()
                {
                    Id = "1005",
                    Title = "Plain",
                    PhotoUri = "http://az616578.vo.msecnd.net/files/2016/06/11/636012615152249351-1424983048_cover4.jpg",
                    PostTime = DateTimeOffset.Now
                });

                realm.Add(new PhotoRealm()
                {
                    Id = "1006",
                    Title = "Helicopter",
                    PhotoUri = "https://i.ytimg.com/vi/_rLTPRGpA60/maxresdefault.jpg",
                    PostTime = DateTimeOffset.Now
                });
            });
        }
 #endregion
        
        #region ---LIFECYCLE---

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
#endregion
    }
}