using System;

namespace ImageSearch
{
    public static class Percents
    {
        //百分比
        public static int Get(int i1, int i2)
        {
            decimal d1 = i1;
            decimal d2 = i2;
            decimal d3 = decimal.Parse((d1 / d2).ToString("0.000")); //保留3位小数
            var v1 = Math.Round(d3, 2);  //四舍五入精确2位
            var v2 = v1 * 100;  //乘
            return (int)v2;
        }
    }
}
