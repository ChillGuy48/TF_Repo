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
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClassBIZ BIZ;

        public MainWindow()
        {
            InitializeComponent();
            BIZ = new ClassBIZ();
            DataContext = BIZ;
        }
        


        private void LoginToAccount(object sender, RoutedEventArgs e)
        {
            //BIZ.loginUser.username = "hej";
            MessageBox.Show($"{BIZ.loginUser.username}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void RegisterNewAccount(object sender, RoutedEventArgs e)
        {

        }
    }
}