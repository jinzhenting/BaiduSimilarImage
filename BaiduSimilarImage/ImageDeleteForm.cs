using Baidu.Aip.ImageSearch;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BaiduSimilarImage
{
    /// <summary>
    /// 在API图库中删除图片窗口
    /// </summary>
    public partial class ImageDeleteForm : Form
    {
        public ImageDeleteForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗口载入时
        /// </summary>
        private void ImageDeleteForm_Load(object sender, EventArgs e)
        {
            if (ApiFunction.GetDepotList() != null) depotListCombobox.DataSource = ApiFunction.GetDepotList();// 图库下拉列表数据源

            try
            {
                Icon = new Icon(Path.Combine(Application.StartupPath, @"Skin\Delete.ico"));
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

        /// <summary>
        /// 浏览图片位置按钮
        /// </summary>
        private void deletePathButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            if (openfiledialog.ShowDialog() != DialogResult.OK) return;
            if (ApiFunction.AcceptFormatByExtension(openfiledialog.FileName))
            {
                deletePathTextBox.Text = openfiledialog.FileName;
                pictureBox.ImageLocation = openfiledialog.FileName;
                progressBar.Value = 0;
                progressLabel.Text = "已选择图片";
            }
            else MessageBox.Show(ApiFunction.GetError("216201"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);// 扩展名不受支持，获取错误提示
        }

        /// <summary>
        /// 上传图片进行删除复选框
        /// </summary>
        private void deleteImageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (deleteImageCheckBox.Checked)
            {
                deleteSignCheckBox.Checked = false;
                deleteSignTextBox.ReadOnly = true;
                deletePathTextBox.ReadOnly = false;
                deletePathButton.Visible = true;
                deleteSignTextBox.Text = "";
            }
            else
            {
                deleteSignCheckBox.Checked = true;
                deleteSignTextBox.ReadOnly = false;
                pictureBox.Image = null;
                deletePathTextBox.Text = "";
            }
        }

        /// <summary>
        /// 输入图片签名进行删除复选框
        /// </summary>
        private void deleteSignCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (deleteSignCheckBox.Checked)
            {
                deletePathTextBox.ReadOnly = true;
                deletePathButton.Visible = false;
                deleteSignTextBox.ReadOnly = false;
                deleteImageCheckBox.Checked = false;
                pictureBox.Image = null;
                deletePathTextBox.Text = "";
            }
            else
            {
                deleteSignTextBox.ReadOnly = true;
                deleteImageCheckBox.Checked = true;
                deletePathTextBox.ReadOnly = false;
                deletePathButton.Visible = true;
                deleteSignTextBox.Text = "";
            }
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(deletePathTextBox.Text) || deleteSignTextBox.Text != "")
            {
                var back = new object[4];// 装箱
                back[0] = depotListCombobox.Text;
                back[1] = deleteImageCheckBox.Checked;
                back[2] = deletePathTextBox.Text;
                back[3] = deleteSignTextBox.Text;
                deleteBack.RunWorkerAsync(back);
                progressBar.Value = 50;
                progressLabel.Text = "删除中...";
            }
            else
            {
                MessageBox.Show("未选择图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 异步删除后台开始
        /// </summary>
        private void deleteBack_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var back = e.Argument as object[];// 拆箱
            string depot = (string)back[0];
            bool mode = (bool)back[1];
            string path = (string)back[2];
            string sign = (string)back[3];
            Api api = ApiFunction.GetApi(depot);
            if (api != null)
            {
                if (mode)// 上传图片进行删除
                {
                    ImageSearch client = ApiFunction.GetClient(api.Appid, api.Apikey, api.Secreykey, api.Timeout);
                    bool delete = Sql.Delete(depot, "DELETE FROM " + api.Table + " WHERE Path = '" + path.Replace(api.Path, "").Replace("'", "''") + "'");// 删除数据记录
                    if (client != null && delete) e.Result = ApiFunction.DeleteByImage(path, client);// 申请删除
                    else return;
                }

                else//输入图片签名进行删除
                {
                    ImageSearch client = ApiFunction.GetClient(api.Appid, api.Apikey, api.Secreykey, api.Timeout);
                    bool delete = Sql.Delete(depot, "DELETE FROM " + api.Table + " WHERE ContSign = '" + sign + "'");// 删除数据记录
                    if (client != null && delete) e.Result = ApiFunction.DeleteBySian(sign, client);// 申请删除
                    else return;
                }
            }
        }

        /// <summary>
        /// 异步删除后台完成
        /// </summary>
        private void deleteBack_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)// 错误
            {
                MessageBox.Show("后台删除错误如下\r\n\r\n" + e.Error.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            JObject json = e.Result as JObject;// 接收传出
            if (json == null || json.ToString() == "")
            {
                MessageBox.Show("API返回了错误，删除失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (json.Property("error_code") != null && json.Property("error_code").ToString() != "")// 返回Json包含错误信息
            {
                MessageBox.Show(ApiFunction.GetError(json["error_code"].ToString()), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar.Value = 100;
                progressLabel.Text = "删除失败";
                return;
            }
            
            if (json.Property("log_id") != null && json.Property("log_id").ToString() != "")// 返回Json正确
            {
                MessageBox.Show("成功提交申请删除日志号为“" + json["log_id"].ToString() + "”的图片，日前百度API图片删除延时生效，每天数据库定时更新进行物理删除，刚删除时仍然可以在图库中检索到（但图库管理后台是同步清除），请过一段时间再验证，一般最多延时4小时左右", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                progressBar.Value = 100;
                progressLabel.Text = "成功提交申请";
                return;
            }

        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (deleteBack.IsBusy)
            {
                //deleteBack.CancelAsync();// 暂不支持取消
                MessageBox.Show("请等待后台操作完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Close();
        }

        /// <summary>
        /// 窗口关闭检测
        /// </summary>
        private void ImageDeleteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (deleteBack.IsBusy)
            {
                e.Cancel = true;
                MessageBox.Show("后台正在删除图片，请勿关闭窗口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
