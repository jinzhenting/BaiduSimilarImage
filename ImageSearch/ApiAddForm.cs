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
        
        private void api_add_save_button_Click(object sender, System.EventArgs e)//完成按钮
        {
            if (depot_name_textbox.Text == "" || api_appid_textbox.Text == "" || api_apikey_textbox.Text == "" || api_secreykey_textbox.Text == "" || api_timeout_textbox.Text == "" || api_tag1_textbox.Text == "" || api_tag2_textbox.Text == "" || api_path_textbox.Text == "" || api_quantity_textbox.Text == "" || sql_serverip_textbox.Text == "" || sql_dataname_textbox.Text == "" || sql_table_textbox.Text == "" || sql_userid_textbox.Text == "" || sql_password_textbox.Text == "")
            {
                MessageBox.Show("必需填写所有项目");
                return;
            }

            if (!Regex.IsMatch(api_tag1_textbox.Text, "^[1-9]$|(^[1-9][0-9]$)|(^[1-9][0-9][0-9]$)|(^[1-9][0-9][0-9][0-9]$)|(^[1-6][0-5][0-5][0-3][0-5]$)") || !Regex.IsMatch(api_tag2_textbox.Text, "^[1-9]$|(^[1-9][0-9]$)|(^[1-9][0-9][0-9]$)|(^[1-9][0-9][0-9][0-9]$)|(^[1-6][0-5][0-5][0-3][0-5]$)"))
            {
                MessageBox.Show("标签只能输入1至65535整数");
                return;
            }

            if (!Regex.IsMatch(api_quantity_textbox.Text, "^[1-9]$|(^[1-9][0-9]$)|(^[1-9][0-9][0-9]$)|1000"))
            {
                MessageBox.Show("结果数只能输入1至1000整数");
                return;
            }

            if (!Regex.IsMatch(api_timeout_textbox.Text, "^[1-9]$|[1-5][0-9]$|60$"))
            {
                MessageBox.Show("超时只能输入1至60整数");
                return;
            }

            if (!Regex.IsMatch(api_appid_textbox.Text, @"^\+?[1-9][0-9]*$"))
            {
                MessageBox.Show("接口识别号只能输入正整数");
                return;
            }

            if (!Regex.IsMatch(sql_dataname_textbox.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("数据库只能输入英文字母");
                return;
            }

            if (!Regex.IsMatch(sql_table_textbox.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("数据表只能输入英文字母");
                return;
            }

            if (!Directory.Exists(api_path_textbox.Text))
            {
                MessageBox.Show("图库本地位置无效");
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

            Sql.CreateTable(api, @"CREATE TABLE " + sql_table_textbox.Text + "(ID INT PRIMARY KEY IDENTITY(1, 1), Names VARCHAR(256) not null, Path VARCHAR(256) not null, LogID VARCHAR(256), ContSign VARCHAR(256), Tsgs1 INT, Tsgs2 INT, Result VARCHAR(256) not null, Message  VARCHAR(256) not null, Times DATETIME not null)");//新建表
            ApiFunction.AddDepot(depot_name_textbox.Text, api);//新建配置

            MessageBox.Show("完成");
            this.Close();
        }

        private void api_path_button_Click(object sender, System.EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK) api_path_textbox.Text = (Regex.IsMatch(folder.SelectedPath, @"[\\]$")) ? folder.SelectedPath : folder.SelectedPath + @"\";
        }

        
    }
}
