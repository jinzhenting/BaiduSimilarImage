using System;
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

        //AppUpPath
        private string appuppath;
        public string AppUpPath
        {
            get { return appuppath; }
            set { appuppath = value; }
        }

        private void SettingsForm_Load(object sender, System.EventArgs e)
        {
            appup_textbox.Text = appuppath;
        }

        //保存按钮
        private void app_settings_save_button_Click(object sender, System.EventArgs e)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load("Settings.xml");
                //获得根节点
                XmlNode rootNode = xml.DocumentElement;
                //在根节点中寻找节点
                foreach (XmlNode xmlnode in rootNode.ChildNodes)
                {
                    //找到对应的节点
                    if (xmlnode.Name == "Up")
                    {
                        //获取对应节点的值
                        xmlnode.Attributes["path"].Value = appup_textbox.Text;
                    }
                }
                xml.Save("Settings.xml");
                MessageBox.Show("保存成功，将在程序重启后生效");
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("无权限访问程序配置文件，请尝试使用管理员权限运行本程序");
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("程序配置文件不存在");
            }
            catch (Exception ex)
            {
                MessageBox.Show("访问程序配置文件时发生如下错误\r\n" + ex.ToString());
            }
        }
        
        //取消按钮
        private void app_settings_cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
