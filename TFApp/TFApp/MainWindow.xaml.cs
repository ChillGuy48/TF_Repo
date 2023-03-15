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
        EditUserPage editUserPage;
        EnergiBarometerPage energiBarometerPage;
        LoginPage loginPage;
        OversigtPage oversigtPage;

        public MainWindow()
        {
            InitializeComponent();
            BIZ = new ClassBIZ();
            MainGrid.DataContext = BIZ;

            editUserPage = new EditUserPage(BIZ, this);
            energiBarometerPage = new EnergiBarometerPage(BIZ, this);
            loginPage = new LoginPage(BIZ, this);
            oversigtPage = new OversigtPage(BIZ, this);

            MainGrid.Children.Add(loginPage);
        }

        public void ChangePage(string Page)
        {
            MainGrid.Children.Remove(editUserPage);
            MainGrid.Children.Remove(energiBarometerPage);
            MainGrid.Children.Remove(loginPage);
            MainGrid.Children.Remove(oversigtPage);
            if (Page == "editUserPage") MainGrid.Children.Add(editUserPage);
            else if (Page == "energiBarometerPage") MainGrid.Children.Add(energiBarometerPage);
            else if (Page == "loginPage") MainGrid.Children.Add(loginPage);
            else if (Page == "oversigtPage") MainGrid.Children.Add(oversigtPage);
        }
    }
}