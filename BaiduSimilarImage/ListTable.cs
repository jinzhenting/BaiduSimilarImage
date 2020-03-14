using System;
using System.Data;
using System.Windows.Forms;

namespace BaiduSimilarImage
{
    /// <summary>
    /// 二维表数据转换
    /// </summary>
    public static class ListTable
    {
        /// <summary>
        /// TataTable转ListView
        /// </summary>
        /// <param name="dataTable">传入的TataTable</param>
        /// <param name="listView">传出的ListView</param>
        public static void ToView(DataTable dataTable, ListView listView)
        {
            try
            {
                // 清空
                if (listView != null)
                {
                    listView.Items.Clear();
                    listView.Columns.Clear();
                }

                // 表头
                for (int i = 0; i < dataTable.Columns.Count; i++) listView.Columns.Add(dataTable.Columns[i].Caption.ToString());

                // 行
                foreach (DataRow datarow in dataTable.Rows)
                {
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.SubItems[0].Text = datarow[0].ToString();
                    for (int i = 1; i < dataTable.Columns.Count; i++) listViewItem.SubItems.Add(datarow[i].ToString());
                    listView.Items.Add(listViewItem);
                }

                // 自动宽ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据转换错误\r\n\r\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //
        public static void ToTable(ListView ListView, DataTable datatable)
        {
            try
            {
                // 清空
                if (datatable.Rows.Count > 0)
                {
                    datatable.Clear();
                    datatable.Columns.Clear();
                }

                int x, y;
                // 表头
                for (x = 0; x < ListView.Columns.Count; x++) datatable.Columns.Add(ListView.Columns[x].Text.Trim(), typeof(string));

                // 行
                DataRow datarow;
                for (x = 0; x < ListView.Items.Count; x++)
                {
                    datarow = datatable.NewRow();
                    for (y = 0; y < ListView.Columns.Count; y++) datarow[y] = ListView.Items[x].SubItems[y].Text.Trim();
                    datatable.Rows.Add(datarow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据转换错误\r\n\r\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
