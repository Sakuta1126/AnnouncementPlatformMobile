using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnnouncementPlatformMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnouncementsPage : ContentPage
    {
        public List<Announcement> Announcements;
        public AnnouncementsPage()
        {
            InitializeComponent();
            LoadAnnouncementsAsync();
            announcementsCollectionView.ItemsSource = Announcements;
        }
        private async void LoadAnnouncementsAsync()
        {
            Announcements = new List<Announcement>();
            List<Announcement> announcements = await App.Database.GetItemsAsync<Announcement>();

            foreach (var announcement in announcements)
            {
                Announcements.Add(announcement);
            }
            announcementsCollectionView.ItemsSource = Announcements;
        }
        private void DeleteAnn_Clicked(object sender, EventArgs e)
        {

        }

        private void ViewDetails_Clicked(object sender, EventArgs e)
        {

        }

        private void Search_Clicked(object sender, EventArgs e)
        {

        }

        private void Clear_Clicked(object sender, EventArgs e)
        {

        }
    }
}