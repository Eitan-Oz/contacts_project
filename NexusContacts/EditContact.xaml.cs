using NexusContacts.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NexusContacts
{
    /// <summary>
    /// Interaction logic for EditContact.xaml
    /// </summary>
    public partial class EditContactWin : Window
    {
        public Pepole P;

        public EditContactWin(Pepole p)
        {
            InitializeComponent();

            this.P = p;
            this.DataContext = P;
            txtFirstName.Text = p.FirstName;
            txtAge.Text = p.Age.ToString();
            txtLastName.Text = p.LastName;
            txtPhone.Text = p.PhoneNumber;
            chkIsFavorite.IsChecked = p.IsFavorite;


            // התיקון: שימוש ב-LINQ כדי למצוא את אובייקט העיר המתאים מתוך הרשימה
            // אנחנו מחפשים עיר שה-CityID שלה שווה ל-CityId של איש הקשר (p)
            if (App.allCities != null)
            {
                cmbCity.ItemsSource = App.allCities.Select(c => c.CityNameHe).ToList();
                cmbCity.SelectedItem = App.allCities.FirstOrDefault(c => c.CityID == p.City.CityID)?.CityID;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // בדיקת תקינות - האם נבחרה עיר
            if (cmbCity.SelectedItem == null)
            {
                MessageBox.Show("Please select a city.");
                return;
            }

            string Fname = txtFirstName.Text.ToString();
            string Lname = txtLastName.Text.ToString();

            // המרת הגיל תוך הנחה שהקלט תקין (לפי הקוד שלך)
            byte age;
            if (!byte.TryParse(txtAge.Text.ToString(), out age))
            {
                MessageBox.Show("Please enter a valid age.");
                return;
            }

            string phoneNum = txtPhone.Text.ToString();
            bool FavoCheck = chkIsFavorite.IsChecked ?? false;
            int favSqlValue = FavoCheck ? 1 : 0;

           
            City city = App.allCities.FirstOrDefault(c => c.CityNameHe == cmbCity.SelectedItem.ToString());

            
            MessageBox.Show($"Selected City: {city.CityNameHe} (ID: {city.CityID})");

            BaseDal baseDal = new BaseDal();

            // בניית שאילתת ה-UPDATE הכוללת את ה-CityId החדש
            string sql = $"UPDATE [Peoples]\n" +
                $"SET Fname = N'{Fname}', Lname = N'{Lname}', PhoneNum = '{phoneNum}', age = {age}, IsFavorite = {favSqlValue}, CityId = {city.CityID}\n" +
                $"WHERE contID = {P.ContID}";

            try
            {
                baseDal.ExecuteUpdateQuery(sql);
                MessageBox.Show("Contact edited");

                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving contact: {ex.Message}");
            }
        }

        private void GoBack(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                ShowAllContacts window = new ShowAllContacts();
                window.Show();
                this.Close();
            }
        }
    }
}