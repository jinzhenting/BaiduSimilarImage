using System;

namespace BaiduSimilarImage
{
    /// <summary>
    /// 百分比
    /// </summary>
    public static class Percents
    {
        /// <summary>
        /// 百分比计算
        /// </summary>
        /// <param name="i1">百分数</param>
        /// <param name="i2">总数</param>
        /// <returns>1至100正整数</returns>
        public static int Get(int i1, int i2)
        {
            decimal d1 = i1;
            decimal d2 = i2;
            decimal d3 = decimal.Parse((d1 / d2).ToString("0.000"));// 保留3位小数
            var v1 = Math.Round(d3, 2);// 四舍五入精确2位
            var v2 = v1 * 100;// 乘
            return (int)v2;
        }
    }
}
