using Books.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            foreach (BookShelf b in user.ShelfCollection.GetBookShelfList())
                ListViewBookShelfCollection.Items.Add(b);
        }

        private void ListViewBookShelfCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BookShelf selectedShelf = user.ShelfCollection.GetBookShelfList().ElementAt(((ListView)sender).SelectedIndex);
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
            BookShelf currentShelf = user.ShelfCollection.GetBookShelfList().ElementAt(ListViewBookShelfCollection.SelectedIndex);
            user.ShelfCollection.RemoveBookShelf(currentShelf);
            if (NewBookshelf(TextBoxBookShelfName.Text, currentShelf.GetBookList()))
                LoadBookShelfCollection();
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            foreach (BookShelf item in ListViewBookShelfCollection.SelectedItems)
            {
                foreach (BookShelf b in user.ShelfCollection.GetBookShelfList())
                    if (b.Name == item.Name)
                    {
                        user.ShelfCollection.RemoveBookShelf(b);
                        break;
                    }
            }
            LoadBookShelfCollection();
        }

        private void TextBoxUserInfo_TextChanged(object sender, TextChangedEventArgs e)
        {
            user.Info = ((TextBox)sender).Text;
        }

        private bool NewBookshelf(string name, List<Book> content = null)
        {
            foreach (BookShelf b in user.ShelfCollection.GetBookShelfList())
                if (b.Name == TextBoxBookShelfName.Text) return false;
            if (user.ShelfCollection.AddBookShelf(new BookShelf(name, content))) return true;
            return false;
        }
    }
}
