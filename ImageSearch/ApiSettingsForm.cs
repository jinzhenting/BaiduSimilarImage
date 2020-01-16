﻿using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ImageSearch
{
    public partial class ApiSettingsForm : Form
    {
        public ApiSettingsForm()
        {
            InitializeComponent();
        }

        private void ApiSettingsForm_Load(object sender, EventArgs e)//窗口载入时
        {
            depot_list_combobox.DataSource = ApiFunction.GetDepotList();//图库下拉列表
        }

        Api api = new Api();
        private void depot_list_combobox_SelectedIndexChanged(object sender, EventArgs e)//图库列表选择
        {
            api = ApiFunction.GetApi(depot_list_combobox.Text);
            api_appid_textbox.Text = api.Appid;
            api_apikey_textbox.Text = api.Apikey;
            api_secreykey_textbox.Text = api.Secreykey;
            api_timeout_textbox.Text = api.Timeout.ToString();
            api_tag1_textbox.Text = api.Tags1.ToString();
            api_tag2_textbox.Text = api.Tags2.ToString();
            api_path_textbox.Text = api.Path;
            api_quantity_textbox.Text = api.Quantity.ToString();
            sql_serverip_textbox.Text = api.Serverip;
            sql_dataname_textbox.Text = api.Dataname;
            sql_userid_textbox.Text = api.Userid;
            sql_password_textbox.Text = api.Password;
            sql_table_textbox.Text = api.Table;
        }

        private void save_api_button_Click(object sender, EventArgs e)//保存按钮
        {
            if (depot_list_combobox.Text == "")
            {
                MessageBox.Show("请选择图库");
                return;
            }

            if (api_appid_textbox.Text == "" || api_apikey_textbox.Text == "" || api_secreykey_textbox.Text == "" || api_timeout_textbox.Text == "" || api_tag1_textbox.Text == "" || api_tag2_textbox.Text == "" || api_path_textbox.Text == "" || api_quantity_textbox.Text == "" || sql_serverip_textbox.Text == "" || sql_dataname_textbox.Text == "" || sql_table_textbox.Text == "" || sql_userid_textbox.Text == "" || sql_password_textbox.Text == "")
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
            
            DialogResult result = MessageBox.Show("修改后将无法恢复", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);//保存
            if (result == DialogResult.OK)
            {
                api.Appid = api_appid_textbox.Text;
                api.Apikey = api_apikey_textbox.Text;
                api.Secreykey = api_secreykey_textbox.Text;
                api.Timeout = int.Parse(api_timeout_textbox.Text);
                api.Tags1 = int.Parse(api_tag1_textbox.Text);
                api.Tags2 = int.Parse(api_tag2_textbox.Text);
                api.Path = (Regex.IsMatch(api_path_textbox.Text, @"[\\]$")) ? api_path_textbox.Text : api_path_textbox.Text + @"\";
                api.Quantity = int.Parse(api_quantity_textbox.Text);
                api.Serverip = sql_serverip_textbox.Text;
                api.Dataname = sql_dataname_textbox.Text;
                api.Userid = sql_userid_textbox.Text;
                api.Password = sql_password_textbox.Text;
                api.Table = sql_table_textbox.Text;
                ApiFunction.SaveApi(depot_list_combobox.Text, api);
            }
        }
        
        private void cel_api_button_Click(object sender, EventArgs e)//清空按钮
        {
            api_appid_textbox.Text = api_apikey_textbox.Text = api_secreykey_textbox.Text = api_timeout_textbox.Text = api_quantity_textbox.Text = api_tag1_textbox.Text = api_tag2_textbox.Text = api_path_textbox.Text = sql_serverip_textbox.Text = sql_dataname_textbox.Text = sql_table_textbox.Text = sql_userid_textbox.Text = sql_password_textbox.Text = "";
        }
        
        private void api_path_button_Click(object sender, EventArgs e)//本地图片目录浏览按钮
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)api_path_textbox.Text = (Regex.IsMatch(folder.SelectedPath, @"[\\]$")) ? folder.SelectedPath : folder.SelectedPath + @"\";
        }

        private void api_add_button_Click(object sender, EventArgs e)//新建图库按钮
        {
            ApiImageUpForm apiaddfrom = new ApiImageUpForm();
            apiaddfrom.ShowDialog();
        }

        private void api_delete_button_Click(object sender, EventArgs e)//删除图库按钮
        {
            if (depot_list_combobox.Text == "")
            {
                MessageBox.Show("未选择图库");
                return;
            }
            
            if (MessageBox.Show("删除后将无法恢复，请谨慎操作！\r\n\r\n本操作只能删除图库的数据库和程序配置，百度服务器中的图片请登陆百度后台删除", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;//警示窗口
            Sql.Dropable(depot_list_combobox.Text, "DROP TABLE " + api.Table);//清除数据库
            ApiFunction.DeleteDepot(depot_list_combobox.Text);//清除配置
            MessageBox.Show("完成");
        }
        
        private void cancel_api_button_Click(object sender, EventArgs e)//取消按钮
        {
            this.Close();
        }

        //
    }
}