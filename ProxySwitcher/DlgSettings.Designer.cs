namespace ProxySwitcher
{
    partial class DlgSettings
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
            this.lstConfigurations = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtOwnIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.txtProxy = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkAutoUpdate = new System.Windows.Forms.CheckBox();
            this.chkConsiderWinHTTP = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstConfigurations
            // 
            this.lstConfigurations.DisplayMember = "Name";
            this.lstConfigurations.FormattingEnabled = true;
            this.lstConfigurations.ItemHeight = 20;
            this.lstConfigurations.Location = new System.Drawing.Point(12, 12);
            this.lstConfigurations.Name = "lstConfigurations";
            this.lstConfigurations.Size = new System.Drawing.Size(219, 244);
            this.lstConfigurations.TabIndex = 0;
            this.lstConfigurations.ValueMember = "Name";
            this.lstConfigurations.SelectedValueChanged += new System.EventHandler(this.lstConfigurations_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(260, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(321, 9);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(286, 26);
            this.txtName.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(255, 117);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(352, 34);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtOwnIP
            // 
            this.txtOwnIP.Location = new System.Drawing.Point(321, 41);
            this.txtOwnIP.Name = "txtOwnIP";
            this.txtOwnIP.Size = new System.Drawing.Size(286, 26);
            this.txtOwnIP.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Own IP:";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(255, 157);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(352, 34);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // txtProxy
            // 
            this.txtProxy.Location = new System.Drawing.Point(321, 73);
            this.txtProxy.Name = "txtProxy";
            this.txtProxy.Size = new System.Drawing.Size(286, 26);
            this.txtProxy.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Proxy:";
            // 
            // chkAutoUpdate
            // 
            this.chkAutoUpdate.AutoSize = true;
            this.chkAutoUpdate.Location = new System.Drawing.Point(255, 197);
            this.chkAutoUpdate.Name = "chkAutoUpdate";
            this.chkAutoUpdate.Size = new System.Drawing.Size(126, 24);
            this.chkAutoUpdate.TabIndex = 9;
            this.chkAutoUpdate.Text = "Auto Update";
            this.chkAutoUpdate.UseVisualStyleBackColor = true;
            this.chkAutoUpdate.CheckedChanged += new System.EventHandler(this.chkAutoUpdate_CheckedChanged);
            // 
            // chkConsiderWinHTTP
            // 
            this.chkConsiderWinHTTP.AutoSize = true;
            this.chkConsiderWinHTTP.Location = new System.Drawing.Point(255, 227);
            this.chkConsiderWinHTTP.Name = "chkConsiderWinHTTP";
            this.chkConsiderWinHTTP.Size = new System.Drawing.Size(163, 24);
            this.chkConsiderWinHTTP.TabIndex = 10;
            this.chkConsiderWinHTTP.Text = "WinHTTP Support";
            this.chkConsiderWinHTTP.UseVisualStyleBackColor = true;
            this.chkConsiderWinHTTP.CheckedChanged += new System.EventHandler(this.chkConsiderWinHTTP_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(424, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 30);
            this.label4.TabIndex = 11;
            this.label4.Text = "WinHTTP Support will only work \r\nwhen run as Administrator";
            // 
            // DlgSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 274);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkConsiderWinHTTP);
            this.Controls.Add(this.chkAutoUpdate);
            this.Controls.Add(this.txtProxy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.txtOwnIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstConfigurations);
            this.Name = "DlgSettings";
            this.Text = "DlgSettings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DlgSettings_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstConfigurations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtOwnIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.TextBox txtProxy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkAutoUpdate;
        private System.Windows.Forms.CheckBox chkConsiderWinHTTP;
        private System.Windows.Forms.Label label4;
    }
}