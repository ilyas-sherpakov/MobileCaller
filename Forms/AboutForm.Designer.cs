namespace MobileCaller.Forms
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.lAllRightsReserved = new System.Windows.Forms.Label();
            this.bOk = new System.Windows.Forms.Button();
            this.lProgramVersion = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.taycoonLink = new System.Windows.Forms.LinkLabel();
            this.lCopyright = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lAllRightsReserved
            // 
            this.lAllRightsReserved.AutoSize = true;
            this.lAllRightsReserved.Location = new System.Drawing.Point(12, 110);
            this.lAllRightsReserved.Name = "lAllRightsReserved";
            this.lAllRightsReserved.Size = new System.Drawing.Size(121, 13);
            this.lAllRightsReserved.TabIndex = 1;
            this.lAllRightsReserved.Text = "Все права защищены.";
            // 
            // bOk
            // 
            this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOk.Location = new System.Drawing.Point(248, 99);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 2;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            // 
            // lProgramVersion
            // 
            this.lProgramVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lProgramVersion.AutoSize = true;
            this.lProgramVersion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lProgramVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lProgramVersion.Location = new System.Drawing.Point(83, 19);
            this.lProgramVersion.Name = "lProgramVersion";
            this.lProgramVersion.Size = new System.Drawing.Size(243, 13);
            this.lProgramVersion.TabIndex = 3;
            this.lProgramVersion.Text = "Версия программы MobileCaller: 1.0.6.0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(77, 56);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox2.Location = new System.Drawing.Point(76, -1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(259, 56);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // taycoonLink
            // 
            this.taycoonLink.AutoSize = true;
            this.taycoonLink.Location = new System.Drawing.Point(13, 62);
            this.taycoonLink.Name = "taycoonLink";
            this.taycoonLink.Size = new System.Drawing.Size(95, 13);
            this.taycoonLink.TabIndex = 7;
            this.taycoonLink.TabStop = true;
            this.taycoonLink.Text = "www.taycoon.com";
            this.taycoonLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.taycoonLink_LinkClicked);
            // 
            // lCopyright
            // 
            this.lCopyright.AutoSize = true;
            this.lCopyright.Location = new System.Drawing.Point(12, 86);
            this.lCopyright.Name = "lCopyright";
            this.lCopyright.Size = new System.Drawing.Size(94, 13);
            this.lCopyright.TabIndex = 4;
            this.lCopyright.Text = "© Taycoon, 2012.";
            // 
            // AboutForm
            // 
            this.AcceptButton = this.bOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 134);
            this.Controls.Add(this.taycoonLink);
            this.Controls.Add(this.lProgramVersion);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lCopyright);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.lAllRightsReserved);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "О программе";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AboutForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lAllRightsReserved;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Label lProgramVersion;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.LinkLabel taycoonLink;
        private System.Windows.Forms.Label lCopyright;
    }
}