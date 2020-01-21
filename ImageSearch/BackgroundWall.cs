using System.Text.RegularExpressions;

namespace ImageSearch
{
    public static class BackgroundWall
    {
        private static string type;// 订单号
        public static string Type
        {
            get { return type; }
            set { type = value; }
        }
        
        private static string number;// 编号
        public static string Number
        {
            get { return number; }
            set { number = value; }
        }
        
        private static string year_month;// 年月
        public static string YearMonth
        {
            get { return year_month; }
            set { year_month = value; }
        }

        public static bool Parser(string str)// 检测
        {
            string regex = @"^([A-Za-z])([0-9][0-9][0-9])([_])([0-9][0-9][0-9][0-9][0-9][0-9])";
            Match match = Regex.Match(str, regex);
            if (match.Groups[0].Value == "" || match.Groups[0] == null) return false;
            type = match.Groups[0].Value;
            number = match.Groups[1].Value + match.Groups[2].Value;
            year_month = match.Groups[4].Value;
            return true;
        }

    }
}
