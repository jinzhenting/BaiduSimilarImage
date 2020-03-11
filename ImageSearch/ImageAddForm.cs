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
    public partial class ImageAddForm : Form
    {
        public ImageAddForm(){InitializeComponent(); }

        /// <summary>
        /// 格式列表载入
        /// </summary>
        private void FormatListLoad()
        {
            if (ApiFunction.GetFormatList() != null) formatCombobox.DataSource = ApiFunction.GetFormatList();
        }

        /// <summary>
        /// 窗口载入时
        /// </summary>
        private void ImageAddForm_Load(object sender, EventArgs e)
        {
            if (ApiFunction.GetDepotList() != null) depotListCombobox.DataSource = ApiFunction.GetDepotList();// 图库下拉列表
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

        #region 日志
        private void LogListSettings()// 日志列表样式
        {
            addLogListview.Columns.Add("序号");// 添加列标题
            addLogListview.Columns.Add("图片");
            addLogListview.Columns.Add("入库时间");
            addLogListview.Columns.Add("入库结果");
            addLogListview.Columns.Add("描述");
            addLogListview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
        }
        
        private void LogWrite(ImageAddLog log)// 写日志
        {
            addLogListview.BeginUpdate();// 挂起UI，避免闪烁并提速
            ListViewItem item = addLogListview.Items.Add(log.ID);// 序号
            item.SubItems.Add(log.Nname);// 图片
            item.SubItems.Add(log.Time);// 入库时间
            item.SubItems.Add(log.Result);// 入库结果
            item.SubItems.Add(log.Message);// 描述
            addLogListview.EndUpdate();// 绘制UI
            addLogListview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
            addLogListview.Items[addLogListview.Items.Count - 1].EnsureVisible();// 定位尾部
        }

        private void resetLogButton_Click(object sender, EventArgs e)// 清空日志按钮
        {
            if (addBack.IsBusy)
            {
                MessageBox.Show("请先停止入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            addLogListview.Clear();
            LogListSettings();
        }
        #endregion 日志

        /// <summary>
        /// API
        /// </summary>
        private Api api;
        private void depotListCombobox_SelectedIndexChanged(object sender, EventArgs e)// 图库列表选择动作
        {
            subCheckbox.Checked = false;// 复原选项
            api = ApiFunction.GetApi(depotListCombobox.Text);// 获取API元素
            if (api != null) addPathTextbox.Text = api.Path;
            if (scanListview.Items.Count > 0)// 清空列表
            {
                scanListview.Clear();
                ScanListSettings();
            }
        }

        private void resetAddImageListButton_Click(object sender, EventArgs e)// 清空图片列表按钮
        {
            if (addBack.IsBusy)
            {
                MessageBox.Show("请先停止入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            scanListview.Clear();
            ScanListSettings();
        }

        #region 扫描
        private List<string> subList = new List<string>();// 包含子目录选项列表
        private void subCheckbox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void sqlCheckbox_CheckedChanged(object sender, EventArgs e)// 匹配数据库选项
        {
            if (sqlCheckbox.Checked) if (MessageBox.Show("如果文件数量大，匹配数据库将十分缓慢，并加大服务器负担。\r\n是否继续勾选", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) sqlCheckbox.Checked = false;
        }

        private void ScanListSettings()// 扫描列表样式
        {
            scanListview.Columns.Add("序号");// 添加列标题
            scanListview.Columns.Add("图片");
            scanListview.Columns.Add("位置");
            scanListview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
        }

        private void addScanButton_Click(object sender, EventArgs e)// 扫描按钮
        {
            if (addScanButton.Text == "扫描图片")
            {
                if (scanBack.IsBusy)
                {
                    MessageBox.Show("请先停止扫描", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (addBack.IsBusy)
                {
                    MessageBox.Show("请先停止入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (depotListCombobox.Text == "")
                {
                    MessageBox.Show("未选择图库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (formatCombobox.Text == "")
                {
                    MessageBox.Show("未选择格式", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (addPathTextbox.Text == "")
                {
                    MessageBox.Show("未选择图片目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!Directory.Exists(addPathTextbox.Text))
                {
                    MessageBox.Show("图片目录不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (scanListview.Items.Count > 0)// 清空列表
                {
                    scanListview.Clear();
                    ScanListSettings();
                }

                var scan = new object[4];// 装箱
                scan[0] = formatCombobox.Text;
                scan[1] = subList;
                scan[2] = sqlCheckbox.Checked;
                scan[3] = depotListCombobox.Text;
                scanBack.RunWorkerAsync(scan);
                addScanButton.Text = "停止扫描";
                addBar.Value = 1;
                addLabel.Text = "% 开始扫描...";
            }
            else
            {
                if (scanBack.IsBusy) scanBack.CancelAsync();
                addScanButton.Text = "扫描图片";
            }
        }

        private void scanBack_DoWork(object sender, DoWorkEventArgs e)// 异步扫描工作
        {
            var scan = e.Argument as object[];// 拆箱
            string extension = (string)scan[0];
            List<string> subList = (List<string>)scan[1];
            bool sqlCheck = (bool)scan[2];
            string depot = (string)scan[3];
            Api api = ApiFunction.GetApi(depot);
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
                if (scanBack.CancellationPending)// 取消检测
                {
                    e.Cancel = true;
                    return;
                }
                
                if (sqlCheck)// 是否匹配数据库
                {
                    DataTable datatable1 = Sql.Select(depot, "SELECT Names, Path, Result, Message FROM " + api.Table + " WHERE Names='" + file.Name.Replace("'", "''") + "' AND Path='" + file.FullName.Replace(api.Path, "").Replace("'", "''") + "'");// 数据库查询
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
                scanBack.ReportProgress(Percents.Get(count, files.Length), file.FullName);// 进度传出
            }

            if (subList.Count < 1) e.Result = datatable;// 没有选中子文件夹//结果传出
            else
            {
                foreach (string str in subList)// 遍历子文件夹
                {
                    if (scanBack.CancellationPending)// 取消检测
                    {
                        e.Cancel = true;
                        return;
                    }

                    DirectoryInfo allDirectory = new DirectoryInfo(str);
                    int countAll = 0;// 文件数
                    try//访问权限捕捉
                    {
                        FileInfo[] allFile = allDirectory.GetFiles(extension, SearchOption.AllDirectories);
                        foreach (FileInfo allfiles in allFile)
                        {
                            if (scanBack.CancellationPending)// 取消检测
                            {
                                e.Cancel = true;
                                return;
                            }

                            if (sqlCheck)// 是否匹配数据库
                            {
                                DataTable datatable2 = Sql.Select(depot, "SELECT Names, Path, Result, Message FROM " + api.Table + " WHERE Names='" + allfiles.Name.Replace("'", "''") + "' AND Path='" + allfiles.FullName.Replace(api.Path, "").Replace("'", "''") + "'");// 获取数据库配置
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
                            datarow["图片"] = allfiles.Name;
                            datarow["位置"] = allfiles.FullName;
                            datatable.Rows.Add(datarow);
                            countAll++;// 文件计数
                            scanBack.ReportProgress(Percents.Get(countAll, allFile.Length), allfiles.FullName);// 进度传出
                        }
                    }
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
                }
            }

            e.Result = datatable;// 结果传出
        }

        private void scanBack_ProgressChanged(object sender, ProgressChangedEventArgs e)// 异步扫描进度
        {
            addBar.Value = (e.ProgressPercentage < 101) ? e.ProgressPercentage : addBar.Value;
            addLabel.Text = addBar.Value.ToString() + "% 发现文件" + e.UserState as string;
        }
        
        private void scanBack_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)// 异步扫描完成
        {
            addScanButton.Text = "扫描图片";

            if (e.Error != null)
            {
                MessageBox.Show("扫描文件错误如下\r\n\r\n" + e.Error.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                addLabel.Text = "扫描文件后台错误";
                return;
            }

            if (e.Cancelled)
            {
                MessageBox.Show("扫描已取消", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                addLabel.Text = "扫描已取消";
                return;
            }

            DataTable datatable = e.Result as DataTable;// 接收传出
            
            if (datatable == null)// 空数据
            {
                addBar.Value = 100;
                MessageBox.Show("扫描失败，返回了空结果", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                addLabel.Text = "扫描失败，返回了空结果";
                return;
            }
            
            if (datatable.Rows.Count == 0)// 0结果
            {
                addBar.Value = 100;
                MessageBox.Show("没有扫描到文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                addLabel.Text = "没有扫描到文件";
                return;
            }
            
            scanListview.BeginUpdate();// 挂起UI，避免闪烁并提速
            ListTable.ToView(datatable, scanListview);// 数据转换
            scanListview.EndUpdate();// 绘制UI
            scanListview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
            scanListview.Items[scanListview.Items.Count - 1].EnsureVisible();// 定位尾部
            
            addBar.Value = 100;
            addLabel.Text = "完成扫描";
        }
        #endregion 扫描

        #region 入库
        private void addStartButton_Click(object sender, EventArgs e)// 入库按钮
        {
            if (addStartButton.Text == "开始入库")
            {
                if (addBack.IsBusy)
                {
                    MessageBox.Show("请先停止入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                if (scanBack.IsBusy)
                {
                    MessageBox.Show("请先停止扫描", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                if (depotListCombobox.Text == "")
                {
                    MessageBox.Show("未选择图库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (formatCombobox.Text == "")
                {
                    MessageBox.Show("未选择图片格式", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (scanListview.Items.Count < 1)
                {
                    MessageBox.Show("请先扫描图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                addLogListview.Clear();// 清空日志
                LogListSettings();

                var add = new object[2];// 装箱
                DataTable datatable = new DataTable();
                ListTable.ToTable(scanListview, datatable);
                add[0] = datatable;
                add[1] = depotListCombobox.Text;
                addBack.RunWorkerAsync(add);
                addStartButton.Text = "停止入库";
                addBar.Value = 1;
                addLabel.Text = "% 开始入库...";
            }
            else
            {
                if (addBack.IsBusy)addBack.CancelAsync();
                addStartButton.Text = "开始入库";
            }
        }

        private void addBack_DoWork(object sender, DoWorkEventArgs e)// 异步入库工作
        {
            #region 拆箱
            var scan = e.Argument as object[];
            DataTable datatable = (DataTable)scan[0];
            string depot = (string)scan[1];
            Api api = ApiFunction.GetApi(depot);
            if (api == null) return;
            #endregion 拆箱

            for (int i = 0; i < datatable.Rows.Count; i++)// 遍历
            {
                if (addBack.CancellationPending) { e.Cancel = true; return; }//取消检测

                #region 定义元素
                string name = datatable.Rows[i]["图片"].ToString();// 文件名
                string fullpath = datatable.Rows[i]["位置"].ToString();// 文件路径
                string uppath = fullpath.Replace(api.Path, "");// 记录时清除主目录，后面SQL转义
                ImageAddLog log = new ImageAddLog();// 日志
                log.ID = (i + 1).ToString();
                log.Nname = fullpath;
                log.Time = DateTime.Now.ToString();
                #endregion 定义元素

                DataTable selectDataTable = Sql.Select(depot, @"SELECT Names, Path, Result, Message, Times FROM " + api.Table + " WHERE Names='" + name.Replace("'", "''") + "' AND Path='" + uppath.Replace("'", "''") + "'");// 数据库查询
                if (selectDataTable == null)
                {
                    MessageBox.Show("数据库操作错误，结束入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #region 多条记录异常
                if (selectDataTable.Rows.Count > 1)// 多条记录异常
                {
                    MessageBox.Show("图片" + selectDataTable.Rows[0]["Names"].ToString() + "存在" + selectDataTable.Rows.Count.ToString() + "条数据记录，请先处理此异常才能继续入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion 多条记录异常

                #region 返回单条记录
                if (selectDataTable.Rows.Count > 0)// 已有1条记录
                {
                    string result = selectDataTable.Rows[0]["Result"].ToString();// 检查完成状态

                    if (result == "End")// 已入库
                    {
                        log.Result = "跳过";
                        log.Message = ApiFunction.GetError("216681");
                        addBack.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);// 日志
                        continue;
                    }

                    else if (result == "Ignore")// 记录忽略
                    {
                        log.Result = "跳过";
                        log.Message = ApiFunction.GetError(selectDataTable.Rows[0]["Message"].ToString().Replace("Error", ""));
                        addBack.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);// 日志
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

                        JObject localJson = ApiFunction.Up(client, fullpath, fullpath.Replace(api.Path, ""), api.Tags1, api.Tags2);// 入库
                        if (localJson == null || localJson.ToString() == "")
                        {
                            if (MessageBox.Show("API连接错误，是否继续入库？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                            else
                            {
                                e.Cancel = true;
                                return;
                            }
                        }

                        if (localJson.Property("error_code") == null)// 入库成功
                        {
                            bool up = Sql.Up(depot, @"UPDATE " + api.Table + " SET LogID = '" + localJson["log_id"].ToString() + "', ContSign = '" + localJson["cont_sign"].ToString() + "', Tsgs1 = '" + api.Tags1 + "', Tsgs2 = '" + api.Tags2 + "', Result = 'End', Message = 'End', Times = '" + DateTime.Now.ToString() + "' WHERE Names = '" + selectDataTable.Rows[0]["Names"].ToString().Replace("'", "''") + "'");
                            if (up)
                            {
                                log.Result = "完成";// 日志
                                log.Message = "已入库";
                                addBack.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
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
                            string errorCode = localJson["error_code"].ToString();// 错误代码
                            if (localJson.Property("cont_sign") != null && localJson.Property("cont_sign").ToString() != "")// 错误含cont_sign即已入过库，此错误是本地扫描后没有入库，但之前已经入过库但未记录，现在进行重复入库造成，更新记录为End状态即可
                            {
                                bool up = Sql.Up(depot, @"UPDATE " + api.Table + " SET LogID = '" + localJson["log_id"].ToString() + "', ContSign = '" + localJson["cont_sign"].ToString() + "', Tsgs1 = '" + api.Tags1 + "', Tsgs2 = '" + api.Tags2 + "', Result = 'End', Message = 'End', Times = '" + DateTime.Now.ToString() + "' WHERE Names = '" + selectDataTable.Rows[0]["Names"].ToString().Replace("'", "''") + "'");
                                if (up)
                                {
                                    log.Result = "错误";// 日志
                                    log.Message = ApiFunction.GetError(errorCode);
                                    addBack.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
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
                                string ignore = (ApiFunction.UpIgnore(errorCode) == "Yes") ? "Ignore" : "Local";// 以后是否忽略入库
                                bool up = Sql.Up(depot, @"UPDATE " + api.Table + " SET Result = '" + ignore + "', Message = 'Error" + errorCode + "', Times = '" + DateTime.Now.ToString() + "' WHERE Names = '" + selectDataTable.Rows[0]["Names"].ToString().Replace("'", "''") + "'");
                                if (up)
                                {
                                    log.Result = "错误";// 日志
                                    log.Message = ApiFunction.GetError(errorCode);
                                    addBack.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
                                    continue;
                                }
                                MessageBox.Show("结束入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    else//Result列数据异常
                    {
                        MessageBox.Show("图片" + selectDataTable.Rows[0]["Names"].ToString() + "的Result列数据" + selectDataTable.Rows[0]["Result"].ToString() + "异常，请先处理再继续入库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        bool insert = Sql.Insert(depot, @"INSERT INTO " + api.Table + " (Names, Path, LogID, ContSign, Tsgs1, Tsgs2, Result, Message, Times) VALUES('" + name.Replace("'", "''") + "', '" + uppath.Replace("'", "''") + "', '" + json["log_id"].ToString() + "', '" + json["cont_sign"].ToString() + "', '" + api.Tags1 + "', '" + api.Tags2 + "', 'End', 'End','" + DateTime.Now.ToString() + "')");// 记录数据库
                        if (insert)
                        {
                            log.Result = "完成";// 日志
                            log.Message = "已入库，已录入数据库";
                            addBack.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
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
                        string errorCode = json["error_code"].ToString();// 错误代码
                        if (json.Property("cont_sign") != null && json.Property("cont_sign").ToString() != "")// 错误里包含cont_sign，即已入过库，此错误是重复入库造成
                        {
                            bool insert = Sql.Insert(depot, @"INSERT INTO " + api.Table + "(Names, Path, LogID, ContSign, Tsgs1, Tsgs2, Result, Message, Times) VALUES('" + name.Replace("'", "''") + "', '" + uppath.Replace("'", "''") + "', '" + json["log_id"].ToString() + "', '" + json["cont_sign"].ToString() + "', '" + api.Tags1 + "', '" + api.Tags2 + "', 'End', 'End','" + DateTime.Now.ToString() + "')");
                            if (insert)
                            {
                                log.Result = "错误";// 日志
                                log.Message = ApiFunction.GetError(errorCode);
                                addBack.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
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
                            string ignore = (ApiFunction.UpIgnore(errorCode) == "Yes") ? "Ignore" : "Local";// 以后是否忽略入库
                            bool insert = Sql.Insert(depot, @"INSERT INTO " + api.Table + "(Names, Path, Result, Message, Times) VALUES('" + name.Replace("'", "''") + "', '" + uppath.Replace("'", "''") + "', '" + ignore + "', 'Error" + errorCode + "', '" + DateTime.Now.ToString() + "')");
                            if (insert)
                            {
                                log.Result = "错误";// 日志
                                log.Message = ApiFunction.GetError(errorCode);
                                addBack.ReportProgress(Percents.Get(i, datatable.Rows.Count), log);
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

        private void addBack_ProgressChanged(object sender, ProgressChangedEventArgs e)// 异步入库进度
        {
            addBar.Value = e.ProgressPercentage;
            ImageAddLog log = e.UserState as ImageAddLog;// 传出解封
            addLabel.Text = addBar.Value.ToString() + "% 正在入库 " + log.Nname;
            LogWrite(log);
        }
        
        private void addBack_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)// 异步入库完成
        {
            addStartButton.Text = "开始入库";
            if (e.Error != null)
            {
                MessageBox.Show("入库后台错误如下\r\n\r\n" + e.Error.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                addLabel.Text = "入库后台错误";
                return;
            }
            if (e.Cancelled)
            {
                MessageBox.Show("入库已取消", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                addLabel.Text = "入库已取消";
                return;
            }
            addBar.Value = 100;
            addLabel.Text = "完成入库";
        }
        #endregion 入库
        
        private void ImageAddForm_FormClosing(object sender, FormClosingEventArgs e)// 窗口关闭检测
        {
            if (addBack.IsBusy)
            {
                e.Cancel = true;
                MessageBox.Show("后台正在入库，请勿关闭窗口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (scanBack.IsBusy)
            {
                e.Cancel = true;
                MessageBox.Show("后台正在扫描文件，请勿关闭窗口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void subCheckbox_Click(object sender, EventArgs e)
        {
            if (addBack.IsBusy)
            {
                MessageBox.Show("后台正在入库，请勿操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (subCheckbox.Checked) subCheckbox.Checked = false;
                else subCheckbox.Checked = true;
                return;
            }

            if (scanBack.IsBusy)
            {
                MessageBox.Show("后台正在扫描文件，请勿操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (subCheckbox.Checked) subCheckbox.Checked = false;
                else subCheckbox.Checked = true;
                return;
            }

            if (!subCheckbox.Checked) return;// 未勾选

            if (subList.Count > 0) subList.Clear();// 清空子目录选项列表

            if (subCheckbox.Checked && depotListCombobox.Text == "")
            {
                MessageBox.Show("未选择图库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (subList.Count > 0) subList.Clear();
                subCheckbox.Checked = false;
                return;
            }

            if (subCheckbox.Checked && addPathTextbox.Text == "")
            {
                MessageBox.Show("未选择图片目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (subList.Count > 0) subList.Clear();
                subCheckbox.Checked = false;
                return;
            }

            if (subCheckbox.Checked && !Directory.Exists(addPathTextbox.Text))
            {
                MessageBox.Show("图片目录不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (subList.Count > 0) subList.Clear();
                subCheckbox.Checked = false;
                return;
            }

            ImageAddSubForm subform = new ImageAddSubForm();
            subform.InPath = api.Path;// 主目录
            subform.ShowDialog();
            if (subform.DialogResult == DialogResult.OK)// 返回确定
            {
                if (subform.List.Count < 1)// 返回无选中项
                {
                    subCheckbox.Checked = false;
                    if (subList.Count > 0) subList.Clear();
                    return;
                }
                subList = subform.List;// 获取列表
                return;
            }
            else//返回取消
            {
                subCheckbox.Checked = false;
                if (subList.Count > 0) subList.Clear();
                return;
            }
        }
    }
}
