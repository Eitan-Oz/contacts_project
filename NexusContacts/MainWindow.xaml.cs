using NexusContacts.models;
using System.Collections.Generic;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NexusContacts
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            SetChoices();

        }

        public void SetChoices()
        {
            string[] choicesL = new string[3] { "Add Contact", "All Contacts", "Favorite Contacts" };

            for (int i = 0; i < choicesL.Length; i++)
            {
                Button btn = new Button();
                btn.Content = choicesL[i];
                btn.Margin = new Thickness(5);
                switch (i)
                {
                    case 0:
                        btn.Click += AddContact;
                        break;
                    case 1:
                        btn.Click += ContactsShow;
                        break;
                    case 2:
                        btn.Click += FavoriteContactsShow;
                        break;
                }
                ChoicesGrid.Children.Add(btn);
            }

        }


        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ContactsShow(object sender, RoutedEventArgs e)
        {
            ShowAllContacts showAllContacts = new ShowAllContacts();
            showAllContacts.Show();
            
            this.Close();
        }
        private void AddContact(object sender, RoutedEventArgs e)
        {
            AddContact addContactWindow = new AddContact();
            addContactWindow.ShowDialog();

        }
        private void FavoriteContactsShow(object sender, RoutedEventArgs e)
        {
            ShowAllContacts take = new ShowAllContacts();
            take.Activate();
            List<Pepole> getP = take.people;
            take.Close();
            AllFavoriteShow favoriteShow = new AllFavoriteShow(getP);
            favoriteShow.Show();
            this.Close();
        }
        private static readonly HttpClient client = new HttpClient();
        public async Task<string> GetGovDataAsync(string APIUrl)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(APIUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Request exception: {e.Message}");
                return null;
            }
        }
        private async void SetCity_Click(object sender, RoutedEventArgs e)
        {
            BaseDal dal = new BaseDal();
            if (!dal.ExecuteSelectBoolQuery("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Cities'"))
            {
                string createTableSql =
                    "CREATE TABLE [Cities] (" +
                    "Id INT IDENTITY(1,1) PRIMARY KEY," +
                    "GovId INT, " +
                    "city_code INT," +
                    "city_name_he NVARCHAR(100)," +
                    "city_name_en NVARCHAR(100)" +
                    ");";
                dal.ExecuteUpdateQuery(createTableSql);
            }

            string url = "https://data.gov.il/api/3/action/datastore_search?resource_id=8f714b6f-c35c-4b40-a0e7-547b675eee0e";
            string jsonResult = await GetGovDataAsync(url);
            if (jsonResult != null)
            {
                var data = JsonSerializer.Deserialize<GovApiResponse>(jsonResult);
                if (data != null && data.Result != null)
                {
                    foreach (CityRecord city in data.Result.Records)
                    {
                        // Use city.CityCode directly (it's now int)
                        int cityCode = city.CityCode;

                        // Clean strings for SQL
                        string safeHebrew = string.IsNullOrEmpty(city.NameHebrew) ? string.Empty : city.NameHebrew.Replace("'", "''");
                        string safeEnglish = string.IsNullOrEmpty(city.NameEnglish) ? string.Empty : city.NameEnglish.Replace("'", "''");

                        // Build and execute insert query
                        string insertSql = $"INSERT INTO Cities (city_code, city_name_he, city_name_en) VALUES ({cityCode}, N'{safeHebrew}', N'{safeEnglish}')";
                        dal.ExecuteInsertQuery(insertSql);
                    }
                    MessageBox.Show($"בוצע בהצלחה! {data.Result.Records.Count} ערים נשמרו.");
                }
            }
        }
    }
}