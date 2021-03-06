﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace BaiduSimilarImage
{
    public partial class ApiSearchResultForm : Form
    {
        /// <summary>
        /// 结果列表
        /// </summary>
        private List<string> briefList;

        /// <summary>
        /// 本地图库模式
        /// </summary>
        private string imageDepot;

        /// <summary>
        /// 本地主目录
        /// </summary>
        private string imagePath;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inBriefList">API结果列表Json["brief"]</param>
        /// <param name="inDepot">图库</param>
        /// <param name="inPath">图库位置</param>
        public ApiSearchResultForm(List<string> inBriefList, string inDepot, string inPath)
        {
            InitializeComponent();
            briefList = inBriefList;
            imageDepot = inDepot;
            imagePath = inPath;
        }

        /// <summary>
        /// 窗口图标设置
        /// </summary>
        private void IconSetting()
        {
            try
            {
                Icon = new Icon(Path.Combine(Application.StartupPath, @"Skin\Icon.ico"));
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

        /// <summary>
        /// 窗口载入时
        /// </summary>
        private void SearchBEForm_Load(object sender, EventArgs e)
        {
            IconSetting();
            IconNullBool();
        }

        /// <summary>
        /// 窗口显示时
        /// </summary>
        private void SearchBEForm_Shown(object sender, EventArgs e)
        {
            iconBackStartr();
        }

        /// <summary>
        /// 项目缺失缩略图检测
        /// </summary>
        private void IconNullBool()
        {
            if (!(File.Exists(@"Skin\ListIcon.jpg")))
            {
                MessageBox.Show("缩略图缺失图标载入错误，程序将自动退出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return;
            }
        }

        /// <summary>
        /// 异步调用
        /// </summary>
        private void iconBackStartr()
        {
            ///
            if (iconBack.IsBusy)
            {
                MessageBox.Show("异步后台被占用", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ///
            iconBack.RunWorkerAsync();
            progressLabel.Text = "% 开始载入缩略图...";
        }

        /// <summary>
        /// 异步实现
        /// </summary>
        private void iconBack_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                ImageList imagelist = new ImageList();// 定义项目列表
                imagelist.ImageSize = new Size(256, 256);
                imagelist.ColorDepth = ColorDepth.Depth32Bit;
                
                for (int i = 0; i < briefList.Count; i++)// 遍历API结果
                {
                    if (iconBack.CancellationPending)// 检测后台取消
                    {
                        e.Cancel = true;
                        return;
                    }
                    string imagepath = Path.Combine(this.imagePath, briefList[i]);
                    if (File.Exists(imagepath)) imagelist.Images.Add(ZoomImage(Image.FromFile(imagepath), 256, 256));// 等比缩放图片// 加载缩略图
                    else imagelist.Images.Add(Image.FromFile(@"Skin\ListIcon.jpg"));// 加载缺失缩略图
                    //进度
                    iconBack.ReportProgress(Percents.Get(i + 1, briefList.Count), briefList[i]);
                }
                
                e.Result = imagelist;
            }
            catch (Exception ex)
            {
                MessageBox.Show("缩略图载入错误，描述如下\r\n\r\n" + ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// 异步进度
        /// </summary>
        private void iconBack_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            progressLabel.Text = progressBar.Value + "% 载入缩略图" + e.UserState as string;
        }

        /// <summary>
        /// 异步完成
        /// </summary>
        private void iconBack_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            openToolStripMenuItem.Enabled = true;
            copyToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Visible = false;
            stopLoadButton.Visible = false;
            openPictrueButton.Enabled = true;
            openPathButton.Enabled = true;

            if (e.Error != null)
            {
                MessageBox.Show("缩略图载入错误如下\r\n\r\n" + e.Error.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar.Value = 0;
                return;
            }

            if (e.Cancelled)
            {
                progressLabel.Text = "已取消载入缩略图";
                progressBar.Value = 0;
                return;
            }

            progressLabel.Text = "列表载入完成";
            progressBar.Value = 100;
            
            ImageList imagelist = (ImageList)e.Result;
            for (int i = 0; i < briefList.Count; i++)// 把项目名遍历到ListView
            {
                ListViewItem ListViewitem = new ListViewItem();// 定义单个项目
                ListViewitem.ImageIndex = i;
                ListViewitem.Text = Path.Combine(imagePath, briefList[i]);
                listView.Items.Add(ListViewitem);
            }
            listView.LargeImageList = imagelist;

        }

        /// <summary>
        /// 异步取消
        /// </summary>
        private void clearLoadButton_Click(object sender, EventArgs e)
        {
            if (iconBack.IsBusy) iconBack.CancelAsync();
        }

        /// <summary>
        /// 浏览文件夹按钮
        /// </summary>
        private void openPath_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count < 1)
            {
                MessageBox.Show("没有选中图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            OpenPath();
        }

        /// <summary>
        /// 双击图片打开
        /// </summary>
        private void listView_DoubleClick(object sender, EventArgs e)
        {
            OpenFile();
        }

        /// <summary>
        /// 找开文件按钮
        /// </summary>
        private void openPictrueButton_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count < 1)
            {
                MessageBox.Show("没有选中图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            OpenFile();
        }

        /// <summary>
        /// 打开图片
        /// </summary>
        private void OpenFile() {
            if (listView.SelectedItems.Count < 1) return;
            if (File.Exists(listView.SelectedItems[0].Text)) Process.Start(listView.SelectedItems[0].Text);
            else MessageBox.Show("图片" + listView.SelectedItems[0].Text + "不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 窗口关闭检测
        /// </summary>
        private void SearchBEForm_FormClosing(object sender, FormClosingEventArgs e)// 窗口关闭前停止后台
        {
            if (iconBack.IsBusy)
            {
                e.Cancel = true;
                MessageBox.Show("后台正在载入，请勿关闭窗口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// 等比缩放
        /// </summary>
        /// <param name="inImage">输入图片</param>
        /// <param name="OutHeight">输出高</param>
        /// <param name="OutWidth">输出宽</param>
        /// <returns>输出图片</returns>
        private Image ZoomImage(Image inImage, int OutHeight, int OutWidth)
        {
            try
            {
                int width = 0, height = 0;
                // 按比例缩放
                int inWidth = inImage.Width;
                int inHeight = inImage.Height;
                if (inHeight > OutHeight || inWidth > OutWidth)
                {
                    if ((inWidth * OutHeight) > (inHeight * OutWidth))
                    {
                        width = OutWidth;
                        height = (OutWidth * inHeight) / inWidth;
                    }
                    else
                    {
                        height = OutHeight;
                        width = (inWidth * OutHeight) / inHeight;
                    }
                }
                else
                {
                    width = inWidth;
                    height = inHeight;
                }
                Bitmap outBitmap = new Bitmap(OutWidth, OutHeight);
                Graphics graphics = Graphics.FromImage(outBitmap);
                graphics.Clear(Color.Transparent);
                // 设置画布的描绘质量           
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(inImage, new Rectangle((OutWidth - width) / 2, (OutHeight - height) / 2, width, height), 0, 0, inImage.Width, inImage.Height, GraphicsUnit.Pixel);
                graphics.Dispose();
                // 设置压缩质量       
                EncoderParameters encoderParams = new EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;
                EncoderParameter encoderParam = new EncoderParameter(Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                inImage.Dispose();
                return outBitmap;
            }
            catch
            {
                return inImage;
            }
        }

        /// <summary>
        /// 打开原图菜单
        /// </summary>
        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (listView.SelectedItems.Count < 1)
            {
                MessageBox.Show("没有选中图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            OpenFile();
        }

        /// <summary>
        /// 打开原图位置菜单
        /// </summary>
        private void openPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count < 1)
            {
                MessageBox.Show("没有选中图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            OpenPath();
        }

        /// <summary>
        /// 打开文件位置
        /// </summary>
        private void OpenPath()
        {
            if (File.Exists(listView.SelectedItems[0].Text))
            {
                ProcessStartInfo psi = new ProcessStartInfo("Explorer.exe");
                psi.Arguments = "/e,/select," + listView.SelectedItems[0].Text;
                Process.Start(psi);
            }
            else MessageBox.Show("图片" + listView.SelectedItems[0].Text + "不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 复制原图到剪贴版菜单
        /// </summary>
        private void copyImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count < 1)
            {
                MessageBox.Show("没有选中图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (File.Exists(listView.SelectedItems[0].Text)) Clipboard.SetImage(Image.FromFile(listView.SelectedItems[0].Text));
            else MessageBox.Show("图片" + listView.SelectedItems[0].Text + "不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 复制图片本地位置菜单
        /// </summary>
        private void copyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count < 1)
            {
                MessageBox.Show("没有选中图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            Clipboard.SetDataObject(listView.SelectedItems[0].Text);
        }

        /// <summary>
        /// 复制图片文件名菜单
        /// </summary>
        private void copyNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count < 1)
            {
                MessageBox.Show("没有选中图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            Clipboard.SetDataObject(Path.GetFileNameWithoutExtension(listView.SelectedItems[0].Text));
        }

        /// <summary>
        /// 停止载入菜单
        /// </summary>
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (iconBack.IsBusy) iconBack.CancelAsync();
        }

        /// <summary>
        /// 另存为菜单
        /// </summary>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count < 1)
            {
                MessageBox.Show("没有选中图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "图形文件(*.jpg)|*.jpg";// 文件类型
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(listView.SelectedItems[0].Text);// 默认文件名
            saveFileDialog.DefaultExt = Path.GetExtension(listView.SelectedItems[0].Text);// 默认格式
            saveFileDialog.AddExtension = true;// 自动添加扩展名
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            ///
            File.Copy(listView.SelectedItems[0].Text, saveFileDialog.FileName);
        }
    }
}
