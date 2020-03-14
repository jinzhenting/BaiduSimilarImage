using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BaiduSimilarImage
{
    /// <summary>
    /// API配置窗口
    /// </summary>
    public partial class DepotSettingsForm : Form
    {
        public DepotSettingsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗口载入时；图库下拉列表
        /// </summary>
        private void ApiSettingsForm_Load(object sender, EventArgs e)
        {
            if (ApiFunction.GetDepotList() != null) depotListCombobox.DataSource = ApiFunction.GetDepotList();
            try
            {
                Icon = new Icon(Path.Combine(Application.StartupPath, @"Skin\Setting.ico"));
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

        Api api = new Api();
        /// <summary>
        /// 图库列表选择
        /// </summary>
        private void depotListCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            api = ApiFunction.GetApi(depotListCombobox.Text);
            if (api != null)
            {
                appidTextBox.Text = api.Appid;
                apikeyTextBox.Text = api.Apikey;
                secreykeyTextBox.Text = api.Secreykey;
                timeoutTextBox.Text = api.Timeout.ToString();
                tag1TextBox.Text = api.Tags1.ToString();
                tag2TextBox.Text = api.Tags2.ToString();
                depotPathTextBox.Text = api.Path;
                sortPathTextBox.Text = api.SortPath;
                quantityTextBox.Text = api.Quantity.ToString();
                sqlIPTextBox.Text = api.Serverip;
                sqlDataTextBox.Text = api.Dataname;
                sqlUserTextBox.Text = api.Userid;
                sqlPasswordTextBox.Text = api.Password;
                sqlTableTextBox.Text = api.Table;
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (depotListCombobox.Text == "")
            {
                MessageBox.Show("请选择图库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (appidTextBox.Text == "" || apikeyTextBox.Text == "" || secreykeyTextBox.Text == "" || timeoutTextBox.Text == "" || tag1TextBox.Text == "" || tag2TextBox.Text == "" || depotPathTextBox.Text == "" || quantityTextBox.Text == "" || sqlIPTextBox.Text == "" || sqlDataTextBox.Text == "" || sqlTableTextBox.Text == "" || sqlUserTextBox.Text == "" || sqlPasswordTextBox.Text == "" || sortPathTextBox.Text == "")
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

            DialogResult result = MessageBox.Show("修改后将无法恢复", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);// 保存
            if (result == DialogResult.OK)
            {
                api.Appid = appidTextBox.Text;
                api.Apikey = apikeyTextBox.Text;
                api.Secreykey = secreykeyTextBox.Text;
                api.Timeout = int.Parse(timeoutTextBox.Text);
                api.Tags1 = int.Parse(tag1TextBox.Text);
                api.Tags2 = int.Parse(tag2TextBox.Text);
                api.Path = depotPathTextBox.Text;
                api.Quantity = int.Parse(quantityTextBox.Text);
                api.Serverip = sqlIPTextBox.Text;
                api.Dataname = sqlDataTextBox.Text;
                api.Userid = sqlUserTextBox.Text;
                api.Password = sqlPasswordTextBox.Text;
                api.Table = sqlTableTextBox.Text;
                if (ApiFunction.SaveApi(depotListCombobox.Text, api)) MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                else MessageBox.Show("保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 清空按钮
        /// </summary>
        private void clearButton_Click(object sender, EventArgs e)// 清空按钮
        {
            appidTextBox.Text = apikeyTextBox.Text = secreykeyTextBox.Text = timeoutTextBox.Text = quantityTextBox.Text = tag1TextBox.Text = tag2TextBox.Text = depotPathTextBox.Text = sqlIPTextBox.Text = sqlDataTextBox.Text = sqlTableTextBox.Text = sqlUserTextBox.Text = sqlPasswordTextBox.Text = "";
        }

        /// <summary>
        /// 本地图片目录浏览按钮
        /// </summary>
        private void depotPathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) depotPathTextBox.Text =folderBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// 新建图库按钮
        /// </summary>
        private void addButton_Click(object sender, EventArgs e)
        {
            DepotAddForm apiImageAddForm = new DepotAddForm();
            apiImageAddForm.ShowDialog();
        }

        /// <summary>
        /// 删除图库按钮
        /// </summary>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (depotListCombobox.Text == "")
            {
                MessageBox.Show("未选择图库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("删除后将无法恢复，请谨慎操作！\r\n\r\n本操作只能删除图库的数据库和程序配置，百度服务器中的图片请登陆百度后台删除，是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)// 警示窗口
            {
                bool deleteDepot = ApiFunction.DeleteDepot(depotListCombobox.Text);// 清除配置
                if (!deleteDepot)
                {
                    MessageBox.Show("删除配置失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool dropTable = Sql.Dropable(depotListCombobox.Text, "DROP TABLE " + api.Table);// 清除数据库
                if (!dropTable)
                {
                    MessageBox.Show("成功删除配置文件，删除数据表失败，请手动删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("删除完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 浏览归类目录
        /// </summary>
        private void sortPathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) sortPathTextBox.Text = folderBrowserDialog.SelectedPath;
        }

        //
    }
}
