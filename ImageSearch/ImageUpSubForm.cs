using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ImageSearch
{
    public partial class ImageUpSubForm : Form
    {
        public ImageUpSubForm()
        {
            InitializeComponent();
        }

        //Path
        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        //List
        private List<string> list;
        public List<string> List
        {
            get { return list; }
            set { list = value; }
        }

        //
        private void ImageUpSubForm_Load(object sender, EventArgs e)
        {
            //
            list = new List<string>();
            DirectoryInfo directoryinfo = new DirectoryInfo(path);
            DirectoryInfo[] directoryinfos = directoryinfo.GetDirectories();
            for (int i = 0; i < directoryinfos.Length; i++)
            {
                sub_checkedlistbox.Items.Add(directoryinfos[i].FullName);
            }
        }

        //确定按钮
        private void sub_ok_button_Click(object sender, EventArgs e)
        {
            if (list.Count>0)
            {
                list.Clear();
            }

            //选中
            for (int i = 0; i < sub_checkedlistbox.Items.Count; i++)
            {
                if (sub_checkedlistbox.GetItemChecked(i))
                {
                    list.Add(sub_checkedlistbox.GetItemText(sub_checkedlistbox.Items[i]));
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //取消按钮
        private void sub_cancel_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //全不选
        private void sub_clear_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sub_checkedlistbox.Items.Count; i++)
            {
                sub_checkedlistbox.SetItemChecked(i, false);
            }
        }

        //全选
        private void sub_all_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <sub_checkedlistbox.Items.Count; i++)
            {
               sub_checkedlistbox.SetItemChecked(i, true);
            }
        }
        
        //
    }
}
