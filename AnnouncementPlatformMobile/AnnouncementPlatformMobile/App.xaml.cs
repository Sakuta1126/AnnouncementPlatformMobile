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
        public static List<Announcement> Announcements;
        public static List <Applied> AppliedAnnouncements;

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

            MainPage = new  MainPage();
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
