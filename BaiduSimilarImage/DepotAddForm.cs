using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BaiduSimilarImage
{
    /// <summary>
    /// 新建图库窗口
    /// </summary>
    public partial class DepotAddForm : Form
    {
        public DepotAddForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 读取程序图标
        /// </summary>
        private void DepotAddForm_Load(object sender, EventArgs e)
        {
            try
            {
                Icon = new Icon(Path.Combine(Application.StartupPath, @"Skin\Add.ico"));
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("无权限加载窗口图标文件，请尝试使用管理员权限重新运行本程序", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("窗口图标文件不存在，程序将自动退出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载窗口图标时发生如下错误，程序将自动退出，错误描述如下\r\n\r\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return;
            }
        }

        /// <summary>
        /// 完成按钮
        /// </summary>
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (depotTextBox.Text == "" || appidTextBox.Text == "" || apikeyTextBox.Text == "" || secreykeyTextBox.Text == "" || timeoutTextBox.Text == "" || tag1TextBox.Text == "" || tag2TextBox.Text == "" || depotPathTextBox.Text == "" || quantityTextBox.Text == "" || sqlIPTextBox.Text == "" || sqlDataTextBox.Text == "" || sqlTableTextBox.Text == "" || sqlUserTextBox.Text == "" || sqlPasswordTextBox.Text == "" || sortPathTextBox.Text == "")
            {
                MessageBox.Show("必需填写所有项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(tag1TextBox.Text, "^[1-9]$|(^[1-9][0-9]$)|(^[1-9][0-9][0-9]$)|(^[1-9][0-9][0-9][0-9]$)|(^[1-6][0-5][0-5][0-3][0-5]$)") || !Regex.IsMatch(tag2TextBox.Text, "^[1-9]$|(^[1-9][0-9]$)|(^[1-9][0-9][0-9]$)|(^[1-9][0-9][0-9][0-9]$)|(^[1-6][0-5][0-5][0-3][0-5]$)"))
            {
                MessageBox.Show("标签只能输入1至65535整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(quantityTextBox.Text, "^[1-9]$|(^[1-9][0-9]$)|(^[1-9][0-9][0-9]$)|1000"))
            {
                MessageBox.Show("结果数只能输入1至1000整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(timeoutTextBox.Text, "^[1-9]$|[1-5][0-9]$|60$"))
            {
                MessageBox.Show("超时只能输入1至60整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(appidTextBox.Text, @"^\+?[1-9][0-9]*$"))
            {
                MessageBox.Show("接口识别号只能输入正整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(sqlDataTextBox.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("数据库只能输入英文字母", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(sqlTableTextBox.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("数据表只能输入英文字母", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(depotPathTextBox.Text))
            {
                MessageBox.Show("图库目录无效", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(sortPathTextBox.Text))
            {
                MessageBox.Show("归类目录无效", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Api api = new Api();
            api.Appid = appidTextBox.Text;
            api.Apikey = apikeyTextBox.Text;
            api.Secreykey = secreykeyTextBox.Text;
            api.Tags1 = int.Parse(tag1TextBox.Text);
            api.Tags2 = int.Parse(tag2TextBox.Text);
            api.Timeout = int.Parse(timeoutTextBox.Text);
            api.Quantity = int.Parse(quantityTextBox.Text);
            api.Serverip = sqlIPTextBox.Text;
            api.Dataname = sqlDataTextBox.Text;
            api.Table = sqlTableTextBox.Text;
            api.Userid = sqlUserTextBox.Text;
            api.Password = sqlPasswordTextBox.Text;
            api.Path = depotPathTextBox.Text;
            api.SortPath = sortPathTextBox.Text;

            bool sql = SqlFunction.CreateTable(api, @"CREATE TABLE " + sqlTableTextBox.Text + "(ID INT PRIMARY KEY IDENTITY(1, 1), Names VARCHAR(256) not null, Path VARCHAR(256) not null, LogID VARCHAR(256), ContSign VARCHAR(256), Tsgs1 INT, Tsgs2 INT, Result VARCHAR(256) not null, Message  VARCHAR(256) not null, Times DATETIME not null)");// 新建表
            if (!sql)
            {
                MessageBox.Show("建立数据表失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool add = ApiFunction.AddDepot(depotTextBox.Text, api);
            if (!add)
            {
                MessageBox.Show("生成配置失败，请手动删除已成功建立的数据表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("保存完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);// 新建配置
            this.Close();
        }

        /// <summary>
        /// 图库目录浏览
        /// </summary>
        private void depotPathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) depotPathTextBox.Text = folderBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// 归类目录浏览
        /// </summary>
        private void sortPathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) sortPathTextBox.Text =folderBrowserDialog.SelectedPath;
        }
    }
}
