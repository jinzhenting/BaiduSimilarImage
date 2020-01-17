using System;
using System.Text.RegularExpressions;

namespace ImageSearch
{
    public static class Emb
    {
        private static string type;//订单号
        public static string Type
        {
            get { return type; }
            set { type = value; }
        }

        private static string customer;//客户
        public static string Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        private static string year;//年
        public static string Year
        {
            get { return year; }
            set { year = value; }
        }

        private static string month;//月
        public static string Month
        {
            get { return month; }
            set { month = value; }
        }

        private static string version;//版本
        public static string Version
        {
            get { return version; }
            set { version = value; }
        }

        private static string count;//数量
        public static string Count
        {
            get { return count; }
            set { version = count; }
        }

        private static string q;//报价
        public static string Q
        {
            get { return q; }
            set { q = value; }
        }

        private static string g;//来源
        public static string G
        {
            get { return g; }
            set { g = value; }
        }

        public static bool Parser(string str)//检测
        {
            return (New(str) || Old(str));
        }

        private static bool New(string str)//新订单
        {
            str = ClearQ(str);//清除Q_和1_-9_开头的估计编号
            string regex = @"^([A-Za-z]{1,5})([0-9][0-9])([1-9A-Ca-c])([Gg])?([0-9][0-9][0-9]?)([A-Za-z])?";//新订单号取2位年份
            Match match = Regex.Match(str, regex);
            if (match.Groups[0].Value == "" || match.Groups[0] == null) return false;
            string year_temp = Years(match.Groups[2].Value); //年转换
            string month_temp = Months(match.Groups[3].Value);//月转换
            DateTime type_month = DateTime.ParseExact(year_temp + month_temp, "yyyyMM", System.Globalization.CultureInfo.CurrentCulture);//本年本月对比
            DateTime now_month = DateTime.Now;
            if (DateTime.Compare(type_month, now_month) > 0) return false;
            type = match.Groups[0].Value;//写数据
            customer = match.Groups[1].Value;
            year = year_temp;
            month = month_temp;
            g = CustomerG(match.Groups[4].Value);
            count = Counts(match.Groups[5].Value);
            version = Versions(match.Groups[6].Value);
            return true;
        }

        private static bool Old(string str)//旧订单
        {
            str = ClearQ(str);//清除Q_和1_-9_开头的估计编号
            string regex = "^([A-Za-z]{1,5})([0-9])([1-9A-Ca-c])([Gg])?([0-9][0-9][0-9]?)([A-Za-z])?";//旧订单号取1位年份
            Match match = Regex.Match(str, regex);
            if (match.Groups[0].Value == "" || match.Groups[0] == null) return false;
            type = match.Groups[0].Value; //写数据
            customer = match.Groups[1].Value;
            year = Years(match.Groups[2].Value);
            month = Months(match.Groups[3].Value);
            g = CustomerG(match.Groups[4].Value);
            count = Counts(match.Groups[5].Value);
            version = Versions(match.Groups[6].Value);
            return true;
        }

        private static string Years(string year)//年转换
        {
            string[] years = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] new_years = { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09" };
            for (int i = 0; i < years.Length; i++)
            {
                if (years[i] == year)
                {
                    year = new_years[i];
                    break;
                }
            }
            return year = "20" + year;
        }

        private static string Months(string month)//月转换
        {
            string[] months = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "a", "b", "c" };
            string[] new_months = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "10", "11", "12" };
            for (int i = 0; i < months.Length; i++)
            {
                if (months[i] == month)
                {
                    month = new_months[i];
                    break;
                }
            }
            return month;
        }

        private static string Counts(string str)//订单数
        {
            return str = Regex.Replace(str, "^[0]+", string.Empty);
        }

        private static string Versions(string version)//版本
        {
            return (version == "") ? "原" : version.ToUpper();
        }

        private static string ClearQ(string str)//清除Q_和1_-9_开头的估计编号
        {
            if (Regex.IsMatch(str, "^([Qq1-9]_)"))
            {
                q = "报价订单";
                return str = Regex.Replace(str, "^([Qq1-9]_)", string.Empty);
            }
            q = "正式订单";
            return str;
        }

        private static string CustomerG(string str)//来源
        {
            if (Regex.IsMatch(str, "^[Gg]$")) return g = "客户送版";//G代表客户送版
            return g = "内部打版";
        }

    }
}
