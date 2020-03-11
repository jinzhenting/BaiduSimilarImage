using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ImageSearch
{
    public static class Sql
    {
        /// <summary>
        /// 读取SQL链接配置
        /// </summary>
        /// <param name="depot">图库名</param>
        /// <returns>SQL链接配置</returns>
        private static string GetConnection(string depot)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(@"Documents\ApiList.xml");
                XmlNode rootNode = xml.DocumentElement;// 获得根节点
                foreach (XmlNode xmlnode in rootNode.ChildNodes) if (xmlnode.Name == depot) return "Server=" + xmlnode.Attributes["serverip"].Value + "; Initial Catalog=" + xmlnode.Attributes["dataname"].Value + "; User ID=" + xmlnode.Attributes["userid"].Value + "; Password=" + Password.Decrypt(xmlnode.Attributes["password"].Value, "12345678");// 在根节点中寻找节点//找到对应的节点//获取对应节点的值
                MessageBox.Show("图库配置文件中查找不到" + depot + "的内容，程序将自动退出，请检查", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Environment.Exit(0);
                return null;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("无权限访问图库配置文件，请尝试使用管理员权限运行本程序，程序将自动退出，描述如下\r\n\r\n" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return null;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("图库配置文件不存在，程序将自动退出，描述如下\r\n\r\n" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("访问图库配置文件时发生错误，程序将自动退出，描述如下\r\n\r\n" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return null;
            }
        }

        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="depot">图库名</param>
        /// <param name="select">SELECT * FROM 表 WHERE 列='值'</param>
        /// <returns>数据表</returns>
        public static DataTable Select(string depot, string select)
        {
            try
            {
                string server = GetConnection(depot);
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
                MessageBox.Show("数据库操作错误，描述如下\r\n\r\n" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// UPDATE
        /// </summary>
        /// <param name="depot">图库名</param>
        /// <param name="up">UPDATE 表 SET 列='值', 列='值' WHERE 列='值'</param>
        /// <returns></returns>
        public static bool Up(string depot, string up)
        {
            try
            {
                string server = GetConnection(depot);
                SqlConnection sqlconnection = new SqlConnection(server);
                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(up, sqlconnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlconnection.Close();
                sqlconnection.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库操作错误，描述如下\r\n\r\n" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// INSERT
        /// </summary>
        /// <param name="depot">图库名</param>
        /// <param name="insert">INSERT INTO 表 (列,列,列) VALUES('值','值','值')</param>
        /// <returns></returns>
        public static bool Insert(string depot, string insert)
        {
            try
            {
                string server = GetConnection(depot);
                SqlConnection sqlconnection = new SqlConnection(server);
                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(insert, sqlconnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlconnection.Close();
                sqlconnection.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库操作错误，描述如下\r\n\r\n" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="depot">图库名</param>
        /// <param name="delete">DELETE FROM 表名称 WHERE 列名称 = 值</param>
        /// <returns></returns>
        public static bool Delete(string depot, string delete)
        {
            try
            {
                string server = GetConnection(depot);
                SqlConnection sqlconnection = new SqlConnection(server);
                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(delete, sqlconnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlconnection.Close();
                sqlconnection.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库操作错误，描述如下\r\n\r\n" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// DROP TABLE
        /// </summary>
        /// <param name="depot">图库名</param>
        /// <param name="drop">DROP TABLE 表名</param>
        /// <returns></returns>
        public static bool Dropable(string depot, string drop)
        {
            try
            {
                string server = GetConnection(depot);
                SqlConnection sqlconnection = new SqlConnection(server);
                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(drop, sqlconnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlconnection.Close();
                sqlconnection.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库操作错误，描述如下\r\n\r\n" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// CREATE TABLE
        /// </summary>
        /// <param name="api">API</param>
        /// <param name="create">CREATE TABLE 表(列 表配置, 列 表配置)</param>
        /// <returns></returns>
        public static bool CreateTable(Api api, string create)
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
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库操作错误，描述如下\r\n\r\n" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
