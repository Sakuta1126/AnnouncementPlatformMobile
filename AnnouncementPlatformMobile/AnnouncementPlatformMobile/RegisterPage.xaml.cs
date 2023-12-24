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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            string login = UsernameEntry.Text;
            string password = PasswordEntry.Text;
            bool isAdmin = (bool)AdminSwitch.IsToggled;
            var existingUser = App.Database.GetItemsAsync<Account>().Result.FirstOrDefault(u => u.Login == login);

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                DisplayAlert("Error", "Fields cant be empty", "OK");
            }
            else if (existingUser == null)
            {

                var newUser = new Account { Login = login, Password = password, IsAdmin = isAdmin };

                App.Database.SaveItemAsync(newUser).Wait();

                DisplayAlert("Information", "Registration complete", "OK");
                


                UsernameEntry.Text = string.Empty;
                PasswordEntry.Text = string.Empty;
                AdminSwitch.IsToggled = false;
                Navigation.PushAsync(new HomePage());
                App.UpdateMenuItems();
            }
            else
            {

                DisplayAlert("Error", "User with this Login already exists", "OK");
            }
        }
    }
}