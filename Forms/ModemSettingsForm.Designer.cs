namespace MobileCaller.Forms
{
    partial class ModemSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModemSettingsForm));
            this.lPort = new System.Windows.Forms.Label();
            this.cbComPort = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lPortRate = new System.Windows.Forms.Label();
            this.cbPortRate = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pbPort = new System.Windows.Forms.PictureBox();
            this.pbPortRate = new System.Windows.Forms.PictureBox();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.tbIEMI = new System.Windows.Forms.TextBox();
            this.lIMEI = new System.Windows.Forms.Label();
            this.lModemConnectionStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPortRate)).BeginInit();
            this.SuspendLayout();
            // 
            // lPort
            // 
            this.lPort.Location = new System.Drawing.Point(66, 22);
            this.lPort.Name = "lPort";
            this.lPort.Size = new System.Drawing.Size(35, 13);
            this.lPort.TabIndex = 0;
            this.lPort.Text = "Порт:";
            this.lPort.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbComPort
            // 
            this.cbComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComPort.FormattingEnabled = true;
            this.cbComPort.Location = new System.Drawing.Point(104, 19);
            this.cbComPort.Name = "cbComPort";
            this.cbComPort.Size = new System.Drawing.Size(70, 21);
            this.cbComPort.TabIndex = 1;
            this.cbComPort.SelectedIndexChanged += new System.EventHandler(this.cbComPort_SelectedIndexChanged);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(90, 160);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Да";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(171, 160);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lPortRate
            // 
            this.lPortRate.Location = new System.Drawing.Point(11, 53);
            this.lPortRate.Name = "lPortRate";
            this.lPortRate.Size = new System.Drawing.Size(90, 13);
            this.lPortRate.TabIndex = 5;
            this.lPortRate.Text = "Скорость порта:";
            this.lPortRate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbPortRate
            // 
            this.cbPortRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPortRate.FormattingEnabled = true;
            this.cbPortRate.Items.AddRange(new object[] {
            "300",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800"});
            this.cbPortRate.Location = new System.Drawing.Point(104, 50);
            this.cbPortRate.Name = "cbPortRate";
            this.cbPortRate.Size = new System.Drawing.Size(85, 21);
            this.cbPortRate.TabIndex = 6;
            this.cbPortRate.SelectedIndexChanged += new System.EventHandler(this.cbPortSpeed_SelectedIndexChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipTitle = "By";
            // 
            // pbPort
            // 
            this.pbPort.Image = ((System.Drawing.Image)(resources.GetObject("pbPort.Image")));
            this.pbPort.Location = new System.Drawing.Point(180, 19);
            this.pbPort.Name = "pbPort";
            this.pbPort.Size = new System.Drawing.Size(20, 20);
            this.pbPort.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPort.TabIndex = 7;
            this.pbPort.TabStop = false;
            // 
            // pbPortRate
            // 
            this.pbPortRate.Image = ((System.Drawing.Image)(resources.GetObject("pbPortRate.Image")));
            this.pbPortRate.Location = new System.Drawing.Point(194, 50);
            this.pbPortRate.Name = "pbPortRate";
            this.pbPortRate.Size = new System.Drawing.Size(20, 20);
            this.pbPortRate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPortRate.TabIndex = 8;
            this.pbPortRate.TabStop = false;
            // 
            // toolTip2
            // 
            this.toolTip2.ShowAlways = true;
            this.toolTip2.ToolTipTitle = "Hello";
            // 
            // tbIEMI
            // 
            this.tbIEMI.Location = new System.Drawing.Point(104, 117);
            this.tbIEMI.Name = "tbIEMI";
            this.tbIEMI.ReadOnly = true;
            this.tbIEMI.Size = new System.Drawing.Size(140, 20);
            this.tbIEMI.TabIndex = 9;
            this.tbIEMI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lIMEI
            // 
            this.lIMEI.AutoSize = true;
            this.lIMEI.Location = new System.Drawing.Point(48, 120);
            this.lIMEI.Name = "lIMEI";
            this.lIMEI.Size = new System.Drawing.Size(53, 13);
            this.lIMEI.TabIndex = 10;
            this.lIMEI.Text = "IMEI код:";
            // 
            // lModemConnectionStatus
            // 
            this.lModemConnectionStatus.ForeColor = System.Drawing.Color.Firebrick;
            this.lModemConnectionStatus.Location = new System.Drawing.Point(16, 88);
            this.lModemConnectionStatus.Name = "lModemConnectionStatus";
            this.lModemConnectionStatus.Size = new System.Drawing.Size(230, 23);
            this.lModemConnectionStatus.TabIndex = 11;
            this.lModemConnectionStatus.Text = "Модем не отвечает на команды.";
            this.lModemConnectionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ModemSettingsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(256, 195);
            this.Controls.Add(this.lModemConnectionStatus);
            this.Controls.Add(this.lIMEI);
            this.Controls.Add(this.tbIEMI);
            this.Controls.Add(this.cbPortRate);
            this.Controls.Add(this.lPortRate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbComPort);
            this.Controls.Add(this.lPort);
            this.Controls.Add(this.pbPort);
            this.Controls.Add(this.pbPortRate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModemSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки модема";
            this.Load += new System.EventHandler(this.ModemSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPortRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lPort;
        private System.Windows.Forms.ComboBox cbComPort;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lPortRate;
        private System.Windows.Forms.ComboBox cbPortRate;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pbPort;
        private System.Windows.Forms.PictureBox pbPortRate;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.TextBox tbIEMI;
        private System.Windows.Forms.Label lIMEI;
        private System.Windows.Forms.Label lModemConnectionStatus;
    }
}