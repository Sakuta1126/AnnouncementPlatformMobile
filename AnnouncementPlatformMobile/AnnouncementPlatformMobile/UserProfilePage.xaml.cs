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
    public partial class UserProfilePage : ContentPage
    {
        public UserProfilePage()
        {
            InitializeComponent();
            LoadUserProfile();
        }
        private async void LoadUserProfile()
        {
            var user = (await App.Database.GetItemsAsync<User>()).FirstOrDefault(u => u.user_id == UserStore.LoggedInUserId);
            if (user != null)
            {
                ProfilePhoto.Source = ImageSource.FromFile(user.PFP);
                NameLabel.Text = $"Name: {user.Name}";
                SurnameLabel.Text = $"Surname: {user.Surname}";
                EmailLabel.Text = $"Email: {user.Email}";
                PhoneNumberLabel.Text = $"Phone: {user.PhoneNumber}";
                ResidenceAddressLabel.Text = $"Address: {user.ResidenceAddress}";
                BirthDateLabel.Text = $"Birth Date: {user.BirthDate.ToShortDateString()}";
                SummaryLabel.Text = $"Summary: {user.Summary}";

                var lang = (await App.Database.GetItemsAsync<Lang>()).FirstOrDefault(l => l.user_id == UserStore.LoggedInUserId);
                if (lang != null)
                {
                    LangLabel.Text = $"Language: {lang.Language}";
                    LangLevelLabel.Text = $"Language Level: {lang.LanguageLevel}";
                }
            }
        }
    }
}