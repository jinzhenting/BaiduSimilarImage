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

        把递归换成栈

        public static void TraverseTree(string root)
        {
            // Data structure to hold names of subfolders to be
            // examined for files.
            Stack<string> dirs = new Stack<string>(20);
            if (!System.IO.Directory.Exists(root)) throw new ArgumentException();
            dirs.Push(root);
            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = Directory.GetDirectories(currentDir);
                }
                // An UnauthorizedAccessException exception will be thrown if we do not have
                // discovery permission on a folder or file. It may or may not be acceptable 
                // to ignore the exception and continue enumerating the remaining files and 
                // folders. It is also possible (but unlikely) that a DirectoryNotFound exception 
                // will be raised. This will happen if currentDir has been deleted by
                // another application or thread after our call to Directory.Exists. The 
                // choice of which exceptions to catch depends entirely on the specific task 
                // you are intending to perform and also on how much you know with certainty 
                // about the systems on which this code will run.
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                string[] files = null;
                try
                {
                    files = Directory.GetFiles(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {

                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                // Perform the required action on each file here.
                // Modify this block to perform your required task.
                foreach (string file in files)
                {
                    try
                    {
                        // Perform whatever action is required in your scenario.
                        FileInfo fi = new System.IO.FileInfo(file);
                        Console.WriteLine("{0}: {1}, {2}", fi.Name, fi.Length, fi.CreationTime);
                    }
                    catch (FileNotFoundException e)
                    {
                        // If file was deleted by a separate application
                        //  or thread since the call to TraverseTree()
                        // then just continue.
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }

                // Push the subdirectories onto the stack for traversal.
                // This could also be done before handing the files.
                foreach (string str in subDirs)
                    dirs.Push(str);
            }
        }













    }
}
