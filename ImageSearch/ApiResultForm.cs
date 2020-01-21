using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageSearch
{
    public partial class ApiResultForm : Form
    {
        private List<string> api_list;// 结果列表
        private string end_mode, image_path;// 本地图库模式//本地主目录
        public ApiResultForm(List<string> api_list1, string end_mode1, string image_path1)
        {
            InitializeComponent();
            api_list = api_list1;
            end_mode = end_mode1;
            image_path = image_path1;
        }

        private void IconSetting()// 窗口图标
        {
            try
            {
                Icon = new Icon(Path.Combine(Application.StartupPath, @"Skin\Icon.ico"));
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

        private void ApiResultForm_Load(object sender, EventArgs e)// 窗口载入时
        {
            IconSetting();
            IconNullBool();
        }

        private void ApiResultForm_Shown(object sender, EventArgs e)// 窗口显示时
        {
            icon_backgroundStartr();
        }

        private void IconNullBool()// 项目缺失缩略图检测
        {
            if (!(File.Exists("ListIcon.jpg")))
            {
                MessageBox.Show("缩略图缺失图标载入错误，程序将自动退出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return;
            }
        }

        private void icon_backgroundStartr()// 异步调用
        {
            if (icon_background.IsBusy)
            {
                MessageBox.Show("异步后台被占用", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var icon = new object[2];// 装箱
            icon[0] = api_list;
            icon[1] = image_path;
            icon_background.RunWorkerAsync(icon);
            progress_label.Text = "% 开始载入缩略图...";
        }

        private void icon_background_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)// 异步实现
        {
            try
            {
                var icon = e.Argument as object[];// 拆箱
                List<string> result_list = (List<string>)icon[0];
                string image_path = (string)icon[1];

                ImageList imagelist = new ImageList();// 定义项目列表
                imagelist.ImageSize = new Size(256, 256);
                imagelist.ColorDepth = ColorDepth.Depth32Bit;

                for (int i = 0; i < result_list.Count; i++)// 遍历API结果
                {
                    if (icon_background.CancellationPending)// 检测后台取消
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (File.Exists(image_path + result_list[i])) imagelist.Images.Add(Image.FromFile(image_path + result_list[i]));// 加载缩略图
                    else imagelist.Images.Add(Image.FromFile("ListIcon.jpg"));// 加载缺失缩略图

                    //进度
                    icon_background.ReportProgress(Percents.Get(i + 1, result_list.Count), result_list[i]);
                }

                var back = new object[1];// 装箱
                back[0] = imagelist;
                e.Result = back;
            }
            catch (Exception ex)
            {
                MessageBox.Show("缩略图载入错误，描述如下\r\n\r\n" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void icon_background_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)// 异步进度
        {
            list_progressbar.Value = e.ProgressPercentage;
            progress_label.Text = list_progressbar.Value + "% 载入缩略图" + e.UserState as string;
        }

        private void icon_background_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)// 异步完成
        {
            if (e.Error != null)
            {
                MessageBox.Show("缩略图载入错误如下\r\n\r\n" + e.Error.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                list_progressbar.Value = 0;
                return;
            }

            if (e.Cancelled)
            {
                MessageBox.Show("已取消载入缩略图", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                list_progressbar.Value = 0;
                return;
            }

            progress_label.Text = "列表载入完成";
            list_progressbar.Value = 100;

            var back = e.Result as object[];// 拆箱
            ImageList imagelist = (ImageList)back[0];
            for (int i = 0; i < api_list.Count; i++)// 把项目名遍历到ListView
            {
                ListViewItem listviewitem = new ListViewItem();// 定义单个项目
                listviewitem.ImageIndex = i;
                listviewitem.Text = image_path + api_list[i];
                icon_listview.Items.Add(listviewitem);
            }
            icon_listview.LargeImageList = imagelist;
        }

        private void clear_result_button_Click(object sender, EventArgs e)// 异步取消
        {
            if (icon_background.IsBusy) icon_background.CancelAsync();
            else MessageBox.Show("载入已完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void online_image_path_button_Click(object sender, EventArgs e)// 浏览文件夹按钮
        {
            if (icon_listview.SelectedItems.Count < 1)
            {
                MessageBox.Show("没有选中图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (File.Exists(icon_listview.SelectedItems[0].Text))
            {
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
                psi.Arguments = "/e,/select," + icon_listview.SelectedItems[0].Text;
                System.Diagnostics.Process.Start(psi);
            }
            else MessageBox.Show("图片" + icon_listview.SelectedItems[0].Text + "不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void icon_listview_DoubleClick(object sender, EventArgs e)// 图片双击打开
        {
            if (icon_listview.SelectedItems.Count < 1) return;
            if (File.Exists(icon_listview.SelectedItems[0].Text)) System.Diagnostics.Process.Start(icon_listview.SelectedItems[0].Text);
            else MessageBox.Show("图片" + icon_listview.SelectedItems[0].Text + "不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ApiResultForm_FormClosing(object sender, FormClosingEventArgs e)// 窗口关闭前停止后台
        {
            if (icon_background.IsBusy)
            {
                e.Cancel = true;
                MessageBox.Show("后台正在载入，请勿关闭窗口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

    }
}
