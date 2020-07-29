namespace MobileCaller.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnAddDiapason = new System.Windows.Forms.Button();
            this.listTelephones = new System.Windows.Forms.ListView();
            this.telephone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateActivation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miSelectActivated = new System.Windows.Forms.ToolStripMenuItem();
            this.miCallSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.miCallLatest = new System.Windows.Forms.ToolStripMenuItem();
            this.miSendNotification = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetActivationSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.miResetActivationSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.listImages = new System.Windows.Forms.ImageList(this.components);
            this.tbFrom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTo = new System.Windows.Forms.TextBox();
            this.btnCall = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miImport = new System.Windows.Forms.ToolStripMenuItem();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.miComment = new System.Windows.Forms.ToolStripMenuItem();
            this.miSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.miSettingsModem = new System.Windows.Forms.ToolStripMenuItem();
            this.miSettingsCall = new System.Windows.Forms.ToolStripMenuItem();
            this.miView = new System.Windows.Forms.ToolStripMenuItem();
            this.miLog = new System.Windows.Forms.ToolStripMenuItem();
            this.miLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.miLanguageRussian = new System.Windows.Forms.ToolStripMenuItem();
            this.miLanguageEnglish = new System.Windows.Forms.ToolStripMenuItem();
            this.miLanguageUkraine = new System.Windows.Forms.ToolStripMenuItem();
            this.miInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.miRegistration = new System.Windows.Forms.ToolStripMenuItem();
            this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.lGroupFilter = new System.Windows.Forms.Label();
            this.cbGroupFilter = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statTelephoneCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statTelephoneActivatedCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statRemainedTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.statBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lUser = new System.Windows.Forms.Label();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbCountryFilter = new System.Windows.Forms.ComboBox();
            this.contextMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddDiapason
            // 
            this.btnAddDiapason.Location = new System.Drawing.Point(208, 10);
            this.btnAddDiapason.Name = "btnAddDiapason";
            this.btnAddDiapason.Size = new System.Drawing.Size(102, 23);
            this.btnAddDiapason.TabIndex = 0;
            this.btnAddDiapason.Text = "Ввести диапазон";
            this.btnAddDiapason.UseVisualStyleBackColor = true;
            this.btnAddDiapason.Click += new System.EventHandler(this.btnAddDiapason_Click);
            // 
            // listTelephones
            // 
            this.listTelephones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.telephone,
            this.date,
            this.dateActivation,
            this.comment});
            this.listTelephones.ContextMenuStrip = this.contextMenu;
            this.listTelephones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTelephones.FullRowSelect = true;
            this.listTelephones.GridLines = true;
            this.listTelephones.Location = new System.Drawing.Point(0, 0);
            this.listTelephones.Name = "listTelephones";
            this.listTelephones.Size = new System.Drawing.Size(559, 218);
            this.listTelephones.SmallImageList = this.listImages;
            this.listTelephones.TabIndex = 1;
            this.listTelephones.UseCompatibleStateImageBehavior = false;
            this.listTelephones.View = System.Windows.Forms.View.Details;
            this.listTelephones.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listTelephones_ColumnClick);
            this.listTelephones.DoubleClick += new System.EventHandler(this.listTelephones_DoubleClick);
            this.listTelephones.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listTelephones_KeyDown);
            this.listTelephones.Resize += new System.EventHandler(this.listTelephones_Resize);
            // 
            // telephone
            // 
            this.telephone.Text = "Номер телефона";
            this.telephone.Width = 169;
            // 
            // date
            // 
            this.date.Text = "Дата ввода";
            this.date.Width = 127;
            // 
            // dateActivation
            // 
            this.dateActivation.Text = "Дата активации";
            this.dateActivation.Width = 136;
            // 
            // comment
            // 
            this.comment.Text = "Комментарий";
            this.comment.Width = 129;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSelectActivated,
            this.miCallSelected,
            this.miCallLatest,
            this.miSendNotification,
            this.miCopyToClipboard,
            this.miSetActivationSelected,
            this.miResetActivationSelected,
            this.miDeleteSelected});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(223, 180);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // miSelectActivated
            // 
            this.miSelectActivated.Name = "miSelectActivated";
            this.miSelectActivated.Size = new System.Drawing.Size(222, 22);
            this.miSelectActivated.Text = "Выделить активированные";
            this.miSelectActivated.Click += new System.EventHandler(this.miSelectActivated_Click);
            // 
            // miCallSelected
            // 
            this.miCallSelected.Name = "miCallSelected";
            this.miCallSelected.Size = new System.Drawing.Size(222, 22);
            this.miCallSelected.Text = "Прозвонить выделенные";
            this.miCallSelected.Click += new System.EventHandler(this.miCallSelected_Click);
            // 
            // miCallLatest
            // 
            this.miCallLatest.Name = "miCallLatest";
            this.miCallLatest.Size = new System.Drawing.Size(222, 22);
            this.miCallLatest.Text = "Прозвонить оставшиеся";
            this.miCallLatest.Click += new System.EventHandler(this.miCallLatest_Click);
            // 
            // miSendNotification
            // 
            this.miSendNotification.Name = "miSendNotification";
            this.miSendNotification.Size = new System.Drawing.Size(222, 22);
            this.miSendNotification.Text = "Отправить уведомление";
            this.miSendNotification.Click += new System.EventHandler(this.miSendNotification_Click);
            // 
            // miCopyToClipboard
            // 
            this.miCopyToClipboard.Name = "miCopyToClipboard";
            this.miCopyToClipboard.Size = new System.Drawing.Size(222, 22);
            this.miCopyToClipboard.Text = "Скопировать в буффер";
            this.miCopyToClipboard.Click += new System.EventHandler(this.miCopyToClipboard_Click);
            // 
            // miSetActivationSelected
            // 
            this.miSetActivationSelected.Name = "miSetActivationSelected";
            this.miSetActivationSelected.Size = new System.Drawing.Size(222, 22);
            this.miSetActivationSelected.Text = "Установить активацию";
            this.miSetActivationSelected.Click += new System.EventHandler(this.miSetActivationSelected_Click);
            // 
            // miResetActivationSelected
            // 
            this.miResetActivationSelected.Name = "miResetActivationSelected";
            this.miResetActivationSelected.Size = new System.Drawing.Size(222, 22);
            this.miResetActivationSelected.Text = "Сбросить активацию";
            this.miResetActivationSelected.Click += new System.EventHandler(this.miResetActivationSelected_Click);
            // 
            // miDeleteSelected
            // 
            this.miDeleteSelected.Name = "miDeleteSelected";
            this.miDeleteSelected.Size = new System.Drawing.Size(222, 22);
            this.miDeleteSelected.Text = "Удалить выделенные";
            this.miDeleteSelected.Click += new System.EventHandler(this.miDeleteSelected_Click);
            // 
            // listImages
            // 
            this.listImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("listImages.ImageStream")));
            this.listImages.TransparentColor = System.Drawing.Color.Transparent;
            this.listImages.Images.SetKeyName(0, "active.png");
            // 
            // tbFrom
            // 
            this.tbFrom.Location = new System.Drawing.Point(12, 12);
            this.tbFrom.Name = "tbFrom";
            this.tbFrom.Size = new System.Drawing.Size(84, 20);
            this.tbFrom.TabIndex = 2;
            this.tbFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFromTo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = ":";
            // 
            // tbTo
            // 
            this.tbTo.Location = new System.Drawing.Point(118, 12);
            this.tbTo.Name = "tbTo";
            this.tbTo.Size = new System.Drawing.Size(84, 20);
            this.tbTo.TabIndex = 4;
            this.tbTo.Enter += new System.EventHandler(this.tbTo_Enter);
            this.tbTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFromTo_KeyPress);
            // 
            // btnCall
            // 
            this.btnCall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCall.Location = new System.Drawing.Point(355, 6);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(102, 23);
            this.btnCall.TabIndex = 7;
            this.btnCall.Text = "Прозвон";
            this.btnCall.UseVisualStyleBackColor = true;
            this.btnCall.Click += new System.EventHandler(this.bCall_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miEdit,
            this.miSettings,
            this.miView,
            this.miLanguage,
            this.miInformation});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(559, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miImport,
            this.miSave,
            this.miExit});
            this.miFile.Name = "miFile";
            this.miFile.Size = new System.Drawing.Size(48, 20);
            this.miFile.Text = "Файл";
            this.miFile.DropDownOpening += new System.EventHandler(this.miFile_DropDownOpening);
            // 
            // miImport
            // 
            this.miImport.Name = "miImport";
            this.miImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.miImport.Size = new System.Drawing.Size(235, 22);
            this.miImport.Text = "Подгрузить номера";
            this.miImport.Click += new System.EventHandler(this.miImport_Click);
            // 
            // miSave
            // 
            this.miSave.Name = "miSave";
            this.miSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSave.Size = new System.Drawing.Size(235, 22);
            this.miSave.Text = "Сохранить изменения";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(235, 22);
            this.miExit.Text = "Выход";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miEdit
            // 
            this.miEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSearch,
            this.miComment});
            this.miEdit.Name = "miEdit";
            this.miEdit.Size = new System.Drawing.Size(59, 20);
            this.miEdit.Text = "Правка";
            this.miEdit.DropDownOpening += new System.EventHandler(this.miEdit_DropDownOpening);
            // 
            // miSearch
            // 
            this.miSearch.Name = "miSearch";
            this.miSearch.ShortcutKeyDisplayString = "";
            this.miSearch.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.miSearch.Size = new System.Drawing.Size(151, 22);
            this.miSearch.Text = "Поиск";
            this.miSearch.Click += new System.EventHandler(this.miSearch_Click);
            // 
            // miComment
            // 
            this.miComment.Name = "miComment";
            this.miComment.Size = new System.Drawing.Size(151, 22);
            this.miComment.Text = "Комментарий";
            this.miComment.Click += new System.EventHandler(this.miComment_Click);
            // 
            // miSettings
            // 
            this.miSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSettingsModem,
            this.miSettingsCall});
            this.miSettings.Name = "miSettings";
            this.miSettings.Size = new System.Drawing.Size(79, 20);
            this.miSettings.Text = "Настройки";
            // 
            // miSettingsModem
            // 
            this.miSettingsModem.Name = "miSettingsModem";
            this.miSettingsModem.Size = new System.Drawing.Size(189, 22);
            this.miSettingsModem.Text = "Настройки модема";
            this.miSettingsModem.Click += new System.EventHandler(this.miModemSettings_Click);
            // 
            // miSettingsCall
            // 
            this.miSettingsCall.Name = "miSettingsCall";
            this.miSettingsCall.Size = new System.Drawing.Size(189, 22);
            this.miSettingsCall.Text = "Настройки прозвона";
            this.miSettingsCall.Click += new System.EventHandler(this.miSettingsCall_Click);
            // 
            // miView
            // 
            this.miView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLog});
            this.miView.Name = "miView";
            this.miView.Size = new System.Drawing.Size(47, 20);
            this.miView.Text = "Окна";
            // 
            // miLog
            // 
            this.miLog.Name = "miLog";
            this.miLog.Size = new System.Drawing.Size(118, 22);
            this.miLog.Text = "Журнал";
            this.miLog.Click += new System.EventHandler(this.miLog_Click);
            // 
            // miLanguage
            // 
            this.miLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLanguageRussian,
            this.miLanguageEnglish,
            this.miLanguageUkraine});
            this.miLanguage.Name = "miLanguage";
            this.miLanguage.Size = new System.Drawing.Size(46, 20);
            this.miLanguage.Text = "Язык";
            // 
            // miLanguageRussian
            // 
            this.miLanguageRussian.Name = "miLanguageRussian";
            this.miLanguageRussian.Size = new System.Drawing.Size(141, 22);
            this.miLanguageRussian.Text = "Русский";
            this.miLanguageRussian.Click += new System.EventHandler(this.miLanguageRussian_Click);
            // 
            // miLanguageEnglish
            // 
            this.miLanguageEnglish.Name = "miLanguageEnglish";
            this.miLanguageEnglish.Size = new System.Drawing.Size(141, 22);
            this.miLanguageEnglish.Text = "Английский";
            this.miLanguageEnglish.Click += new System.EventHandler(this.miLanguageEnglish_Click);
            // 
            // miLanguageUkraine
            // 
            this.miLanguageUkraine.Name = "miLanguageUkraine";
            this.miLanguageUkraine.Size = new System.Drawing.Size(141, 22);
            this.miLanguageUkraine.Text = "Украинский";
            this.miLanguageUkraine.Click += new System.EventHandler(this.miLanguageUkraine_Click);
            // 
            // miInformation
            // 
            this.miInformation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRegistration,
            this.miHelp,
            this.miAbout});
            this.miInformation.Name = "miInformation";
            this.miInformation.Size = new System.Drawing.Size(93, 20);
            this.miInformation.Text = "Информация";
            // 
            // miRegistration
            // 
            this.miRegistration.Name = "miRegistration";
            this.miRegistration.Size = new System.Drawing.Size(149, 22);
            this.miRegistration.Text = "Регистрация";
            this.miRegistration.Click += new System.EventHandler(this.miRegistration_Click);
            // 
            // miHelp
            // 
            this.miHelp.Name = "miHelp";
            this.miHelp.Size = new System.Drawing.Size(149, 22);
            this.miHelp.Text = "Справка";
            this.miHelp.Click += new System.EventHandler(this.miHelp_Click);
            // 
            // miAbout
            // 
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(149, 22);
            this.miAbout.Text = "О программе";
            this.miAbout.Click += new System.EventHandler(this.miProgrammInfo_Click);
            // 
            // lGroupFilter
            // 
            this.lGroupFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lGroupFilter.Location = new System.Drawing.Point(12, 10);
            this.lGroupFilter.Name = "lGroupFilter";
            this.lGroupFilter.Size = new System.Drawing.Size(110, 13);
            this.lGroupFilter.TabIndex = 9;
            this.lGroupFilter.Text = "Фильтр по группам:";
            this.lGroupFilter.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbGroupFilter
            // 
            this.cbGroupFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbGroupFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroupFilter.FormattingEnabled = true;
            this.cbGroupFilter.Location = new System.Drawing.Point(208, 7);
            this.cbGroupFilter.Name = "cbGroupFilter";
            this.cbGroupFilter.Size = new System.Drawing.Size(141, 21);
            this.cbGroupFilter.TabIndex = 10;
            this.cbGroupFilter.SelectedIndexChanged += new System.EventHandler(this.cbGroupFilter_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statTelephoneCount,
            this.statTelephoneActivatedCount,
            this.statRemainedTime,
            this.statBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 420);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(559, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statTelephoneCount
            // 
            this.statTelephoneCount.Margin = new System.Windows.Forms.Padding(2, 3, 0, 2);
            this.statTelephoneCount.Name = "statTelephoneCount";
            this.statTelephoneCount.Size = new System.Drawing.Size(138, 17);
            this.statTelephoneCount.Text = "Количество телефонов:";
            this.statTelephoneCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statTelephoneActivatedCount
            // 
            this.statTelephoneActivatedCount.Name = "statTelephoneActivatedCount";
            this.statTelephoneActivatedCount.Size = new System.Drawing.Size(169, 17);
            this.statTelephoneActivatedCount.Text = "Количество активированных:";
            this.statTelephoneActivatedCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statRemainedTime
            // 
            this.statRemainedTime.Name = "statRemainedTime";
            this.statRemainedTime.Size = new System.Drawing.Size(114, 17);
            this.statRemainedTime.Text = "Оставшееся время:";
            this.statRemainedTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statRemainedTime.Visible = false;
            // 
            // statBar
            // 
            this.statBar.Name = "statBar";
            this.statBar.Size = new System.Drawing.Size(100, 16);
            this.statBar.Visible = false;
            // 
            // lUser
            // 
            this.lUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lUser.Location = new System.Drawing.Point(316, 7);
            this.lUser.Name = "lUser";
            this.lUser.Size = new System.Drawing.Size(231, 32);
            this.lUser.TabIndex = 12;
            this.lUser.Text = "Пользователь:";
            this.lUser.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbLog
            // 
            this.lbLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbLog.Location = new System.Drawing.Point(0, 325);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(559, 95);
            this.lbLog.TabIndex = 0;
            this.lbLog.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(559, 301);
            this.panel1.TabIndex = 13;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.listTelephones);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 46);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(559, 218);
            this.panel4.TabIndex = 15;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lUser);
            this.panel3.Controls.Add(this.tbTo);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.btnAddDiapason);
            this.panel3.Controls.Add(this.tbFrom);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(559, 46);
            this.panel3.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbCountryFilter);
            this.panel2.Controls.Add(this.btnCall);
            this.panel2.Controls.Add(this.lGroupFilter);
            this.panel2.Controls.Add(this.cbGroupFilter);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 264);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(559, 37);
            this.panel2.TabIndex = 13;
            // 
            // cbCountryFilter
            // 
            this.cbCountryFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbCountryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCountryFilter.FormattingEnabled = true;
            this.cbCountryFilter.Location = new System.Drawing.Point(128, 7);
            this.cbCountryFilter.Name = "cbCountryFilter";
            this.cbCountryFilter.Size = new System.Drawing.Size(74, 21);
            this.cbCountryFilter.TabIndex = 11;
            this.cbCountryFilter.SelectedIndexChanged += new System.EventHandler(this.cbCountryFilter_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 442);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Определение активированных номеров";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddDiapason;
        private System.Windows.Forms.ListView listTelephones;
        private System.Windows.Forms.TextBox tbFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTo;
        private System.Windows.Forms.ColumnHeader date;
        private System.Windows.Forms.ImageList listImages;
        private System.Windows.Forms.Button btnCall;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miInformation;
        private System.Windows.Forms.ToolStripMenuItem miSave;
        private System.Windows.Forms.Label lGroupFilter;
        private System.Windows.Forms.ComboBox cbGroupFilter;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem miSettings;
        private System.Windows.Forms.ToolStripMenuItem miImport;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem miDeleteSelected;
        private System.Windows.Forms.ToolStripProgressBar statBar;
        private System.Windows.Forms.ColumnHeader dateActivation;
        private System.Windows.Forms.ToolStripMenuItem miResetActivationSelected;
        private System.Windows.Forms.ToolStripMenuItem miSetActivationSelected;
        private System.Windows.Forms.ToolStripStatusLabel statRemainedTime;
        private System.Windows.Forms.ToolStripMenuItem miSettingsModem;
        private System.Windows.Forms.ToolStripMenuItem miSettingsCall;
        private System.Windows.Forms.ToolStripMenuItem miCallSelected;
        private System.Windows.Forms.ToolStripMenuItem miSendNotification;
        private System.Windows.Forms.ToolStripMenuItem miCopyToClipboard;
        private System.Windows.Forms.ToolStripMenuItem miSelectActivated;
        private System.Windows.Forms.Label lUser;
        private System.Windows.Forms.ToolStripMenuItem miRegistration;
        private System.Windows.Forms.ToolStripMenuItem miAbout;
        private System.Windows.Forms.ToolStripMenuItem miHelp;
        private System.Windows.Forms.ToolStripMenuItem miLanguage;
        private System.Windows.Forms.ToolStripMenuItem miLanguageRussian;
        private System.Windows.Forms.ToolStripMenuItem miLanguageEnglish;
        private System.Windows.Forms.ColumnHeader telephone;
        private System.Windows.Forms.ToolStripStatusLabel statTelephoneCount;
        private System.Windows.Forms.ToolStripMenuItem miCallLatest;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem miView;
        private System.Windows.Forms.ToolStripMenuItem miLog;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStripStatusLabel statTelephoneActivatedCount;
        private System.Windows.Forms.ToolStripMenuItem miEdit;
        private System.Windows.Forms.ToolStripMenuItem miSearch;
        private System.Windows.Forms.ColumnHeader comment;
        private System.Windows.Forms.ToolStripMenuItem miComment;
        private System.Windows.Forms.ToolStripMenuItem miLanguageUkraine;
        private System.Windows.Forms.ComboBox cbCountryFilter;
    }
}

