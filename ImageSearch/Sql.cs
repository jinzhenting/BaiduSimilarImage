using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;

namespace ImageSearch
{
    public static class Sql
    {
        //读取配置文件
        private static string GetConnection(string depot_name)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load("ApiList.xml");
                XmlNode rootNode = xml.DocumentElement;//获得根节点
                foreach (XmlNode xmlnode in rootNode.ChildNodes) if (xmlnode.Name == depot_name) return "Server=" + xmlnode.Attributes["serverip"].Value + "; Initial Catalog=" + xmlnode.Attributes["dataname"].Value + "; User ID=" + xmlnode.Attributes["userid"].Value + "; Password=" + xmlnode.Attributes["password"].Value;//在根节点中寻找节点//找到对应的节点//获取对应节点的值
                MessageBox.Show("图库配置文件中查找不到" + depot_name + "的内容");
                return null;
            }
            catch (UnauthorizedAccessException ex)
            {
                if (MessageBox.Show("无权限访问图库配置文件，请尝试使用管理员权限运行本程序，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK) return null;
                else
                {
                    System.Environment.Exit(0);
                    return null;
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                if (MessageBox.Show("图库配置文件不存在，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK) return null;
                else
                {
                    System.Environment.Exit(0);
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("访问图库配置文件时发生错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK) return null;
                else
                {
                    System.Environment.Exit(0);
                    return null;
                }
            }
        }

        //SELECT = "SELECT * FROM 表 WHERE 列='值'";
        public static DataTable Select(string depot_name, string select)
        {
            try
            {
                string server = GetConnection(depot_name);
                SqlDataAdapter sqldataadapter = new SqlDataAdapter();
                SqlConnection sqlconnection = new SqlConnection(server);
                SqlCommand sqlcommand = new SqlCommand(select, sqlconnection);
                sqlconnection.Open();
                sqldataadapter.SelectCommand = sqlcommand;
                DataTable datatable = new DataTable();
                if (datatable.Rows.Count > 0) datatable.Clear();
                sqldataadapter.Fill(datatable);
                sqlconnection.Close();
                sqlconnection.Dispose();
                return datatable;
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains("error number:102"))
                {
                    if (MessageBox.Show("查询语句\r\n\r\n" + select + "\r\n\r\n语法错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK) return null;
                    else
                    {
                        System.Environment.Exit(0);
                        return null;
                    }
                }
                else if (ex.ToString().ToLower().Contains("error number:53"))
                {
                    if (MessageBox.Show("查询时无法连接数据库服务，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK) return null;
                    else
                    {
                        System.Environment.Exit(0);
                        return null;
                    }
                }
                else if (ex.ToString().ToLower().Contains("error number:4060"))
                {
                    if (MessageBox.Show("查询时数据库名称不存在，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK) return null;
                    else
                    {
                        System.Environment.Exit(0);
                        return null;
                    }
                }
                else if (ex.ToString().ToLower().Contains("error number:18456"))
                {
                    if (MessageBox.Show("查询时数据库用户名或密码错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK) return null;
                    else
                    {
                        System.Environment.Exit(0);
                        return null;
                    }
                }
                else
                {
                    if (MessageBox.Show("查询时数据库返回错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK) return null;
                    else
                    {
                        System.Environment.Exit(0);
                        return null;
                    }
                }
            }
        }

        //UPDATE = "UPDATE 表 SET 列='值', 列='值', 列='值' WHERE 列='值'";
        public static void Up(string depot_name, string up)
        {
            try
            {
                string server = GetConnection(depot_name);
                SqlConnection sqlconnection = new SqlConnection(server);
                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(up, sqlconnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlconnection.Close();
                sqlconnection.Dispose();
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains("error number:102")) if (MessageBox.Show("更新数据语句\r\n\r\n" + up + "\r\n\r\n语法错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (ex.ToString().ToLower().Contains("error number:53")) if (MessageBox.Show("更新数据时无法连接数据库服务，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (ex.ToString().ToLower().Contains("error number:4060")) if (MessageBox.Show("更新数据时数据库名称不存在，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (ex.ToString().ToLower().Contains("error number:18456")) if (MessageBox.Show("更新数据时数据库用户名或密码错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (MessageBox.Show("更新数据时数据库返回错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
            }
        }

        //INSERT = "INSERT INTO 表 (列,列,列) VALUES('值','值','值')";
        public static void Insert(string depot_name, string insert)
        {
            try
            {
                string server = GetConnection(depot_name);
                SqlConnection sqlconnection = new SqlConnection(server);
                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(insert, sqlconnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlconnection.Close();
                sqlconnection.Dispose();
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains("error number:102")) if (MessageBox.Show("录入数据语句\r\n\r\n" + insert + "\r\n\r\n语法错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (ex.ToString().ToLower().Contains("error number:53")) if (MessageBox.Show("录入数据时无法连接数据库服务，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (ex.ToString().ToLower().Contains("error number:4060")) if (MessageBox.Show("录入数据时数据库名称不存在，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (ex.ToString().ToLower().Contains("error number:18456")) if (MessageBox.Show("录入数据时数据库用户名或密码错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (MessageBox.Show("录入数据时数据库返回错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
            }
        }

        //DELETE FROM 表名称 WHERE 列名称 = 值
        public static void Delete(string depot_name, string delete)
        {
            try
            {
                string server = GetConnection(depot_name);
                SqlConnection sqlconnection = new SqlConnection(server);
                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(delete, sqlconnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlconnection.Close();
                sqlconnection.Dispose();
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains("error number:102")) if (MessageBox.Show("删除数据语句\r\n\r\n" + delete + "\r\n\r\n语法错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (ex.ToString().ToLower().Contains("error number:53")) if (MessageBox.Show("删除数据时无法连接数据库服务，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (ex.ToString().ToLower().Contains("error number:4060")) if (MessageBox.Show("删除数据时数据库名称不存在，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (ex.ToString().ToLower().Contains("error number:18456")) if (MessageBox.Show("删除数据时数据库用户名或密码错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (MessageBox.Show("删除数据时数据库返回错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
            }
        }

        //DROP TABLE 表格名;
        public static void Dropable(string depot_name, string drop)
        {
            try
            {
                string server = GetConnection(depot_name);
                SqlConnection sqlconnection = new SqlConnection(server);
                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(drop, sqlconnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlconnection.Close();
                sqlconnection.Dispose();
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains("error number:102")) if (MessageBox.Show("删除表格语句\r\n\r\n" + drop + "\r\n\r\n语法错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (ex.ToString().ToLower().Contains("error number:53")) if (MessageBox.Show("删除表格时无法连接数据库服务，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (ex.ToString().ToLower().Contains("error number:4060")) if (MessageBox.Show("删除表格时数据库名称不存在，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (ex.ToString().ToLower().Contains("error number:18456")) if (MessageBox.Show("删除表格时数据库用户名或密码错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (MessageBox.Show("删除表格时数据库返回错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
            }
        }

        //DROP TABLE 表格名;
        public static void CreateTable(Api api, string create)
        {
            try
            {
                string server = "Server=" + api.Serverip + "; Initial Catalog=" + api.Dataname + "; User ID=" + api.Userid + "; Password=" + api.Password;
                SqlConnection sqlconnection = new SqlConnection(server);
                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(create, sqlconnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlconnection.Close();
                sqlconnection.Dispose();
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains("error number:102")) if (MessageBox.Show("创建表格语句\r\n\r\n" + create + "\r\n\r\n语法错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (ex.ToString().ToLower().Contains("error number:53")) if (MessageBox.Show("创建表格时无法连接数据库服务，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (ex.ToString().ToLower().Contains("error number:4060")) if (MessageBox.Show("创建表格时数据库名称不存在，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (ex.ToString().ToLower().Contains("error number:18456")) if (MessageBox.Show("创建表格时数据库用户名或密码错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
                if (MessageBox.Show("创建表格时数据库返回错误，描述如下\r\n\r\n" + ex + "\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK) System.Environment.Exit(0);
            }
        }


    }
}
