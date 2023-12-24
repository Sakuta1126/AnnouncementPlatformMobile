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
          App.UpdateMenuItems();
            UpdateListView();
        }

        public void UpdateListView()
        {
            menulist.ItemsSource = null;
            menulist.ItemsSource = App.MenuItems;
        }
    }
}