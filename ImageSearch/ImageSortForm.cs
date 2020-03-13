using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ImageSearch
{
    /// <summary>
    /// 本地图片整理窗口
    /// </summary>
    public partial class ImageSortForm : Form
    {
        public ImageSortForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗口载入时
        /// </summary>
        private void ImageSortForm_Load(object sender, EventArgs e)
        {
            LotrListSettings();
            if (ApiFunction.GetDepotList() != null) depotListCombobox.DataSource = ApiFunction.GetDepotList();

            try
            {
                Icon = new Icon(Path.Combine(Application.StartupPath, @"Skin\Sort.ico"));
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
        /// 日志列表样式
        /// </summary>
        private void LotrListSettings()
        {
            sortListView.Columns.Add("文件");
            sortListView.Columns.Add("归类结果");
            sortListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
        }

        /// <summary>
        /// 归类按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sortButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(sourcePathTextBox.Text))
            {
                MessageBox.Show("“准备归类的文件位置”错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (sortBack.IsBusy)
            {
                MessageBox.Show("请先停止归类", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (api != null)
            {
                if (!File.Exists(@"Sort\" + api.Table + ".cs"))
                {
                    MessageBox.Show(@"与图库关联的归类器 Sort\" + api.Table + ".cs 不存在\r\n此图库将不支持匹配查找和文件归类", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                var back = new object[4];// 装箱
                back[0] = sourcePathTextBox.Text;
                back[1] = api;
                back[2] = sortSubCheckBox.Checked;
                back[3] = sortHoldOldCheckBox.Checked;
                sortBack.RunWorkerAsync(back);
                progressBar.Value = 1;
                progressLabel.Text = "% 开始整理...";
            }
        }

        /// <summary>
        /// 异步归类后台开始
        /// </summary>
        private void sortBack_DoWork(object sender, DoWorkEventArgs e)
        {
            sortBack.ReportProgress(0, "正在扫描文件");// 进度

            #region 拆箱
            var back = e.Argument as object[];
            string sourcPath = (string)back[0];// 来源目录
            Api api = (Api)back[1];
            bool sub = (bool)back[2];
            bool holdOld = (bool)back[3];
            #endregion 拆箱

            #region 获取文件列表

            List<string> list = new List<string>();// 返回的文件列表
            Stack<string> stack = new Stack<string>(20);// 栈
            stack.Push(sourcPath);// 主目录入栈
            while (stack.Count > 0)// 栈不为空时遍历
            {
                if (sortBack.CancellationPending)// 检测取消
                {
                    e.Cancel = true;
                    return;
                }

                string mainPath = stack.Pop();// 取栈中第一个目录

                string[] subPaths = null;
                try { subPaths = Directory.GetDirectories(mainPath); }// 栈目录的子目录列表
                #region 异常
                catch (UnauthorizedAccessException ex)
                {
                    if (MessageBox.Show("无权限操作，请尝试使用管理员权限运行本程序，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                catch (FileNotFoundException ex)
                {
                    if (MessageBox.Show("文件或文件夹不存在，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续归类？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show("发生未知如下错误\r\n\r\n" + ex + "\r\n\r\n是否继续归类？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                #endregion 异常

                string[] files = null;
                try { files = Directory.GetFiles(mainPath); }// 栈目录的文件列表
                #region 异常
                catch (UnauthorizedAccessException ex)
                {
                    if (MessageBox.Show("无权限操作，请尝试使用管理员权限运行本程序，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                catch (FileNotFoundException ex)
                {
                    if (MessageBox.Show("文件或文件夹不存在，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续归类？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show("发生未知如下错误\r\n\r\n" + ex + "\r\n\r\n是否继续归类？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                #endregion 异常

                if (files.Length > 0) foreach (string file in files)
                    {
                        list.Add(file);// 栈目录的文件遍历到List
                        sortBack.ReportProgress(1, "发现文件" + file);// 进度日志
                    }
                if (sortSubCheckBox.Checked) if (subPaths.Length > 0) foreach (string subPath in subPaths) stack.Push(subPath);// 如果包含子目录，栈目录的子目录列表入栈 
            }
            #endregion 获取文件列表

            #region 元素声明
            string otherPath = api.Path + @"Other\";// 目标其他文件目录
            string oldName;// 源文件名含扩展名，不含路径
            string newName;// 编译器传回的临时文件名
            string newFullname;// 目标文件名含路径
            int count = 0;// 进度计算
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("文件", Type.GetType("System.String"));
            dataTable.Columns.Add("归类结果", Type.GetType("System.String"));
            #endregion 元素声明
            
            MethodInfo function = new Reflections().Compiler(api.Table);// 调用实时编译
            if (function == null) return;
            foreach (string oldFullname in list)// 源文件名含路径old_fullname
            {
                try
                {
                    if (sortBack.CancellationPending)// 检测取消
                    {
                        e.Cancel = true;
                        return;
                    }

                    count++;
                    sortBack.ReportProgress(Percents.Get(count, list.Count), "发现文件" + oldFullname);// 进度日志

                    oldName = Path.GetFileName(oldFullname);// 文件名含路径
                    newName = (string)function.Invoke(null, new object[] { oldName });// 调用实时编译函数获取文件新目录

                    if (oldName.ToLower() == newName.ToLower()) newFullname = Path.Combine(otherPath, string.Format("{0:yyyyMM}", new FileInfo(oldFullname).LastWriteTime), oldName);// 非订单：其他\文件创建年月\文件名
                    else newFullname = Path.Combine(api.Path, newName);// 是订单，生成新位置

                    if (oldFullname.ToLower() == newFullname.ToLower()) continue;// 扫描到文件本身时跳过

                    if (!File.Exists(newFullname))// 如果新文件不存在，执行复制
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(newFullname))) Directory.CreateDirectory(Path.GetDirectoryName(newFullname));// 建立目录
                        File.Copy(oldFullname, newFullname);// 复制
                        if (holdOld) WriteLog(dataTable, newFullname, "从 " + Path.GetDirectoryName(oldFullname) + " 复制到 " + Path.GetDirectoryName(newFullname));// 不保留原文件时
                        else
                        {
                            File.Delete(oldFullname);
                            WriteLog(dataTable, oldFullname, "从 " + Path.GetDirectoryName(oldFullname) + " 移动到 " + Path.GetDirectoryName(newFullname));
                        }
                    }
                    else if (DateTime.Compare(new FileInfo(oldFullname).LastWriteTime, new FileInfo(newFullname).LastWriteTime) == 0)// 新文件已存在//创建时间相同
                    {
                        if (holdOld) WriteLog(dataTable, newFullname, "文件 " + newFullname + " 已存在，跳过归类");// 不保留原文件时
                        else
                        {
                            File.Delete(oldFullname);
                            WriteLog(dataTable, oldFullname, "已删除" + oldFullname);
                        }
                    }
                    else// 新文件已存在// 创建时间不同
                    {
                        newFullname = GetNewPathForDupes(newFullname);// 增加版本后缀
                        if (!Directory.Exists(Path.GetDirectoryName(newFullname))) Directory.CreateDirectory(Path.GetDirectoryName(newFullname));// 建立目录
                        File.Copy(oldFullname, newFullname);// 复制
                        if (holdOld) WriteLog(dataTable, oldFullname, "从 " + Path.GetDirectoryName(oldFullname) + " 复制到 " + Path.GetDirectoryName(newFullname));// 不保留原文件时
                        else
                        {
                            File.Delete(oldFullname);
                            WriteLog(dataTable, oldFullname, "从 " + Path.GetDirectoryName(oldFullname) + " 移动到 " + Path.GetDirectoryName(newFullname));
                        }
                    }
                }
                #region 异常
                catch (UnauthorizedAccessException ex)
                {
                    if (MessageBox.Show("无权限操作，请尝试使用管理员权限运行本程序，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK) continue;
                    else e.Cancel = true;
                }
                catch (FileNotFoundException ex)
                {
                    if (MessageBox.Show("文件或文件夹不存在，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续归类？", "提示", MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK) continue;
                    else e.Cancel = true;
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show("发生未知如下错误\r\n\r\n" + ex + "\r\n\r\n是否继续归类？", "提示", MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK) continue;
                    else e.Cancel = true;
                }
                #endregion 异常
            }

            e.Result = dataTable;// 传出
        }

        /// <summary>
        /// 异步归类后台进度
        /// </summary>
        private void sortBack_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = (e.ProgressPercentage < 101) ? e.ProgressPercentage : progressBar.Value;
            progressLabel.Text = progressBar.Value.ToString() + "% " + e.UserState as string;
        }

        /// <summary>
        /// 异步归类后台完成
        /// </summary>
        private void sortBack_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("归类后台错误如下\r\n\r\n" + e.Error.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar.Value = 0;
                return;
            }

            if (e.Cancelled)
            {
                MessageBox.Show("归类已取消", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                progressBar.Value = 0;
                return;
            }

            DataTable datatable = e.Result as DataTable;// 传出

            if (datatable == null)// 空数据
            {
                progressBar.Value = 100;
                MessageBox.Show("归类失败，返回了空结果", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressLabel.Text = "归类失败，返回了空结果";
                return;
            }

            if (datatable.Rows.Count == 0)// 0结果
            {
                progressBar.Value = 100;
                MessageBox.Show("没有需要归类的文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                progressLabel.Text = "没有需要归类的文件";
                return;
            }

            sortListView.BeginUpdate();// 挂起UI，避免闪烁并提速
            ListTable.ToView(datatable, sortListView);
            sortListView.EndUpdate();// 绘制UI。
            sortListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
            sortListView.Items[sortListView.Items.Count - 1].EnsureVisible();// 定位尾部
            progressLabel.Text = "完成";
            progressBar.Value = 100;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="datatable">日志DataTable</param>
        /// <param name="fullname">文件名</param>
        /// <param name="msg">归类结果</param>
        private void WriteLog(DataTable datatable, string fullname, string msg)
        {
            DataRow datarow;
            datarow = datatable.NewRow();
            datarow["文件"] = Path.GetFileName(fullname);
            datarow["归类结果"] = msg;
            datatable.Rows.Add(datarow);
        }

        /// <summary>
        /// 文件名增加版本（_x）后缀
        /// </summary>
        /// <param name="name">传入文件名</param>
        /// <returns>传出文件名</returns>
        private string GetNewPathForDupes(string name)
        {
            string directory = Path.GetDirectoryName(name);// 目录
            string fileName = Path.GetFileNameWithoutExtension(name);// 文件名
            string extension = Path.GetExtension(name);// 扩展名
            int counter = 1;
            string newFullName;
            do
            {
                string newFilename = fileName + "_" + counter.ToString() + extension;
                newFullName = Path.Combine(directory, newFilename);
                counter++;
            } while (File.Exists(newFullName));
            return newFullName;
        }

        /// <summary>
        /// 开始位置浏览按钮
        /// </summary>
        private void sourcePathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog vcetor_source_folder = new FolderBrowserDialog();
            if (vcetor_source_folder.ShowDialog() == DialogResult.OK) sourcePathTextBox.Text =vcetor_source_folder.SelectedPath;
        }

        /// <summary>
        /// 取消归类按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (sortBack.IsBusy) sortBack.CancelAsync();
            else MessageBox.Show("未开始归类图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        /// <summary>
        /// API实例
        /// </summary>
        Api api;
        /// <summary>
        /// 图库选择时
        /// </summary>
        private void depotListCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            api = ApiFunction.GetApi(depotListCombobox.Text);
            sourcePathTextBox.Text = api.SortPath;
        }

        /// <summary>
        /// 窗口关闭检测
        /// </summary>
        private void ImageSortForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sortBack.IsBusy)
            {
                e.Cancel = true;
                MessageBox.Show("后台正在归类，请勿关闭窗口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}
