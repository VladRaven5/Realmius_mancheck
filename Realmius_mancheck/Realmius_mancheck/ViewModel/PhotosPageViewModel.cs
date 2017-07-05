using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Realmius_mancheck.RealmEntities;
using Realms;
using Xamarin.Forms;

namespace Realmius_mancheck.ViewModel
{
    public class PhotosPageViewModel : RootViewModel
    {
        public List<PhotoRealm> Photos { get; set; }/* = new List<PhotoRealm>()
        {
            new PhotoRealm() {Id = 1001, Title = "Bike", PhotoUri = "https://auto.ndtvimg.com/bike-images/gallery/honda/cbr-150r/exterior/bike-img.png"},

            new PhotoRealm() {Id = 1002, Title = "Plain", PhotoUri = "http://az616578.vo.msecnd.net/files/2016/06/11/636012615152249351-1424983048_cover4.jpg"},

            new PhotoRealm() {Id = 1003, Title = "Helicopter", PhotoUri = "https://i.ytimg.com/vi/_rLTPRGpA60/maxresdefault.jpg"}
        };*/


        public PhotosPageViewModel()
        {
            InitData();;
        }

        private void InitData()
        {
            var realmPhotos = /*Realm.GetInstance()*/App.GetRealm().All<PhotoRealm>();
            realmPhotos.SubscribeForNotifications((collection, y, e) =>
            {
                Photos = realmPhotos.ToList();
                OnPropertyChanged(nameof(Photos));
            });
        }

        public void Refresh()
        {
            InitData();
            OnPropertyChanged(nameof(Photos));
        }
    }
}
