﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Realmius_mancheck.Model;
using Xamarin.Forms;

namespace Realmius_mancheck.ViewModel
{
    public class SettingsPageViewModel : RootViewModel
    {
        public bool UserAuthorised
        {
            get { return App.UserAuthorised; }
            set { App.UserAuthorised = value; }
        }

        public User CurrentUser => App.CurrenUser;

        public ICommand LoginButtonClickCommand { get; set; }

        public SettingsPageViewModel()
        {
            LoginButtonClickCommand = new Command(LoginClicked);
        }

        public async void LoginClicked()
        {
            if (UserAuthorised)
            {
                App.CurrenUser = new User();
                App.UserAuthorised = false;
                OnPropertyChanged(nameof(UserAuthorised));
                App.Instance.ReinitializeDatabases();
            }
            else
            {
                ((App) App.Current).InitAuthorisation();
            }
            await Task.Delay(1000);
        }

        public void Refresh()
        {
            OnPropertyChanged(nameof(UserAuthorised));
            OnPropertyChanged(nameof(CurrentUser));
        }
    }
}