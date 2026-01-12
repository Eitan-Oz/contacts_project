using NexusContacts.models;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ShowAllContacts.xaml
    /// </summary>
    public partial class ShowAllContacts : Window
    {
        public ShowAllContacts()
        {
            InitializeComponent();
            BaseDal dal = new BaseDal();
            DataTable table = new DataTable();
            table = dal.ExecuteSelectAllQuery($"SELECT * FROM [Peoples]");
            dgContacts.ItemsSource = table.AsDataView();
        }

        private void EditCont(object sender, MouseButtonEventArgs e)
        {
            EditContact editContact = new EditContact();
            editContact.Show();
            this.Close();
        }

        private void del_klickUp(object sender, KeyEventArgs e)
        {
            if (dgContacts.SelectedCells != null)
            {
                
            }
        }
    }
}
