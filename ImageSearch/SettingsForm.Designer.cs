namespace ImageSearch
{
    partial class SettingsForm
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
            this.app_settings_save_button = new System.Windows.Forms.Button();
            this.appup_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.app_settings_cancel_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // app_settings_save_button
            // 
            this.app_settings_save_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.app_settings_save_button.Location = new System.Drawing.Point(220, 226);
            this.app_settings_save_button.Name = "app_settings_save_button";
            this.app_settings_save_button.Size = new System.Drawing.Size(75, 23);
            this.app_settings_save_button.TabIndex = 2;
            this.app_settings_save_button.Text = "保存";
            this.app_settings_save_button.UseVisualStyleBackColor = true;
            this.app_settings_save_button.Click += new System.EventHandler(this.app_settings_save_button_Click);
            // 
            // appup_textbox
            // 
            this.appup_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appup_textbox.Location = new System.Drawing.Point(90, 17);
            this.appup_textbox.Name = "appup_textbox";
            this.appup_textbox.Size = new System.Drawing.Size(286, 23);
            this.appup_textbox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "程序更新地址";
            // 
            // app_settings_cancel_button
            // 
            this.app_settings_cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.app_settings_cancel_button.Location = new System.Drawing.Point(301, 226);
            this.app_settings_cancel_button.Name = "app_settings_cancel_button";
            this.app_settings_cancel_button.Size = new System.Drawing.Size(75, 23);
            this.app_settings_cancel_button.TabIndex = 4;
            this.app_settings_cancel_button.Text = "取消";
            this.app_settings_cancel_button.UseVisualStyleBackColor = true;
            this.app_settings_cancel_button.Click += new System.EventHandler(this.app_settings_cancel_button_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.app_settings_cancel_button;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.app_settings_cancel_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.appup_textbox);
            this.Controls.Add(this.app_settings_save_button);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(400, 300);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选项";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button app_settings_save_button;
        private System.Windows.Forms.TextBox appup_textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button app_settings_cancel_button;
    }
}