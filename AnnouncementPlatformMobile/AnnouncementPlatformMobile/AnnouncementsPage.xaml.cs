using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace AnnouncementPlatformMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnouncementsPage : ContentPage
    {
    
        public AnnouncementsPage()
        {
            InitializeComponent();
            LoadAnnouncementsAsync();
            announcementsCollectionView.ItemsSource =  App.Announcements;
        }
        private async void LoadAnnouncementsAsync()
        {
           App.Announcements = new List<Announcement>();
            List<Announcement> announcements = await App.Database.GetItemsAsync<Announcement>();

            foreach (var announcement in announcements)
            {
                App.Announcements.Add(announcement);
            }
            announcementsCollectionView.ItemsSource = App.Announcements;
        }
      

        private void ViewDetails_Clicked(object sender, EventArgs e)
        {
            var button = sender as Xamarin.Forms.Button;
            if (button != null)
            {
                var announcement = button.CommandParameter as Announcement;
                if (announcement != null)
                {
            
                    var details = $"ID: {announcement.Id}\n" +
                                  $"Position Name: {announcement.PositionName}\n" +
                                  $"Position Level: {announcement.PositionLevel}\n" +
                                  $"Contract Type: {announcement.ContractType}\n" +
                                  $"Company: {announcement.Company}\n" +
                                  $"Localization: {announcement.Localization}\n" +
                                  $"Working Dimension: {announcement.WorkingDimension}\n" +
                                  $"Working Type: {announcement.Workingtype}\n" +
                                  $"Salary: {announcement.Sallary}\n" +
                                  $"Working Days: {announcement.WorkingDays}\n" +
                                  $"Working Hours: {announcement.WorkingHours}\n" +
                                  $"Start Date: {announcement.AnnouncementStart.ToShortDateString()}\n" +
                                  $"End Date: {announcement.AnnouncementEnd.ToShortDateString()}\n" +
                                  $"Category: {announcement.Category}\n" +
                                  $"Duties: {announcement.Duties}\n" +
                                  $"Requirements: {announcement.Requirements}\n" +
                                  $"Benefits: {announcement.Benefits}\n" +
                                  $"About Company: {announcement.AboutCompany}";

                     DisplayAlert("Announcement Details", details, "OK");
                }
            }
        }

        private void Search_Clicked(object sender, EventArgs e)
        {
            // Pobierz tekst wyszukiwania i przekształć go na małe litery.
            string searchText = searchBar.Text?.ToLower() ?? string.Empty;

            // Pobierz wybrane wartości z Pickerów.
            string selectedLevel = GetSelectedPickerValue(PositionLevelPicker);
            string selectedContract = GetSelectedPickerValue(ContractTypePicker);
            string selectedWorkingType = GetSelectedPickerValue(WorkingTypePicker);
            string selectedWorkingDimension = GetSelectedPickerValue(WorkingDimensionPicker);

            // Pobierz ogłoszenia - tutaj musisz dostosować źródło danych.
            var announcements = App.Announcements.ToList();

            // Filtrowanie ogłoszeń.
            var filteredAnnouncements = announcements.Where(ann =>
                (string.IsNullOrEmpty(searchText) || ann.PositionName.ToLower().Contains(searchText)) &&
                (string.IsNullOrEmpty(selectedLevel) || ann.PositionLevel.ToLower() == selectedLevel) &&
                (string.IsNullOrEmpty(selectedContract) || ann.ContractType.ToLower() == selectedContract) &&
                (string.IsNullOrEmpty(selectedWorkingType) || ann.Workingtype.ToLower() == selectedWorkingType) &&
                (string.IsNullOrEmpty(selectedWorkingDimension) || ann.WorkingDimension.ToLower() == selectedWorkingDimension)
            ).ToList();

            // Ustaw źródło danych dla kontrolki, która wyświetla ogłoszenia.
            announcementsCollectionView.ItemsSource = filteredAnnouncements;
        }

        private string GetSelectedPickerValue(Picker picker)
        {
            // Picker.SelectedItem zwraca obiekt, który jest aktualnie wybrany.
            return (picker.SelectedItem as string)?.ToLower() ?? string.Empty;
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            // Wyczyść paski wyszukiwania i wybierania.
            searchBar.Text = string.Empty;
            PositionLevelPicker.SelectedIndex = -1;
            ContractTypePicker.SelectedIndex = -1;
            WorkingTypePicker.SelectedIndex = -1;
            WorkingDimensionPicker.SelectedIndex = -1;

            // Zresetuj źródło danych dla kontrolki wyświetlającej ogłoszenia.
            announcementsCollectionView.ItemsSource = App.Announcements.ToList();
        }

        

        private void Apply_Clicked(object sender, EventArgs e)
        {
            // Pobranie danych związanych z klikniętym elementem.
            var button = (Xamarin.Forms.Button)sender;
            var selectedAnnouncement = (Announcement)button.CommandParameter;

            if (selectedAnnouncement != null)
            {
                Applied appliedItem = new Applied
                {
                    PositionName = selectedAnnouncement.PositionName,
                    PositionLevel = selectedAnnouncement.PositionLevel,
                    ContractType = selectedAnnouncement.ContractType,
                    Company = selectedAnnouncement.Company,
                    Localization = selectedAnnouncement.Localization,
                    WorkingDimension = selectedAnnouncement.WorkingDimension,
                    Workingtype = selectedAnnouncement.Workingtype,
                    Sallary = selectedAnnouncement.Sallary,
                    WorkingDays = selectedAnnouncement.WorkingDays,
                    WorkingHours = selectedAnnouncement.WorkingHours,
                    AnnouncementStart = selectedAnnouncement.AnnouncementStart,
                    AnnouncementEnd = selectedAnnouncement.AnnouncementEnd,
                    Category = selectedAnnouncement.Category,
                    Duties = selectedAnnouncement.Duties,
                    Requirements = selectedAnnouncement.Requirements,
                    Benefits = selectedAnnouncement.Benefits,
                    AboutCompany = selectedAnnouncement.AboutCompany,
                    announcement_id = selectedAnnouncement.Id,
                    user_id = UserStore.LoggedInUserId // Zakładając, że masz klasę UserStore przechowującą ID zalogowanego użytkownika
                };

                // Zapisz aplikację asynchronicznie do bazy danych.
                 App.Database.SaveItemAsync(appliedItem);
                DisplayAlert("Infomation", "Applied", "OK");

                // Aktualizuj listę zastosowanych ogłoszeń, jeśli to konieczne.
                // Możesz potrzebować odświeżyć źródło danych dla 'appliedannouncementsCollectionView'.

            }
        }

        private async void DeleteAnn_Clicked(object sender, EventArgs e)
        {
            if (!(sender is Xamarin.Forms.Button clickedButton)) return;

            var selectedAnnouncement = clickedButton.BindingContext as Announcement;
            if (selectedAnnouncement != null)
            {
                // Asynchroniczne usunięcie ogłoszenia z bazy danych
                await App.Database.DeleteItemAsync(selectedAnnouncement);

                // Usunięcie ogłoszenia z lokalnej listy (zakładam, że istnieje zmienna 'Announcements' typu ObservableCollection<Announcement>)
                App.Announcements.Remove(selectedAnnouncement);

                // Usunięcie powiązanych aplikacji
                await DeleteRelatedApplied(selectedAnnouncement.Id);
            }
            else
            {
                // Wyświetlenie komunikatu o błędzie
                DisplayAlert("Error", "There was an error obtaining the announcement information.", "OK");
            }
        }

        private async Task DeleteRelatedApplied(int announcementId)
        {
            // Pobranie powiązanych aplikacji
            var relatedApplied = await App.Database.GetItemsAsync<Applied>();
            var appliedToDelete = relatedApplied.Where(applied => applied.announcement_id == announcementId).ToList();

            // Asynchroniczne usunięcie powiązanych aplikacji
            foreach (var applied in appliedToDelete)
            {
                await App.Database.DeleteItemAsync(applied);
            }
        }
    }
}