using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace NexusContacts
{
    public partial class MainWindow : Window
    {
        public MessageBoxResult languageResult;

        public MainWindow()
        {
            languageResult = MessageBox.Show("Do you want to stay in English? \n האם תרצה שהתוכנה תשאר באנגלית?",
                                             "Set Language / הגדרת שפה",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);
            InitializeComponent();
            SetChoices();

        }

        public void SetChoices()
        {
            if (languageResult == MessageBoxResult.Yes) 
            {
                string[] choicesL = new string[3] { "Add Contact", "All Contacts", "Favorite Contacts" };

                for (int i = 0; i < choicesL.Length; i++)
                {
                    Button btn = new Button();
                    btn.Content = choicesL[i];
                    btn.Margin = new Thickness(5);
                    switch(i)
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
            else if (languageResult == MessageBoxResult.No) 
            {
                Chetext.Text = "ברוכים הבאים לאפליקצית אנשי הקשר";
                string[] choicesL = new string[3] { "הוספת איש קשר", "רשימת אנשי קשר", "אנשי קשר מועדפים" };

                for (int i = 0; i < choicesL.Length; i++)
                {
                    Button btn = new Button();
                    btn.Content = choicesL[i];
                    btn.Margin = new Thickness(5);
                    ChoicesGrid.Children.Add(btn);
                }
            }
            else
            {
                MessageBox.Show("Something went wrong. I think you closed the window by pressing X.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Stop);
                this.Close();
            }
        }

        private void Btn_Click1(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ContactsShow(object sender, RoutedEventArgs e)
        {

        }
        private void AddContact(object sender, RoutedEventArgs e)
        {
            AddContact addContactWindow = new AddContact();
            addContactWindow.ShowDialog();
            
        }
        private void FavoriteContactsShow(object sender, RoutedEventArgs e)
        {

        }

    }
}