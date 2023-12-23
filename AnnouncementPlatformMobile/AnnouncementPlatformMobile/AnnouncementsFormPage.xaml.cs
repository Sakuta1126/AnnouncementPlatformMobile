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
               PositionLevel.SelectedIndex == -1 ||  // Wybór z listy Picker
               ContractType.SelectedIndex == -1 ||  // Wybór z listy Picker
               string.IsNullOrWhiteSpace(Company.Text) ||
               string.IsNullOrWhiteSpace(Localization.Text) ||
               WorkingDimension.SelectedIndex == -1 ||  // Wybór z listy Picker
               Workingtype.SelectedIndex == -1 ||  // Wybór z listy Picker
               string.IsNullOrWhiteSpace(Sallary.Text) ||
               string.IsNullOrWhiteSpace(WorkingDays.Text) ||
               string.IsNullOrWhiteSpace(WorkingHours.Text) ||
               AnnouncementStart.Date == default(DateTime) ||  // Używamy właściwości Date
               AnnouncementEnd.Date == default(DateTime) ||  // Używamy właściwości Date
               string.IsNullOrWhiteSpace(Category.Text) ||
               string.IsNullOrWhiteSpace(Duties.Text) ||
               string.IsNullOrWhiteSpace(Benefits.Text) ||
               string.IsNullOrWhiteSpace(AboutCompany.Text) ||
               string.IsNullOrWhiteSpace(Requirements.Text))
            {
                 DisplayAlert("Błąd", "Wszystkie wymagane pola muszą być wypełnione.", "OK");
                return;
            }
            if (!Regex.IsMatch(PositionName.Text, @"^[^\d]+$") ||
                !Regex.IsMatch(Company.Text, @"^[^\d]+$") ||
                !Regex.IsMatch(Category.Text, @"^[^\d]+$") ||
                !Regex.IsMatch(Duties.Text, @"^[^\d]+$") ||
                !Regex.IsMatch(AboutCompany.Text, @"^[^\d]+$"))
            {
                 DisplayAlert("Błąd", "Pola PositionName, Company, Category, Duties i AboutCompany nie mogą zawierać cyfr ani znaków specjalnych.", "OK");
                return;
            }
            if (!string.IsNullOrWhiteSpace(Sallary.Text) && !Regex.IsMatch(Sallary.Text, @"^\d+$"))
            {
                 DisplayAlert("Błąd", "Pole Salary może zawierać tylko cyfry.", "OK");
                return;
            }
            if (AnnouncementEnd.Date < AnnouncementStart.Date)
            {
                 DisplayAlert("Błąd", "Data zakończenia ogłoszenia nie może być wcześniejsza niż data rozpoczęcia.", "OK");
                return;
            }
            var newAnnouncement = new Announcement
            {
                PositionName = PositionName.Text,
                PositionLevel = PositionLevel.Items[PositionLevel.SelectedIndex], // zamiast ComboBoxItem
                ContractType = ContractType.Items[ContractType.SelectedIndex], // zamiast ComboBoxItem
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
            DisplayAlert("Informacja", "Dodano ogloszenie", "OK");
            Navigation.PushAsync(new HomePage());
        }
    }
}