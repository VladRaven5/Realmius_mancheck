using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Realmius;
using Realmius_mancheck.RealmEntities;
using Realms;
using Xamarin.Forms;

namespace Realmius_mancheck.ViewModel
{
    public class NotesPageViewModel : RootViewModel
    {
        public IRealmCollection<NoteRealm> Notes { get; set; } /*= new ObservableCollection<NoteRealm>()
        {
            new NoteRealm() {Id = 1001, Title = "Film", Description = "Fight club"},

            new NoteRealm() {Id = 1002, Title = "Cleaning", Description = "Clean the room"},

            new NoteRealm() {Id = 1003, Title = "Pet", Description = "Feed the dog"}
        };*/

        public string NewNoteTitle { get; set; }

        public string NewNoteDescription { get; set; }

        public ICommand AddNoteCommand { get; set; }

        public ICommand RemoveNoteCommand { get; set; }

        public NotesPageViewModel()
        {
            AddNoteCommand = new Command(AddNote);
            RemoveNoteCommand  = new Command<string>(RemoveNote);
            InitData();
        }

        private void InitData()
        {
            var realmNotes = /*Realm.GetInstance()*/App.GetRealm().All<NoteRealm>();
            realmNotes.SubscribeForNotifications((collection, o, e) =>
            {
            });
            Notes = realmNotes.AsRealmCollection();
        }

        private void AddNote()
        {
            string title = !String.IsNullOrWhiteSpace(NewNoteTitle) ? NewNoteTitle : "<none>";
            string description = !String.IsNullOrWhiteSpace(NewNoteDescription) ? NewNoteDescription : "<none>";

            //var realm = Realm.GetInstance();
            var realm = App.GetRealm();
            realm.Write(() =>
            {
                realm.Add(new NoteRealm()
                {
                    Title = title,
                    Description = description,
                    Id = Guid.NewGuid().ToString(),//Notes?.LastOrDefault()?.Id + 1 ?? 0,
                    PostTime = DateTimeOffset.Now
                });
            });
            //Notes = Realm.GetInstance().All<NoteRealm>().ToList();

            //Notes.Add(new NoteRealm()
            //{
            //    Title = title,
            //    Description = description,
            //    Id = Notes?.Last()?.Id + 1 ?? 0 
            //});
            NewNoteDescription = "";
            NewNoteTitle = "";
            OnPropertyChanged(nameof(NewNoteTitle));
            OnPropertyChanged(nameof(NewNoteDescription));
        }

        private void RemoveNote(/*int */string id)
        {
            //Notes.Remove(Notes.First(x => x.Id == id));
            //var realm = Realm.GetInstance();
            var realm = App.GetRealm();
            realm.Write(() =>
                {
                    //realm.Remove(Notes.First(x => x.Id == id));
                    realm.RemoveAndSync(Notes.First(x => x.Id == id));
                }
            );
        }

        public void Refresh()
        {
            InitData();
            OnPropertyChanged(nameof(Notes));
        }
    }
}
