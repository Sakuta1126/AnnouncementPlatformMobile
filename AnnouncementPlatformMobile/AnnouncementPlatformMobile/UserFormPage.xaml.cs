﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnnouncementPlatformMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserFormPage : ContentPage
    {
        public UserFormPage()
        {
            InitializeComponent();
        }

       

        private async void ChooseImage_Clicked(object sender, EventArgs e)
        {
            var result = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();
            if (result != null)
            {
                PhotoPath.Text = result.FullPath;

                ProfileImage.Source = ImageSource.FromFile(result.FullPath);
            }
        }

        private async void ConfirmProfileBtn_Clicked(object sender, EventArgs e)
        {
            var existingUser = (await App.Database.GetItemsAsync<User>()).FirstOrDefault(u => u.user_id == UserStore.LoggedInUserId);

            if (existingUser != null)
            {
                await DisplayAlert("Error", "Form has been filled before", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Name.Text) ||
                string.IsNullOrWhiteSpace(Surname.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(ResidenceAddress.Text) ||
                BirthDate.Date == null ||
                string.IsNullOrWhiteSpace(Email.Text) ||
                string.IsNullOrWhiteSpace(Summary.Text) ||
                string.IsNullOrWhiteSpace(Lang.Text) ||
                LangLevel.SelectedItem == null)
            {
                await DisplayAlert("Error", "All fields must be filled", "OK");
                return;
            }

            if (!Regex.IsMatch(Email.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                await DisplayAlert("Error", "Incorrect format of email", "OK");
                return;
            }

            if (!Regex.IsMatch(PhoneNumber.Text, @"^[0-9]{9}$"))
            {
                await DisplayAlert("Error", "Incorrect format of phone number", "OK");
                return;
            }

            if (BirthDate.Date > DateTime.Now)
            {
                await DisplayAlert("Error", "You cant use it if ur born today damn", "OK");
                return;
            }

            var user = new User
            {
                Name = Name.Text,
                Surname = Surname.Text,
                BirthDate = BirthDate.Date,
                Email = Email.Text,
                PhoneNumber = int.Parse(PhoneNumber.Text),
                ResidenceAddress = ResidenceAddress.Text,
                Summary = Summary.Text,
                PFP = PhotoPath.Text,
                user_id = UserStore.LoggedInUserId
            };

            await App.Database.SaveItemAsync(user);

            var lang = new Lang
            {
                Language = Lang.Text,
                LanguageLevel = LangLevel.SelectedItem?.ToString(),
                user_id = UserStore.LoggedInUserId
            };

            await App.Database.SaveItemAsync(lang);

            await DisplayAlert("Success", "User added succesfully!", "OK");
            App.FlyoutPage.RedirectToHomePage();
            Name.Text = string.Empty;
            Surname.Text = string.Empty;
            PhoneNumber.Text = string.Empty;
            ResidenceAddress.Text = string.Empty;
            BirthDate.Date = DateTime.Now;
            Email.Text = string.Empty;
            Summary.Text = string.Empty;
            Lang.Text = string.Empty;
            LangLevel.SelectedIndex = -1;
            PhotoPath.Text = string.Empty;
            ProfileImage.Source = null;
          
        }
    }
}