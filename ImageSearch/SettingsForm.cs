using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ImageSearch
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private string appuppath;//AppUpPath
        public string AppUpPath
        {
            get { return appuppath; }
            set { appuppath = value; }
        }

        private void SettingsForm_Load(object sender, System.EventArgs e)
        {
            appup_textbox.Text = appuppath;


            try
            {
                Icon = new Icon(Path.Combine(Application.StartupPath, "Setting.ico"));
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("无权限加载窗口图标图标文件，请尝试使用管理员权限重新运行本程序", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("窗口图标图标文件不存在，程序将自动退出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载窗口图标图标时发生如下错误，程序将自动退出，描述如下\r\n\r\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return;
            }
        }

        private void app_settings_save_button_Click(object sender, System.EventArgs e)//保存按钮
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load("Settings.xml");
                XmlNode rootNode = xml.DocumentElement;//获得根节点
                foreach (XmlNode xmlnode in rootNode.ChildNodes) if (xmlnode.Name == "Up") xmlnode.Attributes["path"].Value = appup_textbox.Text;//在根节点中寻找节点//找到对应的节点//获取对应节点的值
                xml.Save("Settings.xml");
                MessageBox.Show("保存成功，将在程序重启后生效", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("无权限访问程序配置文件，请尝试使用管理员权限运行本程序", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("程序配置文件不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("访问程序配置文件时发生如下错误\r\n\r\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //取消按钮
        private void app_settings_cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
