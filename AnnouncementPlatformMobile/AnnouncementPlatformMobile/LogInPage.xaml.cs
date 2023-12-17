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
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
          
            try
            {

                string login = UsernameEntry.Text;
                string password = PasswordEntry.Text;

                var user = App.Database.GetItemsAsync<Account>().Result.FirstOrDefault(u => u.Login == login);

                if (user != null)
                {

                    if (user != null && user.Password == password)
                    {

                        user.IsLoggedIn = true;
                        UserStore.SetLoggedInUserId(user.Id);
                        App.Database.SaveItemAsync(user).Wait();
                        DisplayAlert("Information","Logging went correctly","OK");

                        UsernameEntry.Text = string.Empty;
                        PasswordEntry.Text = string.Empty;
                        


                    }
                    else
                    {

                        DisplayAlert("Error","Incorrect Password","OK");
                    }
                }
                else
                {

                    DisplayAlert("Error", "User with this login doesnt exist", "OK");
                }
            }
            catch (Exception ex)
            {

                DisplayAlert("Error",$"Wystąpił błąd: {ex.Message}","OK");
            }
        }
    }
}