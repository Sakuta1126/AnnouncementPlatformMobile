using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnnouncementPlatformMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogOutPage : ContentPage
    {
        public LogOutPage()
        {
            InitializeComponent();
        }


        private void Logout_Clicked(object sender, EventArgs e)
        {
            var loggedInUser = App.Database.GetItemsAsync<Account>().Result.FirstOrDefault(u => u.IsLoggedIn);

            if (loggedInUser != null)
            {

                loggedInUser.IsLoggedIn = false;
                App.Database.SaveItemAsync(loggedInUser).Wait();
                UserStore.SetLoggedInUser(false);
                DisplayAlert("Information","Wylogowano pomyślnie!","OK");
                Navigation.PushAsync(new HomePage());
                App.UpdateMenuItems();

            }
            else
            {

               DisplayAlert("Information","User is not logged in","OK");
            }
        }
    }
}