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
   
    public partial class AppliedPage : ContentPage
    {

        public AppliedPage()
        {
            InitializeComponent();
           LoadAppliedAsync();
        }
        private async void LoadAppliedAsync()
        {
           
            List<Applied> allApplied = await App.Database.GetItemsAsync<Applied>();

            var loggedInUser = UserStore.LoggedInUserId;



            if (loggedInUser != 0)
            {

                List<Applied> userApplied = allApplied.Where(apply => apply.user_id == loggedInUser).ToList();


                App.AppliedAnnouncements.Clear();


                foreach (Applied userApply in userApplied)
                {
                    App.AppliedAnnouncements.Add(userApply);
                }
                    appliedannouncementsCollectionView.ItemsSource = App.AppliedAnnouncements;
            }
           
           
        }

      
    }
}