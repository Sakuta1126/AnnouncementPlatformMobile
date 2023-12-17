using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncementPlatformMobile
{
    public class DataBase
    {

        readonly SQLiteAsyncConnection _database;

        public DataBase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Announcement>().Wait();
            _database.CreateTableAsync<Lang>().Wait();
            _database.CreateTableAsync<Account>().Wait();
            _database.CreateTableAsync<Applied>().Wait();
        }
        public Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            return _database.Table<T>().ToListAsync();
        }

        public Task<int> SaveItemAsync<T>(T item)
        {
            return _database.InsertAsync(item);
        }
        public Task<int> DeleteItemAsync<T>(T item)
        {
            return _database.DeleteAsync(item);
        }
        //public async Task ClearDatabaseAsync()
        //{
        //    await _database.DropTableAsync<User>();
        //    await _database.CreateTableAsync<User>();
        //    await _database.DropTableAsync<Account>();
        //    await _database.CreateTableAsync<Account>();
        //    await _database.DropTableAsync<Lang>();
        //    await _database.CreateTableAsync<Lang>();
        //    await _database.DropTableAsync<Announcement>();
        //    await _database.CreateTableAsync<Announcement>();
        //    await _database.DropTableAsync<Applied>();
        //    await _database.CreateTableAsync<Applied>();
        //}
        public async Task ShowAllAnnouncementsAsync()
        {
            try
            {
                // Pobieranie wszystkich ogłoszeń z bazy danych
                var announcementsList = await App.Database.GetItemsAsync<Announcement>();

                // Budowanie długiego stringa, który zawiera wszystkie ogłoszenia
                StringBuilder sb = new StringBuilder();
                foreach (var announcement in announcementsList)
                {
                    sb.AppendLine($"ID: {announcement.Id}, Position: {announcement.PositionName}, Level: {announcement.PositionLevel}, Company: {announcement.Company}");
                    // Dodaj więcej szczegółów według potrzeb
                }

                // Sprawdzanie, czy lista jest pusta
                string result = sb.Length > 0 ? sb.ToString() : "Brak ogłoszeń w bazie danych.";

                // Wyświetlanie wszystkich ogłoszeń w alert dialog
                await App.Current.MainPage.DisplayAlert("Wszystkie ogłoszenia", result, "OK");
            }
            catch (Exception ex)
            {
                // Wyświetlanie błędu, jeśli wystąpił problem
                await App.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił problem podczas ładowania ogłoszeń: {ex.Message}", "OK");
            }
        }


    }

}
