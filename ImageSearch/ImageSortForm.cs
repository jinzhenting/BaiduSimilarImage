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
    public partial class ImageSortForm : Form
    {
        public ImageSortForm()
        {
            InitializeComponent();
        }

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

        private void LotrListSettings()// 日志列表样式
        {
            sort_listview.Columns.Add("文件");
            sort_listview.Columns.Add("归类结果");
            sort_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
        }

        private void vector_imageSortButton_Click(object sender, EventArgs e)// 订单归类按钮
        {
            if (!Directory.Exists(sort_in_path_textbox.Text))
            {
                MessageBox.Show("“准备归类的文件位置”错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (sort_background.IsBusy)
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
                back[0] = sort_in_path_textbox.Text;
                back[1] = api;
                back[2] = sort_subCheckbox.Checked;
                back[3] = sort_holdold_checkbox.Checked;
                sort_background.RunWorkerAsync(back);
                searchBar.Value = 1;
                progressLabel.Text = "% 开始整理...";
            }
        }

        private void sort_background_DoWork(object sender, DoWorkEventArgs e)// 异步工作
        {
            sort_background.ReportProgress(0, "正在扫描文件");// 进度

            #region 拆箱
            var back = e.Argument as object[];
            string source_path = (Regex.IsMatch((string)back[0], @"[\\]$")) ? (string)back[0] : (string)back[0] + @"\";// 来源目录
            Api api = (Api)back[1];
            bool sub = (bool)back[2];
            bool hold_old = (bool)back[3];
            #endregion 拆箱

            #region 获取文件列表

            List<string> list = new List<string>();// 返回的文件列表
            Stack<string> stack = new Stack<string>(20);// 栈
            stack.Push(source_path);// 主目录入栈
            while (stack.Count > 0)// 栈不为空时遍历
            {
                if (sort_background.CancellationPending)// 检测取消
                {
                    e.Cancel = true;
                    return;
                }

                string main_path = stack.Pop();// 取栈中第一个目录

                string[] sub_paths = null;
                try { sub_paths = Directory.GetDirectories(main_path); }// 栈目录的子目录列表
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
                try { files = Directory.GetFiles(main_path); }// 栈目录的文件列表
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
                        sort_background.ReportProgress(1, "发现文件" + file);// 进度日志
                    }
                if (sort_subCheckbox.Checked) if (sub_paths.Length > 0) foreach (string sub_path in sub_paths) stack.Push(sub_path);// 如果包含子目录，栈目录的子目录列表入栈 
            }
            #endregion 获取文件列表

            #region 元素声明
            string other_path = api.Path + @"Other\";// 目标其他文件目录
            string old_name;// 源文件名含扩展名，不含路径
            string new_name;// 编译器传回的临时文件名
            string new_fullname;// 目标文件名含路径
            int count = 0;// 进度计算
            DataTable datatable = new DataTable();
            datatable.Columns.Add("文件", Type.GetType("System.String"));
            datatable.Columns.Add("归类结果", Type.GetType("System.String"));
            #endregion 元素声明
            
            MethodInfo function = new Reflections().Compiler(api.Table);// 调用实时编译
            if (function == null) return;
            foreach (string old_fullname in list)// 源文件名含路径old_fullname
            {
                try
                {
                    if (sort_background.CancellationPending)// 检测取消
                    {
                        e.Cancel = true;
                        return;
                    }

                    count++;
                    sort_background.ReportProgress(Percents.Get(count, list.Count), "发现文件" + old_fullname);// 进度日志

                    old_name = Path.GetFileName(old_fullname);// 文件名含路径
                    new_name = (string)function.Invoke(null, new object[] { old_name });// 调用实时编译函数获取文件新目录
                    
                    if (old_name.ToLower() == new_name.ToLower()) new_fullname = other_path + string.Format("{0:yyyyMM}", new FileInfo(old_fullname).LastWriteTime) + @"\" + old_name;// 非订单：其他\文件创建年月\文件名
                    else new_fullname = api.Path + new_name;// 是订单，生成新位置
                    
                    if (old_fullname.ToLower() == new_fullname.ToLower()) continue;// 扫描到文件本身时跳过

                    if (!File.Exists(new_fullname))// 如果新文件不存在，执行复制
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(new_fullname))) Directory.CreateDirectory(Path.GetDirectoryName(new_fullname));// 建立目录
                        File.Copy(old_fullname, new_fullname);// 复制
                        if (hold_old) DataTableAddRows(datatable, new_fullname, "从 " + Path.GetDirectoryName(old_fullname) + " 复制到 " + Path.GetDirectoryName(new_fullname));// 不保留原文件时
                        else
                        {
                            File.Delete(old_fullname);
                            DataTableAddRows(datatable, old_fullname, "从 " + Path.GetDirectoryName(old_fullname) + " 移动到 " + Path.GetDirectoryName(new_fullname));
                        }
                    }
                    else if (DateTime.Compare(new FileInfo(old_fullname).LastWriteTime, new FileInfo(new_fullname).LastWriteTime) == 0)// 新文件已存在//创建时间相同
                    {
                        if (hold_old) DataTableAddRows(datatable, new_fullname, "文件 " + new_fullname + " 已存在，跳过归类");// 不保留原文件时
                        else
                        {
                            File.Delete(old_fullname);
                            DataTableAddRows(datatable, old_fullname, "已删除" + old_fullname);
                        }
                    }
                    else// 新文件已存在// 创建时间不同
                    {
                        new_fullname = GetNewPathForDupes(new_fullname);// 增加版本后缀
                        if (!Directory.Exists(Path.GetDirectoryName(new_fullname))) Directory.CreateDirectory(Path.GetDirectoryName(new_fullname));// 建立目录
                        File.Copy(old_fullname, new_fullname);// 复制
                        if (hold_old) DataTableAddRows(datatable, old_fullname, "从 " + Path.GetDirectoryName(old_fullname) + " 复制到 " + Path.GetDirectoryName(new_fullname));// 不保留原文件时
                        else
                        {
                            File.Delete(old_fullname);
                            DataTableAddRows(datatable, old_fullname, "从 " + Path.GetDirectoryName(old_fullname) + " 移动到 " + Path.GetDirectoryName(new_fullname));
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

            e.Result = datatable;// 传出
        }

        private void DataTableAddRows(DataTable datatable, string fullname, string msg)// 写日志
        {
            DataRow datarow;
            datarow = datatable.NewRow();
            datarow["文件"] = Path.GetFileName(fullname);
            datarow["归类结果"] = msg;
            datatable.Rows.Add(datarow);
        }

        private string GetNewPathForDupes(string fullname)// 重命名
        {
            string directory = Path.GetDirectoryName(fullname);// 目录
            string filename = Path.GetFileNameWithoutExtension(fullname);// 文件名
            string extension = Path.GetExtension(fullname);// 扩展名
            int counter = 1;
            string new_fullnameh;
            do
            {
                string newFilename = filename + "_" + counter.ToString() + extension;
                new_fullnameh = Path.Combine(directory, newFilename);
                counter++;
            } while (File.Exists(new_fullnameh));
            return new_fullnameh;
        }

        private void sort_background_ProgressChanged(object sender, ProgressChangedEventArgs e)// 异步进度
        {
            searchBar.Value = (e.ProgressPercentage < 101) ? e.ProgressPercentage : searchBar.Value;
            progressLabel.Text = searchBar.Value.ToString() + "% " + e.UserState as string;
        }

        private void sort_background_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)// 异步完成
        {
            if (e.Error != null)
            {
                MessageBox.Show("归类后台错误如下\r\n\r\n" + e.Error.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                searchBar.Value = 0;
                return;
            }

            if (e.Cancelled)
            {
                MessageBox.Show("归类已取消", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                searchBar.Value = 0;
                return;
            }

            DataTable datatable = e.Result as DataTable;// 传出

            if (datatable == null)// 空数据
            {
                searchBar.Value = 100;
                MessageBox.Show("归类失败，返回了空结果", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressLabel.Text = "归类失败，返回了空结果";
                return;
            }

            if (datatable.Rows.Count == 0)// 0结果
            {
                searchBar.Value = 100;
                MessageBox.Show("没有需要归类的文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                progressLabel.Text = "没有需要归类的文件";
                return;
            }

            sort_listview.BeginUpdate();// 挂起UI，避免闪烁并提速
            ListTable.ToView(datatable, sort_listview);
            sort_listview.EndUpdate();// 绘制UI。
            sort_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
            sort_listview.Items[sort_listview.Items.Count - 1].EnsureVisible();// 定位尾部
            progressLabel.Text = "完成";
            searchBar.Value = 100;
        }
        
        private void vcetor_source_path_button_Click(object sender, EventArgs e)// 矢量开始位置浏览按钮
        {
            FolderBrowserDialog vcetor_source_folder = new FolderBrowserDialog();
            if (vcetor_source_folder.ShowDialog() == DialogResult.OK) sort_in_path_textbox.Text = (Regex.IsMatch(vcetor_source_folder.SelectedPath, @"[\\]$")) ? vcetor_source_folder.SelectedPath : vcetor_source_folder.SelectedPath + @"\";
        }
        
        private void vector_cancel_button_Click(object sender, EventArgs e)// 矢量取消归类按钮
        {
            if (sort_background.IsBusy) sort_background.CancelAsync();
            else MessageBox.Show("未开始归类图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        Api api;
        private void depotListCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            api = ApiFunction.GetApi(depotListCombobox.Text);
            sort_in_path_textbox.Text = api.SortPath;
        }

        private void ImageSortForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sort_background.IsBusy)
            {
                e.Cancel = true;
                MessageBox.Show("后台正在归类，请勿关闭窗口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}
