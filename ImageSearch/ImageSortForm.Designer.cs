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
            this.sortBack = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.progressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.label13 = new System.Windows.Forms.Label();
            this.sourcePathButton = new System.Windows.Forms.Button();
            this.inPathTextBox = new System.Windows.Forms.TextBox();
            this.sortButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.depotListCombobox = new System.Windows.Forms.ComboBox();
            this.sortSubCheckBox = new System.Windows.Forms.CheckBox();
            this.sortHoldOldCheckBox = new System.Windows.Forms.CheckBox();
            this.sortListView = new System.Windows.Forms.ListView();
            this.label11 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sortBack
            // 
            this.sortBack.WorkerReportsProgress = true;
            this.sortBack.WorkerSupportsCancellation = true;
            this.sortBack.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sortBack_DoWork);
            this.sortBack.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.sortBack_ProgressChanged);
            this.sortBack.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.sortBack_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.progressLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // progressLabel
            // 
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(80, 17);
            this.progressLabel.Text = "等待用户操作";
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
            // sourcePathButton
            // 
            this.sourcePathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sourcePathButton.Location = new System.Drawing.Point(504, 41);
            this.sourcePathButton.Name = "sourcePathButton";
            this.sourcePathButton.Size = new System.Drawing.Size(75, 23);
            this.sourcePathButton.TabIndex = 2;
            this.sourcePathButton.Text = "浏览";
            this.sourcePathButton.UseVisualStyleBackColor = true;
            this.sourcePathButton.Click += new System.EventHandler(this.sourcePathButton_Click);
            // 
            // inPathTextBox
            // 
            this.inPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inPathTextBox.Location = new System.Drawing.Point(43, 41);
            this.inPathTextBox.Name = "inPathTextBox";
            this.inPathTextBox.Size = new System.Drawing.Size(455, 23);
            this.inPathTextBox.TabIndex = 1;
            // 
            // sortButton
            // 
            this.sortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sortButton.Location = new System.Drawing.Point(621, 509);
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(75, 23);
            this.sortButton.TabIndex = 1;
            this.sortButton.Text = "开始";
            this.sortButton.UseVisualStyleBackColor = true;
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(702, 509);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "停止";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // depotListCombobox
            // 
            this.depotListCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.depotListCombobox.FormattingEnabled = true;
            this.depotListCombobox.Location = new System.Drawing.Point(43, 8);
            this.depotListCombobox.Name = "depotListCombobox";
            this.depotListCombobox.Size = new System.Drawing.Size(121, 25);
            this.depotListCombobox.TabIndex = 4;
            this.depotListCombobox.SelectedIndexChanged += new System.EventHandler(this.depotListCombobox_SelectedIndexChanged);
            // 
            // sortSubCheckBox
            // 
            this.sortSubCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sortSubCheckBox.AutoSize = true;
            this.sortSubCheckBox.Location = new System.Drawing.Point(586, 42);
            this.sortSubCheckBox.Name = "sortSubCheckBox";
            this.sortSubCheckBox.Size = new System.Drawing.Size(99, 21);
            this.sortSubCheckBox.TabIndex = 9;
            this.sortSubCheckBox.Text = "包含子文件夹";
            this.sortSubCheckBox.UseVisualStyleBackColor = true;
            // 
            // sortHoldOldCheckBox
            // 
            this.sortHoldOldCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sortHoldOldCheckBox.AutoSize = true;
            this.sortHoldOldCheckBox.Location = new System.Drawing.Point(690, 42);
            this.sortHoldOldCheckBox.Name = "sortHoldOldCheckBox";
            this.sortHoldOldCheckBox.Size = new System.Drawing.Size(87, 21);
            this.sortHoldOldCheckBox.TabIndex = 10;
            this.sortHoldOldCheckBox.Text = "保留原文件";
            this.sortHoldOldCheckBox.UseVisualStyleBackColor = true;
            // 
            // sortListView
            // 
            this.sortListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sortListView.FullRowSelect = true;
            this.sortListView.GridLines = true;
            this.sortListView.Location = new System.Drawing.Point(-1, 71);
            this.sortListView.Name = "sortListView";
            this.sortListView.Size = new System.Drawing.Size(786, 431);
            this.sortListView.TabIndex = 11;
            this.sortListView.UseCompatibleStateImageBehavior = false;
            this.sortListView.View = System.Windows.Forms.View.Details;
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
            this.Controls.Add(this.sourcePathButton);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.inPathTextBox);
            this.Controls.Add(this.sortHoldOldCheckBox);
            this.Controls.Add(this.sortSubCheckBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.depotListCombobox);
            this.Controls.Add(this.sortButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.sortListView);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ImageSortForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "本地图片整理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageSortForm_FormClosing);
            this.Load += new System.EventHandler(this.ImageSortForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker sortBack;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel progressLabel;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.Button sortButton;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button sourcePathButton;
        private System.Windows.Forms.TextBox inPathTextBox;
        private System.Windows.Forms.ComboBox depotListCombobox;
        private System.Windows.Forms.CheckBox sortSubCheckBox;
        private System.Windows.Forms.CheckBox sortHoldOldCheckBox;
        private System.Windows.Forms.ListView sortListView;
        private System.Windows.Forms.Label label11;
    }
}