using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ImageSearch
{
    public partial class ApiImageAddForm : Form
    {
        public ApiImageAddForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 读取程序图标
        /// </summary>
        private void ApiImageAddForm_Load(object sender, EventArgs e)
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
            if (depotTextbox.Text == "" || appidTextbox.Text == "" || apikeyTextbox.Text == "" || secreykeyTextbox.Text == "" || timeoutTextbox.Text == "" || tag1Textbox.Text == "" || tag2Textbox.Text == "" || depotPathTextbox.Text == "" || quantityTextbox.Text == "" || sqlIPTextbox.Text == "" || sqlDataTextbox.Text == "" || sqlTableTextbox.Text == "" || sqlUserTextbox.Text == "" || sqlPasswordTextbox.Text == "" || sortTextbox.Text == "")
            {
                MessageBox.Show("必需填写所有项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(tag1Textbox.Text, "^[1-9]$|(^[1-9][0-9]$)|(^[1-9][0-9][0-9]$)|(^[1-9][0-9][0-9][0-9]$)|(^[1-6][0-5][0-5][0-3][0-5]$)") || !Regex.IsMatch(tag2Textbox.Text, "^[1-9]$|(^[1-9][0-9]$)|(^[1-9][0-9][0-9]$)|(^[1-9][0-9][0-9][0-9]$)|(^[1-6][0-5][0-5][0-3][0-5]$)"))
            {
                MessageBox.Show("标签只能输入1至65535整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(quantityTextbox.Text, "^[1-9]$|(^[1-9][0-9]$)|(^[1-9][0-9][0-9]$)|1000"))
            {
                MessageBox.Show("结果数只能输入1至1000整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(timeoutTextbox.Text, "^[1-9]$|[1-5][0-9]$|60$"))
            {
                MessageBox.Show("超时只能输入1至60整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(appidTextbox.Text, @"^\+?[1-9][0-9]*$"))
            {
                MessageBox.Show("接口识别号只能输入正整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(sqlDataTextbox.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("数据库只能输入英文字母", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(sqlTableTextbox.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("数据表只能输入英文字母", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(depotPathTextbox.Text))
            {
                MessageBox.Show("图库目录无效", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(sortTextbox.Text))
            {
                MessageBox.Show("归类目录无效", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Api api = new Api();
            api.Appid = appidTextbox.Text;
            api.Apikey = apikeyTextbox.Text;
            api.Secreykey = secreykeyTextbox.Text;
            api.Tags1 = int.Parse(tag1Textbox.Text);
            api.Tags2 = int.Parse(tag2Textbox.Text);
            api.Timeout = int.Parse(timeoutTextbox.Text);
            api.Quantity = int.Parse(quantityTextbox.Text);
            api.Serverip = sqlIPTextbox.Text;
            api.Dataname = sqlDataTextbox.Text;
            api.Table = sqlTableTextbox.Text;
            api.Userid = sqlUserTextbox.Text;
            api.Password = sqlPasswordTextbox.Text;
            api.Path = (Regex.IsMatch(depotPathTextbox.Text, @"[\\]$")) ? depotPathTextbox.Text : depotPathTextbox.Text + @"\";
            api.SortPath = (Regex.IsMatch(sortTextbox.Text, @"[\\]$")) ? sortTextbox.Text : sortTextbox.Text + @"\";

            bool sql = Sql.CreateTable(api, @"CREATE TABLE " + sqlTableTextbox.Text + "(ID INT PRIMARY KEY IDENTITY(1, 1), Names VARCHAR(256) not null, Path VARCHAR(256) not null, LogID VARCHAR(256), ContSign VARCHAR(256), Tsgs1 INT, Tsgs2 INT, Result VARCHAR(256) not null, Message  VARCHAR(256) not null, Times DATETIME not null)");// 新建表
            if (!sql)
            {
                MessageBox.Show("建立数据表失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool add = ApiFunction.AddDepot(depotTextbox.Text, api);
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
        private void apiPathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) depotPathTextbox.Text = (Regex.IsMatch(folderBrowserDialog.SelectedPath, @"[\\]$")) ? folderBrowserDialog.SelectedPath : folderBrowserDialog.SelectedPath + @"\";
        }

        /// <summary>
        /// 归类目录浏览
        /// </summary>
        private void apiSortButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) sortTextbox.Text = (Regex.IsMatch(folderBrowserDialog.SelectedPath, @"[\\]$")) ? folderBrowserDialog.SelectedPath : folderBrowserDialog.SelectedPath + @"\";
        }
    }
}
