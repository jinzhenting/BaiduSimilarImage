namespace ImageSearch
{
    partial class ImageUpForm
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
            this.scan_listview = new System.Windows.Forms.ListView();
            this.depot_list_combobox = new System.Windows.Forms.ComboBox();
            this.add_start_button = new System.Windows.Forms.Button();
            this.add_statusstrip = new System.Windows.Forms.StatusStrip();
            this.add_bar = new System.Windows.Forms.ToolStripProgressBar();
            this.add_bar_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.add_path_textbox = new System.Windows.Forms.TextBox();
            this.add_scan_button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.sub_checkbox = new System.Windows.Forms.CheckBox();
            this.add_background = new System.ComponentModel.BackgroundWorker();
            this.reset_add_image_list_button = new System.Windows.Forms.Button();
            this.add_log_listview = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.sql_checkbox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.format_combobox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reset_log_button = new System.Windows.Forms.Button();
            this.scan_background = new System.ComponentModel.BackgroundWorker();
            this.add_statusstrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scan_listview
            // 
            this.scan_listview.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.scan_listview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scan_listview.FullRowSelect = true;
            this.scan_listview.GridLines = true;
            this.scan_listview.Location = new System.Drawing.Point(-1, 73);
            this.scan_listview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.scan_listview.Name = "scan_listview";
            this.scan_listview.Size = new System.Drawing.Size(786, 226);
            this.scan_listview.TabIndex = 8;
            this.scan_listview.UseCompatibleStateImageBehavior = false;
            this.scan_listview.View = System.Windows.Forms.View.Details;
            // 
            // depot_list_combobox
            // 
            this.depot_list_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.depot_list_combobox.FormattingEnabled = true;
            this.depot_list_combobox.Location = new System.Drawing.Point(49, 10);
            this.depot_list_combobox.Name = "depot_list_combobox";
            this.depot_list_combobox.Size = new System.Drawing.Size(121, 25);
            this.depot_list_combobox.TabIndex = 1;
            this.depot_list_combobox.SelectedIndexChanged += new System.EventHandler(this.depot_list_combobox_SelectedIndexChanged);
            // 
            // add_start_button
            // 
            this.add_start_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.add_start_button.Location = new System.Drawing.Point(610, 166);
            this.add_start_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.add_start_button.Name = "add_start_button";
            this.add_start_button.Size = new System.Drawing.Size(80, 23);
            this.add_start_button.TabIndex = 11;
            this.add_start_button.Text = "开始入库";
            this.add_start_button.UseVisualStyleBackColor = true;
            this.add_start_button.Click += new System.EventHandler(this.add_start_button_Click);
            // 
            // add_statusstrip
            // 
            this.add_statusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.add_bar,
            this.add_bar_label});
            this.add_statusstrip.Location = new System.Drawing.Point(0, 539);
            this.add_statusstrip.Name = "add_statusstrip";
            this.add_statusstrip.Size = new System.Drawing.Size(784, 22);
            this.add_statusstrip.TabIndex = 1;
            this.add_statusstrip.Text = "add_statusstrip";
            // 
            // add_bar
            // 
            this.add_bar.Name = "add_bar";
            this.add_bar.Size = new System.Drawing.Size(100, 16);
            // 
            // add_bar_label
            // 
            this.add_bar_label.Name = "add_bar_label";
            this.add_bar_label.Size = new System.Drawing.Size(80, 17);
            this.add_bar_label.Text = "等待用户操作";
            // 
            // add_path_textbox
            // 
            this.add_path_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.add_path_textbox.Location = new System.Drawing.Point(49, 42);
            this.add_path_textbox.Name = "add_path_textbox";
            this.add_path_textbox.ReadOnly = true;
            this.add_path_textbox.Size = new System.Drawing.Size(509, 23);
            this.add_path_textbox.TabIndex = 5;
            // 
            // add_scan_button
            // 
            this.add_scan_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.add_scan_button.Location = new System.Drawing.Point(610, 307);
            this.add_scan_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.add_scan_button.Name = "add_scan_button";
            this.add_scan_button.Size = new System.Drawing.Size(80, 23);
            this.add_scan_button.TabIndex = 9;
            this.add_scan_button.Text = "扫描图片";
            this.add_scan_button.UseVisualStyleBackColor = true;
            this.add_scan_button.Click += new System.EventHandler(this.add_scan_button_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(176, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "格式";
            // 
            // sub_checkbox
            // 
            this.sub_checkbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sub_checkbox.AutoSize = true;
            this.sub_checkbox.Location = new System.Drawing.Point(566, 43);
            this.sub_checkbox.Name = "sub_checkbox";
            this.sub_checkbox.Size = new System.Drawing.Size(87, 21);
            this.sub_checkbox.TabIndex = 6;
            this.sub_checkbox.Text = "包含子目录";
            this.sub_checkbox.UseVisualStyleBackColor = true;
            this.sub_checkbox.CheckedChanged += new System.EventHandler(this.sub_checkbox_CheckedChanged);
            // 
            // add_background
            // 
            this.add_background.WorkerReportsProgress = true;
            this.add_background.WorkerSupportsCancellation = true;
            this.add_background.DoWork += new System.ComponentModel.DoWorkEventHandler(this.add_background_DoWork);
            this.add_background.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.add_background_ProgressChanged);
            this.add_background.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.add_background_RunWorkerCompleted);
            // 
            // reset_add_image_list_button
            // 
            this.reset_add_image_list_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.reset_add_image_list_button.Location = new System.Drawing.Point(696, 307);
            this.reset_add_image_list_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.reset_add_image_list_button.Name = "reset_add_image_list_button";
            this.reset_add_image_list_button.Size = new System.Drawing.Size(80, 23);
            this.reset_add_image_list_button.TabIndex = 10;
            this.reset_add_image_list_button.Text = "清空列表";
            this.reset_add_image_list_button.UseVisualStyleBackColor = true;
            this.reset_add_image_list_button.Click += new System.EventHandler(this.reset_add_image_list_button_Click);
            // 
            // add_log_listview
            // 
            this.add_log_listview.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.add_log_listview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.add_log_listview.FullRowSelect = true;
            this.add_log_listview.GridLines = true;
            this.add_log_listview.Location = new System.Drawing.Point(-1, 0);
            this.add_log_listview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.add_log_listview.Name = "add_log_listview";
            this.add_log_listview.Size = new System.Drawing.Size(786, 159);
            this.add_log_listview.TabIndex = 0;
            this.add_log_listview.UseCompatibleStateImageBehavior = false;
            this.add_log_listview.View = System.Windows.Forms.View.Details;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.sql_checkbox);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.format_combobox);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.sub_checkbox);
            this.splitContainer1.Panel1.Controls.Add(this.reset_add_image_list_button);
            this.splitContainer1.Panel1.Controls.Add(this.add_scan_button);
            this.splitContainer1.Panel1.Controls.Add(this.scan_listview);
            this.splitContainer1.Panel1.Controls.Add(this.add_path_textbox);
            this.splitContainer1.Panel1.Controls.Add(this.depot_list_combobox);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.reset_log_button);
            this.splitContainer1.Panel2.Controls.Add(this.add_log_listview);
            this.splitContainer1.Panel2.Controls.Add(this.add_start_button);
            this.splitContainer1.Size = new System.Drawing.Size(784, 539);
            this.splitContainer1.SplitterDistance = 338;
            this.splitContainer1.TabIndex = 0;
            // 
            // sql_checkbox
            // 
            this.sql_checkbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sql_checkbox.AutoSize = true;
            this.sql_checkbox.Location = new System.Drawing.Point(657, 43);
            this.sql_checkbox.Name = "sql_checkbox";
            this.sql_checkbox.Size = new System.Drawing.Size(123, 21);
            this.sql_checkbox.TabIndex = 7;
            this.sql_checkbox.Text = "忽略数据库已有项";
            this.sql_checkbox.UseVisualStyleBackColor = true;
            this.sql_checkbox.CheckedChanged += new System.EventHandler(this.sql_checkbox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "目录";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "图库";
            // 
            // format_combobox
            // 
            this.format_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.format_combobox.FormattingEnabled = true;
            this.format_combobox.Location = new System.Drawing.Point(214, 10);
            this.format_combobox.Name = "format_combobox";
            this.format_combobox.Size = new System.Drawing.Size(60, 25);
            this.format_combobox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label1.Location = new System.Drawing.Point(3, 320);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "日志";
            // 
            // reset_log_button
            // 
            this.reset_log_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.reset_log_button.Location = new System.Drawing.Point(696, 166);
            this.reset_log_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.reset_log_button.Name = "reset_log_button";
            this.reset_log_button.Size = new System.Drawing.Size(80, 23);
            this.reset_log_button.TabIndex = 1;
            this.reset_log_button.Text = "清空日志";
            this.reset_log_button.UseVisualStyleBackColor = true;
            this.reset_log_button.Click += new System.EventHandler(this.reset_log_button_Click);
            // 
            // scan_background
            // 
            this.scan_background.WorkerReportsProgress = true;
            this.scan_background.WorkerSupportsCancellation = true;
            this.scan_background.DoWork += new System.ComponentModel.DoWorkEventHandler(this.scan_background_DoWork);
            this.scan_background.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.scan_background_ProgressChanged);
            this.scan_background.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.scan_background_RunWorkerCompleted);
            // 
            // ImageUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.add_statusstrip);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ImageUpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "入库";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageUpForm_FormClosing);
            this.Load += new System.EventHandler(this.ImageUpForm_Load);
            this.add_statusstrip.ResumeLayout(false);
            this.add_statusstrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView scan_listview;
        private System.Windows.Forms.ComboBox depot_list_combobox;
        private System.Windows.Forms.Button add_start_button;
        private System.Windows.Forms.StatusStrip add_statusstrip;
        private System.Windows.Forms.ToolStripStatusLabel add_bar_label;
        private System.Windows.Forms.ToolStripProgressBar add_bar;
        private System.Windows.Forms.TextBox add_path_textbox;
        private System.Windows.Forms.Button add_scan_button;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox sub_checkbox;
        private System.ComponentModel.BackgroundWorker add_background;
        private System.Windows.Forms.Button reset_add_image_list_button;
        private System.Windows.Forms.ListView add_log_listview;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox format_combobox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button reset_log_button;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker scan_background;
        private System.Windows.Forms.CheckBox sql_checkbox;
    }
}