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
using System.Windows.Shapes;

namespace TFApp
{
    /// <summary>
    /// Interaction logic for Oversigt.xaml
    /// </summary>
    public partial class Oversigt : Window
    {
        ClassBIZ BIZ;
        public Oversigt()
        {
            InitializeComponent();
            BIZ = new ClassBIZ();
        }

        private void Logud(object sender, RoutedEventArgs e)
        {

        }

        private void Energibarometer(object sender, RoutedEventArgs e)
        {

        }

        private void RedigerBruger(object sender, RoutedEventArgs e)
        {

        }
    }
}
