using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageSearch
{
    public partial class ImageUpForm : Form
    {
        public ImageUpForm(){InitializeComponent(); }

        private void FormatListLoad()
        {
            if (ApiFunction.GetFormatList() != null) format_combobox.DataSource = ApiFunction.GetFormatList();// 格式列表载入
        }

        private void ImageUpForm_Load(object sender, EventArgs e)// 窗口载入时
        {
            if (ApiFunction.GetDepotList() != null) depot_list_combobox.DataSource = ApiFunction.GetDepotList();// 图库下拉列表
            FormatListLoad();
            LogListSettings();
            ScanListSettings();

            try
            {
                Icon = new Icon(Path.Combine(Application.StartupPath, @"Skin\Up.ico"));
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

        #region 日志
        private void LogListSettings()// 日志列表样式
        {
            add_log_listview.Columns.Add("序号");// 添加列标题
            add_log_listview.Columns.Add("图片");
            add_log_listview.Columns.Add("入库时间");
            add_log_listview.Columns.Add("入库结果");
            add_log_listview.Columns.Add("描述");
            add_log_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
        }
        
        private void LogWrite(ImageUpLog log)// 写日志
        {
            add_log_listview.BeginUpdate();// 挂起UI，避免闪烁并提速
            ListViewItem item = add_log_listview.Items.Add(log.ID);// 序号
            item.SubItems.Add(log.Nname);// 图片
            item.SubItems.Add(log.Time);// 入库时间
            item.SubItems.Add(log.Result);// 入库结果
            item.SubItems.Add(log.Message);// 描述
            add_log_listview.EndUpdate();// 绘制UI
            add_log_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
            add_log_listview.Items[add_log_listview.Items.Count - 1].EnsureVisible();// 定位尾部
        }

        private void reset_log_button_Click(object sender, EventArgs e)// 清空日志按钮
        {
            if (add_background.IsBusy)
            {
                MessageBox.Show("请先停止入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            add_log_listview.Clear();
            LogListSettings();
        }
        #endregion 日志

        private Api api;
        private void depot_list_combobox_SelectedIndexChanged(object sender, EventArgs e)// 图库列表选择动作
        {
            sub_checkbox.Checked = false;// 复原选项
            api = ApiFunction.GetApi(depot_list_combobox.Text);// 获取API元素
            if (api != null) add_path_textbox.Text = api.Path;
            if (scan_listview.Items.Count > 0)// 清空列表
            {
                scan_listview.Clear();
                ScanListSettings();
            }
        }

        private void reset_add_image_list_button_Click(object sender, EventArgs e)// 清空图片列表按钮
        {
            if (add_background.IsBusy)
            {
                MessageBox.Show("请先停止入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            scan_listview.Clear();
            ScanListSettings();
        }

        #region 扫描
        private List<string> sub_list = new List<string>();// 包含子目录选项列表
        private void sub_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!sub_checkbox.Checked) return;// 未勾选

            if (sub_list.Count > 0) sub_list.Clear();// 清空子目录选项列表

            if (sub_checkbox.Checked && depot_list_combobox.Text == "")
            {
                MessageBox.Show("未选择图库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (sub_list.Count > 0) sub_list.Clear();
                sub_checkbox.Checked = false;
                return;
            }

            if (sub_checkbox.Checked && add_path_textbox.Text == "")
            {
                MessageBox.Show("未选择图片目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (sub_list.Count > 0) sub_list.Clear();
                sub_checkbox.Checked = false;
                return;
            }

            if (sub_checkbox.Checked && !Directory.Exists(add_path_textbox.Text))
            {
                MessageBox.Show("图片目录不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (sub_list.Count > 0) sub_list.Clear();
                sub_checkbox.Checked = false;
                return;
            }

            ImageUpSubForm subform = new ImageUpSubForm();
            subform.InPath = api.Path;// 主目录
            subform.ShowDialog();
            if (subform.DialogResult == DialogResult.OK)// 返回确定
            {
                if (subform.List.Count < 1)// 返回无选中项
                {
                    sub_checkbox.Checked = false;
                    if (sub_list.Count > 0) sub_list.Clear();
                    return;
                }
                sub_list = subform.List;// 获取列表
                return;
            }
            else//返回取消
            {
                sub_checkbox.Checked = false;
                if (sub_list.Count > 0) sub_list.Clear();
                return;
            }
        }

        private void sql_checkbox_CheckedChanged(object sender, EventArgs e)// 匹配数据库选项
        {
            if (sql_checkbox.Checked)
            {
                if (MessageBox.Show("如果文件数量大，匹配数据库将十分缓慢，并加大服务器负担。\r\n是否继续勾选", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) sql_checkbox.Checked = false;
            }
        }

        private void ScanListSettings()// 扫描列表样式
        {
            scan_listview.Columns.Add("序号");// 添加列标题
            scan_listview.Columns.Add("图片");
            scan_listview.Columns.Add("位置");
            scan_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
        }

        private void add_scan_button_Click(object sender, EventArgs e)// 扫描按钮
        {
            if (add_scan_button.Text == "扫描图片")
            {
                if (scan_background.IsBusy)
                {
                    MessageBox.Show("请先停止扫描", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (add_background.IsBusy)
                {
                    MessageBox.Show("请先停止入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (depot_list_combobox.Text == "")
                {
                    MessageBox.Show("未选择图库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (format_combobox.Text == "")
                {
                    MessageBox.Show("未选择格式", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (add_path_textbox.Text == "")
                {
                    MessageBox.Show("未选择图片目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!Directory.Exists(add_path_textbox.Text))
                {
                    MessageBox.Show("图片目录不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (scan_listview.Items.Count > 0)// 清空列表
                {
                    scan_listview.Clear();
                    ScanListSettings();
                }

                var scan = new object[4];// 装箱
                scan[0] = format_combobox.Text;
                scan[1] = sub_list;
                scan[2] = sql_checkbox.Checked;
                scan[3] = depot_list_combobox.Text;
                scan_background.RunWorkerAsync(scan);
                add_scan_button.Text = "停止扫描";
                add_bar.Value = 1;
                add_bar_label.Text = "% 开始扫描...";
            }
            else
            {
                if (scan_background.IsBusy) scan_background.CancelAsync();
                add_scan_button.Text = "扫描图片";
            }
        }

        private void scan_background_DoWork(object sender, DoWorkEventArgs e)// 异步扫描工作
        {
            var scan = e.Argument as object[];// 拆箱
            string extension = (string)scan[0];
            List<string> sub_list = (List<string>)scan[1];
            bool sql_check = (bool)scan[2];
            string depot_name = (string)scan[3];
            Api api = ApiFunction.GetApi(depot_name);
            if (api == null) return;

            DataTable datatable = new DataTable();// 表
            //datatable.Columns.Add("ID", Type.GetType("System.Int32"));// datatable.Columns[0].AutoIncrement = true;// datatable.Columns[0].AutoIncrementSeed = 1;// datatable.Columns[0].AutoIncrementStep = 1;// 自动序号
            datatable.Columns.Add("序号", Type.GetType("System.String"));
            datatable.Columns.Add("图片", Type.GetType("System.String"));
            datatable.Columns.Add("位置", Type.GetType("System.String"));
            
            int index = 0;// 序号
            
            DirectoryInfo directorys = new DirectoryInfo(api.Path);// 遍历文件夹
            FileInfo[] files = directorys.GetFiles(extension, SearchOption.TopDirectoryOnly);
            int count = 0;// 文件数
            foreach (FileInfo file in files)
            {
                if (scan_background.CancellationPending)// 取消检测
                {
                    e.Cancel = true;
                    return;
                }
                
                if (sql_check)// 是否匹配数据库
                {
                    DataTable datatable1 = Sql.Select(depot_name, "SELECT Names, Path, Result, Message FROM " + api.Table + " WHERE Names='" + file.Name.Replace("'", "''") + "' AND Path='" + file.FullName.Replace(api.Path, "").Replace("'", "''") + "'");// 数据库查询
                    if (datatable1 == null)
                    {
                        MessageBox.Show("数据库操作错误，结束扫描", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (datatable1.Rows.Count > 0) if (datatable1.Rows[0]["Result"].ToString() == "End" || datatable1.Rows[0]["Result"].ToString() == "Ignore") continue;// 如果查询到记录将跳过
                }
                
                index++;// 序号计数
                DataRow datarow;
                datarow = datatable.NewRow();
                datarow["序号"] = index.ToString();
                datarow["图片"] = file.Name;
                datarow["位置"] = file.FullName;
                datatable.Rows.Add(datarow);
                count++;// 文件计数
                scan_background.ReportProgress(Percents.Get(count, files.Length), file.FullName);// 进度传出
            }

            if (sub_list.Count < 1) e.Result = datatable;// 没有选中子文件夹//结果传出
            else
            {
                foreach (string str in sub_list)// 遍历子文件夹
                {
                    if (scan_background.CancellationPending)// 取消检测
                    {
                        e.Cancel = true;
                        return;
                    }

                    DirectoryInfo directory_all = new DirectoryInfo(str);
                    int count_all = 0;// 文件数
                    try//访问权限捕捉
                    {
                        FileInfo[] files_all = directory_all.GetFiles(extension, SearchOption.AllDirectories);
                        foreach (FileInfo file_all in files_all)
                        {
                            if (scan_background.CancellationPending)// 取消检测
                            {
                                e.Cancel = true;
                                return;
                            }

                            if (sql_check)// 是否匹配数据库
                            {
                                DataTable datatable2 = Sql.Select(depot_name, "SELECT Names, Path, Result, Message FROM " + api.Table + " WHERE Names='" + file_all.Name.Replace("'", "''") + "' AND Path='" + file_all.FullName.Replace(api.Path, "").Replace("'", "''") + "'");// 获取数据库配置
                                if (datatable2 == null)
                                {
                                    MessageBox.Show("数据库操作错误，结束扫描", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (datatable2.Rows.Count > 0 && datatable2.Rows[0]["Result"].ToString() != "Local") continue;// 如果查询到记录，且 Result != Local 时跳过
                            }

                            index++;// 序号计数
                            DataRow datarow;
                            datarow = datatable.NewRow();
                            datarow["序号"] = index.ToString();
                            datarow["图片"] = file_all.Name;
                            datarow["位置"] = file_all.FullName;
                            datatable.Rows.Add(datarow);
                            count_all++;// 文件计数
                            scan_background.ReportProgress(Percents.Get(count_all, files_all.Length), file_all.FullName);// 进度传出
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        if (MessageBox.Show("无权限访问：" + directory_all.FullName + "请尝试使用管理员权限运行本程序，是否继续扫描？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                        else return;
                    }
                }
            }

            e.Result = datatable;// 结果传出
        }

        private void scan_background_ProgressChanged(object sender, ProgressChangedEventArgs e)// 异步扫描进度
        {
            add_bar.Value = (e.ProgressPercentage < 101) ? e.ProgressPercentage : add_bar.Value;
            add_bar_label.Text = add_bar.Value.ToString() + "% 发现文件" + e.UserState as string;
        }
        
        private void scan_background_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)// 异步扫描完成
        {
            add_scan_button.Text = "扫描图片";

            if (e.Error != null)
            {
                MessageBox.Show("扫描文件错误如下\r\n\r\n" + e.Error.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                add_bar_label.Text = "扫描文件后台错误";
                return;
            }

            if (e.Cancelled)
            {
                MessageBox.Show("扫描已取消", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                add_bar_label.Text = "扫描已取消";
                return;
            }

            DataTable datatable = e.Result as DataTable;// 接收传出
            
            if (datatable == null)// 空数据
            {
                add_bar.Value = 100;
                MessageBox.Show("扫描失败，返回了空结果", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                add_bar_label.Text = "扫描失败，返回了空结果";
                return;
            }
            
            if (datatable.Rows.Count == 0)// 0结果
            {
                add_bar.Value = 100;
                MessageBox.Show("没有扫描到文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                add_bar_label.Text = "没有扫描到文件";
                return;
            }
            
            scan_listview.BeginUpdate();// 挂起UI，避免闪烁并提速
            ListTable.ToView(datatable, scan_listview);// 数据转换
            scan_listview.EndUpdate();// 绘制UI
            scan_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
            scan_listview.Items[scan_listview.Items.Count - 1].EnsureVisible();// 定位尾部
            
            add_bar.Value = 100;
            add_bar_label.Text = "完成扫描";
        }
        #endregion 扫描

        #region 入库
        private void add_start_button_Click(object sender, EventArgs e)// 入库按钮
        {
            if (add_start_button.Text == "开始入库")
            {
                if (add_background.IsBusy)
                {
                    MessageBox.Show("请先停止入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                if (scan_background.IsBusy)
                {
                    MessageBox.Show("请先停止扫描", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                if (depot_list_combobox.Text == "")
                {
                    MessageBox.Show("未选择图库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (format_combobox.Text == "")
                {
                    MessageBox.Show("未选择图片格式", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (scan_listview.Items.Count < 1)
                {
                    MessageBox.Show("请先扫描图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                add_log_listview.Clear();// 清空日志
                LogListSettings();

                var add = new object[2];// 装箱
                DataTable datatable = new DataTable();
                ListTable.ToTable(scan_listview, datatable);
                add[0] = datatable;
                add[1] = depot_list_combobox.Text;
                add_background.RunWorkerAsync(add);
                add_start_button.Text = "停止入库";
                add_bar.Value = 1;
                add_bar_label.Text = "% 开始入库...";
            }
            else
            {
                if (add_background.IsBusy)add_background.CancelAsync();
                add_start_button.Text = "开始入库";
            }
        }

        private void add_background_DoWork(object sender, DoWorkEventArgs e)// 异步入库工作
        {
            #region 拆箱
            var scan = e.Argument as object[];
            DataTable datatable = (DataTable)scan[0];
            string depot_name = (string)scan[1];
            Api api = ApiFunction.GetApi(depot_name);
            if (api == null) return;
            #endregion 拆箱

            for (int i = 0; i < datatable.Rows.Count; i++)// 遍历
            {
                if (add_background.CancellationPending) { e.Cancel = true; return; }//取消检测

                #region 定义元素
                string name = datatable.Rows[i]["图片"].ToString();// 文件名
                string fullpath = datatable.Rows[i]["位置"].ToString();// 文件路径
                string uppath = fullpath.Replace(api.Path, "");// 记录时清除主目录，后面SQL转义
                ImageUpLog log = new ImageUpLog();// 日志
                log.ID = (i + 1).ToString();
                log.Nname = fullpath;
                log.Time = DateTime.Now.ToString();
                #endregion 定义元素

                DataTable select_datatable = Sql.Select(depot_name, @"SELECT Names, Path, Result, Message, Times FROM " + api.Table + " WHERE Names='" + name.Replace("'", "''") + "' AND Path='" + uppath.Replace("'", "''") + "'");// 数据库查询
                if (select_datatable == null)
                {
                    MessageBox.Show("数据库操作错误，结束入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #region 多条记录异常
                if (select_datatable.Rows.Count > 1)// 多条记录异常
                {
                    MessageBox.Show("图片" + select_datatable.Rows[0]["Names"].ToString() + "存在" + select_datatable.Rows.Count.ToString() + "条数据记录，请先处理此异常才能继续入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion 多条记录异常

                #region 返回单条记录
                if (select_datatable.Rows.Count > 0)// 已有1条记录
                {
                    string result = select_datatable.Rows[0]["Result"].ToString();// 检查完成状态

                    if (result == "End")// 已入库
                    {
                        log.Result = "跳过";
                        log.Message = ApiFunction.GetError("216681");
                        add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);// 日志
                        continue;
                    }

                    else if (result == "Ignore")// 记录忽略
                    {
                        log.Result = "跳过";
                        log.Message = ApiFunction.GetError(select_datatable.Rows[0]["Message"].ToString().Replace("Error", ""));
                        add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);// 日志
                        continue;
                    }

                    else if (result == "Local")// 记录本地
                    {
                        Baidu.Aip.ImageSearch.ImageSearch client = ApiFunction.GetClient(api.Appid, api.Apikey, api.Secreykey, api.Timeout);
                        if (client == null)
                        {
                            if (MessageBox.Show("API配置错误，是否继续入库？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                            else
                            {
                                e.Cancel = true;
                                return;
                            }
                        }

                        JObject local_json = ApiFunction.Up(client, fullpath, fullpath.Replace(api.Path, ""), api.Tags1, api.Tags2);// 入库
                        if (local_json == null || local_json.ToString() == "")
                        {
                            if (MessageBox.Show("API连接错误，是否继续入库？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                            else
                            {
                                e.Cancel = true;
                                return;
                            }
                        }

                        if (local_json.Property("error_code") == null)// 入库成功
                        {
                            bool up = Sql.Up(depot_name, @"UPDATE " + api.Table + " SET LogID = '" + local_json["log_id"].ToString() + "', ContSign = '" + local_json["cont_sign"].ToString() + "', Tsgs1 = '" + api.Tags1 + "', Tsgs2 = '" + api.Tags2 + "', Result = 'End', Message = 'End', Times = '" + DateTime.Now.ToString() + "' WHERE Names = '" + select_datatable.Rows[0]["Names"].ToString().Replace("'", "''") + "'");
                            if (up)
                            {
                                log.Result = "完成";// 日志
                                log.Message = "已入库";
                                add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
                                continue;
                            }
                            else
                            {
                                MessageBox.Show("结束入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        else//入库错误，此情况是本地扫描后未入库，或者入库时出现API错误，先记录数据等待下一次入库
                        {
                            string error_code = local_json["error_code"].ToString();// 错误代码
                            if (local_json.Property("cont_sign") != null && local_json.Property("cont_sign").ToString() != "")// 错误含cont_sign即已入过库，此错误是本地扫描后没有入库，但之前已经入过库但未记录，现在进行重复入库造成，更新记录为End状态即可
                            {
                                bool up = Sql.Up(depot_name, @"UPDATE " + api.Table + " SET LogID = '" + local_json["log_id"].ToString() + "', ContSign = '" + local_json["cont_sign"].ToString() + "', Tsgs1 = '" + api.Tags1 + "', Tsgs2 = '" + api.Tags2 + "', Result = 'End', Message = 'End', Times = '" + DateTime.Now.ToString() + "' WHERE Names = '" + select_datatable.Rows[0]["Names"].ToString().Replace("'", "''") + "'");
                                if (up)
                                {
                                    log.Result = "错误";// 日志
                                    log.Message = ApiFunction.GetError(error_code);
                                    add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
                                    continue;
                                }
                                else
                                {
                                    MessageBox.Show("结束入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else//错误里没有log_id，但有数据记录，此情况是本地扫描后没有入库，现在入库返回了其他错误造成的，把记录更新为Error状态即可
                            {
                                string ignore = (ApiFunction.UpIgnore(error_code) == "Yes") ? "Ignore" : "Local";// 以后是否忽略入库
                                bool up = Sql.Up(depot_name, @"UPDATE " + api.Table + " SET Result = '" + ignore + "', Message = 'Error" + error_code + "', Times = '" + DateTime.Now.ToString() + "' WHERE Names = '" + select_datatable.Rows[0]["Names"].ToString().Replace("'", "''") + "'");
                                if (up)
                                {
                                    log.Result = "错误";// 日志
                                    log.Message = ApiFunction.GetError(error_code);
                                    add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
                                    continue;
                                }
                                MessageBox.Show("结束入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    else//Result列数据异常
                    {
                        MessageBox.Show("图片" + select_datatable.Rows[0]["Names"].ToString() + "的Result列数据" + select_datatable.Rows[0]["Result"].ToString() + "异常，请先处理再继续入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                #endregion 返回单条记录

                #region 无记录
                else
                {
                    Baidu.Aip.ImageSearch.ImageSearch client = ApiFunction.GetClient(api.Appid, api.Apikey, api.Secreykey, api.Timeout);
                    if (client == null)
                    {
                        if (MessageBox.Show("API配置错误，是否继续入库？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                        else
                        {
                            e.Cancel = true;
                            return;
                        }
                    }

                    JObject json = ApiFunction.Up(client, fullpath, fullpath.Replace(api.Path, ""), api.Tags1, api.Tags2);// 入库
                    if (json == null || json.ToString() == "")
                    {
                        if (MessageBox.Show("API连接错误，是否继续入库？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                        else
                        {
                            e.Cancel = true;
                            return;
                        }
                    }

                    if (json.Property("error_code") == null)// 入库成功
                    {
                        bool insert = Sql.Insert(depot_name, @"INSERT INTO " + api.Table + " (Names, Path, LogID, ContSign, Tsgs1, Tsgs2, Result, Message, Times) VALUES('" + name.Replace("'", "''") + "', '" + uppath.Replace("'", "''") + "', '" + json["log_id"].ToString() + "', '" + json["cont_sign"].ToString() + "', '" + api.Tags1 + "', '" + api.Tags2 + "', 'End', 'End','" + DateTime.Now.ToString() + "')");// 记录数据库
                        if (insert)
                        {
                            log.Result = "完成";// 日志
                            log.Message = "已入库，已录入数据库";
                            add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
                            continue;
                        }
                        else
                        {
                            MessageBox.Show("结束入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else//入库错误
                    {
                        string error_code = json["error_code"].ToString();// 错误代码
                        if (json.Property("cont_sign") != null && json.Property("cont_sign").ToString() != "")// 错误里包含cont_sign，即已入过库，此错误是重复入库造成
                        {
                            bool insert = Sql.Insert(depot_name, @"INSERT INTO " + api.Table + "(Names, Path, LogID, ContSign, Tsgs1, Tsgs2, Result, Message, Times) VALUES('" + name.Replace("'", "''") + "', '" + uppath.Replace("'", "''") + "', '" + json["log_id"].ToString() + "', '" + json["cont_sign"].ToString() + "', '" + api.Tags1 + "', '" + api.Tags2 + "', 'End', 'End','" + DateTime.Now.ToString() + "')");
                            if (insert)
                            {
                                log.Result = "错误";// 日志
                                log.Message = ApiFunction.GetError(error_code);
                                add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
                                continue;
                            }
                            else
                            {
                                MessageBox.Show("结束入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else//错误里没有log_id，且无数据记录，即其他错误执行错误结果记录
                        {
                            string ignore = (ApiFunction.UpIgnore(error_code) == "Yes") ? "Ignore" : "Local";// 以后是否忽略入库
                            bool insert = Sql.Insert(depot_name, @"INSERT INTO " + api.Table + "(Names, Path, Result, Message, Times) VALUES('" + name.Replace("'", "''") + "', '" + uppath.Replace("'", "''") + "', '" + ignore + "', 'Error" + error_code + "', '" + DateTime.Now.ToString() + "')");
                            if (insert)
                            {
                                log.Result = "错误";// 日志
                                log.Message = ApiFunction.GetError(error_code);
                                add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
                                continue;
                            }
                            else
                            {
                                MessageBox.Show("结束入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
                #endregion 无记录
            }
        }

        private void add_background_ProgressChanged(object sender, ProgressChangedEventArgs e)// 异步入库进度
        {
            add_bar.Value = e.ProgressPercentage;
            ImageUpLog log = e.UserState as ImageUpLog;// 传出解封
            add_bar_label.Text = add_bar.Value.ToString() + "% 正在入库 " + log.Nname;
            LogWrite(log);
        }
        
        private void add_background_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)// 异步入库完成
        {
            add_start_button.Text = "开始入库";
            if (e.Error != null)
            {
                MessageBox.Show("入库后台错误如下\r\n\r\n" + e.Error.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                add_bar_label.Text = "入库后台错误";
                return;
            }
            if (e.Cancelled)
            {
                MessageBox.Show("入库已取消", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                add_bar_label.Text = "入库已取消";
                return;
            }
            add_bar.Value = 100;
            add_bar_label.Text = "完成入库";
        }
        #endregion 入库
        
        private void ImageUpForm_FormClosing(object sender, FormClosingEventArgs e)// 窗口关闭检测
        {
            if (add_background.IsBusy)
            {
                e.Cancel = true;
                MessageBox.Show("后台正在入库，请勿关闭窗口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (scan_background.IsBusy)
            {
                e.Cancel = true;
                MessageBox.Show("后台正在扫描文件，请勿关闭窗口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }
}
