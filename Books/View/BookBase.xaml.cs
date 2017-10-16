using Books.DataOperation;
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
using System.Windows.Shapes;

namespace Books.View
{
    /// <summary>
    /// Interaction logic for BookBase.xaml
    /// </summary>
    public partial class BookBase : Window
    {
        private BookShelf currentShelf;
        private string CurrentUser { get; set; }

        public BookBase(ref BookShelf currentShelf, string user)
        {
            InitializeComponent();
            this.currentShelf = currentShelf;
            BookController.LoadBookData();
            CurrentUser = user;
            LoadBooks();
        }

        private void LoadBooks()
        {
            ListViewBookBase.Items.Clear();
            if (currentShelf.Content == null) currentShelf.Content = new List<string>();
            foreach (Book b in BookController.GetBookBase()) ListViewBookBase.Items.Add(b);
        }

        private void ButtonAddBook_Click(object sender, RoutedEventArgs e)
        {
            foreach (Book b in ListViewBookBase.SelectedItems) currentShelf.AddBook(b.Id);
            Close();
        }

        private void ButtonAddRequest_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void ListViewBookBase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Book book = BookController.GetBook(((Book)ListViewBookBase.SelectedItem).Id);
                TextBlockName.Text = book.Name;
                TextBlockAuthor.Text = book.Author;
                TextBlockInfo.Text = book.Info;
            } catch { }
        }

        private void ListViewBookBase_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            foreach (Book b in ListViewBookBase.SelectedItems)
            {
                Window window = new BookReviews(BookController.GetBook(b.Id), CurrentUser);
                window.Show();
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            LoadBooks();
        }
    }
}
