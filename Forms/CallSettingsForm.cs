using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MobileCaller.Collections;
using MobileCaller.Enums;
using MobileCaller.Localization;
using MobileCaller.Utils;
using MobileCaller.XML;

namespace MobileCaller.Forms
{
    public partial class CallSettingsForm : Form, ILocalizable
    {
        #region Constants

        // Setup of validation constants
        private const int WaitCallLimit = 1;
        private const int WaitAnswerLimit = 1;

        #endregion

        #region Fields

        private int _waitCall;
        private bool _sendNotification;
        private bool _playSound;
        private bool _repeatable;
        private bool _shutdown;
        private string _workingDirectory;

        #endregion

        #region Properties

        public int WaitCall
        {
            get { return _waitCall; }
            set
            {
                _waitCall = value;
                tbWaitCall.Text = _waitCall.ToString();
            }
        }

        public bool SendNotification
        {
            get { return _sendNotification; }
            set
            {
                cbSendNotification.Checked = _sendNotification = value;
            }
        }

        public bool PlaySound
        {
            get { return _playSound; }
            set
            {
                cbPlaySound.Checked = _playSound = value;
            }
        }

        public bool Repeatable
        {
            get { return _repeatable; }
            set
            {
                cbRepeatable.Checked = _repeatable = value;
            }
        }

        public bool Shutdown
        {
            get { return _shutdown; }
            set
            {
                cbShutdown.Checked = _shutdown = value;
            }
        }

        public string WorkingDirectory
        {
            get { return _workingDirectory; }
            set
            {
                tbWorkingDirectory.Text = _workingDirectory = value;
            }
        }

        public VariantList<XmlGroupSettings> GroupSettings { get; set; }

        #endregion

        public CallSettingsForm()
        {
            InitializeComponent();
        }

        public void PerformTranslation()
        {
            var culture = Application.CurrentCulture;

            Text = ResourceManagerProvider.GetLocalizedString("WND_CALL_SETTINGS_FORM_TITLE", culture);

            lWaitCall.Text = String.Format("{0}:", ResourceManagerProvider.GetLocalizedString("L_WAIT_CALL", culture));
            pbWaitCall.SetHint(ResourceManagerProvider.GetLocalizedString("L_WAIT_CALL_HINT", culture));

            cbSendNotification.Text = ResourceManagerProvider.GetLocalizedString("L_SEND_NOTIFICATION", culture);
            pbSendNotification.SetHint(ResourceManagerProvider.GetLocalizedString("L_SEND_NOTIFICATION_HINT", culture));

            cbPlaySound.Text = ResourceManagerProvider.GetLocalizedString("L_PLAY_SOUND", culture);
            pbPlaySound.SetHint(ResourceManagerProvider.GetLocalizedString("L_PLAY_SOUND_HINT", culture));

            cbRepeatable.Text = ResourceManagerProvider.GetLocalizedString("L_REPEATABLE", culture);
            pbRepeatable.SetHint(ResourceManagerProvider.GetLocalizedString("L_REPEATABLE_HINT", culture));

            cbShutdown.Text = ResourceManagerProvider.GetLocalizedString("L_SHUTDOWN", culture);
            pbShutdown.SetHint(ResourceManagerProvider.GetLocalizedString("L_SHUTDOWN_HINT", culture));

            lWorkingDirectory.Text = String.Format("{0}:", ResourceManagerProvider.GetLocalizedString("L_WORKING_DIRECTORY", culture));
            btnOpenWorkingDirectory.SetHint(ResourceManagerProvider.GetLocalizedString("BTN_OPEN_WORKING_DIRECTORY_HINT", culture));
            pbWorkingDirectory.SetHint(ResourceManagerProvider.GetLocalizedString("L_WORKING_DIRECTORY_HINT", culture));

            btnAddGroup.SetHint(ResourceManagerProvider.GetLocalizedString("BTN_ADD_GROUP_HINT", culture));
            btnRemoveGroup.SetHint(ResourceManagerProvider.GetLocalizedString("BTN_REMOVE_GROUP_HINT", culture));

            btnOk.Text = ResourceManagerProvider.GetLocalizedString("BTN_OK", culture);
            btnCancel.Text = ResourceManagerProvider.GetLocalizedString("BTN_CANCEL", culture);
        }

        private void CallSettingsForm_Load(object sender, EventArgs e)
        {
            PerformTranslation();

            InitializeGroupsGridView();

            DataBind();
        }

        private void InitializeGroupsGridView()
        {
            // Set the style of Data Grid View
            gvGroups.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            gvGroups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvGroups.AutoGenerateColumns = false;

            gvGroups.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GroupName", Name = "GroupName"});
            gvGroups.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "WaitAnswer", Name = "WaitAnswer" });
            var doubleCheckOnTimeoutColumn = new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "DoubleCheckOnTimeout",
                Name = "DoubleCheckOnTimeout"
            };
            gvGroups.Columns.Add(doubleCheckOnTimeoutColumn);
            gvGroups.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SmsRecipient", Name = "SmsRecipient" });
            gvGroups.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SmsText", Name = "SmsText" });
            gvGroups.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UssdText", Name = "UssdText" });
            var notificationTypeColumn = new DataGridViewComboBoxColumn
                            {
                                DataSource = Enum.GetValues(typeof (NotificationType)),
                                DataPropertyName = "NotificationType",
                                Name = "NotificationType"
                            };
            gvGroups.Columns.Add(notificationTypeColumn);

            // Localization of Data Grid View
            var culture = Application.CurrentCulture;

            gvGroups.Columns["GroupName"].HeaderText = ResourceManagerProvider.GetLocalizedString("LST_GROUP_NAME", culture);
            gvGroups.Columns["GroupName"].ToolTipText = ResourceManagerProvider.GetLocalizedString("LST_GROUP_NAME_HINT", culture);
            gvGroups.Columns["WaitAnswer"].HeaderText = ResourceManagerProvider.GetLocalizedString("LST_WAIT_ANSWER", culture);
            gvGroups.Columns["WaitAnswer"].ToolTipText = ResourceManagerProvider.GetLocalizedString("LST_WAIT_ANSWER_HINT", culture);
            gvGroups.Columns["DoubleCheckOnTimeout"].HeaderText = ResourceManagerProvider.GetLocalizedString("LST_DOUBLE_CHECK_ON_TIMEOUT", culture);
            gvGroups.Columns["DoubleCheckOnTimeout"].ToolTipText = String.Format(ResourceManagerProvider.GetLocalizedString("LST_DOUBLE_CHECK_ON_TIMEOUT_HINT", culture),
                                                                                 ResourceManagerProvider.GetLocalizedString("LST_WAIT_ANSWER", culture));
            gvGroups.Columns["SmsRecipient"].HeaderText = ResourceManagerProvider.GetLocalizedString("LST_SMS_RECIPIENT", culture);
            gvGroups.Columns["SmsRecipient"].ToolTipText = ResourceManagerProvider.GetLocalizedString("LST_SMS_RECIPIENT_HINT", culture);
            gvGroups.Columns["SmsText"].HeaderText = ResourceManagerProvider.GetLocalizedString("LST_SMS_TEXT", culture);
            gvGroups.Columns["SmsText"].ToolTipText = ResourceManagerProvider.GetLocalizedString("LST_SMS_TEXT_HINT", culture);
            gvGroups.Columns["UssdText"].HeaderText = ResourceManagerProvider.GetLocalizedString("LST_USSD_TEXT", culture);
            gvGroups.Columns["UssdText"].ToolTipText = ResourceManagerProvider.GetLocalizedString("LST_USSD_TEXT_HINT", culture);
            gvGroups.Columns["NotificationType"].HeaderText = ResourceManagerProvider.GetLocalizedString("LST_NOTIFICATION_TYPE", culture);
            gvGroups.Columns["NotificationType"].ToolTipText = ResourceManagerProvider.GetLocalizedString("LST_NOTIFICATION_TYPE_HINT", culture);
        }

        /// <summary>
        /// Set GroupSettings for dataSource of grid and reset its descriptions
        /// </summary>
        private void DataBind()
        {
            gvGroups.DataSource = null;
            gvGroups.DataSource = GroupSettings;
        }

        private void tbWaitCall_KeyPress(object sender, KeyPressEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = !textBox.IsInteger(e.KeyChar);
        }

        private void tbWaitCall_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            int tmp;
            if (Int32.TryParse(textBox.Text, out tmp))
            {
                if (tmp < WaitCallLimit)
                {
                    var culture = Application.CurrentCulture;
                    var optionWaitCall = ResourceManagerProvider.GetLocalizedString("L_WAIT_CALL", culture);
                    var errorText = String.Format(ResourceManagerProvider.GetLocalizedString("MSG_WAIT_CALL_LIMIT", culture), optionWaitCall, WaitCallLimit);
                    MessageBox.Show(errorText, 
                        ResourceManagerProvider.GetLocalizedString("MSG_INFORMATION_TITLE", culture), 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBox.Text = String.Empty;
                }
                else
                    _waitCall = tmp;
            }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbWaitCall.Text))
            {
                var culture = Application.CurrentCulture;
                var fieldWaitCall = ResourceManagerProvider.GetLocalizedString("L_WAIT_CALL", culture);
                MessageBox.Show(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_FIELD_EMPTY", culture), fieldWaitCall),
                    ResourceManagerProvider.GetLocalizedString("MSG_INFORMATION_TITLE", culture),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bAddGroup_Click(object sender, EventArgs e)
        {
            var smsSettings = new XmlGroupSettings();
            smsSettings.GroupName = ResourceManagerProvider.GetLocalizedString("LST_GROUP_NAME_NEW", Application.CurrentCulture);
            smsSettings.SmsRecipient = "0000";
            smsSettings.SmsText = "%PHONE%";
            smsSettings.UssdText = "%PHONE%";
            GroupSettings.Add(smsSettings);

            DataBind();
        }

        private void bRemoveGroup_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gvGroups.SelectedRows)
            {
                var item = row.DataBoundItem as XmlGroupSettings;
                GroupSettings.Remove(item);

                DataBind();
            }
        }

        private void cbSendNotification_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            _sendNotification = checkBox.Checked;
        }

        private void cbPlaySound_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            _playSound = checkBox.Checked;
        }

        private void cbRepeatable_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            _repeatable = checkBox.Checked;
        }

        private void cbShutdown_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            _shutdown = checkBox.Checked;
        }

        #region DataGridView handlers

        private void gvGroups_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Disable availability to press column header and provide correct stretch of column width
            foreach (DataGridViewColumn column in gvGroups.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        /// <summary>
        /// It is fired just when the DataGridView is about to leave the edition mode and allows you to not accept the value 
        /// that has been entered by the user. To reject the value entered, in the code of the event-handler, 
        /// you must set the e.Cancel to True
        /// </summary>
        private void gvGroups_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var culture = Application.CurrentCulture;
            string columnName = gvGroups.Columns[e.ColumnIndex].Name;
            string value = e.FormattedValue.ToString();
            if (columnName.Equals("GroupName"))
            {
                // Confirm that the cell is not empty.
                if (string.IsNullOrEmpty(value))
                {
                    var fieldGroupName = ResourceManagerProvider.GetLocalizedString("LST_GROUP_NAME", culture);
                    var errorText = String.Format(ResourceManagerProvider.GetLocalizedString("MSG_FIELD_EMPTY", culture), fieldGroupName);
                    gvGroups.Rows[e.RowIndex].ErrorText = errorText;
                    e.Cancel = true; // Disable gvGroups_CellEndEdit
                }
            }
            else if (columnName.Equals("WaitAnswer"))
            {
                int res; 
                if (!Int32.TryParse(value, out res))
                {
                    var fieldWaitAnswer = ResourceManagerProvider.GetLocalizedString("LST_WAIT_ANSWER", culture);
                    var errorText = String.Format(ResourceManagerProvider.GetLocalizedString("MSG_FIELD_NOT_NUMBER", culture), fieldWaitAnswer);
                    gvGroups.Rows[e.RowIndex].ErrorText = errorText;
                    e.Cancel = true; // Disable gvGroups_CellEndEdit
                }
                else if (res < WaitAnswerLimit)
                {
                    var fieldWaitAnswer = ResourceManagerProvider.GetLocalizedString("LST_WAIT_ANSWER", culture);
                    var errorText = String.Format(ResourceManagerProvider.GetLocalizedString("MSG_WAIT_ANSWER_LIMIT", culture), fieldWaitAnswer, WaitAnswerLimit);
                    gvGroups.Rows[e.RowIndex].ErrorText = errorText;
                    e.Cancel = true; // Disable gvGroups_CellEndEdit
                }
            }
            else if (columnName.Equals("SmsRecipient") && (NotificationType)Enum.Parse(typeof(NotificationType), gvGroups.Rows[e.RowIndex].Cells["NotificationType"].Value.ToString()) == NotificationType.SMS)
            {
                // Confirm that the cell is the phone.
                if (!Regex.IsMatch(value, @"^[\d\*]{1,10}$"))
                {
                    var fieldSmsRecipient = ResourceManagerProvider.GetLocalizedString("LST_SMS_RECIPIENT", culture);
                    var errorText = String.Format(ResourceManagerProvider.GetLocalizedString("MSG_FIELD_WRONG_FORMAT", culture), fieldSmsRecipient);
                    gvGroups.Rows[e.RowIndex].ErrorText = errorText;
                    e.Cancel = true; // Disable gvGroups_CellEndEdit
                }
            }
        }

        private void gvGroups_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Clear the row error in case the user presses valid value and CellEndEdit was raised.   
            gvGroups.Rows[e.RowIndex].ErrorText = String.Empty;
        }

        #endregion

        private void btnOpenWorkingDirectory_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(WorkingDirectory);
        }
    }
}
