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
    public partial class FlyoutMenuPage : ContentPage
    {
        public ListView ListView => menulist;
        public FlyoutMenuPage()
        {
            InitializeComponent();
           
            UpdateMenuItems();
        }
        private void UpdateMenuItems()
        {
            var items = new List<FlyoutItemPage>();

            // Elementy menu dostępne dla wszystkich
            items.Add(new FlyoutItemPage { Title = "Home", TargetPage = typeof(HomePage) });

            if (UserStore.LoggedInUserId != null)
            {
                // Elementy menu dla zalogowanych użytkowników
                items.Add(new FlyoutItemPage { Title = "Announcements", TargetPage = typeof(AnnouncementsPage) });
                items.Add(new FlyoutItemPage { Title = "Applied", TargetPage = typeof(AppliedPage) });
                items.Add(new FlyoutItemPage { Title = "Profile Information", TargetPage = typeof(UserFormPage) });
                items.Add(new FlyoutItemPage { Title = "Profile", TargetPage = typeof(UserProfilePage) });
                items.Add(new FlyoutItemPage { Title = "LogOut", TargetPage = typeof(LogOutPage) });

                if (UserStore.LoggedInUserAdmin)
                {
                    // Dodatkowy element menu tylko dla adminów
                    items.Add(new FlyoutItemPage { Title = "Add Announcement", TargetPage = typeof(AnnouncementsFormPage) });
                }
            }
            else
            {
                // Elementy menu dla niezalogowanych użytkowników
                items.Add(new FlyoutItemPage { Title = "Register", TargetPage = typeof(RegisterPage) });
                items.Add(new FlyoutItemPage { Title = "Login", TargetPage = typeof(LogInPage) });
            }

            menulist.ItemsSource = items;
        }
    }
}