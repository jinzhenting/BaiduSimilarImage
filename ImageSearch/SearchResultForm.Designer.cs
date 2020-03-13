namespace ImageSearch
{
    partial class SearchResultForm
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
            this.components = new System.ComponentModel.Container();
            this.listView = new System.Windows.Forms.ListView();
            this.clearLoadButton = new System.Windows.Forms.Button();
            this.openPathButton = new System.Windows.Forms.Button();
            this.iconBack = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.progressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openPictrueButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(9, 10);
            this.listView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(919, 591);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            // 
            // clearLoadButton
            // 
            this.clearLoadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearLoadButton.Location = new System.Drawing.Point(848, 608);
            this.clearLoadButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clearLoadButton.Name = "clearLoadButton";
            this.clearLoadButton.Size = new System.Drawing.Size(80, 23);
            this.clearLoadButton.TabIndex = 2;
            this.clearLoadButton.Text = "取消载入";
            this.clearLoadButton.UseVisualStyleBackColor = true;
            this.clearLoadButton.Click += new System.EventHandler(this.clearLoadButton_Click);
            // 
            // openPathButton
            // 
            this.openPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openPathButton.Location = new System.Drawing.Point(762, 608);
            this.openPathButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.openPathButton.Name = "openPathButton";
            this.openPathButton.Size = new System.Drawing.Size(80, 23);
            this.openPathButton.TabIndex = 1;
            this.openPathButton.Text = "打开文件夹";
            this.openPathButton.UseVisualStyleBackColor = true;
            this.openPathButton.Click += new System.EventHandler(this.openPath_Click);
            // 
            // iconBack
            // 
            this.iconBack.WorkerReportsProgress = true;
            this.iconBack.WorkerSupportsCancellation = true;
            this.iconBack.DoWork += new System.ComponentModel.DoWorkEventHandler(this.iconBack_DoWork);
            this.iconBack.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.iconBack_ProgressChanged);
            this.iconBack.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.iconBack_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.progressLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(934, 22);
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
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // openPictrueButton
            // 
            this.openPictrueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openPictrueButton.Location = new System.Drawing.Point(681, 608);
            this.openPictrueButton.Name = "openPictrueButton";
            this.openPictrueButton.Size = new System.Drawing.Size(75, 23);
            this.openPictrueButton.TabIndex = 5;
            this.openPictrueButton.Text = "打开文件";
            this.openPictrueButton.UseVisualStyleBackColor = true;
            this.openPictrueButton.Click += new System.EventHandler(this.openPictrueButton_Click);
            // 
            // SearchBEForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 661);
            this.Controls.Add(this.openPictrueButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.openPathButton);
            this.Controls.Add(this.clearLoadButton);
            this.Controls.Add(this.listView);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(697, 664);
            this.Name = "SearchBEForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "结果列表 - 双击查看原图";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchBEForm_FormClosing);
            this.Load += new System.EventHandler(this.SearchBEForm_Load);
            this.Shown += new System.EventHandler(this.SearchBEForm_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button clearLoadButton;
        private System.Windows.Forms.Button openPathButton;
        private System.ComponentModel.BackgroundWorker iconBack;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel progressLabel;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button openPictrueButton;
    }
}