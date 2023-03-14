using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class ClassInput : ClassNotify
    {
        private int _id;
        private int _energi;
        private DateTime _time;
        private string _info;
        private int _sleep;
        private int _userid;

        public ClassInput()
        {
            id = 0;
            energi = 0;
            time = DateTime.Now;
            info = "";
            sleep = 0;
            userid = 0;
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

        public int energi
        {
            get { return _energi; }
            set
            {
                if (_energi != value)
                {
                    _energi = value;
                }
                Notify();
            }
        }

        public DateTime time
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                }
                Notify();
            }
        }

        public string info
        {
            get { return _info; }
            set
            {
                if (_info != value)
                {
                    _info = value;
                }
                Notify();
            }
        }

        public int sleep
        {
            get { return _sleep; }
            set
            {
                if (_sleep != value)
                {
                    _sleep = value;
                }
                Notify();
            }
        }

        public int userid
        {
            get { return _userid; }
            set
            {
                if (_userid != value)
                {
                    _userid = value;
                }
                Notify();
            }
        }
    }
}
