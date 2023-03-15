using BIZ;
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

namespace TFApp
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        ClassBIZ BIZ;
        MainWindow mainWindow;
        public LoginPage(ClassBIZ inBIZ, MainWindow inMainWindow)
        {
            InitializeComponent();
            mainWindow = inMainWindow;
            BIZ = inBIZ;
            DataContext = inBIZ;
        }

        private void LoginToAccount(object sender, RoutedEventArgs e)
        {
            if (BIZ.loginUser.username.Length > 0 && BIZ.loginUser.username.Length <= 64)
            {
                if (BIZ.loginUser.password.Length > 0 && BIZ.loginUser.password.Length <= 64)
                {
                    BIZ.Login(BIZ.loginUser.username, BIZ.loginUser.password);
                    if (BIZ.loginstatus.username == "logincorrect")
                    {
                        mainWindow.ChangePage("oversigtPage");
                    }
                }
                else
                {
                    MessageBox.Show($"Hey dig der! dit Password overholder ikke længde reglerne. Max 64 karaktere", "Indtastsfejl", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show($"Hey dig der! dit Username overholder ikke længde reglerne. Max 64 karaktere", "Indtastsfejl", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void RegisterNewAccount(object sender, RoutedEventArgs e)
        {
            if (BIZ.registerUser.navn.Length > 0 && BIZ.loginUser.navn.Length <= 64)
            {
                if (BIZ.registerUser.username.Length > 0 && BIZ.loginUser.username.Length <= 64)
                {
                    if (BIZ.registerUser.password.Length > 0 && BIZ.loginUser.password.Length <= 64)
                    {
                        BIZ.Register(BIZ.registerUser.navn, BIZ.registerUser.username, BIZ.registerUser.password);
                    }
                    else
                    {
                        MessageBox.Show($"Hey dig der! dit Password overholder ikke længde reglerne. Max 64 karaktere", "Indtastsfejl", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show($"Hey dig der! dit Username overholder ikke længde reglerne. Max 64 karaktere", "Indtastsfejl", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show($"Hey dig der! dit Navn overholder ikke længde reglerne. Max 64 karaktere", "Indtastsfejl", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}