namespace ImageSearch
{
    partial class EmptyFolderForm
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
            this.empty_listview = new System.Windows.Forms.ListView();
            this.empty_path_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.empty_path_textbox = new System.Windows.Forms.TextBox();
            this.empty_scan_button = new System.Windows.Forms.Button();
            this.empty_delete_button = new System.Windows.Forms.Button();
            this.empty_cancel_button = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.empty_bar = new System.Windows.Forms.ToolStripProgressBar();
            this.empty_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.empty_background = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // empty_listview
            // 
            this.empty_listview.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.empty_listview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.empty_listview.CheckBoxes = true;
            this.empty_listview.GridLines = true;
            this.empty_listview.Location = new System.Drawing.Point(-2, 43);
            this.empty_listview.Name = "empty_listview";
            this.empty_listview.Size = new System.Drawing.Size(588, 361);
            this.empty_listview.TabIndex = 0;
            this.empty_listview.UseCompatibleStateImageBehavior = false;
            this.empty_listview.View = System.Windows.Forms.View.Details;
            // 
            // empty_path_button
            // 
            this.empty_path_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.empty_path_button.Location = new System.Drawing.Point(320, 10);
            this.empty_path_button.Name = "empty_path_button";
            this.empty_path_button.Size = new System.Drawing.Size(75, 23);
            this.empty_path_button.TabIndex = 1;
            this.empty_path_button.Text = "浏览";
            this.empty_path_button.UseVisualStyleBackColor = true;
            this.empty_path_button.Click += new System.EventHandler(this.empty_path_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "主目录";
            // 
            // empty_path_textbox
            // 
            this.empty_path_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.empty_path_textbox.Location = new System.Drawing.Point(57, 10);
            this.empty_path_textbox.Name = "empty_path_textbox";
            this.empty_path_textbox.Size = new System.Drawing.Size(257, 23);
            this.empty_path_textbox.TabIndex = 3;
            // 
            // empty_scan_button
            // 
            this.empty_scan_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.empty_scan_button.Location = new System.Drawing.Point(401, 10);
            this.empty_scan_button.Name = "empty_scan_button";
            this.empty_scan_button.Size = new System.Drawing.Size(75, 23);
            this.empty_scan_button.TabIndex = 4;
            this.empty_scan_button.Text = "扫描";
            this.empty_scan_button.UseVisualStyleBackColor = true;
            this.empty_scan_button.Click += new System.EventHandler(this.empty_scan_button_Click);
            // 
            // empty_delete_button
            // 
            this.empty_delete_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.empty_delete_button.Location = new System.Drawing.Point(422, 410);
            this.empty_delete_button.Name = "empty_delete_button";
            this.empty_delete_button.Size = new System.Drawing.Size(75, 23);
            this.empty_delete_button.TabIndex = 5;
            this.empty_delete_button.Text = "删除";
            this.empty_delete_button.UseVisualStyleBackColor = true;
            this.empty_delete_button.Click += new System.EventHandler(this.empty_delete_button_Click);
            // 
            // empty_cancel_button
            // 
            this.empty_cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.empty_cancel_button.Location = new System.Drawing.Point(503, 410);
            this.empty_cancel_button.Name = "empty_cancel_button";
            this.empty_cancel_button.Size = new System.Drawing.Size(75, 23);
            this.empty_cancel_button.TabIndex = 6;
            this.empty_cancel_button.Text = "取消";
            this.empty_cancel_button.UseVisualStyleBackColor = true;
            this.empty_cancel_button.Click += new System.EventHandler(this.empty_cancel_button_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.empty_bar,
            this.empty_label});
            this.statusStrip1.Location = new System.Drawing.Point(0, 439);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // empty_bar
            // 
            this.empty_bar.Name = "empty_bar";
            this.empty_bar.Size = new System.Drawing.Size(100, 16);
            // 
            // empty_label
            // 
            this.empty_label.Name = "empty_label";
            this.empty_label.Size = new System.Drawing.Size(80, 17);
            this.empty_label.Text = "等待用户操作";
            // 
            // empty_background
            // 
            this.empty_background.WorkerReportsProgress = true;
            this.empty_background.WorkerSupportsCancellation = true;
            this.empty_background.DoWork += new System.ComponentModel.DoWorkEventHandler(this.empty_background_DoWork);
            this.empty_background.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.empty_background_ProgressChanged);
            this.empty_background.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.empty_background_RunWorkerCompleted);
            // 
            // EmptyFolderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.empty_cancel_button);
            this.Controls.Add(this.empty_delete_button);
            this.Controls.Add(this.empty_scan_button);
            this.Controls.Add(this.empty_path_textbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.empty_path_button);
            this.Controls.Add(this.empty_listview);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "EmptyFolderForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "删除空白目录";
            this.Load += new System.EventHandler(this.EmptyFolderForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView empty_listview;
        private System.Windows.Forms.Button empty_path_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox empty_path_textbox;
        private System.Windows.Forms.Button empty_scan_button;
        private System.Windows.Forms.Button empty_delete_button;
        private System.Windows.Forms.Button empty_cancel_button;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar empty_bar;
        private System.Windows.Forms.ToolStripStatusLabel empty_label;
        private System.ComponentModel.BackgroundWorker empty_background;
    }
}