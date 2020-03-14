using System.Text.RegularExpressions;

public class BackgroundWall
{
    public BackgroundWall() { }

    private static string type;// 订单号
    private static string number;// 编号
    private static string year_month;// 年月

    public static string Sort(string str)
    {
        string regex = @"^([A-Za-z])([0-9][0-9][0-9])([_])([0-9][0-9][0-9][0-9][0-9][0-9])";
        Match match = Regex.Match(str, regex);
        if (match.Groups[0].Value == "" || match.Groups[0] == null) return str;
        type = match.Groups[0].Value;
        number = match.Groups[1].Value + match.Groups[2].Value;
        year_month = match.Groups[4].Value;
        return number + @"\" + number + "_" + year_month + @"\" + str;
    }
}
