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
using static System.Net.Mime.MediaTypeNames;

namespace TFApp
{
    /// <summary>
    /// Interaction logic for OversigtPage.xaml
    /// </summary>
    public partial class OversigtPage : UserControl
    {
        ClassBIZ BIZ;
        MainWindow mainWindow;
        public OversigtPage(ClassBIZ inBIZ, MainWindow inMainWindow)
        {
            InitializeComponent();
            mainWindow = inMainWindow;
            BIZ = inBIZ;
            DataContext = BIZ;
        }

        private void Logud(object sender, RoutedEventArgs e)
        {
            mainWindow.ChangePage("loginPage");
            BIZ.Logout();
        }

        private void Energibarometer(object sender, RoutedEventArgs e)
        {
            mainWindow.ChangePage("energiBarometerPage");
        }

        private void RedigerBruger(object sender, RoutedEventArgs e)
        {
            mainWindow.ChangePage("editUserPage");
        }
    }
}
