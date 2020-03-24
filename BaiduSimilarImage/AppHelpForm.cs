using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BaiduSimilarImage
{
    /// <summary>
    /// 程序使用帮助窗口
    /// </summary>
    public partial class AppHelpForm : Form
    {
        public AppHelpForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗口载入时
        /// </summary>
        private void AppHelpForm_Load(object sender, EventArgs e)
        {
            try
            {
                Icon = new Icon(Path.Combine(Application.StartupPath, @"Skin\Help.ico"));
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
    }
}
