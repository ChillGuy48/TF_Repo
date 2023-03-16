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
        private int _userid;
        //private string _dateStringColor;

        public ClassInput()
        {
            id = 0;
            energi = 0;
            time = DateTime.Now;
            userid = 0;
            //dateStringColor = "";
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

        //public string dateStringColor
        //{
        //    get { return _dateStringColor; }
        //    set
        //    {
        //        if (_dateStringColor != value)
        //        {
        //            _dateStringColor = value;
        //        }
        //        Notify();
        //    }
        //}

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