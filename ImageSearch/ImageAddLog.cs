using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageSearch
{
    /// <summary>
    /// 入库日志
    /// </summary>
    class ImageAddLog
    {
        /// <summary>
        /// 图片序号
        /// </summary>
        private string id;
        /// <summary>
        /// 图片序号
        /// </summary>
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 图片名
        /// </summary>
        private string name;
        /// <summary>
        /// 图片名
        /// </summary>
        public string Nname
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 入库时间
        /// </summary>
        private string time;
        /// <summary>
        /// 入库时间
        /// </summary>
        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        /// <summary>
        /// 入库结果
        /// </summary>
        private string result;
        /// <summary>
        /// 入库结果
        /// </summary>
        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        /// <summary>
        /// 入库结果描述
        /// </summary>
        private string message;
        /// <summary>
        /// 入库结果描述
        /// </summary>
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        
    }
}
