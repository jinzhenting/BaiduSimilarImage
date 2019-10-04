using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ImageSearch
{
    public static class GetFiless
    {
        public static List<string> Get(string path, string extension, bool sub)// 查找文件夹下所有文件
        {
            List<string> files = new List<string>();
            return GetFiles(path, extension, sub, files);
        }

        private static List<string> GetFiles(string path, string extension, bool sub, List<string> list)
        {
            DirectoryInfo root_directoryinfo = new DirectoryInfo(path);
            FileInfo[] files = root_directoryinfo.GetFiles(extension);//一层文件夹
            foreach (FileInfo fileinfo in files) list.Add(fileinfo.FullName);
            if (!sub) return list;//不包含子文件夹立即返回
            DirectoryInfo[] all_directoryinfo = root_directoryinfo.GetDirectories();//获取多层子文件夹内的文件列表，
            foreach (DirectoryInfo directoryinfod in all_directoryinfo)
            {
                try
                {
                    GetFiles(directoryinfod.FullName, extension, sub, list);//递归遍历
                }
                catch (UnauthorizedAccessException)
                {
                    if (MessageBox.Show("无权限访问：" + directoryinfod.FullName + "请尝试使用管理员权限运行本程序，是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;//警示窗口
                    else return null;
                }
            }
            return list;
        }
    }
}
