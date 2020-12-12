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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgSettings));
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
            this.chkCyclicChecking = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRetrytimeAfterEvent = new System.Windows.Forms.TextBox();
            this.helpTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.txtAdditionalExceptions = new System.Windows.Forms.TextBox();
            this.chkGitSupport = new System.Windows.Forms.CheckBox();
            this.chkBypassLocal = new System.Windows.Forms.CheckBox();
            this.dlgSelectGitExecutable = new System.Windows.Forms.OpenFileDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbDefaultPrinter = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lstConfigurations
            // 
            this.lstConfigurations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstConfigurations.DisplayMember = "Name";
            this.lstConfigurations.FormattingEnabled = true;
            this.lstConfigurations.Location = new System.Drawing.Point(8, 8);
            this.lstConfigurations.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lstConfigurations.Name = "lstConfigurations";
            this.lstConfigurations.Size = new System.Drawing.Size(147, 342);
            this.lstConfigurations.TabIndex = 0;
            this.lstConfigurations.ValueMember = "Name";
            this.lstConfigurations.SelectedValueChanged += new System.EventHandler(this.lstConfigurations_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(214, 6);
            this.txtName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(192, 20);
            this.txtName.TabIndex = 1;
            this.helpTooltip.SetToolTip(this.txtName, "Name of the proxy configuration");
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(169, 222);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(235, 22);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtOwnIP
            // 
            this.txtOwnIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOwnIP.Location = new System.Drawing.Point(214, 27);
            this.txtOwnIP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtOwnIP.Name = "txtOwnIP";
            this.txtOwnIP.Size = new System.Drawing.Size(192, 20);
            this.txtOwnIP.TabIndex = 2;
            this.helpTooltip.SetToolTip(this.txtOwnIP, "Regular expression to specify the local IP of the computer");
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Own IP:";
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(169, 248);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(235, 22);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // txtProxy
            // 
            this.txtProxy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProxy.Location = new System.Drawing.Point(214, 47);
            this.txtProxy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtProxy.Name = "txtProxy";
            this.txtProxy.Size = new System.Drawing.Size(192, 20);
            this.txtProxy.TabIndex = 3;
            this.helpTooltip.SetToolTip(this.txtProxy, "Specifies the proxy to use. Should be provided \r\nin the following form: <host or " +
        "ip>:<port>");
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 49);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Proxy:";
            // 
            // chkAutoUpdate
            // 
            this.chkAutoUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoUpdate.AutoSize = true;
            this.chkAutoUpdate.Checked = true;
            this.chkAutoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoUpdate.Location = new System.Drawing.Point(167, 274);
            this.chkAutoUpdate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkAutoUpdate.Name = "chkAutoUpdate";
            this.chkAutoUpdate.Size = new System.Drawing.Size(86, 17);
            this.chkAutoUpdate.TabIndex = 6;
            this.chkAutoUpdate.Text = "Auto Update";
            this.helpTooltip.SetToolTip(this.chkAutoUpdate, "Evaluate network change events");
            this.chkAutoUpdate.UseVisualStyleBackColor = true;
            this.chkAutoUpdate.CheckedChanged += new System.EventHandler(this.chkAutoUpdate_CheckedChanged);
            // 
            // chkConsiderWinHTTP
            // 
            this.chkConsiderWinHTTP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkConsiderWinHTTP.AutoSize = true;
            this.chkConsiderWinHTTP.Location = new System.Drawing.Point(164, 317);
            this.chkConsiderWinHTTP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkConsiderWinHTTP.Name = "chkConsiderWinHTTP";
            this.chkConsiderWinHTTP.Size = new System.Drawing.Size(114, 17);
            this.chkConsiderWinHTTP.TabIndex = 9;
            this.chkConsiderWinHTTP.Text = "WinHTTP Support";
            this.chkConsiderWinHTTP.UseVisualStyleBackColor = true;
            this.chkConsiderWinHTTP.CheckedChanged += new System.EventHandler(this.chkConsiderWinHTTP_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(281, 315);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 18);
            this.label4.TabIndex = 11;
            this.label4.Text = "WinHTTP Support will only work \r\nwhen run as Administrator";
            // 
            // chkCyclicChecking
            // 
            this.chkCyclicChecking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCyclicChecking.AutoSize = true;
            this.chkCyclicChecking.Location = new System.Drawing.Point(279, 274);
            this.chkCyclicChecking.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkCyclicChecking.Name = "chkCyclicChecking";
            this.chkCyclicChecking.Size = new System.Drawing.Size(102, 17);
            this.chkCyclicChecking.TabIndex = 7;
            this.chkCyclicChecking.Text = "Cyclic Checking";
            this.helpTooltip.SetToolTip(this.chkCyclicChecking, "This setting specifies to check every 10 seconds \r\nif a new proxy setting needs t" +
        "o be applied based\r\non the provided configuration.");
            this.chkCyclicChecking.UseVisualStyleBackColor = true;
            this.chkCyclicChecking.CheckedChanged += new System.EventHandler(this.chkCyclicChecking_CheckedChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(171, 295);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Retrytime after Event [s]:";
            // 
            // txtRetrytimeAfterEvent
            // 
            this.txtRetrytimeAfterEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRetrytimeAfterEvent.Location = new System.Drawing.Point(297, 293);
            this.txtRetrytimeAfterEvent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRetrytimeAfterEvent.Name = "txtRetrytimeAfterEvent";
            this.txtRetrytimeAfterEvent.Size = new System.Drawing.Size(85, 20);
            this.txtRetrytimeAfterEvent.TabIndex = 8;
            this.txtRetrytimeAfterEvent.Text = "10";
            this.helpTooltip.SetToolTip(this.txtRetrytimeAfterEvent, resources.GetString("txtRetrytimeAfterEvent.ToolTip"));
            this.txtRetrytimeAfterEvent.TextChanged += new System.EventHandler(this.txtRetrytimeAfterEvent_TextChanged);
            // 
            // txtAdditionalExceptions
            // 
            this.txtAdditionalExceptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdditionalExceptions.Location = new System.Drawing.Point(169, 100);
            this.txtAdditionalExceptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAdditionalExceptions.Multiline = true;
            this.txtAdditionalExceptions.Name = "txtAdditionalExceptions";
            this.txtAdditionalExceptions.Size = new System.Drawing.Size(236, 85);
            this.txtAdditionalExceptions.TabIndex = 16;
            this.helpTooltip.SetToolTip(this.txtAdditionalExceptions, "Allows specification of addresses for which the proxy is to be bypassed. Entries " +
        "are separated by semicolon.");
            // 
            // chkGitSupport
            // 
            this.chkGitSupport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkGitSupport.AutoSize = true;
            this.chkGitSupport.Location = new System.Drawing.Point(164, 338);
            this.chkGitSupport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkGitSupport.Name = "chkGitSupport";
            this.chkGitSupport.Size = new System.Drawing.Size(77, 17);
            this.chkGitSupport.TabIndex = 13;
            this.chkGitSupport.Text = "git Support";
            this.helpTooltip.SetToolTip(this.chkGitSupport, "Specifies if the proxy is also to be updated in git configuration");
            this.chkGitSupport.UseVisualStyleBackColor = true;
            this.chkGitSupport.CheckedChanged += new System.EventHandler(this.chkGitSupport_CheckedChanged);
            // 
            // chkBypassLocal
            // 
            this.chkBypassLocal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBypassLocal.AutoSize = true;
            this.chkBypassLocal.Location = new System.Drawing.Point(168, 68);
            this.chkBypassLocal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkBypassLocal.Name = "chkBypassLocal";
            this.chkBypassLocal.Size = new System.Drawing.Size(136, 17);
            this.chkBypassLocal.TabIndex = 14;
            this.chkBypassLocal.Text = "Bypass local addresses";
            this.helpTooltip.SetToolTip(this.chkBypassLocal, "Specifies that the proxy is to be bypassed for addresses in local networks");
            this.chkBypassLocal.UseVisualStyleBackColor = true;
            // 
            // dlgSelectGitExecutable
            // 
            this.dlgSelectGitExecutable.FileName = "git.exe";
            this.dlgSelectGitExecutable.Filter = "git Executable|git.exe";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(170, 85);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(186, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Additional exceptions [separated by ;]:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(167, 185);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Default Printer:";
            // 
            // cmbDefaultPrinter
            // 
            this.cmbDefaultPrinter.FormattingEnabled = true;
            this.cmbDefaultPrinter.Location = new System.Drawing.Point(169, 200);
            this.cmbDefaultPrinter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbDefaultPrinter.Name = "cmbDefaultPrinter";
            this.cmbDefaultPrinter.Size = new System.Drawing.Size(236, 21);
            this.cmbDefaultPrinter.TabIndex = 18;
            // 
            // DlgSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 380);
            this.Controls.Add(this.cmbDefaultPrinter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtAdditionalExceptions);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chkBypassLocal);
            this.Controls.Add(this.chkGitSupport);
            this.Controls.Add(this.txtRetrytimeAfterEvent);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkCyclicChecking);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(438, 388);
            this.Name = "DlgSettings";
            this.Text = "DlgSettings";
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
        private System.Windows.Forms.CheckBox chkCyclicChecking;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRetrytimeAfterEvent;
        private System.Windows.Forms.ToolTip helpTooltip;
        private System.Windows.Forms.CheckBox chkGitSupport;
        private System.Windows.Forms.OpenFileDialog dlgSelectGitExecutable;
        private System.Windows.Forms.CheckBox chkBypassLocal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAdditionalExceptions;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbDefaultPrinter;
    }
}