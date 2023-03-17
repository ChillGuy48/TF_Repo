using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using IO;
using Repo;

namespace BIZ
{
    public class ClassBIZ : ClassNotify
    {
        ClassTFAppDB classTFAppDB;

        private ClassUser _loginUser;
        private ClassUser _registerUser;
        private ClassInput _energiBarometer;
        private DateTime _selectedDate;
        private DateTime _selectedDatetemp;
        private int _selectedDateNumber;
        private string _selectedDateString;
        private string _selectedDateColor;

        private int _returnvalue;

        public ClassBIZ()
        {
            returnvalue = 0;


            selectedDate = DateTime.Today;
            selectedDatetemp = DateTime.Today;
            selectedDateNumber = 0;
            selectedDateString = "";
            selectedDateColor = "";

            classTFAppDB = new ClassTFAppDB();
            loginUser = new ClassUser();
            registerUser = new ClassUser();
            energiBarometer = new ClassInput();
        }

        public int returnvalue
        {
            get { return _returnvalue; }
            set
            {
                if (_returnvalue != value)
                {
                    _returnvalue = value;
                }
                Notify();
            }
        }

        public string selectedDateColor
        {
            get { return _selectedDateColor; }
            set
            {
                if (_selectedDateColor != value)
                {
                    _selectedDateColor = value;
                }
                Notify();
            }
        }
        public string selectedDateString
        {
            get { return _selectedDateString; }
            set
            {
                if (_selectedDateString != value)
                {
                    _selectedDateString = value;
                }
                Notify();
            }
        }
        public int selectedDateNumber
        {
            get { return _selectedDateNumber; }
            set
            {
                if (_selectedDateNumber != value)
                {
                    _selectedDateNumber = value;
                }
                Notify();
            }
        }
        public DateTime selectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                }
                Notify();
            }
        }
        public DateTime selectedDatetemp
        {
            get { return _selectedDatetemp; }
            set
            {
                if (_selectedDatetemp != value)
                {
                    _selectedDatetemp = value;
                }
                Notify();
            }
        }


        public ClassUser loginUser
        {
            get { return _loginUser; }
            set
            {
                if (_loginUser != value)
                {
                    _loginUser = value;
                }
                Notify();
            }
        }
        public ClassUser registerUser
        {
            get { return _registerUser; }
            set
            {
                if (_registerUser != value)
                {
                    _registerUser = value;
                }
                Notify();
            }
        }
        public ClassInput energiBarometer
        {
            get { return _energiBarometer; }
            set
            {
                if (_energiBarometer != value)
                {
                    _energiBarometer = value;
                }
                Notify();
            }
        }



        // Methods
        public void Login(string username, string password)
        {
            loginUser = classTFAppDB.GetUserData(username, password);
        }
        public void Logout()
        {
            loginUser = new ClassUser();
            MessageBox.Show($"Du er nu logget ud af account", "Log out", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void Register(string navn, string username, string password)
        {
            ClassUser registerUser = new ClassUser();

            registerUser.navn = navn;
            registerUser.username = username;
            registerUser.password = password;

            classTFAppDB.CreateUser(registerUser);

            MessageBox.Show($"Din nye account '{registerUser.username}' er blevet registret", "Register", MessageBoxButton.OK, MessageBoxImage.Information);
            registerUser = new ClassUser();
        }

        public void SelectDate(DateTime time)
        {
            energiBarometer = classTFAppDB.GetEnergibarometerData(loginUser.id, time);
            selectedDatetemp = energiBarometer.time.Date;
            if (energiBarometer.energi == 1)
            {
                selectedDateColor = "PaleVioletRed";
                selectedDateNumber = energiBarometer.energi;
                selectedDateString = "Rød";
            }
            else if (energiBarometer.energi == 2)
            {
                selectedDateColor = "LightGoldenrodYellow";
                selectedDateNumber = energiBarometer.energi;
                selectedDateString = "Gul";
            }
            else if (energiBarometer.energi == 3)
            {
                selectedDateColor = "LightGreen";
                selectedDateNumber = energiBarometer.energi;
                selectedDateString = "Grøn";
            }
        }
        public void UploadEnergibarometer(int energilevel, DateTime time, ClassUser user)
        {
            returnvalue = classTFAppDB.CreateEnergibarometer(energilevel, time, user);
        }
    }
}