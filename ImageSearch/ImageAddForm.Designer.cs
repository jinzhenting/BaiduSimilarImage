namespace ImageSearch
{
    partial class ImageAddForm
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
            this.scanListview = new System.Windows.Forms.ListView();
            this.depotListCombobox = new System.Windows.Forms.ComboBox();
            this.addStartButton = new System.Windows.Forms.Button();
            this.addStatusstrip = new System.Windows.Forms.StatusStrip();
            this.addBar = new System.Windows.Forms.ToolStripProgressBar();
            this.addLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.addPathTextbox = new System.Windows.Forms.TextBox();
            this.addScanButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.subCheckbox = new System.Windows.Forms.CheckBox();
            this.addBack = new System.ComponentModel.BackgroundWorker();
            this.resetAddImageListButton = new System.Windows.Forms.Button();
            this.addLogListview = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.sqlCheckbox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.formatCombobox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.resetLogButton = new System.Windows.Forms.Button();
            this.scanBack = new System.ComponentModel.BackgroundWorker();
            this.addStatusstrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scanListview
            // 
            this.scanListview.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.scanListview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scanListview.FullRowSelect = true;
            this.scanListview.GridLines = true;
            this.scanListview.Location = new System.Drawing.Point(-1, 73);
            this.scanListview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.scanListview.Name = "scanListview";
            this.scanListview.Size = new System.Drawing.Size(786, 226);
            this.scanListview.TabIndex = 8;
            this.scanListview.UseCompatibleStateImageBehavior = false;
            this.scanListview.View = System.Windows.Forms.View.Details;
            // 
            // depotListCombobox
            // 
            this.depotListCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.depotListCombobox.FormattingEnabled = true;
            this.depotListCombobox.Location = new System.Drawing.Point(49, 10);
            this.depotListCombobox.Name = "depotListCombobox";
            this.depotListCombobox.Size = new System.Drawing.Size(121, 25);
            this.depotListCombobox.TabIndex = 1;
            this.depotListCombobox.SelectedIndexChanged += new System.EventHandler(this.depotListCombobox_SelectedIndexChanged);
            // 
            // addStartButton
            // 
            this.addStartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addStartButton.Location = new System.Drawing.Point(610, 166);
            this.addStartButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.addStartButton.Name = "addStartButton";
            this.addStartButton.Size = new System.Drawing.Size(80, 23);
            this.addStartButton.TabIndex = 11;
            this.addStartButton.Text = "开始入库";
            this.addStartButton.UseVisualStyleBackColor = true;
            this.addStartButton.Click += new System.EventHandler(this.addStartButton_Click);
            // 
            // addStatusstrip
            // 
            this.addStatusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addBar,
            this.addLabel});
            this.addStatusstrip.Location = new System.Drawing.Point(0, 539);
            this.addStatusstrip.Name = "addStatusstrip";
            this.addStatusstrip.Size = new System.Drawing.Size(784, 22);
            this.addStatusstrip.TabIndex = 1;
            this.addStatusstrip.Text = "addStatusstrip";
            // 
            // addBar
            // 
            this.addBar.Name = "addBar";
            this.addBar.Size = new System.Drawing.Size(100, 16);
            // 
            // addLabel
            // 
            this.addLabel.Name = "addLabel";
            this.addLabel.Size = new System.Drawing.Size(80, 17);
            this.addLabel.Text = "等待用户操作";
            // 
            // addPathTextbox
            // 
            this.addPathTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addPathTextbox.Location = new System.Drawing.Point(49, 42);
            this.addPathTextbox.Name = "addPathTextbox";
            this.addPathTextbox.ReadOnly = true;
            this.addPathTextbox.Size = new System.Drawing.Size(509, 23);
            this.addPathTextbox.TabIndex = 5;
            // 
            // addScanButton
            // 
            this.addScanButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addScanButton.Location = new System.Drawing.Point(610, 307);
            this.addScanButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.addScanButton.Name = "addScanButton";
            this.addScanButton.Size = new System.Drawing.Size(80, 23);
            this.addScanButton.TabIndex = 9;
            this.addScanButton.Text = "扫描图片";
            this.addScanButton.UseVisualStyleBackColor = true;
            this.addScanButton.Click += new System.EventHandler(this.addScanButton_Click);
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
            // subCheckbox
            // 
            this.subCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.subCheckbox.AutoSize = true;
            this.subCheckbox.Location = new System.Drawing.Point(566, 43);
            this.subCheckbox.Name = "subCheckbox";
            this.subCheckbox.Size = new System.Drawing.Size(87, 21);
            this.subCheckbox.TabIndex = 6;
            this.subCheckbox.Text = "包含子目录";
            this.subCheckbox.UseVisualStyleBackColor = true;
            this.subCheckbox.Click += new System.EventHandler(this.subCheckbox_Click);
            // 
            // addBack
            // 
            this.addBack.WorkerReportsProgress = true;
            this.addBack.WorkerSupportsCancellation = true;
            this.addBack.DoWork += new System.ComponentModel.DoWorkEventHandler(this.addBack_DoWork);
            this.addBack.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.addBack_ProgressChanged);
            this.addBack.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.addBack_RunWorkerCompleted);
            // 
            // resetAddImageListButton
            // 
            this.resetAddImageListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetAddImageListButton.Location = new System.Drawing.Point(696, 307);
            this.resetAddImageListButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.resetAddImageListButton.Name = "resetAddImageListButton";
            this.resetAddImageListButton.Size = new System.Drawing.Size(80, 23);
            this.resetAddImageListButton.TabIndex = 10;
            this.resetAddImageListButton.Text = "清空列表";
            this.resetAddImageListButton.UseVisualStyleBackColor = true;
            this.resetAddImageListButton.Click += new System.EventHandler(this.resetAddImageListButton_Click);
            // 
            // addLogListview
            // 
            this.addLogListview.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.addLogListview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addLogListview.FullRowSelect = true;
            this.addLogListview.GridLines = true;
            this.addLogListview.Location = new System.Drawing.Point(-1, 0);
            this.addLogListview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.addLogListview.Name = "addLogListview";
            this.addLogListview.Size = new System.Drawing.Size(786, 159);
            this.addLogListview.TabIndex = 0;
            this.addLogListview.UseCompatibleStateImageBehavior = false;
            this.addLogListview.View = System.Windows.Forms.View.Details;
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
            this.splitContainer1.Panel1.Controls.Add(this.sqlCheckbox);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.formatCombobox);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.subCheckbox);
            this.splitContainer1.Panel1.Controls.Add(this.resetAddImageListButton);
            this.splitContainer1.Panel1.Controls.Add(this.addScanButton);
            this.splitContainer1.Panel1.Controls.Add(this.scanListview);
            this.splitContainer1.Panel1.Controls.Add(this.addPathTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.depotListCombobox);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.resetLogButton);
            this.splitContainer1.Panel2.Controls.Add(this.addLogListview);
            this.splitContainer1.Panel2.Controls.Add(this.addStartButton);
            this.splitContainer1.Size = new System.Drawing.Size(784, 539);
            this.splitContainer1.SplitterDistance = 338;
            this.splitContainer1.TabIndex = 0;
            // 
            // sqlCheckbox
            // 
            this.sqlCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sqlCheckbox.AutoSize = true;
            this.sqlCheckbox.Location = new System.Drawing.Point(657, 43);
            this.sqlCheckbox.Name = "sqlCheckbox";
            this.sqlCheckbox.Size = new System.Drawing.Size(123, 21);
            this.sqlCheckbox.TabIndex = 7;
            this.sqlCheckbox.Text = "忽略数据库已有项";
            this.sqlCheckbox.UseVisualStyleBackColor = true;
            this.sqlCheckbox.CheckedChanged += new System.EventHandler(this.sqlCheckbox_CheckedChanged);
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
            // formatCombobox
            // 
            this.formatCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formatCombobox.FormattingEnabled = true;
            this.formatCombobox.Location = new System.Drawing.Point(214, 10);
            this.formatCombobox.Name = "formatCombobox";
            this.formatCombobox.Size = new System.Drawing.Size(60, 25);
            this.formatCombobox.TabIndex = 3;
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
            // resetLogButton
            // 
            this.resetLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetLogButton.Location = new System.Drawing.Point(696, 166);
            this.resetLogButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.resetLogButton.Name = "resetLogButton";
            this.resetLogButton.Size = new System.Drawing.Size(80, 23);
            this.resetLogButton.TabIndex = 1;
            this.resetLogButton.Text = "清空日志";
            this.resetLogButton.UseVisualStyleBackColor = true;
            this.resetLogButton.Click += new System.EventHandler(this.resetLogButton_Click);
            // 
            // scanBack
            // 
            this.scanBack.WorkerReportsProgress = true;
            this.scanBack.WorkerSupportsCancellation = true;
            this.scanBack.DoWork += new System.ComponentModel.DoWorkEventHandler(this.scanBack_DoWork);
            this.scanBack.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.scanBack_ProgressChanged);
            this.scanBack.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.scanBack_RunWorkerCompleted);
            // 
            // ImageAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.addStatusstrip);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ImageAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "入库";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageAddForm_FormClosing);
            this.Load += new System.EventHandler(this.ImageAddForm_Load);
            this.addStatusstrip.ResumeLayout(false);
            this.addStatusstrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView scanListview;
        private System.Windows.Forms.ComboBox depotListCombobox;
        private System.Windows.Forms.Button addStartButton;
        private System.Windows.Forms.StatusStrip addStatusstrip;
        private System.Windows.Forms.ToolStripStatusLabel addLabel;
        private System.Windows.Forms.ToolStripProgressBar addBar;
        private System.Windows.Forms.TextBox addPathTextbox;
        private System.Windows.Forms.Button addScanButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox subCheckbox;
        private System.ComponentModel.BackgroundWorker addBack;
        private System.Windows.Forms.Button resetAddImageListButton;
        private System.Windows.Forms.ListView addLogListview;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox formatCombobox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button resetLogButton;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker scanBack;
        private System.Windows.Forms.CheckBox sqlCheckbox;
    }
}