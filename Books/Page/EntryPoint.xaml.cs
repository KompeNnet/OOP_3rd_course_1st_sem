using Books.DataOperation;
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
        private UserController controller;

        public EntryPoint()
        {
            InitializeComponent();
            FillAuthorisationMethodsDict();
            try { controller = new UserController(); }
            catch (ArgumentNullException e)
            {
                MessageBoxResult result = MessageBox.Show("Sorry, smth wrong with " + e.ParamName + ".", "Oups!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                if (result == MessageBoxResult.Cancel) Close();
            }
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
            if (!controller.AddNewUser(TextBoxLogin.Text, PaswordBoxLogin.Password)) return false;
            OpenMainWindow();
            return true;
        }

        private bool LogIn()
        {
            if (controller.CanLogin(TextBoxLogin.Text, PaswordBoxLogin.Password)) return false;
            OpenMainWindow();
            return true;
        }

        private void OpenMainWindow()
        {
            Window window = new MainWindow();
            window.Show();
            Close();
        }
    }
}
