namespace BaiduSimilarImage
{
    partial class ImageDeleteForm
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
            this.label11 = new System.Windows.Forms.Label();
            this.depotListCombobox = new System.Windows.Forms.ComboBox();
            this.deletePathTextBox = new System.Windows.Forms.TextBox();
            this.deletePathButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.deleteImageCheckBox = new System.Windows.Forms.CheckBox();
            this.deleteSignCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.deleteSignTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.progressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.deleteBack = new System.ComponentModel.BackgroundWorker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 17);
            this.label11.TabIndex = 1;
            this.label11.Text = "选择图库";
            // 
            // depotListCombobox
            // 
            this.depotListCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.depotListCombobox.FormattingEnabled = true;
            this.depotListCombobox.Location = new System.Drawing.Point(68, 8);
            this.depotListCombobox.Name = "depotListCombobox";
            this.depotListCombobox.Size = new System.Drawing.Size(121, 25);
            this.depotListCombobox.TabIndex = 2;
            // 
            // deletePathTextBox
            // 
            this.deletePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deletePathTextBox.Location = new System.Drawing.Point(69, 40);
            this.deletePathTextBox.Name = "deletePathTextBox";
            this.deletePathTextBox.Size = new System.Drawing.Size(429, 23);
            this.deletePathTextBox.TabIndex = 2;
            // 
            // deletePathButton
            // 
            this.deletePathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deletePathButton.Location = new System.Drawing.Point(504, 40);
            this.deletePathButton.Name = "deletePathButton";
            this.deletePathButton.Size = new System.Drawing.Size(75, 23);
            this.deletePathButton.TabIndex = 3;
            this.deletePathButton.Text = "浏览";
            this.deletePathButton.UseVisualStyleBackColor = true;
            this.deletePathButton.Click += new System.EventHandler(this.deletePathButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Location = new System.Drawing.Point(421, 409);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "提交申请";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(502, 409);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // deleteImageCheckBox
            // 
            this.deleteImageCheckBox.AutoSize = true;
            this.deleteImageCheckBox.Checked = true;
            this.deleteImageCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deleteImageCheckBox.Location = new System.Drawing.Point(12, 13);
            this.deleteImageCheckBox.Name = "deleteImageCheckBox";
            this.deleteImageCheckBox.Size = new System.Drawing.Size(123, 21);
            this.deleteImageCheckBox.TabIndex = 0;
            this.deleteImageCheckBox.Text = "上传图片进行删除";
            this.deleteImageCheckBox.UseVisualStyleBackColor = true;
            this.deleteImageCheckBox.CheckedChanged += new System.EventHandler(this.deleteImageCheckBox_CheckedChanged);
            // 
            // deleteSignCheckBox
            // 
            this.deleteSignCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteSignCheckBox.AutoSize = true;
            this.deleteSignCheckBox.Location = new System.Drawing.Point(12, 303);
            this.deleteSignCheckBox.Name = "deleteSignCheckBox";
            this.deleteSignCheckBox.Size = new System.Drawing.Size(147, 21);
            this.deleteSignCheckBox.TabIndex = 4;
            this.deleteSignCheckBox.Text = "输入图片签名进行删除";
            this.deleteSignCheckBox.UseVisualStyleBackColor = true;
            this.deleteSignCheckBox.CheckedChanged += new System.EventHandler(this.deleteSignCheckBox_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.deleteSignCheckBox);
            this.panel1.Controls.Add(this.deleteSignTextBox);
            this.panel1.Controls.Add(this.deleteImageCheckBox);
            this.panel1.Controls.Add(this.deletePathButton);
            this.panel1.Controls.Add(this.deletePathTextBox);
            this.panel1.Location = new System.Drawing.Point(-2, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 362);
            this.panel1.TabIndex = 4;
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(69, 69);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(429, 228);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 62;
            this.pictureBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "图片位置";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 333);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "图片编号";
            // 
            // deleteSignTextBox
            // 
            this.deleteSignTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteSignTextBox.Location = new System.Drawing.Point(69, 330);
            this.deleteSignTextBox.Name = "deleteSignTextBox";
            this.deleteSignTextBox.ReadOnly = true;
            this.deleteSignTextBox.Size = new System.Drawing.Size(193, 23);
            this.deleteSignTextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label3.Location = new System.Drawing.Point(12, 413);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(221, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "* 图片签名在入库时由API返回并录入数据库";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel2.Location = new System.Drawing.Point(-1, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(589, 1);
            this.panel2.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.progressLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 439);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 8;
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
            // deleteBack
            // 
            this.deleteBack.WorkerSupportsCancellation = true;
            this.deleteBack.DoWork += new System.ComponentModel.DoWorkEventHandler(this.deleteBack_DoWork);
            this.deleteBack.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.deleteBack_RunWorkerCompleted);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel3.Location = new System.Drawing.Point(-2, 402);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(589, 1);
            this.panel3.TabIndex = 4;
            // 
            // ImageDeleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.depotListCombobox);
            this.Controls.Add(this.label11);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "ImageDeleteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "在库中删除图片";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageDeleteForm_FormClosing);
            this.Load += new System.EventHandler(this.ImageDeleteForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox depotListCombobox;
        private System.Windows.Forms.TextBox deletePathTextBox;
        private System.Windows.Forms.Button deletePathButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox deleteSignCheckBox;
        private System.Windows.Forms.CheckBox deleteImageCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox deleteSignTextBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel progressLabel;
        private System.ComponentModel.BackgroundWorker deleteBack;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}