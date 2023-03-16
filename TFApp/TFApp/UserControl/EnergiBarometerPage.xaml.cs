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
    /// Interaction logic for EnergiBarometerPage.xaml
    /// </summary>
    public partial class EnergiBarometerPage : UserControl
    {
        ClassBIZ BIZ;
        MainWindow mainWindow;
        public EnergiBarometerPage(ClassBIZ inBIZ, MainWindow inMainWindow)
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

        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.ChangePage("oversigtPage");
        }

        private void SelectDate(object sender, RoutedEventArgs e)
        {
            BIZ.SelectDate(BIZ.selectedDate.Date);
            //MessageBox.Show($"userid: {BIZ.energiBarometer.userid}\nid: {BIZ.energiBarometer.id}\ntime: {BIZ.energiBarometer.time}\nenergi: {BIZ.energiBarometer.energi}", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void GreenEnergibarometer(object sender, RoutedEventArgs e)
        {
            BIZ.UploadEnergibarometer(3, BIZ.selectedDate, BIZ.loginUser);
            if (BIZ.returnvalue >= 1)
            {
                BIZ.SelectDate(BIZ.selectedDate.Date);
                MessageBox.Show($"Du tilføjet noget til energibarometer", "Energibarometer Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                
            }
        }

        private void YellowEnergibarometer(object sender, RoutedEventArgs e)
        {
            BIZ.UploadEnergibarometer(2, BIZ.selectedDate, BIZ.loginUser);
        }

        private void RedEnergibarometer(object sender, RoutedEventArgs e)
        {
            BIZ.UploadEnergibarometer(1, BIZ.selectedDate, BIZ.loginUser);
        }
    }
}
