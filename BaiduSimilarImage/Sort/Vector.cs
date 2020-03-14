using System.Windows.Forms;
using System.Text.RegularExpressions;
using System;

public class Vector
{
    public Vector() { }

    private static string type;// ������
    private static string customer;// �ͻ�
    private static string year;// ��
    private static string month;// ��
    private static string version;// �汾
    private static string count;// ����
    private static string q;// ����
    private static string g;// ��Դ

    public static string Sort(string str)
    {
        if (Parser(str)) return year + month + @"\" + customer + @"\" + str;
        else return str;
    }

    private static bool Parser(string str)// ���
    {
        return (New(str) || Old(str));
    }

    private static bool New(string str)// �¶���
    {
        str = ClearQ(str);// ���Q_��1_-9_��ͷ�Ĺ��Ʊ��
        string regex = @"^([A-Za-z]{1,5})([0-9][0-9])([1-9A-Ca-c])([Gg])?([0-9][0-9][0-9]?)([A-Za-z])?";// �¶�����ȡ2λ���
        Match match = Regex.Match(str, regex);
        if (match.Groups[0].Value == "" || match.Groups[0] == null) return false;
        string year_temp = Years(match.Groups[2].Value);// ��ת��
        string month_temp = Months(match.Groups[3].Value);// ��ת��
        DateTime type_month = DateTime.ParseExact(year_temp + month_temp, "yyyyMM", System.Globalization.CultureInfo.CurrentCulture);// ���걾�¶Ա�
        DateTime now_month = DateTime.Now;
        if (DateTime.Compare(type_month, now_month) > 0) return false;
        type = match.Groups[0].Value;// д����
        customer = match.Groups[1].Value;
        year = year_temp;
        month = month_temp;
        g = CustomerG(match.Groups[4].Value);
        count = Counts(match.Groups[5].Value);
        version = Versions(match.Groups[6].Value);
        return true;
    }

    private static bool Old(string str)// �ɶ���
    {
        str = ClearQ(str);// ���Q_��1_-9_��ͷ�Ĺ��Ʊ��
        string regex = "^([A-Za-z]{1,5})([0-9])([1-9A-Ca-c])([Gg])?([0-9][0-9][0-9]?)([A-Za-z])?";// �ɶ�����ȡ1λ���
        Match match = Regex.Match(str, regex);
        if (match.Groups[0].Value == "" || match.Groups[0] == null) return false;
        type = match.Groups[0].Value;// д����
        customer = match.Groups[1].Value;
        year = Years(match.Groups[2].Value);
        month = Months(match.Groups[3].Value);
        g = CustomerG(match.Groups[4].Value);
        count = Counts(match.Groups[5].Value);
        version = Versions(match.Groups[6].Value);
        return true;
    }

    private static string Years(string year)// ��ת��
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

    private static string Months(string month)// ��ת��
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

    private static string Counts(string str)// ������
    {
        return str = Regex.Replace(str, "^[0]+", string.Empty);
    }

    private static string Versions(string version)// �汾
    {
        return (version == "") ? "ԭ" : version.ToUpper();
    }

    private static string ClearQ(string str)// ���Q_��1_-9_��ͷ�Ĺ��Ʊ��
    {
        if (Regex.IsMatch(str, "^([Qq1-9]_)"))
        {
            q = "���۶���";
            return str = Regex.Replace(str, "^([Qq1-9]_)", string.Empty);
        }
        q = "��ʽ����";
        return str;
    }

    private static string CustomerG(string str)// ��Դ
    {
        if (Regex.IsMatch(str, "^[Gg]$")) return g = "�ͻ��Ͱ�";// G����ͻ��Ͱ�
        return g = "�ڲ����";
    }

}