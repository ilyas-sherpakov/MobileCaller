namespace MobileCaller.Forms
{
    partial class CKeyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CKeyForm));
            this.lEmail = new System.Windows.Forms.Label();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.lActivationKey = new System.Windows.Forms.Label();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lUser = new System.Windows.Forms.Label();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.tbIMEI = new System.Windows.Forms.TextBox();
            this.lIMEI = new System.Windows.Forms.Label();
            this.lRegistrationInvitation = new System.Windows.Forms.Label();
            this.lRegistrationWarning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lEmail
            // 
            this.lEmail.AutoSize = true;
            this.lEmail.Location = new System.Drawing.Point(120, 104);
            this.lEmail.Name = "lEmail";
            this.lEmail.Size = new System.Drawing.Size(82, 13);
            this.lEmail.TabIndex = 0;
            this.lEmail.Text = "Введите e-mail:";
            // 
            // tbEmail
            // 
            this.tbEmail.Location = new System.Drawing.Point(123, 120);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(148, 20);
            this.tbEmail.TabIndex = 1;
            // 
            // lActivationKey
            // 
            this.lActivationKey.AutoSize = true;
            this.lActivationKey.Location = new System.Drawing.Point(5, 143);
            this.lActivationKey.Name = "lActivationKey";
            this.lActivationKey.Size = new System.Drawing.Size(129, 13);
            this.lActivationKey.TabIndex = 2;
            this.lActivationKey.Text = "Введите код активации:";
            // 
            // tbKey
            // 
            this.tbKey.Location = new System.Drawing.Point(6, 159);
            this.tbKey.Multiline = true;
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(431, 20);
            this.tbKey.TabIndex = 3;
            // 
            // btnEnter
            // 
            this.btnEnter.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnEnter.Location = new System.Drawing.Point(281, 185);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(75, 23);
            this.btnEnter.TabIndex = 4;
            this.btnEnter.Text = "Ввести";
            this.btnEnter.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(362, 185);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lUser
            // 
            this.lUser.AutoSize = true;
            this.lUser.Location = new System.Drawing.Point(5, 104);
            this.lUser.Name = "lUser";
            this.lUser.Size = new System.Drawing.Size(83, 13);
            this.lUser.TabIndex = 6;
            this.lUser.Text = "Пользователь:";
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(6, 120);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(100, 20);
            this.tbLogin.TabIndex = 0;
            // 
            // tbIMEI
            // 
            this.tbIMEI.Location = new System.Drawing.Point(289, 120);
            this.tbIMEI.Name = "tbIMEI";
            this.tbIMEI.Size = new System.Drawing.Size(148, 20);
            this.tbIMEI.TabIndex = 2;
            // 
            // lIMEI
            // 
            this.lIMEI.AutoSize = true;
            this.lIMEI.Location = new System.Drawing.Point(286, 104);
            this.lIMEI.Name = "lIMEI";
            this.lIMEI.Size = new System.Drawing.Size(98, 13);
            this.lIMEI.TabIndex = 8;
            this.lIMEI.Text = "Введите IMEI код:";
            // 
            // lRegistrationInvitation
            // 
            this.lRegistrationInvitation.AutoSize = true;
            this.lRegistrationInvitation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lRegistrationInvitation.Location = new System.Drawing.Point(12, 81);
            this.lRegistrationInvitation.Name = "lRegistrationInvitation";
            this.lRegistrationInvitation.Size = new System.Drawing.Size(389, 13);
            this.lRegistrationInvitation.TabIndex = 9;
            this.lRegistrationInvitation.Text = "Введите регистрационные данные и полученный код активации";
            // 
            // lRegistrationWarning
            // 
            this.lRegistrationWarning.AutoSize = true;
            this.lRegistrationWarning.Location = new System.Drawing.Point(12, 13);
            this.lRegistrationWarning.MaximumSize = new System.Drawing.Size(430, 0);
            this.lRegistrationWarning.Name = "lRegistrationWarning";
            this.lRegistrationWarning.Size = new System.Drawing.Size(425, 52);
            this.lRegistrationWarning.TabIndex = 10;
            this.lRegistrationWarning.Text = resources.GetString("lRegistrationWarning.Text");
            // 
            // CKeyForm
            // 
            this.AcceptButton = this.btnEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(445, 216);
            this.Controls.Add(this.lRegistrationWarning);
            this.Controls.Add(this.lRegistrationInvitation);
            this.Controls.Add(this.tbIMEI);
            this.Controls.Add(this.lIMEI);
            this.Controls.Add(this.tbLogin);
            this.Controls.Add(this.lUser);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.tbKey);
            this.Controls.Add(this.lActivationKey);
            this.Controls.Add(this.tbEmail);
            this.Controls.Add(this.lEmail);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CKeyForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ввод ключа";
            this.Load += new System.EventHandler(this.CKeyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lEmail;
        private System.Windows.Forms.TextBox tbEmail;
        private System.Windows.Forms.Label lActivationKey;
        private System.Windows.Forms.TextBox tbKey;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lUser;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.TextBox tbIMEI;
        private System.Windows.Forms.Label lIMEI;
        private System.Windows.Forms.Label lRegistrationInvitation;
        private System.Windows.Forms.Label lRegistrationWarning;
    }
}