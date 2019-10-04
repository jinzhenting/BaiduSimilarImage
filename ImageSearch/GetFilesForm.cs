using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace ImageSearch
{
    public partial class GetFilesForm : Form
    {
        public GetFilesForm()
        {
            InitializeComponent();
        }

        private void GetFilesForm_Load(object sender, EventArgs e)
        {
            FindFileInDir(@"E:\背景墙2020图册设计");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        //声明WIN32API函数以及结构
        [Serializable,
    System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Auto),
    System.Runtime.InteropServices.BestFitMapping(false)]
        private struct WIN32_FIND_DATA
        {
            public int dwFileAttributes;
            public int ftCreationTime_dwLowDateTime;
            public int ftCreationTime_dwHighDateTime;
            public int ftLastAccessTime_dwLowDateTime;
            public int ftLastAccessTime_dwHighDateTime;
            public int ftLastWriteTime_dwLowDateTime;
            public int ftLastWriteTime_dwHighDateTime;
            public int nFileSizeHigh;
            public int nFileSizeLow;
            public int dwReserved0;
            public int dwReserved1;
            [System.Runtime.InteropServices.MarshalAs
                (System.Runtime.InteropServices.UnmanagedType.ByValTStr,
                SizeConst = 260)]
            public string cFileName;
            [System.Runtime.InteropServices.MarshalAs
              (System.Runtime.InteropServices.UnmanagedType.ByValTStr,
              SizeConst = 14)]
            public string cAlternateFileName;
        }
        [System.Runtime.InteropServices.DllImport
          ("kernel32.dll",
          CharSet = System.Runtime.InteropServices.CharSet.Auto,
          SetLastError = true)]
        private static extern IntPtr FindFirstFile(string pFileName, ref WIN32_FIND_DATA pFindFileData);
        [System.Runtime.InteropServices.DllImport
          ("kernel32.dll",
          CharSet = System.Runtime.InteropServices.CharSet.Auto,
          SetLastError = true)]
        private static extern bool FindNextFile(IntPtr hndFindFile, ref WIN32_FIND_DATA lpFindFileData);
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FindClose(IntPtr hndFindFile);

        //具体方法函数
        Stack<string> m_scopes = new Stack<string>();
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        WIN32_FIND_DATA FindFileData;
        private IntPtr hFind = INVALID_HANDLE_VALUE;
        void FindFileInDir(string rootDir)
        {
            string path = rootDir;
            start:
            new FileIOPermission(FileIOPermissionAccess.PathDiscovery, Path.Combine(path, ".")).Demand();
            if (path[path.Length - 1] != '\\')
            {
                path = path + "\\";
            }
            Console.Write("文件夹为：" + path + "<br>");
            hFind = FindFirstFile(Path.Combine(path, "*"), ref FindFileData);
            if (hFind != INVALID_HANDLE_VALUE)
            {
                do
                {
                    if (FindFileData.cFileName.Equals(@".") || FindFileData.cFileName.Equals(@".."))
                        continue;
                    if ((FindFileData.dwFileAttributes & 0x10) != 0)
                    {
                        m_scopes.Push(Path.Combine(path, FindFileData.cFileName));
                    }
                    else
                    {
                        Console.Write(FindFileData.cFileName + "<br>");
                    }
                }
                while (FindNextFile(hFind, ref FindFileData));
            }
            FindClose(hFind);
            if (m_scopes.Count > 0)
            {
                path = m_scopes.Pop();
                goto start;
            }
        }


        //
    }
}
