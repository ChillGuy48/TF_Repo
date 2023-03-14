using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class ClassUser : ClassNotify
    {
        private int _id;
        private string _username;
        private string _password;
        private string _navn;

        public ClassUser()
        {
            id = 0;
            username = "";
            password = "";
            navn = "";
        }

        public int id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                }
                Notify();
            }
        }

        public string username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                }
                Notify();
            }
        }

        public string password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                }
                Notify();
            }
        }

        public string navn
        {
            get { return _navn; }
            set
            {
                if (_navn != value)
                {
                    _navn = value;
                }
                Notify();
            }
        }
    }
}
