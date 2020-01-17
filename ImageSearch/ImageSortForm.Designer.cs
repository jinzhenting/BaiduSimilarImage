namespace ImageSearch
{
    partial class ImageSortForm
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
            this.sort_background = new System.ComponentModel.BackgroundWorker();
            this.search_statusstrip = new System.Windows.Forms.StatusStrip();
            this.search_bar = new System.Windows.Forms.ToolStripProgressBar();
            this.progress_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.label13 = new System.Windows.Forms.Label();
            this.vcetor_source_path_button = new System.Windows.Forms.Button();
            this.sort_in_path_textbox = new System.Windows.Forms.TextBox();
            this.vector_sort_button = new System.Windows.Forms.Button();
            this.vector_cancel_button = new System.Windows.Forms.Button();
            this.depot_list_combobox = new System.Windows.Forms.ComboBox();
            this.sort_sub_checkbox = new System.Windows.Forms.CheckBox();
            this.sort_holdold_checkbox = new System.Windows.Forms.CheckBox();
            this.sort_listview = new System.Windows.Forms.ListView();
            this.label11 = new System.Windows.Forms.Label();
            this.search_statusstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // sort_background
            // 
            this.sort_background.WorkerReportsProgress = true;
            this.sort_background.WorkerSupportsCancellation = true;
            this.sort_background.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sort_background_DoWork);
            this.sort_background.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.sort_background_ProgressChanged);
            this.sort_background.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.sort_background_RunWorkerCompleted);
            // 
            // search_statusstrip
            // 
            this.search_statusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.search_bar,
            this.progress_label});
            this.search_statusstrip.Location = new System.Drawing.Point(0, 539);
            this.search_statusstrip.Name = "search_statusstrip";
            this.search_statusstrip.Size = new System.Drawing.Size(784, 22);
            this.search_statusstrip.TabIndex = 3;
            this.search_statusstrip.Text = "search_statusstrip";
            // 
            // search_bar
            // 
            this.search_bar.Name = "search_bar";
            this.search_bar.Size = new System.Drawing.Size(100, 16);
            // 
            // progress_label
            // 
            this.progress_label.Name = "progress_label";
            this.progress_label.Size = new System.Drawing.Size(80, 17);
            this.progress_label.Text = "等待用户操作";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 44);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 17);
            this.label13.TabIndex = 0;
            this.label13.Text = "目录";
            // 
            // vcetor_source_path_button
            // 
            this.vcetor_source_path_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.vcetor_source_path_button.Location = new System.Drawing.Point(504, 41);
            this.vcetor_source_path_button.Name = "vcetor_source_path_button";
            this.vcetor_source_path_button.Size = new System.Drawing.Size(75, 23);
            this.vcetor_source_path_button.TabIndex = 2;
            this.vcetor_source_path_button.Text = "浏览";
            this.vcetor_source_path_button.UseVisualStyleBackColor = true;
            this.vcetor_source_path_button.Click += new System.EventHandler(this.vcetor_source_path_button_Click);
            // 
            // sort_in_path_textbox
            // 
            this.sort_in_path_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sort_in_path_textbox.Location = new System.Drawing.Point(43, 41);
            this.sort_in_path_textbox.Name = "sort_in_path_textbox";
            this.sort_in_path_textbox.Size = new System.Drawing.Size(455, 23);
            this.sort_in_path_textbox.TabIndex = 1;
            this.sort_in_path_textbox.Text = "\\\\nrl\\Vector\\Today";
            // 
            // vector_sort_button
            // 
            this.vector_sort_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.vector_sort_button.Location = new System.Drawing.Point(621, 509);
            this.vector_sort_button.Name = "vector_sort_button";
            this.vector_sort_button.Size = new System.Drawing.Size(75, 23);
            this.vector_sort_button.TabIndex = 1;
            this.vector_sort_button.Text = "开始";
            this.vector_sort_button.UseVisualStyleBackColor = true;
            this.vector_sort_button.Click += new System.EventHandler(this.vector_sort_button_Click);
            // 
            // vector_cancel_button
            // 
            this.vector_cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.vector_cancel_button.Location = new System.Drawing.Point(702, 509);
            this.vector_cancel_button.Name = "vector_cancel_button";
            this.vector_cancel_button.Size = new System.Drawing.Size(75, 23);
            this.vector_cancel_button.TabIndex = 2;
            this.vector_cancel_button.Text = "停止";
            this.vector_cancel_button.UseVisualStyleBackColor = true;
            this.vector_cancel_button.Click += new System.EventHandler(this.vector_cancel_button_Click);
            // 
            // depot_list_combobox
            // 
            this.depot_list_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.depot_list_combobox.FormattingEnabled = true;
            this.depot_list_combobox.Location = new System.Drawing.Point(43, 8);
            this.depot_list_combobox.Name = "depot_list_combobox";
            this.depot_list_combobox.Size = new System.Drawing.Size(121, 25);
            this.depot_list_combobox.TabIndex = 4;
            // 
            // sort_sub_checkbox
            // 
            this.sort_sub_checkbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sort_sub_checkbox.AutoSize = true;
            this.sort_sub_checkbox.Location = new System.Drawing.Point(586, 42);
            this.sort_sub_checkbox.Name = "sort_sub_checkbox";
            this.sort_sub_checkbox.Size = new System.Drawing.Size(99, 21);
            this.sort_sub_checkbox.TabIndex = 9;
            this.sort_sub_checkbox.Text = "包含子文件夹";
            this.sort_sub_checkbox.UseVisualStyleBackColor = true;
            // 
            // sort_holdold_checkbox
            // 
            this.sort_holdold_checkbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sort_holdold_checkbox.AutoSize = true;
            this.sort_holdold_checkbox.Location = new System.Drawing.Point(690, 42);
            this.sort_holdold_checkbox.Name = "sort_holdold_checkbox";
            this.sort_holdold_checkbox.Size = new System.Drawing.Size(87, 21);
            this.sort_holdold_checkbox.TabIndex = 10;
            this.sort_holdold_checkbox.Text = "保留原文件";
            this.sort_holdold_checkbox.UseVisualStyleBackColor = true;
            // 
            // sort_listview
            // 
            this.sort_listview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sort_listview.FullRowSelect = true;
            this.sort_listview.GridLines = true;
            this.sort_listview.Location = new System.Drawing.Point(-1, 71);
            this.sort_listview.Name = "sort_listview";
            this.sort_listview.Size = new System.Drawing.Size(786, 431);
            this.sort_listview.TabIndex = 11;
            this.sort_listview.UseCompatibleStateImageBehavior = false;
            this.sort_listview.View = System.Windows.Forms.View.Details;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 17);
            this.label11.TabIndex = 8;
            this.label11.Text = "图库";
            // 
            // ImageSortForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.vcetor_source_path_button);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.sort_in_path_textbox);
            this.Controls.Add(this.sort_holdold_checkbox);
            this.Controls.Add(this.sort_sub_checkbox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.depot_list_combobox);
            this.Controls.Add(this.vector_sort_button);
            this.Controls.Add(this.search_statusstrip);
            this.Controls.Add(this.vector_cancel_button);
            this.Controls.Add(this.sort_listview);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ImageSortForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "本地图片整理";
            this.Load += new System.EventHandler(this.ImageSortForm_Load);
            this.search_statusstrip.ResumeLayout(false);
            this.search_statusstrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker sort_background;
        private System.Windows.Forms.StatusStrip search_statusstrip;
        private System.Windows.Forms.ToolStripStatusLabel progress_label;
        private System.Windows.Forms.ToolStripProgressBar search_bar;
        private System.Windows.Forms.Button vector_sort_button;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button vector_cancel_button;
        private System.Windows.Forms.Button vcetor_source_path_button;
        private System.Windows.Forms.TextBox sort_in_path_textbox;
        private System.Windows.Forms.ComboBox depot_list_combobox;
        private System.Windows.Forms.CheckBox sort_sub_checkbox;
        private System.Windows.Forms.CheckBox sort_holdold_checkbox;
        private System.Windows.Forms.ListView sort_listview;
        private System.Windows.Forms.Label label11;
    }
}