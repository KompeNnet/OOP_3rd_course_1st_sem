using Books.DataOperation;
using Books.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Books
{
    /// <summary>
    /// Interaction logic for EntryPoint.xaml
    /// </summary>
    public partial class EntryPoint : Window
    {
        private Dictionary<string, bool> authorisationMethodsDict = new Dictionary<string, bool>();
        private RadioButton authoriseMethod;

        public EntryPoint()
        {
            InitializeComponent();
            FillAuthorisationMethodsDict();
        }

        private void FillAuthorisationMethodsDict()
        {
            authorisationMethodsDict.Add(RadioButtonLogin.Name, (bool)RadioButtonLogin.IsChecked);
            authorisationMethodsDict.Add(RadioButtonRegistration.Name, (bool)RadioButtonRegistration.IsChecked);
        }

        private void ButtonLetMeIn_Click(object sender, RoutedEventArgs e)
        {
            switch (authoriseMethod.Name)
            {
                case "RadioButtonLogin":
                    {
                        LogIn();
                        break;
                    }
                case "RadioButtonRegistration":
                    {
                        Registrate();
                        break;
                    }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            authoriseMethod = (RadioButton)sender;
        }

        private bool Registrate()
        {
            try
            {
                if (!UserController.AddNewUser(TextBoxAuthentication.Text, PaswordBoxAuthentication.Password)) return false;
                OpenMainWindow(UserController.GetUser(TextBoxAuthentication.Text));
                return true;
            }
            catch (ArgumentNullException e)
            {
                MessageBoxResult result = MessageBox.Show("Sorry, smth wrong with " + e.ParamName + ".", "Oups!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                if (result == MessageBoxResult.Cancel) Close();
                return false;
            }
        }

        private bool LogIn()
        {
            try
            {
                if (!UserController.AddNewUser(TextBoxAuthentication.Text, PaswordBoxAuthentication.Password)) return false;
                OpenMainWindow(UserController.GetUser(TextBoxAuthentication.Text));
                return true;
            }
            catch
            {
                if (!UserController.CanLogin(TextBoxAuthentication.Text, PaswordBoxAuthentication.Password)) return false;
                OpenMainWindow(UserController.GetUser(TextBoxAuthentication.Text));
                return false;
            }
        }

        private void OpenMainWindow(User user)
        {
            Window window = new MainWindow(user);
            window.Show();
            Close();
        }
    }
}
