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
        private ClassUser _loginUser;
        private ClassUser _temploginUser;
        private ClassUser _registerUser;
        private ClassUser _tempregisterUser;
        private ClassInput _energiBarometer;

        public ClassBIZ()
        {
            loginUser = new ClassUser();
            temploginUser = new ClassUser();
            registerUser = new ClassUser();
            tempregisterUser = new ClassUser();
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
        public ClassUser temploginUser
        {
            get { return _temploginUser; }
            set
            {
                if (_temploginUser != value)
                {
                    _temploginUser = value;
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
        public ClassUser tempregisterUser
        {
            get { return _tempregisterUser; }
            set
            {
                if (_tempregisterUser != value)
                {
                    _tempregisterUser = value;
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
    }
}
