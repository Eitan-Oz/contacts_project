using NexusContacts.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;

namespace NexusContacts
{
    /// <summary>
    /// Interaction logic for AddContact.xaml
    /// </summary>
    public partial class AddContact : Window
    {
        public int NextContactID { get; set; }
        public AddContact()
        {
            InitializeComponent();
            BaseDal cont = new BaseDal();
            this.DataContext = this;
            int count = cont.ExecuteSelectIntQuery("SELECT COUNT(contID) FROM [Peoples]");
            cmbCity.ItemsSource = App.allCities.Select(c => c.CityNameHe).ToList();
            this.NextContactID = count + 1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string Fname = txtFirstName.Text.ToString();
            string Lname = txtLastName.Text.ToString();
            if (!byte.TryParse(txtAge.Text.ToString(), out byte age)||age>120)
            {
                MessageBox.Show("Please enter a valid age.");
                return;
            }
            string phoneNum = txtPhone.Text.ToString();
            bool FavoCheck = chkIsFavorite.IsChecked ?? false;
            int favSqlValue = FavoCheck ? 1 : 0;
            BaseDal baseDal = new BaseDal();
            string sql1 = $"SELECT COUNT(*) FROM [Peoples] WHERE [FName] = N'{Fname}' OR [LName] = N'{Lname}'";
            bool isDuplicate = baseDal.ExecuteSelectBoolQuery(sql1);
            if (isDuplicate)
            {
                MessageBoxResult saveResult = MessageBox.Show("A contact with the same name already exists. Do you want to save anyway?", "Duplicate Contact", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (saveResult == MessageBoxResult.Yes)
                {
                    

                    string sql = $"INSERT INTO [Peoples] (Fname, Lname, PhoneNum, age, [IsFavorite ], [CityID]) " +
                     $"VALUES (N'{Fname}', N'{Lname}', N'{phoneNum}', {age}, {favSqlValue}," +
                     $"{App.allCities[cmbCity.SelectedIndex].CityID})";
                    baseDal.ExecuteInsertQuery(sql);
                    MessageBox.Show("Contact added");
                    this.Close();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to change the contact information?", "Close or continue?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        // User wants to change the contact information, so we do nothing and allow them to edit the fields
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            else
            {
                int cityID = App.allCities[cmbCity.SelectedIndex].CityID;   
                string sql = $"INSERT INTO [Peoples] (Fname, Lname, PhoneNum, age, [IsFavorite ], [CityID]) " +
                     $"VALUES (N'{Fname}', N'{Lname}', N'{phoneNum}', {age}, {favSqlValue}," +
                     $"{cityID})";
                baseDal.ExecuteInsertQuery(sql);
                MessageBox.Show("Contact added");
                this.Close();
            }



        }

        private void GoBack(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }
        private void NumericTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void TextOnlyTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^a-z A-Z à-ú]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
