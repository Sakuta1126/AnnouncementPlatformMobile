using AnnouncementPlatformMobile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnnouncementPlatformMobile
{
    public partial class App : Application
    {
        public static List<FlyoutItemPage> MenuItems { get; set; }
        public static List<Announcement> Announcements;
        public static List<Applied> AppliedAnnouncements = new List<Applied>();

        public static FlyoutMenuPage FlyoutMenuPage { get; set; }
        public static MainPage FlyoutPage { get; set; }

        static DataBase database;

            public static DataBase Database
            {
                get
                {
                    if (database == null)
                    {
                        database = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "announcement.db3"));
                    }
                    return database;
                }
            }
            public App()
        {
            InitializeComponent();
            MenuItems = new List<FlyoutItemPage>();
            MainPage = new  MainPage();
        }
        public static  void UpdateMenuItems()
        {
            var items = new List<FlyoutItemPage>();

            items.Add(new FlyoutItemPage { Title = "Home", TargetPage = typeof(HomePage), IconSource = "home.png" });

            if (UserStore.islogged == true )
            {
                items.Add(new FlyoutItemPage { Title = "Announcements", TargetPage = typeof(AnnouncementsPage), IconSource = "announcement.png" });
                items.Add(new FlyoutItemPage { Title = "Applied", TargetPage = typeof(AppliedPage), IconSource = "checked.png" });
                items.Add(new FlyoutItemPage { Title = "Profile Information", TargetPage = typeof(UserFormPage), IconSource = "form.png" });
                items.Add(new FlyoutItemPage { Title = "Profile", TargetPage = typeof(UserProfilePage), IconSource = "user.png" });
                items.Add(new FlyoutItemPage { Title = "LogOut", TargetPage = typeof(LogOutPage), IconSource = "logout.png" });

                if (UserStore.LoggedInUserAdmin)
                {
                    items.Add(new FlyoutItemPage { Title = "Add Announcement", TargetPage = typeof(AnnouncementsFormPage), IconSource = "form.png" });
                }
            }
            else
            {
                items.Add(new FlyoutItemPage { Title = "Register", TargetPage = typeof(RegisterPage), IconSource = "register.png" });
                items.Add(new FlyoutItemPage { Title = "Login", TargetPage = typeof(LogInPage), IconSource = "login.png" });
            }
            App.MenuItems = items;
            App.FlyoutMenuPage.UpdateListView();
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
