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
            return GetFile(path, extension, sub, files);
        }

        private static List<string> GetFile(string path, string extension, bool sub, List<string> list)
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
                    GetFile(directoryinfod.FullName, extension, sub, list);//递归遍历
                }
                catch (UnauthorizedAccessException)
                {
                    if (MessageBox.Show("无权限访问：" + directoryinfod.FullName + "请尝试使用管理员权限运行本程序，是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;//警示窗口
                    else return null;
                }
            }
            return list;
        }

        //把递归换成栈，设计中

        /// <summary>
        /// 获取目录下文件
        /// </summary>
        /// <param name="path">目录</param>
        /// <param name="extension">指定文件名格式，*.* 表示所有文件</param>
        /// <param name="sub">是否包含所有子目录</param>
        public static List<string> getFiles(string path, string extension, bool sub)
        {
            if (!Directory.Exists(path))
            {
                MessageBox.Show("需要获取文件列表的主目录无效", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            List<string> list = new List<string>();//返回的文件列表

            Stack<string> stack = new Stack<string>(20);//栈
            stack.Push(path);//主目录入栈
            while (stack.Count > 0)//栈不为空时遍历
            {
                string main_path = stack.Pop();//取栈中第一个目录
                string[] sub_paths = null;
                sub_paths = Directory.GetDirectories(main_path);//栈目录的子目录列表
                string[] files = null;
                files = Directory.GetFiles(main_path);//栈目录的文件列表
                foreach (string file in files) list.Add(file);//栈目录的文件遍历到List
                foreach (string sub_path in sub_paths) stack.Push(sub_path);//栈目录的子目录列表入栈
            }
            return list;
        }

    }
}
