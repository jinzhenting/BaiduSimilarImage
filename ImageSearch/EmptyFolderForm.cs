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
        
        /// <summary>
        /// 窗口载入时
        /// </summary>
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
        /// 扫描列表样式
        /// </summary>
        private void EmptyListSettings()
        {
            listView.Columns.Add("空白目录列表");// 添加列标题
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
        }

        /// <summary>
        /// 浏览按钮
        /// </summary>
        private void openButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) pathTextBox.Text = folderBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// 扫描按钮
        /// </summary>
        private void scanButton_Click(object sender, EventArgs e)
        {
            if (pathTextBox.Text == "")// 主目录
            {
                MessageBox.Show("未选择主目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(pathTextBox.Text))
            {
                MessageBox.Show("主目录不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listView.Items.Count > 0)
            {
                listView.Clear();
                EmptyListSettings();
            }

            if (scanButton.Text == "扫描")
            {
                scanButton.Text = "停止";
                if (!emptyBack.IsBusy) emptyBack.RunWorkerAsync(pathTextBox.Text);
                progressBar.Value = 1;
                progressLabel.Text = "% 开始扫描...";
            }
            else
            {
                scanButton.Text = "扫描";
                if (emptyBack.IsBusy) emptyBack.CancelAsync();
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        private void cancelBbutton_Click(object sender, EventArgs e)
        {
            if (emptyBack.IsBusy)// 入库运行中
            {
                MessageBox.Show("请先停止扫描", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Close();
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (listView.CheckedItems.Count < 1)
            {
                MessageBox.Show("没有选中项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (ListViewItem item in listView.CheckedItems)
            {
                try
                {
                    progressBar.Value = 50;
                    progressLabel.Text = "正在删除";
                    Directory.Delete(item.Text, true);
                    listView.Items.Remove(item);
                    progressBar.Value = 100;
                    progressLabel.Text = "删除完成";
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

        /// <summary>
        /// 开始后台
        /// </summary>
        private void emptyBack_DoWork(object sender, DoWorkEventArgs e)
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
                    if (emptyBack.CancellationPending)// 用户取消
                    {
                        e.Cancel = true;
                        return;
                    }

                    string tempPath = nodesStack.Pop();// 顶层目录出栈
                    pathList.Add(tempPath);// 记录出栈目录
                    emptyBack.ReportProgress(50, tempPath);// 进度

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

        /// <summary>
        /// 后台进度
        /// </summary>
        private void emptyBack_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = (e.ProgressPercentage < 101) ? e.ProgressPercentage : progressBar.Value;
            progressLabel.Text = progressBar.Value.ToString() + "% 正在扫描：" + e.UserState as string;
        }

        /// <summary>
        /// 后台完成
        /// </summary>
        private void emptyBack_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("扫描文件错误如下\r\n" + e.Error.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressLabel.Text = "扫描文件后台错误";
                return;
            }
            
            if (e.Cancelled)
            {
                MessageBox.Show("扫描已取消", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                progressLabel.Text = "扫描已取消";
                return;
            }
            
            scanButton.Text = "扫描";
            progressLabel.Text = "扫描已完成";
            progressBar.Value = 100;
            
            List<string> list = e.Result as List<string>;// 接收传出
            if (list == null)
            {
                progressLabel.Text = "扫描错误";
                return;
            }
            if (list.Count < 1)
            {
                MessageBox.Show("没有发现空白目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            
            foreach (string str in list)
            {
                ListViewItem iistViewItem = listView.Items.Add(str);
                listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
                listView.Items[listView.Items.Count - 1].EnsureVisible();
            }
        }
        
    }
}
