using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows.Forms;

namespace ImageSearch
{
    public partial class ImageDeleteForm : Form
    {
        public ImageDeleteForm()
        {
            InitializeComponent();
        }

        private void ImageDeleteForm_Load(object sender, EventArgs e)//窗口载入时
        {
            depot_list_combobox.DataSource = ApiFunction.GetDepotList();//图库下拉列表数据源
        }

        private void delete_path_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            if (openfiledialog.ShowDialog() != DialogResult.OK) return;
            if (ApiFunction.AcceptFormat2(openfiledialog.FileName))
            {
                delete_path_textbox.Text = openfiledialog.FileName;
                delete_picturebox.ImageLocation = openfiledialog.FileName;
                delete_bar.Value = 0;
                delete_label.Text = "已选择图片";
            }
            else MessageBox.Show(ApiFunction.GetError("216201"));//扩展名不受支持，获取错误提示
        }

        private void delete_image_checkbox_CheckedChanged(object sender, EventArgs e)//复选框
        {
            if (delete_image_checkbox.Checked)
            {
                delete_sign_checkbox.Checked = false;
                delete_sign_textbox.ReadOnly = true;
                delete_path_textbox.ReadOnly = false;
                delete_path_button.Visible = true;
                delete_sign_textbox.Text = "";
            }
            else
            {
                delete_sign_checkbox.Checked = true;
                delete_sign_textbox.ReadOnly = false;
                delete_picturebox.Image = null;
                delete_path_textbox.Text = "";
            }
        }

        private void delete_sign_checkbox_CheckedChanged(object sender, EventArgs e)//复选框
        {
            if (delete_sign_checkbox.Checked)
            {
                delete_path_textbox.ReadOnly = true;
                delete_path_button.Visible = false;
                delete_sign_textbox.ReadOnly = false;
                delete_image_checkbox.Checked = false;
                delete_picturebox.Image = null;
                delete_path_textbox.Text = "";
            }
            else
            {
                delete_sign_textbox.ReadOnly = true;
                delete_image_checkbox.Checked = true;
                delete_path_textbox.ReadOnly = false;
                delete_path_button.Visible = true;
                delete_sign_textbox.Text = "";
            }
        }

        private void delete_button_Click(object sender, EventArgs e)//删除按钮
        {
            if (File.Exists(delete_path_textbox.Text) || delete_sign_textbox.Text != "")
            {
                var back = new object[4];//装箱
                back[0] = depot_list_combobox.Text;
                back[1] = delete_image_checkbox.Checked;
                back[2] = delete_path_textbox.Text;
                back[3] = delete_sign_textbox.Text;
                delete_background.RunWorkerAsync(back);
                delete_bar.Value = 50;
                delete_label.Text = "删除中...";
            }
            else
            {
                MessageBox.Show("未选择图片");
                return;
            }
        }

        private void delete_background_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var back = e.Argument as object[];//拆箱
            string depot_name = (string)back[0];
            bool mode = (bool)back[1];
            string path = (string)back[2];
            string sign = (string)back[3];
            Api api = ApiFunction.GetApi(depot_name);
            
            if (mode)//上传图片进行删除
            {
                Baidu.Aip.ImageSearch.ImageSearch client = ApiFunction.GetClient(api.Appid, api.Apikey, api.Secreykey, api.Timeout);
                e.Result = ApiFunction.DeleteByImage(path, client);//申请删除
                string delete = "DELETE FROM " + api.Table + " WHERE Path = '" + path.Replace(api.Path, "").Replace("'", "''") + "'";
                Sql.Delete(depot_name, delete);//删除数据记录
            }
            
            else//输入图片签名进行删除
            {
                Baidu.Aip.ImageSearch.ImageSearch client = ApiFunction.GetClient(api.Appid, api.Apikey, api.Secreykey, api.Timeout);
                e.Result = ApiFunction.DeleteBySian(sign, client);//申请删除
                string delete = "DELETE FROM " + api.Table + " WHERE ContSign = '" + sign + "'";
                Sql.Delete(depot_name, delete);//删除数据记录
            }
        }
        
        private void delete_background_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)//后台完成
        {
            if (e.Error != null)//错误
            {
                MessageBox.Show("后台删除错误如下\r\n" + e.Error.ToString());
                return;
            }
            
            JObject json = e.Result as JObject;//接收传出
            if (json == null || json.ToString() == "")
            {
                MessageBox.Show("API返回了错误，删除失败");
                return;
            }

            if (json.Property("error_code") != null && json.Property("error_code").ToString() != "")//返回Json包含错误信息
            {
                MessageBox.Show(ApiFunction.GetError(json["error_code"].ToString()));
                delete_bar.Value = 100;
                delete_label.Text = "删除失败";
                return;
            }
            
            if (json.Property("log_id") != null && json.Property("log_id").ToString() != "")//返回Json正确
            {
                MessageBox.Show("成功提交申请删除日志号为“" + json["log_id"].ToString() + "”的图片，日前百度API图片删除延时生效，每天数据库定时更新进行物理删除，刚删除时仍然可以在图库中检索到（但图库管理后台是同步清除），请过一段时间再验证，一般最多延时4小时左右");
                delete_bar.Value = 100;
                delete_label.Text = "成功提交申请";
                return;
            }

        }

        private void delete_cancel_button_Click(object sender, EventArgs e)//取消按钮
        {
            if (delete_background.IsBusy)
            {
                //delete_background.CancelAsync();//暂不支持取消
                MessageBox.Show("请等待后台操作完成");
                return;
            }
            this.Close();
        }

        private void ImageDeleteForm_FormClosing(object sender, FormClosingEventArgs e)//窗口关闭检测
        {
            if (delete_background.IsBusy)
            {
                e.Cancel = true;
                MessageBox.Show("后台正在删除图片，请勿关闭窗口");
            }
        }

    }
}
