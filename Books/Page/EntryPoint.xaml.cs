using Books.DataOperation;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Books
{
    /// <summary>
    /// Interaction logic for EntryPoint.xaml
    /// </summary>
    public partial class EntryPoint : Window
    {
        private string dataBasePath = "Base.mbb";
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
            if (CheckDataBaseAccessability())
            {
                switch (authoriseMethod.Name)
                {
                    case "RadioButtonLogin":
                        {
                            if (!LogIn()) MessageBox.Show("Sorry, smth goes wrong while we tried ypu to log in.", "Oups!", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        }
                    case "RadioButtonRegistration":
                        {
                            if (!Registrate()) MessageBox.Show("Sorry, smth goes wrong while we tried to registrate you.", "Oups!", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        }
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            authoriseMethod = (RadioButton)sender;
        }

        private bool Registrate()
        {
            if (!UserController.IsConsists(TextBoxLogin.Text, PaswordBoxLogin.Password))
            {
                UserController.AddNewUser(TextBoxLogin.Text, PaswordBoxLogin.Password);
                OpenMainWindow();
                return true;
            }
            return false;
        }

        private bool LogIn()
        {
            if (UserController.IsConsists(TextBoxLogin.Text, PaswordBoxLogin.Password))
            {
                OpenMainWindow();
                return true;
            }
            return false;
        }

        private void OpenMainWindow()
        {
            Window window = new MainWindow();
            window.Show();
            Close();
        }

        private bool CheckDataBaseAccessability()
        {
            if (!File.Exists(dataBasePath))
            {
                MessageBoxResult answer = MessageBox.Show("Smth goes wrong with our database. Sorry for that troble :(", "Ouch...", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (answer == MessageBoxResult.Cancel) Close();
                else return false;
            }
            return true;
        }
    }
}
