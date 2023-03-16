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
    }
}
