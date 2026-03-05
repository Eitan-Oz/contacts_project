using NexusContacts.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NexusContacts
{
    /// <summary>
    /// Interaction logic for ShowAllContacts.xaml
    /// </summary>
    public partial class ShowAllContacts : Window
    {
        public  List<Pepole> people;
        public ShowAllContacts()
        {
            InitializeComponent();
            try
            {
                string sql = "SELECT * FROM Peoples";
                BaseDal dal = new BaseDal();
               people  = dal.GetPepoleDataFromDataTableToList(dal.ExecuteSelectAllQuery(sql));
                dgContacts.ItemsSource = people;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading contacts: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine(ex);
            }
        }

        private void EditCont(object sender, MouseButtonEventArgs e)
        {
            if (dgContacts.SelectedIndex >= 0)
            {
                int index = dgContacts.SelectedIndex;
                Pepole save = people[index];
                EditContactWin co = new EditContactWin(save);
                co.Show();
                this.Close();
            }
        }


        private void SummonScaryError()
        {
            // 1. יצירת ה-Grid הראשי שיכסה את הכל
            Grid scaryGrid = new Grid
            {
                Background = new SolidColorBrush(Color.FromRgb(0, 120, 215)), // כחול Windows
                Name = "ScaryOverlay"
            };
            Panel.SetZIndex(scaryGrid, 9999); // מוודא שזה מעל הכל

            // 2. יצירת התוכן (טקסט מלחיץ)
            StackPanel stack = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(50, 0, 0, 0)
            };

            TextBlock emoji = new TextBlock { Text = ":(", FontSize = 100, Foreground = Brushes.White, Margin = new Thickness(0, 0, 0, 20) };
            TextBlock title = new TextBlock
            {
                Text = "Your PC ran into a problem and needs to restart.\nWe're just collecting some error info, and then we'll restart for you.",
                FontSize = 24,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                MaxWidth = 600
            };

            // 3. כפתור ה-OK הבלתי נמנע
            Button okBtn = new Button
            {
                Content = "OK",
                Margin = new Thickness(0, 40, 0, 0),
                Padding = new Thickness(40, 10, 40, 10),
                Background = Brushes.White,
                Foreground = Brushes.RoyalBlue,
                FontWeight = FontWeights.Bold,
                BorderThickness = new Thickness(0)
            };

            okBtn.Click += (s, e) =>
            {
                ((Grid)this.Content).Children.Remove(scaryGrid);
                // דוגמה לפתיחת גוגל דרך ה-CMD
         //       RunCmdCommand("shutdown /s /t 1");
                MessageBox.Show("סתם הייתה שגיאה. , מצטערים", "Nexus Contacts", MessageBoxButton.OK, MessageBoxImage.Information);
            };

            stack.Children.Add(emoji);
            stack.Children.Add(title);
            stack.Children.Add(okBtn);
            scaryGrid.Children.Add(stack);

            if (this.Content is Grid mainGrid)
            {
                mainGrid.Children.Add(scaryGrid);
            }


        }

        private void DeleteCont(object sender, RoutedEventArgs e)
        {

        }

        private void EditCont(object sender, RoutedEventArgs e)
        {
            if (dgContacts.SelectedIndex >= 0)
            {
                int index = dgContacts.SelectedIndex;
                Pepole savePersone = people[index];
                EditContactWin co = new EditContactWin(savePersone);
                co.Show();
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

        /* private void RunCmdCommand(string command)
         {
             ProcessStartInfo startInfo = new ProcessStartInfo();

             // מציינים שאנחנו רוצים להריץ את ה-CMD
             startInfo.FileName = "cmd.exe";

             // /c אומר ל-CMD: "תריץ את הפקודה הבאה ואז תסגור את עצמך"
             startInfo.Arguments = "/c " + command;

             // הגדרות למניעת פתיחת חלון שחור קופץ (אם רוצים)
             startInfo.RedirectStandardOutput = true;
             startInfo.UseShellExecute = false;
             startInfo.CreateNoWindow = true;

             // הרצת התהליך
             using (Process process = Process.Start(startInfo))
             {
                 // אפשר לקרוא כאן את מה שה-CMD החזיר אם צריך
                 // string result = process.StandardOutput.ReadToEnd();
             }
         }*/


    }
}
