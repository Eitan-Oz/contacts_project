using NexusContacts.models;
using System.Collections.Generic;
using System.Diagnostics;
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
        private async void  SetCity_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://data.gov.il/api/3/action/datastore_search?resource_id=8f714b6f-c35c-4b40-a0e7-547b675eee0e";
            string jsonResult = await GetGovDataAsync(url);
            if (jsonResult != null)
            {

              //  BaseDal dal = new BaseDal();
              //  string sqlsand = "CREATE TABLE [Cities] (\n" +
              //      "Id INT PRIMARY KEY,\n" +
              //      "city_code INT,\n" +
              //      "city_name_he NVARCHAR(100),\n" +
              //      "city_name_en NVARCHAR(100)\n" +
              //      ");";
                   
              //int esolt=  dal.ExecuteSelectIntQuery(sqlsand);
                MessageBox.Show("I got the data"+jsonResult);
            }
            else
            {
                MessageBox.Show("We got aproblam to get the data");
            }
        }
    }
}