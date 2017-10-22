using Books.DataOperation;
using Books.Model;
using Books.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Books
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User user;

        public MainWindow(User currenntUser)
        {
            InitializeComponent();
            user = currenntUser;
            FillInfoInForm();
        }

        private void FillInfoInForm()
        {
            TextBlockGreeting.Text = "Hello, " + user.Login + "!";
            TextBoxUserInfo.Text = user.Info;
            LoadBookShelfCollection();
        }

        private void LoadBookShelfCollection()
        {
            ListViewBookShelfCollection.Items.Clear();
            if (user.ShelfCollection != null)
            foreach (BookShelf b in user.ShelfCollection.Values)
                ListViewBookShelfCollection.Items.Add(b);
        }

        private void ListViewBookShelfCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BookShelf selectedShelf = user.ShelfCollection.ElementAt(((ListView)sender).SelectedIndex).Value;
                TextBoxBookShelfName.Text = selectedShelf.Name;
            } catch { }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxBookShelfName.Text == "Hi!" || TextBoxBookShelfName.Text == "") return;
            if (NewBookshelf(TextBoxBookShelfName.Text)) LoadBookShelfCollection();
        }
        
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewBookShelfCollection.Items.Count <= 0 || ListViewBookShelfCollection.SelectedItems.Count <= 0) return;
            if (TextBoxBookShelfName.Text == "") return;
            BookShelf currentShelf = user.ShelfCollection.ElementAt(ListViewBookShelfCollection.SelectedIndex).Value;
            user.ShelfCollection.Remove(currentShelf.Name);
            if (NewBookshelf(TextBoxBookShelfName.Text, currentShelf.Content))
                LoadBookShelfCollection();
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            foreach (BookShelf item in ListViewBookShelfCollection.SelectedItems)
            {
                foreach (BookShelf b in user.ShelfCollection.Values)
                    if (b.Name == item.Name)
                    {
                        user.ShelfCollection.Remove(b.Name);
                        break;
                    }
            }
            LoadBookShelfCollection();
        }

        private void TextBoxUserInfo_TextChanged(object sender, TextChangedEventArgs e)
        {
            user.Info = ((TextBox)sender).Text;
        }

        private bool NewBookshelf(string name, List<string> content = null)
        {
            if (user.ShelfCollection.ContainsKey(name)) return false;
            user.ShelfCollection.Add(name, new BookShelf(name, content));
            LoadBookShelfCollection();
            return false;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            UserController.SaveUserData();
        }

        private void ListViewBookShelfCollection_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            foreach (BookShelf item in ListViewBookShelfCollection.SelectedItems)
            {
                if (user.ShelfCollection[item.Name].IsOpened()) continue;
                Window window = new BookShelfContent(user.ShelfCollection[item.Name], user.Login);
                window.Show();
                user.ShelfCollection[item.Name].BookShelfOpened();
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            LoadBookShelfCollection();
        }
    }
}
