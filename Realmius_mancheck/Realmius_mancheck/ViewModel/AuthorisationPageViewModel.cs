using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Realmius_mancheck.Model;
using Xamarin.Forms;

namespace Realmius_mancheck.ViewModel
{
    public class AuthorisationPageViewModel : RootViewModel
    {
        public bool UserAuthorised
        {
            get { return App.UserAuthorised; }
            set { App.UserAuthorised = value; }
        }

        public ICommand LoginCommand { get; set; }

        public ICommand SkipCommand { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public User CurrentUser
        {
            get { return App.CurrenUser; }
            set { App.CurrenUser = value; }
        }

        public string ErrorMsg { get; set; }

        public bool AccessGranted { get; set; }

        public AuthorisationPageViewModel()
        {
            LoginCommand = new Command(Login);
            SkipCommand = new Command(Skip);
        }

        private void Login()
        {
            if (String.IsNullOrWhiteSpace(UserName) || String.IsNullOrWhiteSpace(UserPassword))
            {
                ErrorMsg = "Fill all fields!";
                OnPropertyChanged(nameof(ErrorMsg));
                return;
            }

            if (UsersCredentialsDict.CheckUser(UserName, UserPassword))
            {
                ErrorMsg = null;
                CurrentUser = new User(UserName, UserPassword);
                OnPropertyChanged(nameof(CurrentUser));
                UserAuthorised = true;
                OnPropertyChanged(nameof(UserAuthorised));
                GrantAccess();
            }
            else
            {
                ErrorMsg = "Wrong name or password!";
            }
            OnPropertyChanged(nameof(ErrorMsg));
        }

        public void Skip()
        {
            var testUser = UsersCredentialsDict.GetUser(0);
            CurrentUser = new User(testUser.Key, testUser.Value);
            OnPropertyChanged(nameof(CurrentUser));
            GrantAccess();
        }

        public async void GrantAccess()
        {
            AccessGranted = true;
            OnPropertyChanged(nameof(AccessGranted));
            await Task.Delay(4000);
            OnAuthorisePageClosed?.Invoke(new object(), EventArgs.Empty);
        }

        public EventHandler OnAuthorisePageClosed;
    }
}
