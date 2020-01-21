using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace ImageSearch
{
    public static class ApiFunction
    {
        public static List<string> GetDepotList()// 获取图库列表
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(@"Documents\ApiList.xml");
                XmlNodeList nodelist = xml.SelectSingleNode("//ApiList").ChildNodes;
                List<string> list = new List<string>();
                foreach (XmlNode node in nodelist) list.Add(node.Name);
                if (list != null && list.Count > 0) return list;
                return null;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("无权限访问图库配置文件，请尝试使用管理员权限运行本程序，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return null;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("图库配置文件不存在，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("访问图库配置文件时发生错误，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return null;
            }
        }

        public static bool AcceptFormat1(string path)// 格式支持，以编码检测
        {
            Image img = Image.FromFile(path);
            if (img.RawFormat.Equals(ImageFormat.Jpeg) || img.RawFormat.Equals(ImageFormat.Bmp) || img.RawFormat.Equals(ImageFormat.Png)) return true;
            else return false;
        }

        public static bool AcceptFormat2(string path)// 格式支持，以扩展名检测
        {
            if (GetFormatList() != null)
            {
                foreach (string str in GetFormatList()) if (Path.GetExtension(path).ToLower() == Path.GetExtension(str).ToLower()) return true;// 遍历检测扩展名匹配
                return false;
            }
            else
            {
                MessageBox.Show("未创建格式列表", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static List<string> GetFormatList()// 获取格式列表
        {
            try
            {
                XmlDocument xml = new XmlDocument();// 注意XML内容区分大小写
                xml.Load(@"Documents\FormatList.xml");
                XmlNodeList nodelist = xml.SelectSingleNode("//FormatList").ChildNodes;// 搜索定位子节点FormatList
                List<string> list = new List<string>();
                foreach (XmlNode node in nodelist) list.Add(node.Attributes["extension"].Value);// 遍历字段
                if (list.Count > 0) return list;
                return null;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("无权限访问格式列表配置文件，请尝试使用管理员权限运行本程序，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return null;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("格式列表配置文件不存在，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("访问格式列表配置文件时发生错误，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return null;
            }
        }

        public static Api GetApi(string depot_name)// 获取API配
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(@"Documents\ApiList.xml");
                XmlNode rootNode = xml.DocumentElement;// 根节点
                foreach (XmlNode xmlnode in rootNode.ChildNodes)// 在根节点中寻找节点
                {
                    if (xmlnode.Name == depot_name)// 获取对应节点的值
                    {
                        Api api = new Api();
                        api.Appid = xmlnode.Attributes["appid"].Value;
                        api.Apikey = xmlnode.Attributes["apikey"].Value;
                        api.Secreykey = xmlnode.Attributes["secreykey"].Value;
                        api.Timeout = int.Parse(xmlnode.Attributes["timeout"].Value);
                        api.Tags1 = int.Parse(xmlnode.Attributes["tags1"].Value);
                        api.Tags2 = int.Parse(xmlnode.Attributes["tags2"].Value);
                        api.Quantity = int.Parse(xmlnode.Attributes["quantity"].Value);
                        api.Path = (Regex.IsMatch(xmlnode.Attributes["path"].Value, @"[\\]$")) ? xmlnode.Attributes["path"].Value : xmlnode.Attributes["path"].Value + @"\";
                        api.Serverip = xmlnode.Attributes["serverip"].Value;
                        api.Dataname = xmlnode.Attributes["dataname"].Value;
                        api.Userid = xmlnode.Attributes["userid"].Value;
                        api.Password = Password.Decrypt(xmlnode.Attributes["password"].Value, "12345678");
                        api.Table = xmlnode.Attributes["table"].Value;
                        return api;
                    }
                }
                MessageBox.Show("图库配置文件中查找不到" + depot_name + "的内容，请检查", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return null;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("无权限访问图库配置文件，请尝试使用管理员权限运行本程序，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return null;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("图库配置文件不存在，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("访问图库配置文件时发生错误，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return null;
            }
        }

        public static bool SaveApi(string depot_name, Api api)// 保存API配置
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(@"Documents\ApiList.xml");
                XmlNode rootNode = xml.DocumentElement;// 获得根节点
                foreach (XmlNode xmlnode in rootNode.ChildNodes)// 在根节点中寻找节点
                {
                    if (xmlnode.Name == depot_name)// 保存对应节点的值
                    {
                        xmlnode.Attributes["appid"].Value = api.Appid;
                        xmlnode.Attributes["apikey"].Value = api.Apikey;
                        xmlnode.Attributes["secreykey"].Value = api.Secreykey;
                        xmlnode.Attributes["timeout"].Value = api.Timeout.ToString();
                        xmlnode.Attributes["tags1"].Value = api.Tags1.ToString();
                        xmlnode.Attributes["tags2"].Value = api.Tags2.ToString();
                        xmlnode.Attributes["quantity"].Value = api.Quantity.ToString();
                        xmlnode.Attributes["path"].Value = api.Path;
                        xmlnode.Attributes["serverip"].Value = api.Serverip;
                        xmlnode.Attributes["dataname"].Value = api.Dataname;
                        xmlnode.Attributes["userid"].Value = api.Userid;
                        xmlnode.Attributes["password"].Value = Password.Encrypt(api.Password, "12345678");
                        xmlnode.Attributes["table"].Value = api.Table;
                        xml.Save(@"Documents\ApiList.xml");
                        return true;
                    }
                }
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("无权限访问图库配置文件，请尝试使用管理员权限运行本程序，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return false;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("图库配置文件不存在，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("访问图库配置文件时发生错误，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return false;
            }
        }

        public static Baidu.Aip.ImageSearch.ImageSearch GetClient(string appid, string apikey, string secreykey, int timeout)// 获取GetClient
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                var app_id = appid;
                var api_key = apikey;
                var secrey_key = secreykey;
                var clients = new Baidu.Aip.ImageSearch.ImageSearch(api_key, secrey_key);
                clients.Timeout = timeout * 1000;// 超时时间
                return clients;
            }
            catch (Exception ex)
            {
                MessageBox.Show("接口配置错误，信息如下\n\r" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static string FileToBase64(string fileName)// 字节转换，来自路径
        {
            try
            {
                FileStream filestream = new FileStream(fileName, FileMode.Open);
                byte[] arr = new byte[filestream.Length];
                filestream.Read(arr, 0, (int)filestream.Length);
                string baser64 = Convert.ToBase64String(arr);
                filestream.Close();
                return baser64;
            }
            catch (Exception ex)
            {
                MessageBox.Show("字节转换错误，信息如下\n\r" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static byte[] ImagetoByte(Image image)// 字节转换，来自Image
        {
            try
            {
                ImageFormat imageformat = image.RawFormat;
                using (MemoryStream memorystream = new MemoryStream())
                {
                    if (imageformat.Equals(ImageFormat.Jpeg)) image.Save(memorystream, ImageFormat.Jpeg);
                    else if (imageformat.Equals(ImageFormat.Png)) image.Save(memorystream, ImageFormat.Png);
                    else if (imageformat.Equals(ImageFormat.Bmp)) image.Save(memorystream, ImageFormat.Bmp);
                    else if (imageformat.Equals(ImageFormat.Gif)) image.Save(memorystream, ImageFormat.Gif);
                    else image.Save(memorystream, ImageFormat.Jpeg);
                    byte[] buffer = new byte[memorystream.Length];
                    memorystream.Seek(0, SeekOrigin.Begin);// Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                    memorystream.Read(buffer, 0, buffer.Length);
                    return buffer;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("字节转换错误，信息如下\n\r" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static string GetError(string error_number)// 错误代码
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(@"Documents\ApiErrorList.xml");
                XmlNode xmlnode = xml.DocumentElement;// 获得根节点
                foreach (XmlNode node in xmlnode.ChildNodes) if (node.Name == "Error" + error_number) return node.Attributes["chs"].Value;// 在根节点中寻找节点
                MessageBox.Show("查询不到错误代码，程序将自动退出，请检查", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                System.Environment.Exit(0);
                return null;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("无权限访问错误代码配置文件，请尝试使用管理员权限运行本程序，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return null;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("错误代码配置文件不存在，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("访问错误代码配置文件时发生错误，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return null;
            }
        }

        public static string UpIgnore(string error_number)// Ignore
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(@"Documents\ApiErrorList.xml");
                XmlNode xmlnode = xml.DocumentElement;// 获得根节点
                foreach (XmlNode node in xmlnode.ChildNodes) if (node.Name == "Error" + error_number) return node.Attributes["ignore"].Value;// 在根节点中寻找节点
                MessageBox.Show("查询不到错误代码Ignore节点，程序将自动退出，请检查", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                System.Environment.Exit(0);
                return null;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("无权限访问错误代码配置文件，请尝试使用管理员权限运行本程序，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return null;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("错误代码配置文件不存在，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("访问错误代码配置文件时发生错误，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return null;
            }
        }

        public static JObject Up(Baidu.Aip.ImageSearch.ImageSearch imagesearch, string path, string name, int tags1, int tags2)// 入库
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                var image = File.ReadAllBytes(path);
                var result = imagesearch.SimilarAdd(image);
                var options = new Dictionary<string, object> {//如果有可选参数
                        { "brief", name},
                        { "tags", tags1+","+tags2 }
                    };
                return result = imagesearch.SimilarAdd(image, options);// 带参数调用相似图检索—入库, 图片参数为本地图片
            }
            catch (Exception ex)
            {
                MessageBox.Show("API链接错误，如果其他图片能正常入库，此错误可能是图片编码错误造成的，描述如下\r\n\r\n" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static JObject Search(Baidu.Aip.ImageSearch.ImageSearch imagesearch, byte[] bytes, int tags1, int tags2, int quantity)// 搜索
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;// 安全链接类型，未解决XP问题//ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;                               
                var result = imagesearch.SimilarSearch(bytes);
                var options = new Dictionary<string, object> {
                        { "tags", tags1+","+tags2 },
                        { "tag_logic", "0" },
                        { "pn", "0" },
                        { "rn", quantity }
                    };
                return result = imagesearch.SimilarSearch(bytes, options);
            }
            catch (Exception ex)
            {
                MessageBox.Show("API链接错误如下\r\n\r\n" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static JObject DeleteByImage(string path, Baidu.Aip.ImageSearch.ImageSearch client)// 删除，以图片
        {
            try
            {
                var image = File.ReadAllBytes(path);
                var result = client.SimilarDeleteByImage(image);
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("API链接错误如下\r\n\r\n" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static JObject DeleteBySian(string sign, Baidu.Aip.ImageSearch.ImageSearch client)// 删除，以图片签名
        {
            try
            {
                var result = client.SimilarDeleteBySign(sign);
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("API链接错误如下\r\n\r\n" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static bool AddDepot(string depot_name, Api api)// 创建图库配置
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(@"Documents\ApiList.xml");
                XmlNode xmlnode = xmlDoc.DocumentElement;// 定位根节点，用于追加新节点
                XmlElement element = xmlDoc.CreateElement(depot_name);// 创建节点
                element.InnerText = "";// 空白串联值，不设置不生成一对节点
                element.SetAttribute("appid", api.Appid);// 创建属性
                element.SetAttribute("apikey", api.Apikey);
                element.SetAttribute("secreykey", api.Secreykey);
                element.SetAttribute("timeout", api.Timeout.ToString());
                element.SetAttribute("tags1", api.Tags1.ToString());
                element.SetAttribute("tags2", api.Tags2.ToString());
                element.SetAttribute("quantity", api.Quantity.ToString());
                element.SetAttribute("path", api.Path);
                element.SetAttribute("serverip", api.Serverip);
                element.SetAttribute("dataname", api.Dataname);
                element.SetAttribute("userid", api.Userid);
                element.SetAttribute("password", Password.Encrypt(api.Password, "12345678"));
                element.SetAttribute("table", api.Table);
                xmlnode.AppendChild(element);// 追加到结尾
                xmlDoc.Save(@"Documents\ApiList.xml");
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("无权限访问图库配置文件，请尝试使用管理员权限运行本程序，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return false;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("图库配置文件不存在，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("访问图库配置文件时发生错误，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return false;
            }
        }

        public static bool DeleteDepot(string depot_name)// 删除图库配置
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(@"Documents\ApiList.xml");
                XmlNode xmlnode = xmlDoc.DocumentElement;// 定位根节点
                var element = xmlDoc.SelectSingleNode("//ApiList/" + depot_name);// 搜索节点
                xmlnode.RemoveChild(element);// 清除
                xmlDoc.Save(@"Documents\ApiList.xml");
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("无权限访问图库配置文件，请尝试使用管理员权限运行本程序，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return false;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("图库配置文件不存在，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("访问图库配置文件时发生错误，程序将自动退出，描述如下\r\n\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
                return false;
            }
        }

    }
}
