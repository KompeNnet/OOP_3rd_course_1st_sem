using Books.DataOperation;
using Books.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            CurrentUser = user;
            LoadBooks();
        }

        private void LoadBooks()
        {
            try
            {
                ListViewBookBase.Items.Clear();
                if (currentShelf.Content == null) currentShelf.Content = new List<string>();
                foreach (Book b in BookController.GetBookBase()) ListViewBookBase.Items.Add(b);
            }
            catch (ArgumentNullException e)
            {
                MessageBoxResult result = MessageBox.Show("Sorry, smth wrong with " + e.ParamName + ".", "Oups!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                if (result == MessageBoxResult.Cancel) Close();
            }
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
            try
            {
                foreach (Book b in ListViewBookBase.SelectedItems)
                {
                    Window window = new BookReviews(BookController.GetBook(b.Id), CurrentUser);
                    window.Show();
                }
            }
            catch (ArgumentNullException ae)
            {
                MessageBoxResult result = MessageBox.Show("Sorry, smth wrong with " + ae.ParamName + ".", "Oups!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                if (result == MessageBoxResult.Cancel) Close();
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            LoadBooks();
        }
    }
}
