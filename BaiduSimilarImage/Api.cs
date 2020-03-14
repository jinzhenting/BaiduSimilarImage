namespace BaiduSimilarImage
{
    /// <summary>
    /// 百度相似图搜索API实例
    /// </summary>
    public class Api
    {
        /// <summary>
        /// API应用ID
        /// </summary>
        private string appid;
        /// <summary>
        /// API应用ID
        /// </summary>
        public string Appid
        {
            get { return appid; }
            set { appid = value; }
        }

        /// <summary>
        /// API密钥
        /// </summary>
        private string apikey;
        /// <summary>
        /// API密钥
        /// </summary>
        public string Apikey
        {
            get { return apikey; }
            set { apikey = value; }
        }

        /// <summary>
        /// API服务密钥
        /// </summary>
        private string secreykey;
        /// <summary>
        /// API服务密钥
        /// </summary>
        public string Secreykey
        {
            get { return secreykey; }
            set { secreykey = value; }
        }

        /// <summary>
        /// 请求超时（1-60秒）
        /// </summary>
        private int timeout;
        /// <summary>
        /// 请求超时（1-60秒）
        /// </summary>
        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        /// <summary>
        /// 图片标签1（1至65535整数）
        /// </summary>
        private int tags1;
        /// <summary>
        /// 图片标签1（1至65535整数）
        /// </summary>
        public int Tags1
        {
            get { return tags1; }
            set { tags1 = value; }
        }

        /// <summary>
        /// 图片标签2（1至65535整数）
        /// </summary>
        private int tags2;
        /// <summary>
        /// 图片标签2（1至65535整数）
        /// </summary>
        public int Tags2
        {
            get { return tags2; }
            set { tags2 = value; }
        }

        /// <summary>
        /// 返回结果数量（1至1000整数）
        /// </summary>
        private int quantity;
        /// <summary>
        /// 返回结果数量（1至1000整数）
        /// </summary>
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        /// <summary>
        /// 图库本地目录
        /// </summary>
        private string path;
        /// <summary>
        /// 图库本地目录
        /// </summary>
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        /// <summary>
        /// 图库默认本地整理目录
        /// </summary>
        private string sortpath;
        /// <summary>
        /// 图库默认本地整理目录
        /// </summary>
        public string SortPath
        {
            get { return sortpath; }
            set { sortpath = value; }
        }

        /// <summary>
        /// 图库SQL数据库地址
        /// </summary>
        private string serverip;
        /// <summary>
        /// 图库SQL数据库地址
        /// </summary>
        public string Serverip
        {
            get { return serverip; }
            set { serverip = value; }
        }

        /// <summary>
        /// 图库SQL数据库
        /// </summary>
        private string dataname;
        /// <summary>
        /// 图库SQL数据库
        /// </summary>
        public string Dataname
        {
            get { return dataname; }
            set { dataname = value; }
        }

        /// <summary>
        /// 图库SQL数据库用户
        /// </summary>
        private string userid;
        /// <summary>
        /// 图库SQL数据库用户
        /// </summary>
        public string Userid
        {
            get { return userid; }
            set { userid = value; }
        }

        /// <summary>
        /// 图库SQL数据库登陆密码
        /// </summary>
        private string password;
        /// <summary>
        /// 图库SQL数据库登陆密码
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// 图库SQL数据表
        /// </summary>
        private string table;
        /// <summary>
        /// 图库SQL数据表
        /// </summary>
        public string Table
        {
            get { return table; }
            set { table = value; }
        }

    }
}
