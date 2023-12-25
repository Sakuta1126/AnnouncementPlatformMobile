using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AnnouncementPlatformMobile
{
    public partial class MainPage : FlyoutPage
    {
       
        public MainPage()
        {
            InitializeComponent();
          
            App.FlyoutPage = this;
            flyoutmenu.menulist.ItemSelected += OnSelectedItem;
        }
       
        private void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutItemPage;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetPage));
                flyoutmenu.menulist.SelectedItem = null;
                IsPresented = false;

            }
        }

        public void RedirectToHomePage()
        {
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));
            flyoutmenu.menulist.SelectedItem = null;
            IsPresented = false;
        }
    }
}
