namespace ImageSearch
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
            this.depot_list_combobox = new System.Windows.Forms.ComboBox();
            this.delete_path_textbox = new System.Windows.Forms.TextBox();
            this.delete_path_button = new System.Windows.Forms.Button();
            this.delete_button = new System.Windows.Forms.Button();
            this.delete_cancel_button = new System.Windows.Forms.Button();
            this.delete_image_checkbox = new System.Windows.Forms.CheckBox();
            this.delete_sign_checkbox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.delete_picturebox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.delete_sign_textbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.delete_bar = new System.Windows.Forms.ToolStripProgressBar();
            this.delete_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.delete_background = new System.ComponentModel.BackgroundWorker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delete_picturebox)).BeginInit();
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
            // depot_list_combobox
            // 
            this.depot_list_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.depot_list_combobox.FormattingEnabled = true;
            this.depot_list_combobox.Location = new System.Drawing.Point(68, 8);
            this.depot_list_combobox.Name = "depot_list_combobox";
            this.depot_list_combobox.Size = new System.Drawing.Size(121, 25);
            this.depot_list_combobox.TabIndex = 2;
            // 
            // delete_path_textbox
            // 
            this.delete_path_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.delete_path_textbox.Location = new System.Drawing.Point(69, 40);
            this.delete_path_textbox.Name = "delete_path_textbox";
            this.delete_path_textbox.Size = new System.Drawing.Size(429, 23);
            this.delete_path_textbox.TabIndex = 2;
            // 
            // delete_path_button
            // 
            this.delete_path_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.delete_path_button.Location = new System.Drawing.Point(504, 40);
            this.delete_path_button.Name = "delete_path_button";
            this.delete_path_button.Size = new System.Drawing.Size(75, 23);
            this.delete_path_button.TabIndex = 3;
            this.delete_path_button.Text = "浏览";
            this.delete_path_button.UseVisualStyleBackColor = true;
            this.delete_path_button.Click += new System.EventHandler(this.delete_path_button_Click);
            // 
            // delete_button
            // 
            this.delete_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.delete_button.Location = new System.Drawing.Point(421, 409);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(75, 23);
            this.delete_button.TabIndex = 6;
            this.delete_button.Text = "提交申请";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // delete_cancel_button
            // 
            this.delete_cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.delete_cancel_button.Location = new System.Drawing.Point(502, 409);
            this.delete_cancel_button.Name = "delete_cancel_button";
            this.delete_cancel_button.Size = new System.Drawing.Size(75, 23);
            this.delete_cancel_button.TabIndex = 7;
            this.delete_cancel_button.Text = "取消";
            this.delete_cancel_button.UseVisualStyleBackColor = true;
            this.delete_cancel_button.Click += new System.EventHandler(this.delete_cancel_button_Click);
            // 
            // delete_image_checkbox
            // 
            this.delete_image_checkbox.AutoSize = true;
            this.delete_image_checkbox.Checked = true;
            this.delete_image_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.delete_image_checkbox.Location = new System.Drawing.Point(12, 13);
            this.delete_image_checkbox.Name = "delete_image_checkbox";
            this.delete_image_checkbox.Size = new System.Drawing.Size(123, 21);
            this.delete_image_checkbox.TabIndex = 0;
            this.delete_image_checkbox.Text = "上传图片进行删除";
            this.delete_image_checkbox.UseVisualStyleBackColor = true;
            this.delete_image_checkbox.CheckedChanged += new System.EventHandler(this.delete_image_checkbox_CheckedChanged);
            // 
            // delete_sign_checkbox
            // 
            this.delete_sign_checkbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.delete_sign_checkbox.AutoSize = true;
            this.delete_sign_checkbox.Location = new System.Drawing.Point(12, 303);
            this.delete_sign_checkbox.Name = "delete_sign_checkbox";
            this.delete_sign_checkbox.Size = new System.Drawing.Size(147, 21);
            this.delete_sign_checkbox.TabIndex = 4;
            this.delete_sign_checkbox.Text = "输入图片签名进行删除";
            this.delete_sign_checkbox.UseVisualStyleBackColor = true;
            this.delete_sign_checkbox.CheckedChanged += new System.EventHandler(this.delete_sign_checkbox_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.delete_picturebox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.delete_sign_checkbox);
            this.panel1.Controls.Add(this.delete_sign_textbox);
            this.panel1.Controls.Add(this.delete_image_checkbox);
            this.panel1.Controls.Add(this.delete_path_button);
            this.panel1.Controls.Add(this.delete_path_textbox);
            this.panel1.Location = new System.Drawing.Point(-2, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 362);
            this.panel1.TabIndex = 4;
            // 
            // delete_picturebox
            // 
            this.delete_picturebox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.delete_picturebox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.delete_picturebox.Location = new System.Drawing.Point(69, 69);
            this.delete_picturebox.Name = "delete_picturebox";
            this.delete_picturebox.Size = new System.Drawing.Size(429, 228);
            this.delete_picturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.delete_picturebox.TabIndex = 62;
            this.delete_picturebox.TabStop = false;
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
            // delete_sign_textbox
            // 
            this.delete_sign_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.delete_sign_textbox.Location = new System.Drawing.Point(69, 330);
            this.delete_sign_textbox.Name = "delete_sign_textbox";
            this.delete_sign_textbox.ReadOnly = true;
            this.delete_sign_textbox.Size = new System.Drawing.Size(193, 23);
            this.delete_sign_textbox.TabIndex = 6;
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
            this.delete_bar,
            this.delete_label});
            this.statusStrip1.Location = new System.Drawing.Point(0, 439);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // delete_bar
            // 
            this.delete_bar.Name = "delete_bar";
            this.delete_bar.Size = new System.Drawing.Size(100, 16);
            // 
            // delete_label
            // 
            this.delete_label.Name = "delete_label";
            this.delete_label.Size = new System.Drawing.Size(80, 17);
            this.delete_label.Text = "等待用户操作";
            // 
            // delete_background
            // 
            this.delete_background.WorkerSupportsCancellation = true;
            this.delete_background.DoWork += new System.ComponentModel.DoWorkEventHandler(this.delete_background_DoWork);
            this.delete_background.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.delete_background_RunWorkerCompleted);
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
            this.Controls.Add(this.delete_cancel_button);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.depot_list_combobox);
            this.Controls.Add(this.label11);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "ImageDeleteForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "在库中删除图片";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageDeleteForm_FormClosing);
            this.Load += new System.EventHandler(this.ImageDeleteForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delete_picturebox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox depot_list_combobox;
        private System.Windows.Forms.TextBox delete_path_textbox;
        private System.Windows.Forms.Button delete_path_button;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.Button delete_cancel_button;
        private System.Windows.Forms.CheckBox delete_sign_checkbox;
        private System.Windows.Forms.CheckBox delete_image_checkbox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox delete_sign_textbox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar delete_bar;
        private System.Windows.Forms.ToolStripStatusLabel delete_label;
        private System.ComponentModel.BackgroundWorker delete_background;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox delete_picturebox;
    }
}