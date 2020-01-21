using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ImageSearch
{
    public partial class ApiImageUpForm : Form
    {
        public ApiImageUpForm()
        {
            InitializeComponent();
        }

        private void api_add_save_button_Click(object sender, System.EventArgs e)// 完成按钮
        {
            if (depot_name_textbox.Text == "" || api_appid_textbox.Text == "" || api_apikey_textbox.Text == "" || api_secreykey_textbox.Text == "" || api_timeout_textbox.Text == "" || api_tag1_textbox.Text == "" || api_tag2_textbox.Text == "" || api_path_textbox.Text == "" || api_quantity_textbox.Text == "" || sql_serverip_textbox.Text == "" || sql_dataname_textbox.Text == "" || sql_table_textbox.Text == "" || sql_userid_textbox.Text == "" || sql_password_textbox.Text == "")
            {
                MessageBox.Show("必需填写所有项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(api_tag1_textbox.Text, "^[1-9]$|(^[1-9][0-9]$)|(^[1-9][0-9][0-9]$)|(^[1-9][0-9][0-9][0-9]$)|(^[1-6][0-5][0-5][0-3][0-5]$)") || !Regex.IsMatch(api_tag2_textbox.Text, "^[1-9]$|(^[1-9][0-9]$)|(^[1-9][0-9][0-9]$)|(^[1-9][0-9][0-9][0-9]$)|(^[1-6][0-5][0-5][0-3][0-5]$)"))
            {
                MessageBox.Show("标签只能输入1至65535整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(api_quantity_textbox.Text, "^[1-9]$|(^[1-9][0-9]$)|(^[1-9][0-9][0-9]$)|1000"))
            {
                MessageBox.Show("结果数只能输入1至1000整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(api_timeout_textbox.Text, "^[1-9]$|[1-5][0-9]$|60$"))
            {
                MessageBox.Show("超时只能输入1至60整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(api_appid_textbox.Text, @"^\+?[1-9][0-9]*$"))
            {
                MessageBox.Show("接口识别号只能输入正整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(sql_dataname_textbox.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("数据库只能输入英文字母", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(sql_table_textbox.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("数据表只能输入英文字母", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(api_path_textbox.Text))
            {
                MessageBox.Show("图库本地位置无效", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Api api = new Api();
            api.Appid = api_appid_textbox.Text;
            api.Apikey = api_apikey_textbox.Text;
            api.Secreykey = api_secreykey_textbox.Text;
            api.Tags1 = int.Parse(api_tag1_textbox.Text);
            api.Tags2 = int.Parse(api_tag2_textbox.Text);
            api.Timeout = int.Parse(api_timeout_textbox.Text);
            api.Quantity = int.Parse(api_quantity_textbox.Text);
            api.Serverip = sql_serverip_textbox.Text;
            api.Dataname = sql_dataname_textbox.Text;
            api.Table = sql_table_textbox.Text;
            api.Userid = sql_userid_textbox.Text;
            api.Password = sql_password_textbox.Text;
            api.Path = (Regex.IsMatch(api_path_textbox.Text, @"[\\]$")) ? api_path_textbox.Text : api_path_textbox.Text + @"\";

            bool sql = Sql.CreateTable(api, @"CREATE TABLE " + sql_table_textbox.Text + "(ID INT PRIMARY KEY IDENTITY(1, 1), Names VARCHAR(256) not null, Path VARCHAR(256) not null, LogID VARCHAR(256), ContSign VARCHAR(256), Tsgs1 INT, Tsgs2 INT, Result VARCHAR(256) not null, Message  VARCHAR(256) not null, Times DATETIME not null)");// 新建表
            if (!sql)
            {
                MessageBox.Show("建立数据表失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool adddepot = ApiFunction.AddDepot(depot_name_textbox.Text, api);
            if (!adddepot)
            {
                MessageBox.Show("生成配置失败，请手动删除已成功建立的数据表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("保存完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);// 新建配置
            this.Close();
        }

        private void api_path_button_Click(object sender, System.EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK) api_path_textbox.Text = (Regex.IsMatch(folder.SelectedPath, @"[\\]$")) ? folder.SelectedPath : folder.SelectedPath + @"\";
        }

        private void ApiImageUpForm_Load(object sender, System.EventArgs e)
        {
            try//读取程序图标
            {
                Icon = new Icon(Path.Combine(Application.StartupPath, @"Skin\Add.ico"));
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("无权限加载窗口图标文件，请尝试使用管理员权限重新运行本程序", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("窗口图标文件不存在，程序将自动退出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载窗口图标时发生如下错误，程序将自动退出，错误描述如下\r\n\r\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return;
            }
        }
    }
}
