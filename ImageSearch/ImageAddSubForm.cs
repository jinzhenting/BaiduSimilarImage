using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageSearch
{
    /// <summary>
    /// 入库扫描子目录窗口
    /// </summary>
    public partial class ImageAddSubForm : Form
    {
        public ImageAddSubForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 主目录
        /// </summary>
        private string inpath;
        /// <summary>
        /// 主目录
        /// </summary>
        public string InPath
        {
            get { return inpath; }
            set { inpath = value; }
        }

        /// <summary>
        /// 选中目录列表
        /// </summary>
        private List<string> list;
        /// <summary>
        /// 选中目录列表
        /// </summary>
        public List<string> List
        {
            get { return list; }
            set { list = value; }
        }

        /// <summary>
        /// 窗口载入时
        /// </summary>
        private void ImageAddSubForm_Load(object sender, EventArgs e)
        {
            list = new List<string>();
            DirectoryInfo directoryinfo = new DirectoryInfo(inpath);
            DirectoryInfo[] directoryinfos = directoryinfo.GetDirectories();
            for (int i = 0; i < directoryinfos.Length; i++) checkedListBox.Items.Add(directoryinfos[i].FullName);

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

        /// <summary>
        /// 确定按钮
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            if (list.Count > 0) list.Clear();
            for (int i = 0; i < checkedListBox.Items.Count; i++) if (checkedListBox.GetItemChecked(i)) list.Add(checkedListBox.GetItemText(checkedListBox.Items[i]));
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 全不勾选
        /// </summary>
        private void clearButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++) checkedListBox.SetItemChecked(i, false);
        }

        /// <summary>
        /// 全勾选
        /// </summary>
        private void allButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++) checkedListBox.SetItemChecked(i, true);
        }

        //
    }
}
