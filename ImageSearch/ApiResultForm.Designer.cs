namespace ImageSearch
{
    partial class ApiResultForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.icon_listview = new System.Windows.Forms.ListView();
            this.clear_result_button = new System.Windows.Forms.Button();
            this.online_image_path_button = new System.Windows.Forms.Button();
            this.icon_background = new System.ComponentModel.BackgroundWorker();
            this.list_statusstrip = new System.Windows.Forms.StatusStrip();
            this.list_progressbar = new System.Windows.Forms.ToolStripProgressBar();
            this.progress_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.list_statusstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // icon_listview
            // 
            this.icon_listview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.icon_listview.FullRowSelect = true;
            this.icon_listview.GridLines = true;
            this.icon_listview.Location = new System.Drawing.Point(9, 10);
            this.icon_listview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.icon_listview.Name = "icon_listview";
            this.icon_listview.Size = new System.Drawing.Size(919, 591);
            this.icon_listview.TabIndex = 0;
            this.icon_listview.UseCompatibleStateImageBehavior = false;
            this.icon_listview.DoubleClick += new System.EventHandler(this.icon_listview_DoubleClick);
            // 
            // clear_result_button
            // 
            this.clear_result_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clear_result_button.Location = new System.Drawing.Point(848, 608);
            this.clear_result_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clear_result_button.Name = "clear_result_button";
            this.clear_result_button.Size = new System.Drawing.Size(80, 23);
            this.clear_result_button.TabIndex = 2;
            this.clear_result_button.Text = "取消载入";
            this.clear_result_button.UseVisualStyleBackColor = true;
            this.clear_result_button.Click += new System.EventHandler(this.clear_result_button_Click);
            // 
            // online_image_path_button
            // 
            this.online_image_path_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.online_image_path_button.Location = new System.Drawing.Point(762, 608);
            this.online_image_path_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.online_image_path_button.Name = "online_image_path_button";
            this.online_image_path_button.Size = new System.Drawing.Size(80, 23);
            this.online_image_path_button.TabIndex = 1;
            this.online_image_path_button.Text = "打开文件夹";
            this.online_image_path_button.UseVisualStyleBackColor = true;
            this.online_image_path_button.Click += new System.EventHandler(this.online_image_path_button_Click);
            // 
            // icon_background
            // 
            this.icon_background.WorkerReportsProgress = true;
            this.icon_background.WorkerSupportsCancellation = true;
            this.icon_background.DoWork += new System.ComponentModel.DoWorkEventHandler(this.icon_background_DoWork);
            this.icon_background.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.icon_background_ProgressChanged);
            this.icon_background.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.icon_background_RunWorkerCompleted);
            // 
            // list_statusstrip
            // 
            this.list_statusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.list_progressbar,
            this.progress_label});
            this.list_statusstrip.Location = new System.Drawing.Point(0, 639);
            this.list_statusstrip.Name = "list_statusstrip";
            this.list_statusstrip.Size = new System.Drawing.Size(934, 22);
            this.list_statusstrip.TabIndex = 3;
            this.list_statusstrip.Text = "list_statusstrip";
            // 
            // list_progressbar
            // 
            this.list_progressbar.Name = "list_progressbar";
            this.list_progressbar.Size = new System.Drawing.Size(100, 16);
            // 
            // progress_label
            // 
            this.progress_label.Name = "progress_label";
            this.progress_label.Size = new System.Drawing.Size(80, 17);
            this.progress_label.Text = "等待用户操作";
            // 
            // ApiResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 661);
            this.Controls.Add(this.list_statusstrip);
            this.Controls.Add(this.online_image_path_button);
            this.Controls.Add(this.clear_result_button);
            this.Controls.Add(this.icon_listview);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(697, 664);
            this.Name = "ApiResultForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "结果列表 - 双击查看原图";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ApiResultForm_FormClosing);
            this.Load += new System.EventHandler(this.ApiResultForm_Load);
            this.Shown += new System.EventHandler(this.ApiResultForm_Shown);
            this.list_statusstrip.ResumeLayout(false);
            this.list_statusstrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView icon_listview;
        private System.Windows.Forms.Button clear_result_button;
        private System.Windows.Forms.Button online_image_path_button;
        private System.ComponentModel.BackgroundWorker icon_background;
        private System.Windows.Forms.StatusStrip list_statusstrip;
        private System.Windows.Forms.ToolStripStatusLabel progress_label;
        private System.Windows.Forms.ToolStripProgressBar list_progressbar;
    }
}