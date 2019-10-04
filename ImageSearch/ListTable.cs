using System;
using System.Data;
using System.Windows.Forms;

namespace ImageSearch
{
    public static class ListTable
    {
        //
        public static void ToView(DataTable datatable, ListView listview)
        {
            try
            {
                //清空
                if (datatable != null)
                {
                    listview.Items.Clear();
                    listview.Columns.Clear();
                }

                //表头
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    listview.Columns.Add(datatable.Columns[i].Caption.ToString());
                }

                //行
                foreach (DataRow datarow in datatable.Rows)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.SubItems[0].Text = datarow[0].ToString();
                    for (int i = 1; i < datatable.Columns.Count; i++)
                    {
                        lvi.SubItems.Add(datarow[i].ToString());
                    }
                    listview.Items.Add(lvi);
                }

                //自动宽listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据转换错误\r\n" + ex.ToString());
            }
        }

        //
        public static void ToTable(ListView listview, DataTable datatable)
        {
            try
            {
                //清空
                if (datatable.Rows.Count > 0)
                {
                    datatable.Clear();
                    datatable.Columns.Clear();
                }

                int x, y;
                //表头
                for (x = 0; x < listview.Columns.Count; x++)
                {
                    datatable.Columns.Add(listview.Columns[x].Text.Trim(), typeof(string));
                }

                //行
                DataRow datarow;
                for (x = 0; x < listview.Items.Count; x++)
                {
                    datarow = datatable.NewRow();
                    for (y = 0; y < listview.Columns.Count; y++)
                    {
                        datarow[y] = listview.Items[x].SubItems[y].Text.Trim();
                    }
                    datatable.Rows.Add(datarow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据转换错误\r\n" + ex.ToString());
            }
        }

    }
}
