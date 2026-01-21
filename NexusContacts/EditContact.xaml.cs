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
        public int tContactID { get; set; }
        public EditContactWin(Pepole p)
        {
            InitializeComponent();
            this.tContactID = p.ContID;
            this.DataContext = this;
            txtFirstName.Text = p.FirstName;
            txtAge.Text = p.Age.ToString();
            txtLastName.Text = p.LastName;
            txtPhone.Text = p.PhoneNumber;

        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string Fname = txtFirstName.Text.ToString();
            string Lname = txtLastName.Text.ToString();
            byte age = byte.Parse(txtAge.Text.ToString());
            string phoneNum = txtPhone.Text.ToString();
            bool FavoCheck = chkIsFavorite.IsChecked ?? false;
            int favSqlValue = FavoCheck ? 1 : 0;
            BaseDal baseDal = new BaseDal();
            string sql = $"UPDATE [Peoples]\n" +
                $"SET Fname = '{Fname}', Lname = '{Lname}', PhoneNum = '{phoneNum}', age = {age}, IsFavorite = {favSqlValue}\n" +
                $"WHERE  contID ={tContactID}";
            baseDal.ExecuteUpdateQuery(sql);
            MessageBox.Show("Contact edited");
            this.Close();
        }

    }
}
