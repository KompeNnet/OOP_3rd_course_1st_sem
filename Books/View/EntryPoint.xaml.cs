﻿using Books.DataOperation;
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
            if (!controller.AddNewUser(TextBoxRegistration.Text, PaswordBoxRegistration.Password)) return false;
            OpenMainWindow(controller.GetUser(TextBoxRegistration.Text));
            return true;
        }

        private bool LogIn()
        {
            if (!controller.CanLogin(TextBoxLogin.Text, PaswordBoxLogin.Password)) return false;
            OpenMainWindow(controller.GetUser(TextBoxLogin.Text));
            return true;
        }

        private void OpenMainWindow(User user)
        {
            Window window = new MainWindow(user);
            window.Show();
            Close();
        }
    }
}