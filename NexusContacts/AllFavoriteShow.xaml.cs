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
    /// Interaction logic for AllFavoriteShow.xaml
    /// </summary>
    public partial class AllFavoriteShow : Window
    {
        public AllFavoriteShow(List<Pepole> pepole)
        {
            InitializeComponent();
            // Set the ItemsSource to the list of favorite people
            dgContacts.ItemsSource = pepole.Where(p => p.IsFavorite == true).ToList();
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
    }
}
