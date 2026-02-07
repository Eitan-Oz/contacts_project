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
            this.NextContactID = count + 1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string Fname= txtFirstName.Text.ToString();
            string Lname = txtLastName.Text.ToString();
            byte age = byte.Parse(txtAge.Text.ToString());
            string phoneNum = txtPhone.Text.ToString();
            bool FavoCheck = chkIsFavorite.IsChecked ?? false;
            int favSqlValue = FavoCheck ? 1 : 0;
            BaseDal baseDal = new BaseDal();
            if (baseDal.ExecuteSelectBoolQuery($"SELECT COUNT(*) FROM [Peoples] WHERE [FName] = N'{Fname}' OR [LName] = N'{Lname}'"))
            {
                MessageBoxResult  saveResult =MessageBox.Show("A contact with the same name already exists. Do you want to save anyway?", "Duplicate Contact", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                  if (saveResult==MessageBoxResult.Yes)
                {
                    if (baseDal.ExecuteSelectBoolQuery($"SELECT COUNT(*) FROM [Peoples] WHERE [FName] = N'{Fname}' OR [LName] = N'{Lname}'")) 
                    { 
                    }
                        string sql = $"INSERT INTO [Peoples] (Fname, Lname, PhoneNum, age, [IsFavorite ]) " +
                         $"VALUES (N'{Fname}', N'{Lname}', N'{phoneNum}', {age}, {favSqlValue})";
                    baseDal.ExecuteInsertQuery(sql);
                    MessageBox.Show("Contact added");
                    this.Close();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to chaing the contect informasion?", "close or continue?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                }
            }
            else
            {
                string sql = $"INSERT INTO [Peoples] (Fname, Lname, PhoneNum, age, [IsFavorite ]) " +
                         $"VALUES (N'{Fname}', N'{Lname}', N'{phoneNum}', {age}, {favSqlValue})";
                baseDal.ExecuteInsertQuery(sql);
                MessageBox.Show("Contact added");
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
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^a-z A-Z א-ת]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
