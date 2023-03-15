using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ClassBIZ()
        {
            classTFAppDB = new ClassTFAppDB();
            loginUser = new ClassUser();
            registerUser = new ClassUser();
            energiBarometer = new ClassInput();
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
            classTFAppDB.GetUserData(username, password, );
        }
        public void Logout()
        {

        }

        public void Register(string navn, string username, string password)
        {
            ClassUser registerUser = new ClassUser();

            registerUser.navn = navn;
            registerUser.username = username;
            registerUser.password = password;

            classTFAppDB.CreateUser(registerUser);
        }
    }
}