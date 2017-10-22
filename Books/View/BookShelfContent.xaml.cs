using Books.DataOperation;
using Books.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Books.View
{
    /// <summary>
    /// Interaction logic for BookShelfContent.xaml
    /// </summary>
    public partial class BookShelfContent : Window
    {
        private BookShelf DisplayedShelf;
        private string CurrentUser { get; set; }

        public BookShelfContent(BookShelf bookShelf, string user)
        {
            InitializeComponent();
            DisplayedShelf = bookShelf;
            Title = DisplayedShelf.Name;
            CurrentUser = user;
            LoadBooks();
        }

        private void LoadBooks()
        {
            ListViewBookCollection.Items.Clear();
            if (DisplayedShelf.Content == null) DisplayedShelf.Content = new List<string>();
            foreach (string id in DisplayedShelf.Content) ListViewBookCollection.Items.Add(BookController.GetBook(id));
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Window window = new BookBase(ref DisplayedShelf, CurrentUser);
            window.Show();
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            foreach (Book item in ListViewBookCollection.SelectedItems)
            {
                foreach (string id in DisplayedShelf.Content)
                    if (id == item.Id)
                    {
                        DisplayedShelf.RemoveBook(id);
                        break;
                    }
            }
            LoadBooks();
        }

        private void ListViewBookCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Book book = BookController.GetBook(((Book)ListViewBookCollection.SelectedItem).Id);
                TextBlockName.Text = book.Name;
                TextBlockAuthor.Text = book.Author;
                TextBlockInfo.Text = book.Info;
            } catch { }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            DisplayedShelf.BookShelfClosed();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            LoadBooks();
        }

        private void ListViewBookCollection_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            foreach (Book b in ListViewBookCollection.SelectedItems)
            {
                Window window = new BookReviews(BookController.GetBook(b.Id), CurrentUser);
                window.Show();
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            DisplayedShelf.ClearBookShelfContent();
            LoadBooks();
        }
    }
}
