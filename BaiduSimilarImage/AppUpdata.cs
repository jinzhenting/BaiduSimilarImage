﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BaiduSimilarImage
{
    /// <summary>
    /// 程序更新
    /// </summary>
    static class AppUpdata
    {
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="upURL">更新地址</param>
        /// <param name="showWindows">结果是否弹窗</param>
        public static void Updata(string upURL, bool showWindows)
        {
            if (!Directory.Exists(upURL))// 地址检测
            {
                if (MessageBox.Show("程序更新地址 " + upURL + " 无效，是否重新设置？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;// 新版本更新选择窗口
                else
                {
                    AppSettingsForm settingsForm = new AppSettingsForm();
                    settingsForm.ShowDialog();
                    Environment.Exit(0);
                }
                return;
            }

            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(upURL);
                FileInfo[] files = directoryInfo.GetFiles(@"Baidu Similar Image*.exe", SearchOption.TopDirectoryOnly);// 扫描BaiduSimilarImage开头命名的exe文件
                if (files.Length == 0)// 没有发现BaiduSimilarImage开头命名的exe文件
                {
                    if (showWindows) MessageBox.Show("没有发现新版本", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                foreach (FileInfo file in files)// 遍历1或多个BaiduSimilarImage程序文件
                {
                    string version = Regex.Match(file.Name, @"[1-9]+[.][0-9]+[.][0-9]+[.][0-9]+").Groups[0].Value;// 获取版本号
                    if (version == "" || version == null) continue;// 在exe文件名中没有找到版本号
                    int web = int.Parse(version.Replace(".", ""));
                    int local = int.Parse(Application.ProductVersion.Replace(".", ""));
                    if (web < local) continue;// 本地比服务器版本高
                    if (web == local) continue;// 本地与服务器版本相同
                    if (MessageBox.Show("发现新版本：" + version + "\r\n当前版本：" + Application.ProductVersion + "\r\n按确定立即更新，请耐心等待后台下载完成安装", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;// 新版本更新选择窗口
                    Process.Start(file.FullName);
                    Environment.Exit(0);
                    return;
                }

                if (showWindows) MessageBox.Show("没有发现新版本", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (UnauthorizedAccessException)
            {
                if (MessageBox.Show("无权限访问更新地址，请是否继续运行？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;
            }
            catch (FileNotFoundException)
            {
                if (MessageBox.Show("更新地址不存在，请是否继续运行？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("程序更新时发生如下错误，是否继续运行？信息如下\r\n\r\n" + ex.ToString(), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;
            }
        }

    }
}
