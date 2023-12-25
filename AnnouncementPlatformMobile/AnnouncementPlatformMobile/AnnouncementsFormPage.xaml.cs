using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnnouncementPlatformMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnouncementsFormPage : ContentPage
    {
        public ObservableCollection<Announcement> Announcements { get; set; }
        public AnnouncementsFormPage()
        {
            InitializeComponent();
        }

        private void AddAnnouncement_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PositionName.Text) ||
               PositionLevel.SelectedIndex == -1 || 
               ContractType.SelectedIndex == -1 ||  
               string.IsNullOrWhiteSpace(Company.Text) ||
               string.IsNullOrWhiteSpace(Localization.Text) ||
               WorkingDimension.SelectedIndex == -1 ||  
               Workingtype.SelectedIndex == -1 || 
               string.IsNullOrWhiteSpace(Sallary.Text) ||
               string.IsNullOrWhiteSpace(WorkingDays.Text) ||
               string.IsNullOrWhiteSpace(WorkingHours.Text) ||
               AnnouncementStart.Date == default(DateTime) || 
               AnnouncementEnd.Date == default(DateTime) ||  
               string.IsNullOrWhiteSpace(Category.Text) ||
               string.IsNullOrWhiteSpace(Duties.Text) ||
               string.IsNullOrWhiteSpace(Benefits.Text) ||
               string.IsNullOrWhiteSpace(AboutCompany.Text) ||
               string.IsNullOrWhiteSpace(Requirements.Text))
            {
                 DisplayAlert("Błąd", "All fields must be filled.", "OK");
                return;
            }
            if (!Regex.IsMatch(PositionName.Text, @"^[^\d]+$") ||
                !Regex.IsMatch(Company.Text, @"^[^\d]+$") ||
                !Regex.IsMatch(Category.Text, @"^[^\d]+$") ||
                !Regex.IsMatch(Duties.Text, @"^[^\d]+$") ||
                !Regex.IsMatch(AboutCompany.Text, @"^[^\d]+$")||
                !Regex.IsMatch(WorkingDays.Text, @"^[^\d]+$")||
                !Regex.IsMatch(WorkingHours.Text, @"^[^\d]+$"))
            {
                 DisplayAlert("Błąd", "Fields PositionName, Company, Category,WorkingHours,WorkingDays, Duties and AboutCompany cant include special signs or numbers", "OK");
                return;
            }
            if (!string.IsNullOrWhiteSpace(Sallary.Text) && !Regex.IsMatch(Sallary.Text, @"^\d+$"))
            {
                 DisplayAlert("Błąd", "Sallary field must contain numbers only", "OK");
                return;
            }
            if (AnnouncementEnd.Date < AnnouncementStart.Date)
            {
                 DisplayAlert("Błąd", "End date cant be smaller than start date", "OK");
                return;
            }
            var newAnnouncement = new Announcement
            {
                PositionName = PositionName.Text,
                PositionLevel = PositionLevel.Items[PositionLevel.SelectedIndex],
                ContractType = ContractType.Items[ContractType.SelectedIndex], 
                Company = Company.Text,
                Localization = Localization.Text,
                WorkingDimension = WorkingDimension.Items[WorkingDimension.SelectedIndex], 
                Workingtype = Workingtype.Items[Workingtype.SelectedIndex], 
                Sallary = string.IsNullOrWhiteSpace(Sallary.Text) ? 0 : decimal.Parse(Sallary.Text),
                WorkingDays = WorkingDays.Text,
                WorkingHours = WorkingHours.Text,
                AnnouncementStart = AnnouncementStart.Date, 
                AnnouncementEnd = AnnouncementEnd.Date, 
                Category = Category.Text,
                Duties = Duties.Text,
                Requirements = Requirements.Text,
                Benefits = Benefits.Text,
                AboutCompany = AboutCompany.Text,
            };
           
            App.Database.SaveItemAsync(newAnnouncement);
            DisplayAlert("Information", "Anno Added", "OK");
            App.FlyoutPage.RedirectToHomePage();
            App.UpdateMenuItems();
        }
    }
}