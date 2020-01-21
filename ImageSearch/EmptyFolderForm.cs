using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ImageSearch
{
    public partial class EmptyFolderForm : Form
    {
        public EmptyFolderForm()
        {
            InitializeComponent();
        }
        
        private void EmptyFolderForm_Load(object sender, EventArgs e)
        {
            EmptyListSettings();

            try
            {
                Icon = new Icon(Path.Combine(Application.StartupPath, @"Skin\EmptyFolder.ico"));
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

        private void EmptyListSettings()// 扫描列表样式
        {
            empty_listview.Columns.Add("空白目录列表");// 添加列标题
            empty_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
        }

        private void empty_path_button_Click(object sender, EventArgs e)// 浏览按钮
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK) empty_path_textbox.Text = folder.SelectedPath;
        }

        private void empty_scan_button_Click(object sender, EventArgs e)// 扫描按钮
        {
            if (empty_path_textbox.Text == "")// 主目录
            {
                MessageBox.Show("未选择主目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(empty_path_textbox.Text))
            {
                MessageBox.Show("主目录不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (empty_listview.Items.Count > 0)
            {
                empty_listview.Clear();
                EmptyListSettings();
            }

            if (empty_scan_button.Text == "扫描")
            {
                empty_scan_button.Text = "停止";
                if (!empty_background.IsBusy) empty_background.RunWorkerAsync(empty_path_textbox.Text);
                empty_bar.Value = 1;
                empty_label.Text = "% 开始扫描...";
            }
            else
            {
                empty_scan_button.Text = "扫描";
                if (empty_background.IsBusy) empty_background.CancelAsync();
            }
        }

        private void empty_cancel_button_Click(object sender, EventArgs e)// 取消按钮
        {
            if (empty_background.IsBusy)// 入库运行中
            {
                MessageBox.Show("请先停止扫描", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Close();
        }

        private void empty_delete_button_Click(object sender, EventArgs e)// 删除按钮
        {
            if (empty_listview.CheckedItems.Count < 1)
            {
                MessageBox.Show("没有选中项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (ListViewItem item in empty_listview.CheckedItems)
            {
                try
                {
                    empty_bar.Value = 50;
                    empty_label.Text = "正在删除";
                    Directory.Delete(item.Text, true);
                    empty_listview.Items.Remove(item);
                    empty_bar.Value = 100;
                    empty_label.Text = "删除完成";
                }
                catch (UnauthorizedAccessException)
                {
                    if (MessageBox.Show("无权限访问：" + item.Text + "请尝试使用管理员权限运行本程序，是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;// 警示窗口
                    else return;
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show(item.Text + "不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除" + item.Text + "时发生如下错误\r\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void empty_background_DoWork(object sender, DoWorkEventArgs e)// 开始后台
        {
            string path = e.Argument as string;// 接收

            Stack<string> nodesStack = new Stack<string>();// 栈
            List<string> pathList = new List<string>();// 文件夹
            List<Array> fileList = new List<Array>();// 文件
            List<string> nulllist = new List<string>();// 空文件夹

            string[] dio = Directory.GetDirectories(path, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string str in dio)
            {
                nodesStack.Push(str);// 将顶层目录压栈
                while (nodesStack.Count > 0)
                {
                    if (empty_background.CancellationPending)// 用户取消
                    {
                        e.Cancel = true;
                        return;
                    }

                    string tempPath = nodesStack.Pop();// 顶层目录出栈
                    pathList.Add(tempPath);// 记录出栈目录
                    empty_background.ReportProgress(50, tempPath);// 进度

                    try
                    {
                        if (Directory.GetDirectories(tempPath).Length == 0 && Directory.GetFiles(tempPath).Length == 0) nulllist.Add(tempPath);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        if (MessageBox.Show("无权限访问：" + tempPath + "请尝试使用管理员权限运行本程序，是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;// 警示窗口
                        else return;
                    }


                    FileInfo fi = new FileInfo(tempPath);
                    if ((fi.Attributes & FileAttributes.Directory) != 0)
                    {
                        Array subDire = null, subFiles = null;// subDire: 子目录组 subFiles: 子文件组
                        subDire = Directory.GetDirectories(tempPath);
                        subFiles = Directory.GetFiles(tempPath);
                        fileList.Add(subFiles);// 记录文件目录不再入栈
                        if (subDire != null && subFiles != null) foreach (var ex in subDire) nodesStack.Push(ex.ToString());// 子目录组中每个目录进行遍历再次压入栈
                    }
                }
            }

            e.Result = nulllist;// 传出
        }

        private void empty_background_ProgressChanged(object sender, ProgressChangedEventArgs e)// 后台进度
        {
            empty_bar.Value = (e.ProgressPercentage < 101) ? e.ProgressPercentage : empty_bar.Value;
            empty_label.Text = empty_bar.Value.ToString() + "% 正在扫描：" + e.UserState as string;
        }

        private void empty_background_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)// 后台完成
        {
            if (e.Error != null)
            {
                MessageBox.Show("扫描文件错误如下\r\n" + e.Error.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                empty_label.Text = "扫描文件后台错误";
                return;
            }
            
            if (e.Cancelled)
            {
                MessageBox.Show("扫描已取消", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                empty_label.Text = "扫描已取消";
                return;
            }
            
            empty_scan_button.Text = "扫描";
            empty_label.Text = "扫描已完成";
            empty_bar.Value = 100;
            
            List<string> list = e.Result as List<string>;// 接收传出
            if (list == null)
            {
                empty_label.Text = "扫描错误";
                return;
            }
            if (list.Count < 1)
            {
                MessageBox.Show("没有发现空白目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            
            foreach (string str in list)
            {
                ListViewItem item = empty_listview.Items.Add(str);
                empty_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
                empty_listview.Items[empty_listview.Items.Count - 1].EnsureVisible();
            }
        }
        
    }
}
