using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realmius_mancheck.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Realmius_mancheck
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorisationPage : ContentPage
    {
        public AuthorisationPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            //var bindingContext = ((TabbedPageViewModel)BindingContext);
            //if (!bindingContext.UserAuthorised)
            //    bindingContext.Skip();
            base.OnDisappearing();
        }
    }
}