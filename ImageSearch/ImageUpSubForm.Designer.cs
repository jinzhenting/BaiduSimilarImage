namespace ImageSearch
{
    partial class ImageUpSubForm
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
            this.sub_checkedlistbox = new System.Windows.Forms.CheckedListBox();
            this.sub_cancel_button = new System.Windows.Forms.Button();
            this.sub_ok_button = new System.Windows.Forms.Button();
            this.sub_all_button = new System.Windows.Forms.Button();
            this.sub_clear_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sub_checkedlistbox
            // 
            this.sub_checkedlistbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sub_checkedlistbox.FormattingEnabled = true;
            this.sub_checkedlistbox.Location = new System.Drawing.Point(-1, 0);
            this.sub_checkedlistbox.Name = "sub_checkedlistbox";
            this.sub_checkedlistbox.Size = new System.Drawing.Size(331, 364);
            this.sub_checkedlistbox.TabIndex = 0;
            // 
            // sub_cancel_button
            // 
            this.sub_cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sub_cancel_button.Location = new System.Drawing.Point(248, 370);
            this.sub_cancel_button.Name = "sub_cancel_button";
            this.sub_cancel_button.Size = new System.Drawing.Size(75, 23);
            this.sub_cancel_button.TabIndex = 4;
            this.sub_cancel_button.Text = "取消";
            this.sub_cancel_button.UseVisualStyleBackColor = true;
            this.sub_cancel_button.Click += new System.EventHandler(this.sub_cancel_button_Click);
            // 
            // sub_ok_button
            // 
            this.sub_ok_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sub_ok_button.Location = new System.Drawing.Point(167, 370);
            this.sub_ok_button.Name = "sub_ok_button";
            this.sub_ok_button.Size = new System.Drawing.Size(75, 23);
            this.sub_ok_button.TabIndex = 3;
            this.sub_ok_button.Text = "确定";
            this.sub_ok_button.UseVisualStyleBackColor = true;
            this.sub_ok_button.Click += new System.EventHandler(this.sub_ok_button_Click);
            // 
            // sub_all_button
            // 
            this.sub_all_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sub_all_button.Location = new System.Drawing.Point(5, 370);
            this.sub_all_button.Name = "sub_all_button";
            this.sub_all_button.Size = new System.Drawing.Size(75, 23);
            this.sub_all_button.TabIndex = 1;
            this.sub_all_button.Text = "全选";
            this.sub_all_button.UseVisualStyleBackColor = true;
            this.sub_all_button.Click += new System.EventHandler(this.sub_all_button_Click);
            // 
            // sub_clear_button
            // 
            this.sub_clear_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sub_clear_button.Location = new System.Drawing.Point(86, 370);
            this.sub_clear_button.Name = "sub_clear_button";
            this.sub_clear_button.Size = new System.Drawing.Size(75, 23);
            this.sub_clear_button.TabIndex = 2;
            this.sub_clear_button.Text = "全不选";
            this.sub_clear_button.UseVisualStyleBackColor = true;
            this.sub_clear_button.Click += new System.EventHandler(this.sub_clear_button_Click);
            // 
            // ImageUpSubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 399);
            this.Controls.Add(this.sub_all_button);
            this.Controls.Add(this.sub_clear_button);
            this.Controls.Add(this.sub_ok_button);
            this.Controls.Add(this.sub_cancel_button);
            this.Controls.Add(this.sub_checkedlistbox);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(345, 438);
            this.Name = "ImageUpSubForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择子目录";
            this.Load += new System.EventHandler(this.ImageUpSubForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox sub_checkedlistbox;
        private System.Windows.Forms.Button sub_cancel_button;
        private System.Windows.Forms.Button sub_ok_button;
        private System.Windows.Forms.Button sub_all_button;
        private System.Windows.Forms.Button sub_clear_button;
    }
}