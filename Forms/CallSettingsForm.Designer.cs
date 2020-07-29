namespace MobileCaller.Forms
{
    partial class CallSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CallSettingsForm));
            this.cbSendNotification = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.pbSendNotification = new System.Windows.Forms.PictureBox();
            this.tbWaitCall = new System.Windows.Forms.TextBox();
            this.lWaitCall = new System.Windows.Forms.Label();
            this.pbWaitCall = new System.Windows.Forms.PictureBox();
            this.gvGroups = new System.Windows.Forms.DataGridView();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.btnRemoveGroup = new System.Windows.Forms.Button();
            this.cbPlaySound = new System.Windows.Forms.CheckBox();
            this.pbPlaySound = new System.Windows.Forms.PictureBox();
            this.tbWorkingDirectory = new System.Windows.Forms.TextBox();
            this.lWorkingDirectory = new System.Windows.Forms.Label();
            this.pbWorkingDirectory = new System.Windows.Forms.PictureBox();
            this.btnOpenWorkingDirectory = new System.Windows.Forms.Button();
            this.pbRepeatable = new System.Windows.Forms.PictureBox();
            this.cbRepeatable = new System.Windows.Forms.CheckBox();
            this.pbShutdown = new System.Windows.Forms.PictureBox();
            this.cbShutdown = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSendNotification)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWaitCall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlaySound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWorkingDirectory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRepeatable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbShutdown)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSendNotification
            // 
            this.cbSendNotification.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSendNotification.Location = new System.Drawing.Point(182, 7);
            this.cbSendNotification.Name = "cbSendNotification";
            this.cbSendNotification.Size = new System.Drawing.Size(156, 17);
            this.cbSendNotification.TabIndex = 0;
            this.cbSendNotification.Text = "Отправлять уведомление";
            this.cbSendNotification.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSendNotification.UseVisualStyleBackColor = true;
            this.cbSendNotification.CheckedChanged += new System.EventHandler(this.cbSendNotification_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(719, 166);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(629, 166);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Да";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // pbSendNotification
            // 
            this.pbSendNotification.Image = ((System.Drawing.Image)(resources.GetObject("pbSendNotification.Image")));
            this.pbSendNotification.Location = new System.Drawing.Point(344, 4);
            this.pbSendNotification.Name = "pbSendNotification";
            this.pbSendNotification.Size = new System.Drawing.Size(20, 20);
            this.pbSendNotification.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbSendNotification.TabIndex = 9;
            this.pbSendNotification.TabStop = false;
            // 
            // tbWaitCall
            // 
            this.tbWaitCall.Location = new System.Drawing.Point(123, 7);
            this.tbWaitCall.Name = "tbWaitCall";
            this.tbWaitCall.Size = new System.Drawing.Size(27, 20);
            this.tbWaitCall.TabIndex = 18;
            this.tbWaitCall.TextChanged += new System.EventHandler(this.tbWaitCall_TextChanged);
            this.tbWaitCall.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbWaitCall_KeyPress);
            // 
            // lWaitCall
            // 
            this.lWaitCall.Location = new System.Drawing.Point(9, 9);
            this.lWaitCall.Name = "lWaitCall";
            this.lWaitCall.Size = new System.Drawing.Size(112, 13);
            this.lWaitCall.TabIndex = 17;
            this.lWaitCall.Text = "Задержка прозвона:";
            this.lWaitCall.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbWaitCall
            // 
            this.pbWaitCall.Image = ((System.Drawing.Image)(resources.GetObject("pbWaitCall.Image")));
            this.pbWaitCall.Location = new System.Drawing.Point(156, 6);
            this.pbWaitCall.Name = "pbWaitCall";
            this.pbWaitCall.Size = new System.Drawing.Size(20, 20);
            this.pbWaitCall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbWaitCall.TabIndex = 19;
            this.pbWaitCall.TabStop = false;
            // 
            // gvGroups
            // 
            this.gvGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvGroups.Location = new System.Drawing.Point(10, 63);
            this.gvGroups.Name = "gvGroups";
            this.gvGroups.Size = new System.Drawing.Size(809, 97);
            this.gvGroups.TabIndex = 23;
            this.gvGroups.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvGroups_CellEndEdit);
            this.gvGroups.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gvGroups_CellValidating);
            this.gvGroups.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gvGroups_DataBindingComplete);
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnAddGroup.Image")));
            this.btnAddGroup.Location = new System.Drawing.Point(757, 28);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(28, 28);
            this.btnAddGroup.TabIndex = 24;
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.bAddGroup_Click);
            // 
            // btnRemoveGroup
            // 
            this.btnRemoveGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveGroup.Image")));
            this.btnRemoveGroup.Location = new System.Drawing.Point(791, 28);
            this.btnRemoveGroup.Name = "btnRemoveGroup";
            this.btnRemoveGroup.Size = new System.Drawing.Size(28, 28);
            this.btnRemoveGroup.TabIndex = 25;
            this.btnRemoveGroup.UseVisualStyleBackColor = true;
            this.btnRemoveGroup.Click += new System.EventHandler(this.bRemoveGroup_Click);
            // 
            // cbPlaySound
            // 
            this.cbPlaySound.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbPlaySound.Location = new System.Drawing.Point(370, 6);
            this.cbPlaySound.Name = "cbPlaySound";
            this.cbPlaySound.Size = new System.Drawing.Size(120, 17);
            this.cbPlaySound.TabIndex = 26;
            this.cbPlaySound.Text = "Проигрывать звук";
            this.cbPlaySound.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbPlaySound.UseVisualStyleBackColor = true;
            this.cbPlaySound.CheckedChanged += new System.EventHandler(this.cbPlaySound_CheckedChanged);
            // 
            // pbPlaySound
            // 
            this.pbPlaySound.Image = ((System.Drawing.Image)(resources.GetObject("pbPlaySound.Image")));
            this.pbPlaySound.Location = new System.Drawing.Point(496, 3);
            this.pbPlaySound.Name = "pbPlaySound";
            this.pbPlaySound.Size = new System.Drawing.Size(20, 20);
            this.pbPlaySound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPlaySound.TabIndex = 27;
            this.pbPlaySound.TabStop = false;
            // 
            // tbWorkingDirectory
            // 
            this.tbWorkingDirectory.Location = new System.Drawing.Point(123, 33);
            this.tbWorkingDirectory.Name = "tbWorkingDirectory";
            this.tbWorkingDirectory.ReadOnly = true;
            this.tbWorkingDirectory.Size = new System.Drawing.Size(500, 20);
            this.tbWorkingDirectory.TabIndex = 28;
            // 
            // lWorkingDirectory
            // 
            this.lWorkingDirectory.AutoSize = true;
            this.lWorkingDirectory.Location = new System.Drawing.Point(13, 37);
            this.lWorkingDirectory.Name = "lWorkingDirectory";
            this.lWorkingDirectory.Size = new System.Drawing.Size(95, 13);
            this.lWorkingDirectory.TabIndex = 29;
            this.lWorkingDirectory.Text = "Рабочий каталог:";
            // 
            // pbWorkingDirectory
            // 
            this.pbWorkingDirectory.Image = ((System.Drawing.Image)(resources.GetObject("pbWorkingDirectory.Image")));
            this.pbWorkingDirectory.Location = new System.Drawing.Point(663, 33);
            this.pbWorkingDirectory.Name = "pbWorkingDirectory";
            this.pbWorkingDirectory.Size = new System.Drawing.Size(20, 20);
            this.pbWorkingDirectory.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbWorkingDirectory.TabIndex = 30;
            this.pbWorkingDirectory.TabStop = false;
            // 
            // btnOpenWorkingDirectory
            // 
            this.btnOpenWorkingDirectory.Location = new System.Drawing.Point(629, 28);
            this.btnOpenWorkingDirectory.Name = "btnOpenWorkingDirectory";
            this.btnOpenWorkingDirectory.Size = new System.Drawing.Size(28, 28);
            this.btnOpenWorkingDirectory.TabIndex = 31;
            this.btnOpenWorkingDirectory.Text = "...";
            this.btnOpenWorkingDirectory.UseVisualStyleBackColor = true;
            this.btnOpenWorkingDirectory.Click += new System.EventHandler(this.btnOpenWorkingDirectory_Click);
            // 
            // pbRepeatable
            // 
            this.pbRepeatable.Image = ((System.Drawing.Image)(resources.GetObject("pbRepeatable.Image")));
            this.pbRepeatable.Location = new System.Drawing.Point(629, 2);
            this.pbRepeatable.Name = "pbRepeatable";
            this.pbRepeatable.Size = new System.Drawing.Size(20, 20);
            this.pbRepeatable.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbRepeatable.TabIndex = 33;
            this.pbRepeatable.TabStop = false;
            // 
            // cbRepeatable
            // 
            this.cbRepeatable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbRepeatable.Location = new System.Drawing.Point(521, 5);
            this.cbRepeatable.Name = "cbRepeatable";
            this.cbRepeatable.Size = new System.Drawing.Size(102, 17);
            this.cbRepeatable.TabIndex = 32;
            this.cbRepeatable.Text = "Непрерывный";
            this.cbRepeatable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbRepeatable.UseVisualStyleBackColor = true;
            this.cbRepeatable.CheckedChanged += new System.EventHandler(this.cbRepeatable_CheckedChanged);
            // 
            // pbShutdown
            // 
            this.pbShutdown.Image = ((System.Drawing.Image)(resources.GetObject("pbShutdown.Image")));
            this.pbShutdown.Location = new System.Drawing.Point(805, 2);
            this.pbShutdown.Name = "pbShutdown";
            this.pbShutdown.Size = new System.Drawing.Size(20, 20);
            this.pbShutdown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbShutdown.TabIndex = 35;
            this.pbShutdown.TabStop = false;
            // 
            // cbShutdown
            // 
            this.cbShutdown.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbShutdown.Location = new System.Drawing.Point(656, 5);
            this.cbShutdown.Name = "cbShutdown";
            this.cbShutdown.Size = new System.Drawing.Size(143, 17);
            this.cbShutdown.TabIndex = 34;
            this.cbShutdown.Text = "Выключить компьютер";
            this.cbShutdown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbShutdown.UseVisualStyleBackColor = true;
            this.cbShutdown.CheckedChanged += new System.EventHandler(this.cbShutdown_CheckedChanged);
            // 
            // CallSettingsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(831, 201);
            this.Controls.Add(this.pbShutdown);
            this.Controls.Add(this.cbShutdown);
            this.Controls.Add(this.pbRepeatable);
            this.Controls.Add(this.cbRepeatable);
            this.Controls.Add(this.btnOpenWorkingDirectory);
            this.Controls.Add(this.pbWorkingDirectory);
            this.Controls.Add(this.lWorkingDirectory);
            this.Controls.Add(this.tbWorkingDirectory);
            this.Controls.Add(this.pbPlaySound);
            this.Controls.Add(this.cbPlaySound);
            this.Controls.Add(this.btnRemoveGroup);
            this.Controls.Add(this.btnAddGroup);
            this.Controls.Add(this.gvGroups);
            this.Controls.Add(this.tbWaitCall);
            this.Controls.Add(this.lWaitCall);
            this.Controls.Add(this.pbWaitCall);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbSendNotification);
            this.Controls.Add(this.pbSendNotification);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CallSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки прозвона";
            this.Load += new System.EventHandler(this.CallSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSendNotification)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWaitCall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlaySound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWorkingDirectory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRepeatable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbShutdown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbSendNotification;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox pbSendNotification;
        private System.Windows.Forms.TextBox tbWaitCall;
        private System.Windows.Forms.Label lWaitCall;
        private System.Windows.Forms.PictureBox pbWaitCall;
        private System.Windows.Forms.DataGridView gvGroups;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.Button btnRemoveGroup;
        private System.Windows.Forms.CheckBox cbPlaySound;
        private System.Windows.Forms.PictureBox pbPlaySound;
        private System.Windows.Forms.TextBox tbWorkingDirectory;
        private System.Windows.Forms.Label lWorkingDirectory;
        private System.Windows.Forms.PictureBox pbWorkingDirectory;
        private System.Windows.Forms.Button btnOpenWorkingDirectory;
        private System.Windows.Forms.PictureBox pbRepeatable;
        private System.Windows.Forms.CheckBox cbRepeatable;
        private System.Windows.Forms.PictureBox pbShutdown;
        private System.Windows.Forms.CheckBox cbShutdown;
    }
}