using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace ImageSearch
{
    public partial class ImageUpForm : Form
    {
        public ImageUpForm()
        {
            InitializeComponent();
        }

        #region 初始化
        private void FormatListLoad()//格式列表载入
        {
            format_combobox.DataSource = ApiFunction.GetFormatList();
        }

        private void ImageUpForm_Load(object sender, EventArgs e)//窗口载入时
        {
            depot_list_combobox.DataSource = ApiFunction.GetDepotList();//图库下拉列表
            FormatListLoad();
            LogListSettings();
            ScanListSettings();
        }
        #endregion 初始化

        #region 日志
        private void LogListSettings()//日志列表样式
        {
            add_log_listview.Columns.Add("序号");//添加列标题
            add_log_listview.Columns.Add("图片");
            add_log_listview.Columns.Add("入库时间");
            add_log_listview.Columns.Add("入库结果");
            add_log_listview.Columns.Add("描述");
            add_log_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//自动列宽
        }

        //写日志
        private void LogWrite(ImageUpLog log)
        {
            ListViewItem item = add_log_listview.Items.Add(log.ID);//序号
            item.SubItems.Add(log.Nname);//图片
            item.SubItems.Add(log.Time);//入库时间
            item.SubItems.Add(log.Result);//入库结果
            item.SubItems.Add(log.Message);//描述
            add_log_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//自动列宽
            add_log_listview.Items[add_log_listview.Items.Count - 1].EnsureVisible();//定位尾部
        }

        private void reset_log_button_Click(object sender, EventArgs e)//清空日志按钮
        {
            //
            if (add_background.IsBusy)
            {
                MessageBox.Show("请先停止入库");
                return;
            }
            add_log_listview.Clear();
            LogListSettings();
        }
        #endregion 日志

        private Api api;
        private void depot_list_combobox_SelectedIndexChanged(object sender, EventArgs e)//图库列表选择动作
        {
            //复原选项
            sub_checkbox.Checked = false;

            //获取API元素
            api = ApiFunction.GetApi(depot_list_combobox.Text);
            add_path_textbox.Text = api.Path;
        }

        private void reset_add_image_list_button_Click(object sender, EventArgs e)//清空图片列表按钮
        {
            //
            if (add_background.IsBusy)
            {
                MessageBox.Show("后台已在入库");
                return;
            }
            scan_listview.Clear();
            ScanListSettings();
        }

        #region 扫描
        private List<string> sub_list = new List<string>();//包含子目录选项
        private void sub_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (sub_checkbox.Checked)
            {
                if (sub_checkbox.Checked && depot_list_combobox.Text == "")
                {
                    MessageBox.Show("未选择图库");
                    if (sub_list.Count > 0) sub_list.Clear();
                    sub_checkbox.Checked = false;
                    return;
                }

                if (sub_checkbox.Checked && add_path_textbox.Text == "")
                {
                    MessageBox.Show("未选择图片目录");
                    if (sub_list.Count > 0) sub_list.Clear();
                    sub_checkbox.Checked = false;
                    return;
                }
                if (sub_checkbox.Checked && !Directory.Exists(add_path_textbox.Text))
                {
                    MessageBox.Show("图片目录不正确");
                    if (sub_list.Count > 0) sub_list.Clear();
                    sub_checkbox.Checked = false;
                    return;
                }

                ImageUpSubForm subform = new ImageUpSubForm();
                subform.Path = api.Path;//目录
                subform.ShowDialog();
                if (subform.DialogResult == DialogResult.OK)
                {
                    if (subform.List.Count < 1)
                    {
                        sub_checkbox.Checked = false;
                        if (sub_list.Count > 0) sub_list.Clear();
                        return;
                    }
                    sub_list = subform.List;//获取列表
                    return;
                }
                else
                {
                    sub_checkbox.Checked = false;
                    if (sub_list.Count > 0) sub_list.Clear();
                    return;
                }
            }

            //
            if (sub_list.Count > 0) sub_list.Clear();

        }


        private void sql_checkbox_CheckedChanged(object sender, EventArgs e)//匹配数据库选项
        {
            if (sql_checkbox.Checked)
            {
                //警示窗口
                DialogResult msg = MessageBox.Show("如果文件数量大，匹配数据库将十分缓慢，并加大服务器负担。\r\n按确定勾选，按取消不勾选", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (msg != DialogResult.OK) sql_checkbox.Checked = false;
            }
        }

        //扫描列表样式
        private void ScanListSettings()
        {
            //添加列标题
            scan_listview.Columns.Add("序号");
            scan_listview.Columns.Add("图片");
            scan_listview.Columns.Add("位置");
            //自动列宽
            scan_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        //扫描按钮
        private void add_scan_button_Click(object sender, EventArgs e)
        {
            //入库运行中
            if (add_background.IsBusy)
            {
                MessageBox.Show("请先停止入库");
                return;
            }

            //异步调用
            if (scan_background.IsBusy)
            {
                MessageBox.Show("后台扫描中");
                return;
            }

            //图库
            if (depot_list_combobox.Text == "")
            {
                MessageBox.Show("未选择图库");
                return;
            }

            //图库
            if (format_combobox.Text == "")
            {
                MessageBox.Show("未选择格式");
                return;
            }


            if (add_path_textbox.Text == "")//目录
            {
                MessageBox.Show("未选择图片目录");
                return;
            }

            if (!Directory.Exists(add_path_textbox.Text))
            {
                MessageBox.Show("图片目录不正确");
                return;
            }


            if (scan_listview.Items.Count > 0)//清空列表
            {
                scan_listview.Clear();
                ScanListSettings();
            }

            var scan = new object[4];//装箱
            scan[0] = format_combobox.Text;
            scan[1] = sub_list;
            scan[2] = sql_checkbox.Checked;
            scan[3] = depot_list_combobox.Text;
            scan_background.RunWorkerAsync(scan);

            //这里放在DOWORK里
            add_bar_label.Text = "正在获取文件...";
        }

        //异步扫描工作
        private void scan_background_DoWork(object sender, DoWorkEventArgs e)
        {
            //拆箱
            var scan = e.Argument as object[];
            string extension = (string)scan[0];
            List<string> sub_list = (List<string>)scan[1];
            bool sql_check = (bool)scan[2];
            string depot_name = (string)scan[3];
            Api api = ApiFunction.GetApi(depot_name);

            DataTable datatable = new DataTable();//表
            //datatable.Columns.Add("ID", Type.GetType("System.Int32"));//datatable.Columns[0].AutoIncrement = true;//datatable.Columns[0].AutoIncrementSeed = 1;//datatable.Columns[0].AutoIncrementStep = 1;//自动序号
            datatable.Columns.Add("序号", Type.GetType("System.String"));
            datatable.Columns.Add("图片", Type.GetType("System.String"));
            datatable.Columns.Add("位置", Type.GetType("System.String"));

            //序号
            int index = 0;

            //遍历文件夹
            DirectoryInfo directorys = new DirectoryInfo(api.Path);
            FileInfo[] files = directorys.GetFiles(extension, SearchOption.TopDirectoryOnly);
            int count = 0;
            foreach (FileInfo file in files)
            {
                //取消检测
                if (scan_background.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //匹配数据库
                if (sql_check)
                {
                    DataTable datatable1 = Sql.Select(depot_name, "SELECT Names, Path, Result, Message FROM " + api.Table + " WHERE Names='" + file.Name.Replace("'", "''") + "' AND Path='" + file.FullName.Replace(api.Path, "").Replace("'", "''") + "'");//数据库查询
                    if (datatable1.Rows.Count > 0) if (datatable1.Rows[0]["Result"].ToString() == "End" || datatable1.Rows[0]["Result"].ToString() == "Ignore") continue; //如果查询到记录将跳过
                }

                //
                index++;
                //
                DataRow datarow;
                datarow = datatable.NewRow();
                datarow["序号"] = index.ToString();
                datarow["图片"] = file.Name;
                datarow["位置"] = file.FullName;
                datatable.Rows.Add(datarow);

                //进度传出
                count++;
                scan_background.ReportProgress(Percents.Get(count, files.Length), file.FullName);
            }

            //遍历子文件夹
            if (sub_list.Count < 1)
            {
                //结果传出
                e.Result = datatable;
            }

            //
            foreach (string str in sub_list)
            {
                DirectoryInfo directory_all = new DirectoryInfo(str);
                //
                int count_all = 0;
                try//访问权限捕捉
                {
                    FileInfo[] files_all = directory_all.GetFiles(extension, SearchOption.AllDirectories);
                    foreach (FileInfo file_all in files_all)
                    {
                        //取消检测
                        if (scan_background.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        //匹配数据库
                        if (sql_check)
                        {
                            //获取数据库配置
                            DataTable datatable2 = Sql.Select(depot_name, "SELECT Names, Path, Result, Message FROM " + api.Table + " WHERE Names='" + file_all.Name.Replace("'", "''") + "' AND Path='" + file_all.FullName.Replace(api.Path, "").Replace("'", "''") + "'");

                            //如果查询到记录将跳过
                            if (datatable2.Rows.Count > 0)
                            {
                                continue;
                            }
                        }

                        //
                        index++;
                        //
                        DataRow datarow;
                        datarow = datatable.NewRow();
                        datarow["序号"] = index.ToString();
                        datarow["图片"] = file_all.Name;
                        datarow["位置"] = file_all.FullName;
                        datatable.Rows.Add(datarow);
                        //进度传出
                        count_all++;
                        scan_background.ReportProgress(Percents.Get(count_all, files_all.Length), file_all.FullName);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("无权限访问：" + directory_all.FullName + "请尝试使用管理员权限运行本程序");
                }
            }

            //结果传出
            e.Result = datatable;
        }

        //异步扫描进度
        private void scan_background_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            add_bar.Value = (e.ProgressPercentage < 101) ? e.ProgressPercentage : add_bar.Value;
            add_bar_label.Text = add_bar.Value.ToString() + "% 发现文件" + e.UserState as string;
        }

        //异步扫描完成
        private void scan_background_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("扫描文件错误如下\r\n" + e.Error.ToString());
                add_bar_label.Text = "扫描文件后台错误";
                return;
            }

            if (e.Cancelled)
            {
                MessageBox.Show("扫描已取消");
                add_bar_label.Text = "扫描已取消";
                return;
            }

            //接收传出
            DataTable datatable = e.Result as DataTable;

            //空数据
            if (datatable == null)
            {
                add_bar.Value = 100;
                MessageBox.Show("扫描失败，返回了空结果");
                add_bar_label.Text = "扫描失败，返回了空结果";
                return;
            }

            //0结果
            if (datatable.Rows.Count == 0)
            {
                add_bar.Value = 100;
                MessageBox.Show("没有扫描到文件");
                add_bar_label.Text = "没有扫描到文件";
                return;
            }

            //遍历
            scan_listview.BeginUpdate();//挂起UI，避免闪烁并提速
            ListTable.ToView(datatable, scan_listview);
            scan_listview.EndUpdate();  //绘制UI。
            scan_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//自动列宽
            scan_listview.Items[scan_listview.Items.Count - 1].EnsureVisible();//定位尾部

            //
            add_bar.Value = 100;
            add_bar_label.Text = "完成扫描";
        }
        #endregion 扫描

        //停止入库停止扫描按钮
        private void add_cancel_button_Click(object sender, EventArgs e)
        {
            //
            if (add_background.IsBusy)
            {
                add_background.CancelAsync();
                return;
            }

            //
            if (scan_background.IsBusy)
            {
                scan_background.CancelAsync();
                return;
            }
            //
            MessageBox.Show("后台未启动");
        }


        /**********************************************************************************入库****************************************************************************************/

        //入库按钮
        private void add_start_button_Click(object sender, EventArgs e)
        {
            //选择图库
            if (depot_list_combobox.Text == "")
            {
                MessageBox.Show("未选择图库");
                return;
            }

            //选择图片格式
            if (format_combobox.Text == "")
            {
                MessageBox.Show("未选择图片格式");
                return;
            }

            //无文件
            if (scan_listview.Items.Count < 1)
            {
                MessageBox.Show("没有扫描到图片");
                return;
            }

            //异步进程
            if (add_background.IsBusy)
            {
                MessageBox.Show("后台正在入库");
                return;
            }

            //清空日志
            add_log_listview.Clear();
            LogListSettings();

            var add = new object[2];//装箱
            DataTable datatable = new DataTable();
            ListTable.ToTable(scan_listview, datatable);
            add[0] = datatable;
            add[1] = depot_list_combobox.Text;
            add_background.RunWorkerAsync(add);
        }

        //异步入库工作
        private void add_background_DoWork(object sender, DoWorkEventArgs e)
        {
            #region 拆箱
            var scan = e.Argument as object[];
            DataTable datatable = (DataTable)scan[0];
            string depot_name = (string)scan[1];
            Api api = ApiFunction.GetApi(depot_name);
            #endregion 拆箱

            for (int i = 0; i < datatable.Rows.Count; i++)//遍历
            {
                if (add_background.CancellationPending) { e.Cancel = true; return; }//取消检测

                #region 定义元素
                string name = datatable.Rows[i]["图片"].ToString();//文件名
                string fullpath = datatable.Rows[i]["位置"].ToString();//文件路径
                string uppath = fullpath.Replace(api.Path, "");//记录时清除主目录，后面SQL转义
                ImageUpLog log = new ImageUpLog();//日志
                log.ID = (i + 1).ToString();
                log.Nname = fullpath;
                log.Time = DateTime.Now.ToString();
                #endregion 定义元素

                DataTable select_datatable = Sql.Select(depot_name, @"SELECT Names, Path, Result, Message, Times FROM " + api.Table + " WHERE Names='" + name.Replace("'", "''") + "' AND Path='" + uppath.Replace("'", "''") + "'");//数据库查询

                #region 多条记录异常
                if (select_datatable.Rows.Count > 1)//多条记录异常
                {
                    MessageBox.Show("图片" + select_datatable.Rows[0]["Names"].ToString() + "存在" + select_datatable.Rows.Count.ToString() + "条数据记录，请先处理此异常才能继续入库");
                    return;
                }
                #endregion 多条记录异常

                #region 返回单条记录
                if (select_datatable.Rows.Count > 0)//已有1条记录
                {
                    string result = select_datatable.Rows[0]["Result"].ToString();//检查完成状态

                    if (result == "End")//已入库
                    {
                        log.Result = "跳过";
                        log.Message = ApiFunction.GetError("216681");
                        add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);//日志
                        continue;
                    }

                    else if (result == "Ignore")//记录忽略
                    {
                        log.Result = "跳过";
                        log.Message = ApiFunction.GetError(select_datatable.Rows[0]["Message"].ToString().Replace("Error", ""));
                        add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);//日志
                        continue;
                    }

                    else if (result == "Local")//记录本地
                    {
                        JObject local_json = ApiFunction.Up(ApiFunction.GetClient(api.Appid, api.Apikey, api.Secreykey, api.Timeout), fullpath, fullpath.Replace(api.Path, ""), api.Tags1, api.Tags2);//入库
                        if (local_json == null || local_json.ToString() == "")
                        {
                            e.Cancel = true;
                            MessageBox.Show("API返回了错误，入库已终止");
                            return;
                        }

                        if (local_json.Property("error_code") == null )//入库成功
                        {
                            Sql.Up(depot_name, @"UPDATE " + api.Table + " SET LogID = '" + local_json["log_id"].ToString() + "', ContSign = '" + local_json["cont_sign"].ToString() + "', Tsgs1 = '" + api.Tags1 + "', Tsgs2 = '" + api.Tags2 + "', Result = 'End', Message = 'End', Times = '" + DateTime.Now.ToString() + "' WHERE Names = '" + select_datatable.Rows[0]["Names"].ToString().Replace("'", "''") + "'");
                            log.Result = "完成";//日志
                            log.Message = "已入库";
                            add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
                            continue;
                        }

                        else//入库错误，此情况是本地扫描后未入库，或者入库时出现API错误，先记录数据等待下一次入库
                        {
                            string error_code = local_json["error_code"].ToString();//错误代码
                            if (local_json.Property("cont_sign") != null && local_json.Property("cont_sign").ToString() != "")//错误含cont_sign即已入过库，此错误是本地扫描后没有入库，但之前已经入过库但未记录，现在进行重复入库造成，更新记录为End状态即可
                            {
                                Sql.Up(depot_name, @"UPDATE " + api.Table + " SET LogID = '" + local_json["log_id"].ToString() + "', ContSign = '" + local_json["cont_sign"].ToString() + "', Tsgs1 = '" + api.Tags1 + "', Tsgs2 = '" + api.Tags2 + "', Result = 'End', Message = 'End', Times = '" + DateTime.Now.ToString() + "' WHERE Names = '" + select_datatable.Rows[0]["Names"].ToString().Replace("'", "''") + "'");
                                log.Result = "错误";//日志
                                log.Message = ApiFunction.GetError(error_code);
                                add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
                                continue;
                            }
                            else//错误里没有log_id，但有数据记录，此情况是本地扫描后没有入库，现在入库返回了其他错误造成的，把记录更新为Error状态即可
                            {
                                string ignore = (ApiFunction.UpIgnore(error_code) == "Yes") ? "Ignore" : "Local";//以后是否忽略入库
                                Sql.Up(depot_name, @"UPDATE " + api.Table + " SET Result = '" + ignore + "', Message = 'Error" + error_code + "', Times = '" + DateTime.Now.ToString() + "' WHERE Names = '" + select_datatable.Rows[0]["Names"].ToString().Replace("'", "''") + "'");
                                log.Result = "错误";//日志
                                log.Message = ApiFunction.GetError(error_code);
                                add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
                                continue;
                            }
                        }
                    }

                    else//Result列数据异常
                    {
                        MessageBox.Show("图片" + select_datatable.Rows[0]["Names"].ToString() + "的Result列数据" + select_datatable.Rows[0]["Result"].ToString() + "异常，请先处理继续入库");
                        return;
                    }
                }
                #endregion 返回单条记录

                #region 无记录
                else
                {
                    JObject json = ApiFunction.Up(ApiFunction.GetClient(api.Appid, api.Apikey, api.Secreykey, api.Timeout), fullpath, fullpath.Replace(api.Path, ""), api.Tags1, api.Tags2);//入库
                    if (json == null || json.ToString() == "")
                    {
                        e.Cancel = true;
                        MessageBox.Show("API返回了错误，入库已终止");
                        return;
                    }

                    if (json.Property("error_code") == null)//入库成功
                    {
                        Sql.Insert(depot_name, @"INSERT INTO " + api.Table + " (Names, Path, LogID, ContSign, Tsgs1, Tsgs2, Result, Message, Times) VALUES('" + name.Replace("'", "''") + "', '" + uppath.Replace("'", "''") + "', '" + json["log_id"].ToString() + "', '" + json["cont_sign"].ToString() + "', '" + api.Tags1 + "', '" + api.Tags2 + "', 'End', 'End','" + DateTime.Now.ToString() + "')");//记录数据库
                        log.Result = "完成";//日志
                        log.Message = "已入库，已录入数据库";
                        add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
                        continue;
                    }
                    else//入库错误
                    {
                        string error_code = json["error_code"].ToString();//错误代码
                        if (json.Property("cont_sign") != null && json.Property("cont_sign").ToString() != "")//错误里包含cont_sign，即已入过库，此错误是重复入库造成
                        {
                            Sql.Insert(depot_name, @"INSERT INTO " + api.Table + "(Names, Path, LogID, ContSign, Tsgs1, Tsgs2, Result, Message, Times) VALUES('" + name.Replace("'", "''") + "', '" + uppath.Replace("'", "''") + "', '" + json["log_id"].ToString() + "', '" + json["cont_sign"].ToString() + "', '" + api.Tags1 + "', '" + api.Tags2 + "', 'End', 'End','" + DateTime.Now.ToString() + "')");
                            log.Result = "错误";//日志
                            log.Message = ApiFunction.GetError(error_code);
                            add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
                            continue;
                        }
                        else//错误里没有log_id，且无数据记录，即其他错误执行错误结果记录
                        {
                            string ignore = (ApiFunction.UpIgnore(error_code) == "Yes") ? "Ignore" : "Local";//以后是否忽略入库
                            Sql.Insert(depot_name, @"INSERT INTO " + api.Table + "(Names, Path, Result, Message, Times) VALUES('" + name.Replace("'", "''") + "', '" + uppath.Replace("'", "''") + "', '" + ignore + "', 'Error" + error_code + "', '" + DateTime.Now.ToString() + "')");
                            log.Result = "错误";//日志
                            log.Message = ApiFunction.GetError(error_code);
                            add_background.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
                            continue;
                        }
                    }
                }
                #endregion 无记录

            }

        }

        private void add_background_ProgressChanged(object sender, ProgressChangedEventArgs e)//异步入库进度
        {
            add_bar.Value = e.ProgressPercentage;
            ImageUpLog log = e.UserState as ImageUpLog;//传出解封
            add_bar_label.Text = add_bar.Value.ToString() + "% 正在入库 " + log.Nname;
            LogWrite(log);
        }

        //异步入库完成
        private void add_background_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("入库后台错误如下\r\n" + e.Error.ToString());
                add_bar_label.Text = "入库后台错误";
                return;
            }
            if (e.Cancelled)
            {
                MessageBox.Show("入库已取消");
                add_bar_label.Text = "入库已取消";
                return;
            }
            add_bar.Value = 100;
            add_bar_label.Text = "完成入库";
        }
        /**********************************************************************************入库****************************************************************************************/



        //窗口关闭检测
        private void ImageUpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (add_background.IsBusy)
            {
                e.Cancel = true;
                MessageBox.Show("后台正在入库，请勿关闭窗口");
            }
            if (scan_background.IsBusy)
            {
                e.Cancel = true;
                MessageBox.Show("后台正在扫描文件，请勿关闭窗口");
            }
        }

        //
    }
}
