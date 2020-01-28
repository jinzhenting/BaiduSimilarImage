using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;


/*
------------------------------漏洞------------------------------
XP下API安全链接出错

------------------------------进度------------------------------
本地图片整理增加功能：
清除数据库中本地没有图片的记录。
扫描本地图片记录到数据库，初始为Result=local，

清空文件夹优化传出时卡顿问题
软件更新改版，放在单独类中
本地搜索增加3个DATA索引
更新功能
帮助功能
安装程序
*/

namespace ImageSearch
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        #region 程序更新
        private string app_up_path;// 地址

        private void AppUpdata(bool show_windows)
        {
            if (!Directory.Exists(app_up_path))// 地址检测
            {
                if (MessageBox.Show("程序更新地址 " + app_up_path + " 无效，是否重新设置？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;// 新版本更新选择窗口
                else
                {
                    SettingsForm settingsform = new SettingsForm();
                    settingsform.ShowDialog();
                    System.Environment.Exit(0);
                }
                return;
            }

            try
            {
                DirectoryInfo directorys = new DirectoryInfo(app_up_path);
                FileInfo[] files = directorys.GetFiles(@"ImageSearch*.exe", SearchOption.TopDirectoryOnly);// 扫描ImageSearch开头命名的exe文件
                if (files.Length == 0)// 没有发现ImageSearch开头命名的exe文件
                {
                    if (show_windows) MessageBox.Show("没有发现新版本", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                foreach (FileInfo file in files)// 遍历1或多个ImageSearch程序文件
                {
                    string version = Regex.Match(file.Name, @"[1-9]+[.][0-9]+[.][0-9]+[.][0-9]+").Groups[0].Value.Replace(".", "");// 获取版本号
                    if (version == "" || version == null) continue;// 在exe文件名中没有找到版本号
                    if (int.Parse(version) < int.Parse(Application.ProductVersion.Replace(".", ""))) continue;// 本地比服务器版本高
                    if (int.Parse(version) == int.Parse(Application.ProductVersion.Replace(".", ""))) continue;// 本地与服务器版本相同
                    if (MessageBox.Show("发现新版本：" + version + "\r\n当前版本：" + Application.ProductVersion + "\r\n按确定立即更新，请耐心等待后台下载完成安装", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;// 新版本更新选择窗口
                    System.Diagnostics.Process.Start(file.FullName);
                    System.Environment.Exit(0);
                    return;
                }

                if (show_windows) MessageBox.Show("没有发现新版本", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);// 存在ImageSearch程序文件，但版本相同或更旧
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("无权限访问更新地址，请尝试使用管理员权限运行本程序", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("更新地址不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("程序更新时发生如下错误\r\n\r\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        #endregion 程序更新

        #region 程序配置
        private void Settings()
        {
            try //读取程序配置
            {
                XmlDocument xmldocument = new XmlDocument();
                xmldocument.Load(@"Documents\Settings.xml");
                XmlNode rootNode = xmldocument.DocumentElement;// 定位根节点
                foreach (XmlNode xmlnode in rootNode.ChildNodes)
                {
                    if (xmlnode.Name == "Text")
                    {
                        this.Text = xmlnode.Attributes["name"].Value + " " + Application.ProductVersion;// 程序标题
                        app_about_menu.Text = "关于 " + xmlnode.Attributes["name"].Value;// 关于菜单
                    }
                    if (xmlnode.Name == "Up") app_up_path = xmlnode.Attributes["path"].Value;// 程序更新地址
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("无权限访问程序配置文件，程序将自动退出，请尝试使用管理员权限重新运行本程序", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("程序配置文件不存在，程序将自动退出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("访问程序配置文件时发生错误，程序将自动退出，错误描述如下\r\n\r\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return;
            }

            try//读取程序图标
            {
                Icon = new Icon(Path.Combine(Application.StartupPath, @"Skin\Icon.ico"));
                image_add_button.Image = Image.FromFile(@"Skin\Add.png");
                image_up_button.Image = Image.FromFile(@"Skin\Up.png");
                image_delete_button.Image = Image.FromFile(@"Skin\Delete.png");
                api_settings_button.Image = Image.FromFile(@"Skin\Setting.png");
                sort_button.Image = Image.FromFile(@"Skin\Sort.png");
                help_button.Image = Image.FromFile(@"Skin\Help.png");
                emptyfolder_button.Image = Image.FromFile(@"Skin\EmptyFolder.png");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("无权限加载工具栏图标文件，请尝试使用管理员权限重新运行本程序", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("工具栏图标文件不存在，程序将自动退出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载工具栏图标时发生如下错误，程序将自动退出，错误描述如下\r\n\r\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return;
            }
        }
        #endregion 程序配置

        #region 初始化
        private void HomeForm_Load(object sender, EventArgs e)// 窗体体载入时
        {
            //Control.CheckForIllegalCrossThreadCalls = false;// 关闭back访问限制
            Settings();// 程序配置
            AppUpdata(false);// 程序升级
            if (ApiFunction.GetDepotList() != null) online_depot_combobox.DataSource = local_image_combobox.DataSource = ApiFunction.GetDepotList();// 所有图库下拉列表数据源
            LocalListviewSettings();// 本地搜索列表配置
            online_picturebox.AllowDrop = true;// 重写AllowDrop使其接受拖放
        }
        #endregion 初始化

        #region 在线搜索
        private Api online_api;// API实例

        private void online_depot_combobox_SelectedIndexChanged(object sender, EventArgs e)// 在线图库选择时
        {
            online_api = ApiFunction.GetApi(online_depot_combobox.Text);// 获取API配置
            if (online_api != null) online_quantity_combobox.Text = online_api.Quantity.ToString();// 返回数
        }

        private void online_search_button_Click(object sender, EventArgs e)// 在线搜索按钮
        {
            if (online_picturebox.Image == null)
            {
                if (online_image_path_textbox.Text == "")// 图片未加载 - 路径空
                {
                    MessageBox.Show("未选择图片位置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!File.Exists(online_image_path_textbox.Text))// 图片未加载 - 路径错误
                {
                    MessageBox.Show("图片位置不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ApiFunction.AcceptFormat2(online_image_path_textbox.Text))// 图片未加载 - 格式不支持
                {
                    MessageBox.Show(ApiFunction.GetError("216201"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);// 扩展名不受支持，获取错误提示
                    return;
                }
            }

            if (online_depot_combobox.Text == "")
            {
                MessageBox.Show("未选择图库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(online_api.Path))
            {
                MessageBox.Show("本地图片库 " + online_api.Path + "不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (search_background.IsBusy)// 在线搜索异步调用
            {
                MessageBox.Show("请先停止搜索", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            byte[] bytes = ApiFunction.ImagetoByte(online_picturebox.Image);// 图片转换字节
            var back = new object[3];// 装箱
            back[0] = online_depot_combobox.Text;// 图库
            back[1] = bytes;// 图片
            back[2] = int.Parse(online_quantity_combobox.Text);// 返回数
            search_background.RunWorkerAsync(back);// 传入
            search_bar.Value = 1;
            progress_label.Text = "准备搜索...";
        }

        private void search_background_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)// 在线搜索后台
        {
            var back = e.Argument as object[];// 拆箱
            Api api = ApiFunction.GetApi((string)back[0]);// 获取API配置
            if (api != null)
            {
                byte[] bytes = (byte[])back[1];// 图片
                int quantity = (int)back[2];// 返回数
                search_background.ReportProgress(50, "搜索中");// 进度

                Baidu.Aip.ImageSearch.ImageSearch client = ApiFunction.GetClient(api.Appid, api.Apikey, api.Secreykey, api.Timeout);// 搜索后台启用
                if (client == null) return;

                JObject json = ApiFunction.Search(client, bytes, api.Tags1, api.Tags2, quantity);// 入库
                if (json == null) return;

                e.Result = json;
            }
        }

        private void search_background_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)// 在线搜索进度
        {
            search_bar.Value = (e.ProgressPercentage < 101) ? e.ProgressPercentage : search_bar.Value;
            progress_label.Text = search_bar.Value.ToString() + "% " + e.UserState as string;
        }

        private void search_background_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)// 在线搜索后台完成
        {
            if (e.Error != null)// 错误
            {
                MessageBox.Show("搜索后台错误如下\r\n\r\n" + e.Error.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            JObject json = e.Result as JObject;// 接收传出
            if (json == null || json.ToString() == "")
            {
                MessageBox.Show("API返回了错误，搜索已终止", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (json.Property("error_code") != null && json.Property("error_code").ToString() != "") MessageBox.Show(ApiFunction.GetError(json["error_code"].ToString()));// 返回Json包含错误信息
            if (json.Property("result_num") != null && json.Property("result_num").ToString() != "")// 返回Json正确
            {
                int num = json["result_num"].Value<int>();// 总数
                if (num == 0)
                {
                    MessageBox.Show("图库返回了0条结果", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    search_bar.Value = 100;
                    progress_label.Text = "搜索完成";
                    return;
                }
                JToken result = json["result"];
                List<string> list = new List<string>();
                for (int i = 0; i < num; i++) list.Add(result[i]["brief"].ToString());
                search_bar.Value = 100;
                progress_label.Text = "搜索完成";
                ApiResultForm ApiResultForm = new ApiResultForm(list, online_depot_combobox.Text, online_api.Path);
                ApiResultForm.ShowDialog();
            }
        }

        private void online_image_path_button_Click(object sender, EventArgs e)// 浏览图片按钮
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            if (openfiledialog.ShowDialog() != DialogResult.OK) return;
            if (ApiFunction.AcceptFormat2(openfiledialog.FileName))
            {
                online_picturebox.ImageLocation = openfiledialog.FileName;
                online_image_path_textbox.Text = openfiledialog.FileName;
                search_bar.Value = 0;
                progress_label.Text = "已选择图片";
            }
            else MessageBox.Show(ApiFunction.GetError("216201"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);// 扩展名不受支持，获取错误提示
        }

        private void online_image_path_textbox_TextChanged(object sender, EventArgs e)// 位置输入实时检测
        {
            if (File.Exists(online_image_path_textbox.Text) && ApiFunction.AcceptFormat2(online_image_path_textbox.Text)) online_picturebox.ImageLocation = online_image_path_textbox.Text;// 路径与格式同时检测
        }

        private void online_picturebox_DragEnter(object sender, DragEventArgs e)// 拖放输入
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.All;
            else e.Effect = DragDropEffects.None;
        }

        private void online_picturebox_DragDrop(object sender, DragEventArgs e)// 拖放动作
        {
            string image_name = (e.Data.GetData(DataFormats.FileDrop, false) as string[])[0];// 获取第一个文件名
            if (ApiFunction.AcceptFormat2(image_name))
            {
                online_picturebox.ImageLocation = image_name;
                online_image_path_textbox.Text = image_name;
                search_bar.Value = 0;
                progress_label.Text = "已选择图片";
            }
            else MessageBox.Show(ApiFunction.GetError("216201"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);// 扩展名不受支持，获取错误提示
        }

        private void online_clear_button_Click(object sender, EventArgs e)// 清除搜索图片
        {
            if (search_background.IsBusy)// 在线搜索异步调用
            {
                MessageBox.Show("请先停止搜索", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            online_picturebox.Image = null;
            search_bar.Value = 0;
            online_image_path_textbox.Text = "";
            progress_label.Text = "等待用户操作";
        }
        #endregion 在线搜索

        #region 本地搜索
        private Api outline_api;// 本地图库

        private string local_image_path;// 本地图库路径

        private void local_image_combobox_SelectedIndexChanged(object sender, EventArgs e)// 本地图库选择时
        {
            outline_api = ApiFunction.GetApi(local_image_combobox.Text);
            if (outline_api != null) local_image_path = outline_api.Path;
        }

        private void LocalListviewSettings()// 本地搜索结果列表
        {
            local_listview.Columns.Add("文件名");
            local_listview.Columns.Add("位置");
            local_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void local_search_button_Click(object sender, EventArgs e)// 本地匹配查找按钮
        {
            if (local_type_textbox.Text == "")
            {
                MessageBox.Show("请输入内容", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (local_image_combobox.Text == "")
            {
                MessageBox.Show("未选择图库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(outline_api.Path))
            {
                MessageBox.Show(outline_api.Path + " 目录不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MethodInfo function = new Reflections().Compiler(outline_api.Table);// 传入类名调用实时编译
            if (function == null) return;
            string new_type = (string)function.Invoke(null, new object[] { local_type_textbox.Text });// 调用函数获取返回值
            if (local_type_textbox.Text == new_type)
            {
                MessageBox.Show(local_type_textbox.Text + "不是" + local_image_combobox.Text);
                return;
            }

            if (local_listview.Items.Count > 0)// 清空列表
            {
                local_listview.Clear();
                LocalListviewSettings();
            }

            string path = Path.GetDirectoryName(outline_api.Path + new_type);// 目录
            string type = local_type_textbox.Text;// 订单号

            search_bar.Value = 50;
            progress_label.Text = "查找中...";

            List<string> list = new List<string>();// 返回的文件列表
            if (Directory.Exists(path))
            {
                #region 获取文件列表
                Stack<string> stack = new Stack<string>(20);// 栈
                stack.Push(path);// 主目录入栈
                while (stack.Count > 0)// 栈不为空时遍历
                {
                    string main_path = stack.Pop();// 取栈中第一个目录

                    string[] sub_paths = null;
                    try { sub_paths = Directory.GetDirectories(main_path); }// 栈目录的子目录列表
                    #region 异常
                    catch (UnauthorizedAccessException ex)
                    {
                        if (MessageBox.Show("无权限操作，请尝试使用管理员权限运行本程序，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                        else return;
                    }
                    catch (FileNotFoundException ex)
                    {
                        if (MessageBox.Show("文件或文件夹不存在，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续归类？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                        else return;
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show("发生未知如下错误\r\n\r\n" + ex + "\r\n\r\n是否继续归类？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                        else return;
                    }
                    #endregion 异常

                    string[] files = null;
                    try { files = Directory.GetFiles(main_path); }// 栈目录的文件列表
                    #region 异常
                    catch (UnauthorizedAccessException ex)
                    {
                        if (MessageBox.Show("无权限操作，请尝试使用管理员权限运行本程序，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                        else return;
                    }
                    catch (FileNotFoundException ex)
                    {
                        if (MessageBox.Show("文件或文件夹不存在，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续归类？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                        else return;
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show("发生未知如下错误\r\n\r\n" + ex + "\r\n\r\n是否继续归类？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) continue;
                        else return;
                    }
                    #endregion 异常

                    if (files.Length > 0) foreach (string file in files) list.Add(file);// 栈目录的文件遍历到List
                    if (sub_paths.Length > 0) foreach (string sub_path in sub_paths) stack.Push(sub_path);// 栈目录的子目录列表入栈
                }
                #endregion 获取文件列表
            }
            else
            {
                MessageBox.Show(type + "订单号正确，但目录 " + path + " 不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                search_bar.Value = 100;
                progress_label.Text = "查找完成";
                return;
            }

            foreach (string s in list)// 遍历
            {
                if (Path.GetFileName(s).Contains(type))// 只获取包含订单号的文件
                {
                    ListViewItem item = local_listview.Items.Add(Path.GetFileName(s));// 文件名
                    item.SubItems.Add(s);// 位置
                }
            }

            if (local_listview.Items.Count > 0) local_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
            else MessageBox.Show(type + "订单号正确，但目录 " + path + " 内没有查找到相关文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            search_bar.Value = 100;
            progress_label.Text = "查找完成";
        }

        private void local_search2_button_Click(object sender, EventArgs e)// 本地相似查找按钮
        {
            if (local_type_textbox.Text == "")
            {
                MessageBox.Show("请输入内容", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (local_image_combobox.Text == "")
            {
                MessageBox.Show("未选择图库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(outline_api.Path))
            {
                MessageBox.Show(outline_api.Path + " 目录不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (local_listview.Items.Count > 0)// 清空结果
            {
                local_listview.Clear();
                LocalListviewSettings();
            }

            if (local_search2_button.Text == "数据库查找")
            {
                local_search2_button.Text = "停止";
                if (!outline_background.IsBusy)// 后台占用
                {
                    var back = new object[3];
                    back[0] = outline_api;
                    back[1] = local_image_combobox.Text;
                    back[2] = local_type_textbox.Text;
                    outline_background.RunWorkerAsync(back);// 传入
                    search_bar.Value = 1;
                    progress_label.Text = "% 开始查找...";
                }
            }
            else
            {
                local_search2_button.Text = "数据库查找";
                if (outline_background.IsBusy) outline_background.CancelAsync();
            }
        }

        private void outline_background_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)// 相似搜索后台开始
        {
            var back = e.Argument as object[];// 接收
            Api api = (Api)back[0];
            string depot_name = (string)back[1];
            string type = (string)back[2];
            string select = "SELECT Names, Path FROM " + api.Table + " WHERE Names LIKE '%" + type.Replace("'", "''") + "%'";
            DataTable datatable = Sql.Select(depot_name, select);
            if (datatable == null)
            {
                MessageBox.Show("数据库操作错误，结束搜索", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable result = new DataTable();
            result.Columns.Add("文件名", Type.GetType("System.String"));
            result.Columns.Add("位置", Type.GetType("System.String"));
            foreach (DataRow datarows in datatable.Rows)
            {
                string path = Path.Combine(api.Path, datarows["Path"].ToString());
                string name = Path.GetFileName(path);
                DataRow datarow;
                datarow = result.NewRow();
                datarow["文件名"] = name;
                datarow["位置"] = path;
                result.Rows.Add(datarow);
            }

            e.Result = result;// 传出
        }

        private void outline_background_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            search_bar.Value = (e.ProgressPercentage < 101) ? e.ProgressPercentage : search_bar.Value;
            progress_label.Text = search_bar.Value.ToString() + "% 正在查找：" + e.UserState as string;
        }

        private void outline_background_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            local_search2_button.Text = "数据库查找";

            if (e.Error != null)
            {
                MessageBox.Show("查找文件错误如下\r\n\r\n" + e.Error.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progress_label.Text = "查找文件后台错误";
                return;
            }

            if (e.Cancelled)
            {
                MessageBox.Show("查找已取消", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                progress_label.Text = "查找已取消";
                return;
            }

            DataTable datatable = e.Result as DataTable;// 接收

            if (datatable == null)// 空数据
            {
                search_bar.Value = 100;
                MessageBox.Show("查找失败，返回了空结果", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                progress_label.Text = "查找失败，返回了空结果";
                return;
            }

            //0结果
            if (datatable.Rows.Count == 0)
            {
                search_bar.Value = 100;
                MessageBox.Show("没有找到相关文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                progress_label.Text = "没有找到相关文件";
                return;
            }

            //遍历
            local_listview.BeginUpdate();// 挂起UI，避免闪烁并提速
            ListTable.ToView(datatable, local_listview);
            local_listview.EndUpdate();// 绘制UI。
            local_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);// 自动列宽
            local_listview.Items[local_listview.Items.Count - 1].EnsureVisible();// 定位尾部

            search_bar.Value = 100;
            progress_label.Text = "查找完成";
        }

        private void local_open_button_Click(object sender, EventArgs e)// 从列表打开按钮
        {
            if (local_listview.SelectedItems.Count < 1)// 没有选中文件
            {
                MessageBox.Show("没有选中文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (File.Exists(local_listview.SelectedItems[0].SubItems[1].Text))// 打开
            {
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
                psi.Arguments = "/e,/select," + local_listview.SelectedItems[0].SubItems[1].Text;
                System.Diagnostics.Process.Start(psi);
            }
            else MessageBox.Show("文件不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void local_listview_DoubleClick(object sender, EventArgs e)// 双击列表
        {
            if (local_listview.SelectedItems.Count < 1) return;// 空列表
            if (File.Exists(local_listview.SelectedItems[0].SubItems[1].Text)) System.Diagnostics.Process.Start(local_listview.SelectedItems[0].SubItems[1].Text);// 打开
            else MessageBox.Show("文件不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion 本地搜索

        #region 工具菜单
        private void app_close_menu_Click(object sender, EventArgs e)// 程序退出菜单
        {
            System.Environment.Exit(0);
            return;
        }

        private void app_about_menu_Click(object sender, EventArgs e)// 程序关于菜单
        {
            MessageBox.Show("程序名：" + Text + "\r\n版本：" + Application.ProductVersion + "\r\n开发者：Jinzhenting@Aliyun.com\r\n", "关于", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void app_updata_menuItem_Click(object sender, EventArgs e)// 程序更新菜单
        {
            AppUpdata(true);
        }

        private void delete_empty_menuItem_Click(object sender, EventArgs e)// 删除空白文件夹菜单
        {
            EmptyFolder();
        }
        private void emptyfolder_button_Click(object sender, EventArgs e)// 删除空白文件夹按钮
        {
            EmptyFolder();
        }
        private void EmptyFolder()// 删除空白文件夹功能
        {
            EmptyFolderForm emptyfolderform = new EmptyFolderForm();
            emptyfolderform.ShowDialog();
        }

        private void sort_menuItem_Click(object sender, EventArgs e)// 图片整理菜单
        {
            Sort();
        }
        private void sort_button_Click(object sender, EventArgs e)// 图片整理按钮
        {
            Sort();
        }
        private void Sort()// 图片整理功能
        {
            ImageSortForm sortform = new ImageSortForm();
            sortform.ShowDialog();
        }

        private void api_settings_menuItem_Click(object sender, EventArgs e)// 接口配置菜单
        {
            ApiSettings();
        }
        private void api_setting_button_Click(object sender, EventArgs e)// 接口配置按钮
        {
            ApiSettings();
        }
        private void ApiSettings()// 接口配置功能
        {
            ApiSettingsForm apiform = new ApiSettingsForm();
            apiform.ShowDialog();
        }

        private void image_add_menuItem_Click(object sender, EventArgs e)// 图库入库菜单
        {
            ImageAdd();
        }
        private void image_add_button_Click(object sender, EventArgs e)// 图库入库按钮
        {
            ImageAdd();
        }
        private void ImageAdd()// 图库入库功能
        {
            ImageAddForm addform = new ImageAddForm();
            addform.ShowDialog();
        }

        private void image_up_menuItem_Click(object sender, EventArgs e)// 图库更新菜单
        {
            ImageUp();
        }
        private void image_up_button_Click(object sender, EventArgs e)// 图库更新按钮
        {
            ImageUp();
        }
        private void ImageUp()// 图库更新功能
        {
            ImageUpForm imageupform = new ImageUpForm();
            imageupform.ShowDialog();
        }

        private void image_delete_menuItem_Click(object sender, EventArgs e)// 图库删除菜单
        {
            ImageDelete();
        }
        private void image_delete_button_Click(object sender, EventArgs e)// 图库删除按钮
        {
            ImageDelete();
        }
        private void ImageDelete()// 图库删除功能
        {
            ImageDeleteForm deleteform = new ImageDeleteForm();
            deleteform.ShowDialog();
        }

        private void help_stripmenu_Click(object sender, EventArgs e)// 程序帮助菜单
        {
            Help();
        }
        private void help_button_Click(object sender, EventArgs e)// 程序帮助按钮
        {
            Help();
        }
        private void Help()// 程序帮助功能
        {
            HelpForm helpform = new HelpForm();
            helpform.ShowDialog();
        }

        private void settings_stripmenu_Click(object sender, EventArgs e)// 程序选项菜单
        {
            SettingsForm SettingsForm = new SettingsForm();
            SettingsForm.AppUpPath = app_up_path;
            SettingsForm.ShowDialog();
        }

        private void imagebox_menustrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)// 图片右键菜单定义
        {
            if (online_picturebox.Image == null) imagebox_copy_menuitem.Enabled = false;// 复制
            else imagebox_copy_menuitem.Enabled = true;

            Image img = Clipboard.GetImage();// 粘贴
            if (img == null) imagebox_paste_menuitem.Enabled = false;
            else imagebox_paste_menuitem.Enabled = true;

            if (online_picturebox.Image == null) imagebox_delete_menuitem.Enabled = false;// 删除
            else imagebox_delete_menuitem.Enabled = true;
        }

        private void imagebox_copy_menuitem_Click(object sender, EventArgs e)// 图片右键菜单复制
        {
            Clipboard.SetImage(online_picturebox.Image);
        }

        private void imagebox_paste_menuitem_Click(object sender, EventArgs e)// 图片右键菜单粘贴
        {
            Image img = Clipboard.GetImage();
            if (img != null)
            {
                online_picturebox.Image = img;
                online_image_path_textbox.Text = "系统剪贴板";
                search_bar.Value = 0;
                progress_label.Text = "已选择图片";
            }
            else MessageBox.Show("系统剪贴板没有图像", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void imagebox_delete_menuitem_Click(object sender, EventArgs e)// 图片右键菜单删除
        {
            online_picturebox.Image = null;
            search_bar.Value = 0;
            online_image_path_textbox.Text = "";
            progress_label.Text = "等待用户操作";
        }

        #endregion 工具菜单

        //
    }
}
