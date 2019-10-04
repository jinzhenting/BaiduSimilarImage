using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageSearch
{
    class ImageUpLog
    {
        //ID
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        //Nname
        private string name;
        public string Nname
        {
            get { return name; }
            set { name = value; }
        }

        //Time
        private string time;
        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        //Result
        private string result;
        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        //Message
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        
    }
}
