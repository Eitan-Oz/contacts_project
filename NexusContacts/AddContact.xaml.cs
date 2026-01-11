using System;
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

        }
    }
}
