using NexusContacts.models;
using System.Collections.Generic;
using System.Diagnostics;
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

    }
}