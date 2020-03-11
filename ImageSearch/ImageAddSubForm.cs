using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageSearch
{
    public partial class ImageAddSubForm : Form
    {
        public ImageAddSubForm()
        {
            InitializeComponent();
        }

        //InPath
        private string inpath;
        public string InPath
        {
            get { return inpath; }
            set { inpath = value; }
        }

        //List
        private List<string> list;
        public List<string> List
        {
            get { return list; }
            set { list = value; }
        }

        //
        private void ImageAddSubForm_Load(object sender, EventArgs e)
        {
            //
            list = new List<string>();
            DirectoryInfo directoryinfo = new DirectoryInfo(inpath);
            DirectoryInfo[] directoryinfos = directoryinfo.GetDirectories();
            for (int i = 0; i < directoryinfos.Length; i++)
            {
                sub_checkedlistbox.Items.Add(directoryinfos[i].FullName);
            }

            try
            {
                Icon = new Icon(Path.Combine(Application.StartupPath, @"Skin\Up.ico"));
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("无权限加载窗口图标图标文件，请尝试使用管理员权限重新运行本程序", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("窗口图标图标文件不存在，程序将自动退出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载窗口图标图标时发生如下错误，程序将自动退出，描述如下\r\n\r\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return;
            }
        }

        //确定按钮
        private void sub_ok_button_Click(object sender, EventArgs e)
        {
            if (list.Count > 0)
            {
                list.Clear();
            }

            //选中
            for (int i = 0; i < sub_checkedlistbox.Items.Count; i++)
            {
                if (sub_checkedlistbox.GetItemChecked(i))
                {
                    list.Add(sub_checkedlistbox.GetItemText(sub_checkedlistbox.Items[i]));
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //取消按钮
        private void sub_cancel_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //全不选
        private void sub_clear_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sub_checkedlistbox.Items.Count; i++)
            {
                sub_checkedlistbox.SetItemChecked(i, false);
            }
        }

        //全选
        private void sub_all_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sub_checkedlistbox.Items.Count; i++)
            {
                sub_checkedlistbox.SetItemChecked(i, true);
            }
        }

        //
    }
}
