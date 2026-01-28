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
                string sqlsand = "CREATE TABLE [Cities] (\n" +
                "Id INT IDENTITY(1,1) PRIMARY KEY,\n" +
                "GovId INT, \n" +
                "city_code INT,\n" +
                "city_name_he NVARCHAR(100),\n" +
                "city_name_en NVARCHAR(100)\n" +
                ");";

                  dal.ExecuteUpdateQuery(sqlsand);
            }


            string url = "https://data.gov.il/api/3/action/datastore_search?resource_id=8f714b6f-c35c-4b40-a0e7-547b675eee0e";
            string jsonResult = await GetGovDataAsync(url);
            if (jsonResult != null)
            {
                // 1. המרה: הופכים את הטקסט לאובייקטים
                var data = JsonSerializer.Deserialize<GovApiResponse>(jsonResult);

                if (data != null && data.Result != null)
                {
                    // 2. לולאה ושמירה למסד הנתונים
                    foreach (var city in data.Result.Records)
                    {
                        // המרת סמל היישוב למספר
                        int codeVal = 0;
                        int.TryParse(city.CityCode, out codeVal);

                        // ניקוי גרשיים כדי לא לשבור את ה-SQL
                        string safeHebrew = city.NameHebrew != null ? city.NameHebrew.Replace("'", "''") : "";
                        string safeEnglish = city.NameEnglish != null ? city.NameEnglish.Replace("'", "''") : "";

                        // יצירת השאילתה
                        string insertSql = $"INSERT INTO Cities (city_code, city_name_he, city_name_en) VALUES ({codeVal}, N'{safeHebrew}', N'{safeEnglish}')";

                        // ביצוע השמירה
                        dal.ExecuteInsertQuery(insertSql);
                    }

                    MessageBox.Show($"בוצע בהצלחה! {data.Result.Records.Count} ערים נשמרו.");
                }
            }
        }
    }
}