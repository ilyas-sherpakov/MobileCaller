using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using MobileCaller.Collections;
using MobileCaller.ComPort;
using MobileCaller.Enums;
using MobileCaller.Localization;
using System.Globalization;
using System.Text.RegularExpressions;
using MobileCaller.Utils;
using MobileCaller.XML;

namespace MobileCaller.Forms
{
    public partial class MainForm : Form, ILocalizable
    {
        #region Fields

        private readonly string _workingDirectory;
        private readonly IPortReader _portReader = new PortReader();
        private readonly IPhoneProvider _xmlWrapper;
        private readonly List<MenuLocaleInfo> _menuLocaleInfoList = new List<MenuLocaleInfo>();
        private SessionStatistics _currentSessionStatistics;
        private readonly Regex _regexCountryCode = new Regex(@"\((?<Code>\+\d+)\)");

        #endregion

        #region Constructors

        public MainForm()
        {
            InitializeComponent();

            _workingDirectory = DefineWorkingDirectory();
            Logger.WorkingDirectory = CPassword.WorkingDirectory = _portReader.WorkingDirectory = _workingDirectory;
            _xmlWrapper = new XmlPhoneProvider(_workingDirectory);
        }

        #endregion

        #region Initialization Methods
        
        /// <summary>
        /// Checks if current user has permission to create files in directory. If not then gets Local App Data folder path.  
        /// </summary>
        /// <returns>Directory path where program writes all necessary files.</returns>
        private static string DefineWorkingDirectory()
        {
            string workingDirectory;
            var appStartupDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            var tempFileName = Path.Combine(appStartupDirectory, "tmp.txt");

            try
            {
                var fs = File.Create(tempFileName);
                fs.Close();
                //Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                workingDirectory = appStartupDirectory;
            }
            catch (Exception)
            {
                workingDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MobileCaller");
                if (!Directory.Exists(workingDirectory))
                {
                    Directory.CreateDirectory(workingDirectory);
                }
            }
            finally
            {
                if (File.Exists(tempFileName))
                {
                    File.Delete(tempFileName);
                }
            }

            return workingDirectory;
        }

        /// <summary>
        /// Initialize menu items related to language settings
        /// </summary>
        private void InitLocaleInfo()
        {
            _menuLocaleInfoList.Add(new MenuLocaleInfo(miLanguageRussian, "ru-RU"));
            _menuLocaleInfoList.Add(new MenuLocaleInfo(miLanguageEnglish, "en-US"));
            _menuLocaleInfoList.Add(new MenuLocaleInfo(miLanguageUkraine, "uk-UA"));

            var currentLocale = Properties.Settings.Default.Locale;
            foreach (var mi in _menuLocaleInfoList)
            {
                if (mi.Locale != null && mi.Locale == currentLocale)
                    PerformTranslation(mi.MenuItem);
            }
        }

        /// <summary>
        /// Initialization of filters.
        /// </summary>
        private void CountryFilterDataBind()
        {
            cbCountryFilter.Items.Clear();
            cbCountryFilter.Items.AddRange(_xmlWrapper.CountrySettings.Select(s => s.CountryName).ToArray());
            cbCountryFilter.SelectedIndex = 0;
        }

        private void GroupFilterDataBind()
        {
            var selectedCountry = _xmlWrapper.CountrySettings.First(c => c.CountryName == (string)cbCountryFilter.SelectedItem);
            cbGroupFilter.Items.Clear();
            cbGroupFilter.Items.Add(ResourceManagerProvider.GetLocalizedString("CB_GROUP_FILTER_ALL", Application.CurrentCulture));
            cbGroupFilter.Items.AddRange(_xmlWrapper.GroupSettings.Where(s => selectedCountry.GroupNameList.Contains(s.GroupName)).Select(s => s.GroupName).ToArray());
            cbGroupFilter.SelectedIndex = 0;
        }

        private void UpdateRegisteredUserInfo()
        {
            var culture = Application.CurrentCulture;
            lUser.Text = String.Format("{0}:\n{1}", ResourceManagerProvider.GetLocalizedString("L_REGISTERED_USER", culture), CPassword.User);
        }

        private void AdjustLastColumn(ListView listView)
        {
            listView.Columns[listView.Columns.Count - 1].Width = -2;
        }

        public void PerformTranslation()
        {
            var culture = Application.CurrentCulture;

            Text = ResourceManagerProvider.GetLocalizedString("WND_MAIN_FORM_TITLE", culture);

            // Fill menu items
            miFile.Text = ResourceManagerProvider.GetLocalizedString("MI_FILE", culture);
            miImport.Text = ResourceManagerProvider.GetLocalizedString("MI_IMPORT", culture);
            miSave.Text = ResourceManagerProvider.GetLocalizedString("MI_SAVE", culture);
            miExit.Text = ResourceManagerProvider.GetLocalizedString("MI_EXIT", culture);
            miEdit.Text = ResourceManagerProvider.GetLocalizedString("MI_EDIT", culture);
            miSearch.Text = ResourceManagerProvider.GetLocalizedString("MI_SEARCH", culture);
            miSettings.Text = ResourceManagerProvider.GetLocalizedString("MI_SETTINGS", culture);
            miSettingsModem.Text = ResourceManagerProvider.GetLocalizedString("MI_SETTINGS_MODEM", culture);
            miSettingsCall.Text = ResourceManagerProvider.GetLocalizedString("MI_SETTINGS_CALL", culture);
            miLanguage.Text = ResourceManagerProvider.GetLocalizedString("MI_LANGUAGE", culture);
            miLanguageRussian.Text = ResourceManagerProvider.GetLocalizedString("MI_LANGUAGE_RUSSIAN", culture);
            miLanguageEnglish.Text = ResourceManagerProvider.GetLocalizedString("MI_LANGUAGE_ENGLISH", culture);
            miLanguageUkraine.Text = ResourceManagerProvider.GetLocalizedString("MI_LANGUAGE_UKRAINE", culture);
            miInformation.Text = ResourceManagerProvider.GetLocalizedString("MI_INFORMATION", culture);
            miView.Text = ResourceManagerProvider.GetLocalizedString("MI_VIEW", culture);
            miLog.Text = ResourceManagerProvider.GetLocalizedString("MI_LOG", culture);
            miRegistration.Text = ResourceManagerProvider.GetLocalizedString("MI_REGISTRATION", culture);
            miHelp.Text = ResourceManagerProvider.GetLocalizedString("MI_HELP", culture);
            miAbout.Text = ResourceManagerProvider.GetLocalizedString("MI_ABOUT", culture);
            // Fill context menu
            miSelectActivated.Text = ResourceManagerProvider.GetLocalizedString("MI_SELECT_ACTIVATED", culture);
            miCallSelected.Text = ResourceManagerProvider.GetLocalizedString("MI_CALL_SELECTED", culture);
            miCallLatest.Text = ResourceManagerProvider.GetLocalizedString("MI_CALL_LATEST", culture);
            miSendNotification.Text = ResourceManagerProvider.GetLocalizedString("MI_SEND_NOTIFICATION", culture);
            miCopyToClipboard.Text = ResourceManagerProvider.GetLocalizedString("MI_COPY_TO_CLIPBOARD", culture);
            miSetActivationSelected.Text = ResourceManagerProvider.GetLocalizedString("MI_SET_ACTIVATION_SELECTED", culture);
            miResetActivationSelected.Text = ResourceManagerProvider.GetLocalizedString("MI_RESET_ACTIVATION_SELECTED", culture);
            miDeleteSelected.Text = ResourceManagerProvider.GetLocalizedString("MI_DELETE_SELECTED", culture);

            btnCall.Text = ResourceManagerProvider.GetLocalizedString("BTN_CALL", culture);
            btnAddDiapason.Text = ResourceManagerProvider.GetLocalizedString("BTN_ADD_DIAPASON", culture);

            if (CPassword.Verify())
            {
                UpdateRegisteredUserInfo();
            }
            else
            {
                lUser.Text = ResourceManagerProvider.GetLocalizedString("L_USER", culture);
            }

            listTelephones.Columns[0].Text = ResourceManagerProvider.GetLocalizedString("LST_TELEPHONES_NUMBER", culture);
            listTelephones.Columns[1].Text = ResourceManagerProvider.GetLocalizedString("LST_TELEPHONES_DATE", culture);
            listTelephones.Columns[2].Text = ResourceManagerProvider.GetLocalizedString("LST_TELEPHONES_DATE_ACTIVATION", culture);
            listTelephones.Columns[3].Text = ResourceManagerProvider.GetLocalizedString("LST_TELEPHONES_COMMENT", culture);
            AdjustLastColumn(listTelephones);

            lGroupFilter.Text = String.Format("{0}:", ResourceManagerProvider.GetLocalizedString("L_GROUP_FILTER", culture));
            // Update All label by removing and adding new localized text
            if (cbGroupFilter.Items.Count > 0)
            {
                var showAll = cbGroupFilter.SelectedIndex == 0;
                cbGroupFilter.Items.RemoveAt(0);
                cbGroupFilter.Items.Insert(0, ResourceManagerProvider.GetLocalizedString("CB_GROUP_FILTER_ALL", culture));
                if (showAll)
                    cbGroupFilter.Text = ResourceManagerProvider.GetLocalizedString("CB_GROUP_FILTER_ALL", culture);
            }

            //Set button text of MessageBox from resources
            MessageBoxManager.OK = ResourceManagerProvider.GetLocalizedString("BTN_OK", culture);
            MessageBoxManager.Cancel = ResourceManagerProvider.GetLocalizedString("BTN_CANCEL", culture);
            /* MessageBoxManager.Retry = ResourceManagerProvider.GetLocalizedString("CB_GROUP_FILTER_ALL", culture);
            MessageBoxManager.Ignore = ResourceManagerProvider.GetLocalizedString("CB_GROUP_FILTER_ALL", culture);
            MessageBoxManager.Abort = ResourceManagerProvider.GetLocalizedString("CB_GROUP_FILTER_ALL", culture); */
            MessageBoxManager.Yes = ResourceManagerProvider.GetLocalizedString("BTN_YES", culture);
            MessageBoxManager.No = ResourceManagerProvider.GetLocalizedString("BTN_NO", culture);
        }

        /// <summary>
        /// Set the specific language for application and translate all text
        /// </summary>
        /// <param name="item">Menu item which is selected</param>
        private void PerformTranslation(object item)
        {
            if (!(item is ToolStripMenuItem))
                return;
            var itemSelected = (ToolStripMenuItem)item;

            // Set the check for selected menu item and reset for others
            foreach (ToolStripMenuItem ddItem in miLanguage.DropDownItems)
            {
                ddItem.Checked = ddItem.Equals(itemSelected);
            }

            // If the locale was changed then set the new locale for application and save it in settings
            foreach (var localeInfo in _menuLocaleInfoList)
            {
                if (itemSelected.Equals(localeInfo.MenuItem))
                {
                    if (Application.CurrentCulture.Name != localeInfo.Locale)
                    {
                        var selectedCulture = CultureInfo.GetCultureInfo(localeInfo.Locale);
                        Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = selectedCulture;
                        Properties.Settings.Default.Locale = localeInfo.Locale;
                        Properties.Settings.Default.Save();
                    }

                    PerformTranslation();
                    break;
                }
            }
        }

        /// <summary>
        /// Add new phone number in database.
        /// </summary>
        /// <param name="phone">New telephone number.</param>
        /// <param name="dateInput">Date of input.</param>
        /// <param name="dateActivated">Date of activation.</param>
        /// <param name="commentary">Comment.</param>
        /// <returns>True if the current number already existed in database.</returns>
        private bool AddNewTelephone(string phone, string dateInput = null, string dateActivated = null, string commentary = null)
        {
            var culture = Application.CurrentCulture;
            if (phone.Length < 6)
            {
                var warningInvalidFormatText = String.Format(ResourceManagerProvider.GetLocalizedString("MSG_NUMBER_INVALID_FORMAT", culture), phone);
                if (MessageBox.Show(warningInvalidFormatText,
                    ResourceManagerProvider.GetLocalizedString("MSG_WARNING_TITLE", culture),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    return true;
                }
                return false;
            }

            phone = XmlWrapperHelper.ResolvePhoneNumber(phone);
            if (_xmlWrapper.TelephoneItems.All(x => x.Telephone != phone))
            {
                var groupName = XmlWrapperHelper.ResolvePhoneGroupName(phone);
                if (string.IsNullOrEmpty(groupName))
                {
                    groupName = cbGroupFilter.Items[cbGroupFilter.SelectedIndex].ToString();
                }
                var phoneItem = String.IsNullOrEmpty(dateInput) ? new XmlTelephoneItem(phone, groupName, DateTime.Now) : new XmlTelephoneItem(phone, groupName, dateInput, dateActivated, commentary);
                _xmlWrapper.TelephoneItems.Add(phoneItem);
                listTelephones.AddTelephone(phoneItem);
                return true;
            }

            var warningDuplicatedText = String.Format(ResourceManagerProvider.GetLocalizedString("MSG_NUMBER_DUPLICATED", culture), phone);
            if (MessageBox.Show(warningDuplicatedText, ResourceManagerProvider.GetLocalizedString("MSG_WARNING_TITLE", culture),
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Set the ability in GUI to operate with functionalities depends on mode in which we are.
        /// </summary>
        /// <param name="enabled">Flag which is false if process is calling. True - if process is finished.</param>
        private void SetGUIAvailableForCallProcess(bool enabled)
        {
            btnCall.Text = ResourceManagerProvider.GetLocalizedString(enabled ? "BTN_CALL" : "BTN_CALL_STOP", Application.CurrentCulture);

            miSettings.Enabled = enabled;
            miLanguage.Enabled = enabled;
            cbCountryFilter.Enabled = enabled;
            cbGroupFilter.Enabled = enabled;
            statBar.Visible = !enabled;
            statRemainedTime.Visible = !enabled;

            #region Diapason availability

            btnAddDiapason.Enabled = enabled;
            tbFrom.Enabled = enabled;
            tbTo.Enabled = enabled;
            miImport.Enabled = enabled;

            #endregion
        }

        private void ShowComment()
        {
            var item = listTelephones.SelectedItems().FirstOrDefault();
            if (item == null)
            {
                return;
            }
            var xmlItem = _xmlWrapper.TelephoneItems.First(x => x.Telephone == item);
            var commentForm = new CommentForm
            {
                PhoneNumber = xmlItem.Telephone,
                Comment = xmlItem.Comment
            };
            if (commentForm.ShowDialog() == DialogResult.OK)
            {
                listTelephones.UpdateComment(_xmlWrapper, commentForm.PhoneNumber, commentForm.Comment);
            }
        }

        /// <summary>
        /// Activate/deactivate possibility to start or stop process.
        /// </summary>
        /// <param name="enabled">If true then disable process button.</param>
        private void SetCallButtonAvailability(bool enabled)
        {
            btnCall.Enabled = enabled;
            Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;
        }

        private void UpdateStatusBarInfo()
        {
            var culture = Application.CurrentCulture;

            int seconds = 0;
            if (_currentSessionStatistics.Telephones != null)
            {
                seconds = _currentSessionStatistics.Telephones
                    .Skip(_currentSessionStatistics.ProcessedCount)
                                                   .SelectMany(
                                                       tel =>
                                                       _xmlWrapper.GroupSettings.Where(g => tel.GroupName == g.GroupName)
                                                                  .Select(gg => gg.WaitAnswer))
                                                   .Sum();
                seconds += _currentSessionStatistics.RemainedCount * _xmlWrapper.CallSettings.WaitCall;
            }

            var remainedTime = new TimeSpan(0, 0, seconds);
            
            statTelephoneCount.Text = String.Format(ResourceManagerProvider.GetLocalizedString("STAT_TELEPHONE_COUNT", culture), listTelephones.Items.Count);
            statTelephoneActivatedCount.Text = String.Format(ResourceManagerProvider.GetLocalizedString("STAT_TELEPHONE_ACTIVATED_COUNT", culture), listTelephones.ActivatedCount());
            statRemainedTime.Text = String.Format(ResourceManagerProvider.GetLocalizedString("STAT_SESSION_REMAINED_TIME", culture), remainedTime.Hours, remainedTime.Minutes);

            statBar.Value = _currentSessionStatistics.ProcessedCount;
            statBar.ToolTipText = String.Format("{0}\t\n{1}\t\n{2}\t\n{3}\t",
                ResourceManagerProvider.GetLocalizedString("STAT_SESSION_HEADER", culture),
                String.Format(ResourceManagerProvider.GetLocalizedString("STAT_SESSION_PROCESSED_COUNT", culture), _currentSessionStatistics.ProcessedCount),
                String.Format(ResourceManagerProvider.GetLocalizedString("STAT_SESSION_REMAINED_COUNT", culture), _currentSessionStatistics.RemainedCount),
                String.Format(ResourceManagerProvider.GetLocalizedString("STAT_SESSION_ACTIVATED_COUNT", culture), _currentSessionStatistics.ActivatedCount));
        }

        private void StartOperation(int groupSelectedIndex, PortReaderOperation operation)
        {
            IEnumerable<XmlTelephoneItem> telephones = groupSelectedIndex == 0 ? _xmlWrapper.TelephoneItems
                : _xmlWrapper.TelephoneItems.Where(x => x.GroupName == cbGroupFilter.Items[groupSelectedIndex].ToString());
            var nonActiveTelephones = telephones.Where(tel => !tel.IsActivated()).ToList();
            LogExtensions.WriteSettingsLog(_xmlWrapper.CallSettings, _xmlWrapper.GroupSettings, _workingDirectory);
            StartOperation(nonActiveTelephones, operation);
        }

        /// <summary>
        /// Start call of notification sending operation
        /// </summary>
        private void StartOperation(IList<XmlTelephoneItem> telephones, PortReaderOperation operation)
        {
            _portReader.Operation = operation;

            _portReader.PortName = _xmlWrapper.ModemSettings.ComPort;
            _portReader.BaudRate = _xmlWrapper.ModemSettings.BaudRate;
            
            _portReader.WaitCall = _xmlWrapper.CallSettings.WaitCall;
            _portReader.SendNotification = _xmlWrapper.CallSettings.SendNotification;

            try
            {
                if (_portReader.Start(telephones, _xmlWrapper.GroupSettings))
                {
                    _currentSessionStatistics.ActivatedCount = 0;
                    _currentSessionStatistics.ProcessedCount = 0;
                    _currentSessionStatistics.RemainedCount = telephones.Count;

                    if (operation == PortReaderOperation.Call)
                    {
                        _currentSessionStatistics.Telephones = telephones;
                    }

                    statBar.Maximum = _currentSessionStatistics.RemainedCount;
                    UpdateStatusBarInfo();

                    SetGUIAvailableForCallProcess(false);
                }
            }
            catch (Exception)
            {
                var culture = Application.CurrentCulture;
                MessageBox.Show(ResourceManagerProvider.GetLocalizedString("MSG_COM_PORT_OR_MOBILE_ERROR", culture),
                    ResourceManagerProvider.GetLocalizedString("MSG_SYSTEM_ERROR_TITLE", culture),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PlaySystemSound()
        {
            // Get parent of System folder to have Windows folder
            var dirWindowsFolder = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System));
            // Concatenate Media folder onto Windows folder.
            string mediaFolderPath = Path.Combine(dirWindowsFolder.FullName, "Media");

            string soundFilePath = String.Format("{0}\\{1}", mediaFolderPath, "notify.wav");
            using (var stream = File.OpenRead(soundFilePath))
            using (var player = new SoundPlayer(stream))
            {
                player.Play();
            }
        }

        #endregion

        #region Event Handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            CPassword.GetRegistrationInfo();

            InitLocaleInfo();

            MessageBoxManager.Register();

            _xmlWrapper.LoadXml();

            Logger.Write(String.Format("File LoadXml Operation Starts : {0} {1}{2}",
                DateTime.Now.ToLongDateString(),
                DateTime.Now.ToLongTimeString(),
                "\r\n\t=========================================================\r\n"));


            if (CPassword.Verify())
            {
                miRegistration.Visible = false;
            }

            _portReader.NotificationReadReady += PortReader_NotificationReadReady;
            _portReader.NotificationFinishedReady += PortReader_NotificationFinishedReady;
            _portReader.TelephoneReadReady += PortReader_TelephoneReadReady;
            _portReader.TelephoneFinishedReady += PortReader_TelephoneFinishedReady;

            CountryFilterDataBind();
            GroupFilterDataBind();
        }

            #region Port Reader logic

        private void PortReader_TelephoneReadReady(object sender, ReadPortEvent e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                var culture = Application.CurrentCulture;

                _currentSessionStatistics.ProcessedCount++;
                _currentSessionStatistics.RemainedCount--;

                if (e.Activated)
                {
                    listTelephones.UpdateDateActivated(_xmlWrapper, e.Telephone, DateTime.Now);

                    _currentSessionStatistics.ActivatedCount++;

                    if (e.Code == ResponseCode.TIMEOUT)
                    {
                        listTelephones.FindItem(e.Telephone).BackColor = Color.Yellow;
                    }
                    else
                    {
                        listTelephones.FindItem(e.Telephone).BackColor = Color.Aqua;
                    }

                    Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_ACTIVATED_NUMBER", culture), e.Telephone));
                }
                else
                {
                    switch (e.Code)
                    {
                        case ResponseCode.BLACKLISTED:
                        case ResponseCode.NO_ANSWER_MODEM:
                        case ResponseCode.ERROR:
                            listTelephones.FindItem(e.Telephone).BackColor = Color.Red;
                            break;
                        default:
                            listTelephones.FindItem(e.Telephone).BackColor = Color.Aqua;
                            break;
                    }
                }

                UpdateStatusBarInfo();

                lbLog.AddLogItem(e);
            });
        }

        private void PortReader_TelephoneFinishedReady(object sender, FinishPortEvent e)
        {
            var culture = Application.CurrentCulture;

            if (e.Exception != null)
            {
                MessageBox.Show(e.Exception.Message, ResourceManagerProvider.GetLocalizedString("MSG_ERROR_MODEM_TITLE", Application.CurrentCulture), 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Enable gui as call process is finished
                SetGUIAvailableForCallProcess(true);
                SetCallButtonAvailability(true);
            }
            else
            {
                var nonActivatedList = _currentSessionStatistics.Telephones.Where(tel => !tel.IsActivated()).ToList();
                // If we have to repeat call process and stop button was not pressed and list contains any not activated telephone
                if (_xmlWrapper.CallSettings.Repeatable && btnCall.Enabled && nonActivatedList.Any())
                {
                    StartOperation(nonActivatedList, PortReaderOperation.Call);

                    lbLog.AddLogItem(ResourceManagerProvider.GetLocalizedString("LOG_CALL_RESTARTED", culture), _currentSessionStatistics);
                    return;
                }

                // Enable gui as call process is finished
                SetGUIAvailableForCallProcess(true);
                SetCallButtonAvailability(true);

                if (_xmlWrapper.CallSettings.PlaySound)
                {
                    PlaySystemSound();
                }
                
                if(_xmlWrapper.CallSettings.Shutdown)
                {
                    _xmlWrapper.SaveXml();
                    try
                    {
                        Process.Start("Shutdown", "-s -t 10");
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex.Message + ex.StackTrace);
                    }
                    Application.Exit();
                }
            }

            lbLog.AddLogItem(ResourceManagerProvider.GetLocalizedString("LOG_CALL_FINISHED", culture), _currentSessionStatistics);
        }

        private void PortReader_NotificationReadReady(object sender, ReadPortEvent e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                var culture = Application.CurrentCulture;

                _currentSessionStatistics.ProcessedCount++;
                _currentSessionStatistics.RemainedCount--;

                if (e.Code == ResponseCode.ERROR)
                {
                    listTelephones.FindItem(e.Telephone).BackColor = Color.Red;
                }
                else
                {
                    _currentSessionStatistics.ActivatedCount++;
                    listTelephones.FindItem(e.Telephone).BackColor = Color.Aqua;
                }

                UpdateStatusBarInfo();
            });
        }

        private void PortReader_NotificationFinishedReady(object sender, FinishPortEvent e)
        {
            var culture = Application.CurrentCulture;

            if (e.Exception != null)
            {
                MessageBox.Show(e.Exception.Message, ResourceManagerProvider.GetLocalizedString("MSG_ERROR_MODEM_TITLE", Application.CurrentCulture),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Enable gui as call process is finished
                SetGUIAvailableForCallProcess(true);
                SetCallButtonAvailability(true);
            }
            else
            {
                // Enable gui as call process is finished
                SetGUIAvailableForCallProcess(true);
                SetCallButtonAvailability(true);
                
                if (_xmlWrapper.CallSettings.PlaySound)
                {
                    PlaySystemSound();
                }
            }

            lbLog.AddLogItem(String.Format(ResourceManagerProvider.GetLocalizedString("LOG_NOTIFICATION_FINISHED", culture), _currentSessionStatistics.ActivatedCount));
        }

        #endregion

        private void tbFromTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = !textBox.IsTelephone(e.KeyChar);
        }

        private void btnAddDiapason_Click(object sender, EventArgs e)
        {
            if (tbFrom.Text.Length == 0 || tbTo.Text.Length == 0)
            {
                var culture = Application.CurrentCulture;
                MessageBox.Show(ResourceManagerProvider.GetLocalizedString("MSG_DIAPASON_EMPTY", culture),
                                ResourceManagerProvider.GetLocalizedString("MSG_INFORMATION_TITLE", culture),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var nFrom = Convert.ToInt64(tbFrom.Text);
            var nTo = Convert.ToInt64(tbTo.Text);
            var index = nFrom;
            while (index <= nTo)
            {
                string countryCode = string.Empty;
                var match = _regexCountryCode.Match((string)cbCountryFilter.SelectedItem);
                if (match.Success)
                {
                    countryCode = match.Groups["Code"].Value;
                }

                // pad left with zero until 10 numbers are in string
                var newTelephone = String.Format("{0}{1:D10}", countryCode, index);
                if (!AddNewTelephone(newTelephone))
                {
                    break;
                }
                ++index;
            }
            if (nFrom == nTo)
            {
                tbFrom.Text = tbTo.Text = String.Empty;
            }
        }

        private void bCall_Click(object sender, EventArgs e)
        {
            if (_portReader.IsBusy)
            {
                SetCallButtonAvailability(false);
                _portReader.Stop();
            }
            else
            {
                StartOperation(cbGroupFilter.SelectedIndex, PortReaderOperation.Call);
            }
        }

        private void listTelephones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                miDeleteSelected_Click(sender, e);
            }
            else if (e.KeyCode == Keys.A && e.Control)
            {
                listTelephones.SelectAllItems();
            }
        }

        private void cbCountryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupFilterDataBind();
        }

        private void cbGroupFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbGroups = sender as ComboBox;

            // Reset column sorting
            listTelephones.ListViewItemSorter = null;
            // Reset sort icon presentation
            for (int i = 0; i < listTelephones.Columns.Count; i++)
            {
                ListViewColumnHeaderHelper.SetColumnHeaderSortIcon(listTelephones, i, SortOrder.None);
            }

            if (cbGroups.SelectedIndex == 0)
            {
                // Show all selected
                listTelephones.ResetFilterItems(_xmlWrapper.TelephoneItems);
            }
            else
            {
                string groupName = cbGroups.Items[cbGroups.SelectedIndex].ToString();
                listTelephones.FilterItems(_xmlWrapper.TelephoneItems, groupName);
            }
            
            UpdateStatusBarInfo();
        }

        private void listTelephones_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var listView = sender as ListView;
            var sorter = listView.ListViewItemSorter as ListViewColumnSorter;
            if (sorter == null)
            {
                sorter = new ListViewColumnSorter(e.Column);
                listView.ListViewItemSorter = sorter;
            }

            // Determine if clicked column is already the column that is being sorted  
            if (e.Column == sorter.SortColumn)
            {
                // Reverse the current sort direction for this column  
                if (sorter.SortOrder == SortOrder.Ascending)
                {
                    sorter.SortOrder = SortOrder.Descending;
                }
                else
                {
                    sorter.SortOrder = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending
                sorter.SortColumn = e.Column;
                sorter.SortOrder = SortOrder.Ascending;
            }

            // Set sort icon presentation
            for (int i = 0; i < listView.Columns.Count; i++)
            {
                ListViewColumnHeaderHelper.SetColumnHeaderSortIcon(listView, i, i == sorter.SortColumn ? sorter.SortOrder : SortOrder.None);
            }

            // Perform the sort with these new sort options.  
            listView.Sort();
        }

        private void tbTo_Enter(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbTo.Text))
            {
                tbTo.Text = tbFrom.Text;
            }
        }

            #region Menu Item logic

        private void miImport_Click(object sender, EventArgs e)
        {
            var culture = Application.CurrentCulture;
            var dialog = new OpenFileDialog
            {
                Filter = ResourceManagerProvider.GetLocalizedString("MSG_SELECT_TEXT_FILE_EXT", culture),
                InitialDirectory = Environment.CurrentDirectory,
                Title = ResourceManagerProvider.GetLocalizedString("MSG_SELECT_TEXT_FILE_TITLE", culture)
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var regexPhoneTemplate = new Regex(@"^\s*(?<Phone>\+?\d+)\s*(?<InputDate>\d{2}\.\d{2}\.\d{4})?\s*(?<ActDate>\d{2}\.\d{2}\.\d{4})?\s*(?<Comment>\S+)?$",
                RegexOptions.Compiled | RegexOptions.Singleline);

                using (var reader = new StreamReader(dialog.FileName))
                {
                    /*var c = reader.ReadToEnd();
                    foreach (Match match in regexPhoneTemplate.Matches(c))
                    {
                        GroupCollection groups = match.Groups;
                        string newTelephone = groups["Phone"].Value;
                        string n2 = groups["InputDate"].Value;
                        string ne3 = groups["ActDate"].Value;
                    }*/

                    string phoneRow;
                    while ((phoneRow = reader.ReadLine()) != null)
                    {
                        var phoneMatch = regexPhoneTemplate.Match(phoneRow);
                        if (phoneMatch.Success)
                        {
                            if (!AddNewTelephone(phoneMatch.Groups["Phone"].Value, phoneMatch.Groups["InputDate"].Value,
                                phoneMatch.Groups["ActDate"].Value, phoneMatch.Groups["Comment"].Value.Replace(@"\n", Environment.NewLine)))
                            {
                                break;
                            }
                        }
                    }
                }
                UpdateStatusBarInfo();
            }
        }

        private void miDeleteSelected_Click(object sender, EventArgs e)
        {
            var culture = Application.CurrentCulture;
            if (MessageBox.Show(
                String.Format(ResourceManagerProvider.GetLocalizedString("MSG_DELETE_SELECTED_CONFIRMATION", culture), listTelephones.SelectedItems.Count),
                ResourceManagerProvider.GetLocalizedString("MSG_DELETION_TITLE", culture),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var phoneDeleted in listTelephones.DeleteSelected())
                {
                    _xmlWrapper.TelephoneItems.Remove(_xmlWrapper.TelephoneItems.Find(x => x.Telephone == phoneDeleted));
                }
                UpdateStatusBarInfo();
            }
        }

        private void miSetActivationSelected_Click(object sender, EventArgs e)
        {
            listTelephones.UpdateDateActivatedSelected(_xmlWrapper, DateTime.Now);
            UpdateStatusBarInfo();
        }

        private void miResetActivationSelected_Click(object sender, EventArgs e)
        {
            listTelephones.UpdateDateActivatedSelected(_xmlWrapper, null);
            UpdateStatusBarInfo();
        }

        private void miModemSettings_Click(object sender, EventArgs e)
        {
            using (var dialog = new ModemSettingsForm(_portReader))
            {
                dialog.ComPort = _xmlWrapper.ModemSettings.ComPort;
                dialog.BaudRate = _xmlWrapper.ModemSettings.BaudRate;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    _xmlWrapper.ModemSettings.ComPort = dialog.ComPort;
                    _xmlWrapper.ModemSettings.BaudRate = dialog.BaudRate;
                }
            }
        }

        private void miSearch_Click(object sender, EventArgs e)
        {
            using (var dialog = new SearchForm())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var searchedPhoneItem = listTelephones.FindItem(dialog.SearchedPhone);
                    if (searchedPhoneItem != null)
                    {
                        listTelephones.SelectedItems.Clear();
                        listTelephones.Focus();
                        searchedPhoneItem.Selected = true;
                        searchedPhoneItem.EnsureVisible();
                    }
                }
            }
        }

        private void miSettingsCall_Click(object sender, EventArgs e)
        {
            using (var dialog = new CallSettingsForm())
            {
                dialog.WaitCall = _xmlWrapper.CallSettings.WaitCall;
                dialog.SendNotification = _xmlWrapper.CallSettings.SendNotification;
                dialog.PlaySound = _xmlWrapper.CallSettings.PlaySound;
                dialog.Repeatable = _xmlWrapper.CallSettings.Repeatable;
                dialog.Shutdown = _xmlWrapper.CallSettings.Shutdown;
                dialog.WorkingDirectory = _workingDirectory;

                dialog.GroupSettings = _xmlWrapper.GroupSettings.Clone();

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    _xmlWrapper.CallSettings.WaitCall = dialog.WaitCall;
                    _xmlWrapper.CallSettings.SendNotification = dialog.SendNotification;
                    _xmlWrapper.CallSettings.PlaySound = dialog.PlaySound;
                    _xmlWrapper.CallSettings.Repeatable = dialog.Repeatable;
                    _xmlWrapper.CallSettings.Shutdown = dialog.Shutdown;

                    // Don't reassign object reference and rebind group filter ComboBox if no changes for groups were done
                    if (dialog.GroupSettings.IsChanged)
                    {
                        _xmlWrapper.GroupSettings = dialog.GroupSettings;

                        GroupFilterDataBind();
                    }
                }
            }
        }

        private void miCallSelected_Click(object sender, EventArgs e)
        {
            LogExtensions.WriteSettingsLog(_xmlWrapper.CallSettings, _xmlWrapper.GroupSettings, _workingDirectory);
            StartOperation(listTelephones.SelectedItems().SelectMany(i => _xmlWrapper.TelephoneItems.Where(x => x.Telephone == i)).Where(tel => !tel.IsActivated()).ToList(), PortReaderOperation.Call);
        }

        private void miCallLatest_Click(object sender, EventArgs e)
        {
            LogExtensions.WriteSettingsLog(_xmlWrapper.CallSettings, _xmlWrapper.GroupSettings, _workingDirectory);
            StartOperation(listTelephones.NotCalledItems().SelectMany(i => _xmlWrapper.TelephoneItems.Where(x => x.Telephone == i))
                .Where(tel => !tel.IsActivated()).ToList(), PortReaderOperation.Call);
        }

        private void miSendNotification_Click(object sender, EventArgs e)
        {
            StartOperation(listTelephones.SelectedItems().SelectMany(i => _xmlWrapper.TelephoneItems.Where(x => x.Telephone == i)).ToList(), PortReaderOperation.Notification);
        }

        private void miCopyToClipboard_Click(object sender, EventArgs e)
        {
            string buffer = string.Empty;
            foreach (var item in listTelephones.SelectedItems().SelectMany(i => _xmlWrapper.TelephoneItems.Where(x => x.Telephone == i)))
            {
                buffer += String.Format("{0} {1} {2} {3}\r\n", item.Telephone, item.Date, item.DateActivated, item.Comment.Replace(Environment.NewLine, @"\n"));
            }
            Clipboard.SetDataObject(buffer);
        }

        private void miSelectActivated_Click(object sender, EventArgs e)
        {
            listTelephones.SelectActivated();
        }

        private void miFile_DropDownOpening(object sender, EventArgs e)
        {
            miSave.Enabled = _xmlWrapper.IsChanged;
        }

        private void miLog_Click(object sender, EventArgs e)
        {
            miLog.Checked = !miLog.Checked;
            lbLog.Visible = miLog.Checked;
        }

        private void miProgrammInfo_Click(object sender, EventArgs e)
        {
            using (var dialog = new AboutForm())
            {
                dialog.ShowDialog(this);
            }
        }

        private void miRegistration_Click(object sender, EventArgs e)
        {
            var passwordForm = new CKeyForm();
            if (passwordForm.ShowDialog() == DialogResult.OK)
            {
                // The interruption on half of one second at registration of user data allows to avoid searching of keys in hack
                Thread.Sleep(500);

                CPassword.UpdateHash(passwordForm.Login + passwordForm.Email + passwordForm.IMEI + "'", passwordForm.Key);
                var culture = Application.CurrentCulture;
                if (CPassword.Verify())
                {
                    MessageBox.Show(ResourceManagerProvider.GetLocalizedString("MSG_REGISTRATION_SUCCESSFUL", culture),
                        ResourceManagerProvider.GetLocalizedString("MSG_INFORMATION_TITLE", culture),
                        MessageBoxButtons.OK, MessageBoxIcon.None);

                    // Successful registration data
                    CPassword.UpdateRegistrationInfo(passwordForm.Login, passwordForm.Email, passwordForm.IMEI);
                    miRegistration.Visible = false;
                    UpdateRegisteredUserInfo();
                }
                else
                {
                    // Wrong registration data
                    MessageBox.Show(ResourceManagerProvider.GetLocalizedString("MSG_REGISTRATION_WRONG", culture),
                        ResourceManagerProvider.GetLocalizedString("MSG_INFORMATION_TITLE", culture),
                        MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
        }

        private void miHelp_Click(object sender, EventArgs e)
        {
            try
            {
                var sysInfo = new Process();
                sysInfo.StartInfo.ErrorDialog = true;
                var currentLocale = Properties.Settings.Default.Locale;
                switch (currentLocale)
                {
                    case "ru-RU":
                        {
                            sysInfo.StartInfo.FileName = Path.Combine(Application.StartupPath, @"Help\Help-ru.chm");
                            break;
                        }
                    case "en-US":
                        {
                            sysInfo.StartInfo.FileName = Path.Combine(Application.StartupPath, @"Help\Help.chm");
                            break;
                        }
                    default:
                        {
                            sysInfo.StartInfo.FileName = Path.Combine(Application.StartupPath, @"Help\Help-ru.chm");
                            break;
                        }

                }
                sysInfo.Start();
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message + ex.StackTrace);
            }
        }

        private void miLanguageRussian_Click(object sender, EventArgs e)
        {
            PerformTranslation(sender);
        }

        private void miLanguageEnglish_Click(object sender, EventArgs e)
        {
            PerformTranslation(sender);
        }

        private void miLanguageUkraine_Click(object sender, EventArgs e)
        {
            PerformTranslation(sender);
        }

        private void miSave_Click(object sender, EventArgs e)
        {
            _xmlWrapper.SaveXml();
        }

        private void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_portReader.IsBusy)
            {
                miCallSelected.Enabled = false;
                miCallLatest.Enabled = false;
                miSendNotification.Enabled = false;
                miSetActivationSelected.Enabled = false;
                miResetActivationSelected.Enabled = false;
                miDeleteSelected.Enabled = false;
            }
            else
            {
                var seletedPhones = listTelephones.SelectedItems().SelectMany(i => _xmlWrapper.TelephoneItems.Where(x => x.Telephone == i));
                var isAllSelectedActivated = seletedPhones.All(i => i.IsActivated());
                var isAnySelectedActivated = seletedPhones.Any(i => i.IsActivated());
                var isAnySelectedNotActivated = seletedPhones.Any(i => !i.IsActivated());
                
                miCallSelected.Enabled = isAnySelectedNotActivated;
                miCallLatest.Enabled = true;
                miSendNotification.Enabled = isAllSelectedActivated;
                miSetActivationSelected.Enabled = isAnySelectedNotActivated;
                miResetActivationSelected.Enabled = isAnySelectedActivated;
                miDeleteSelected.Enabled = listTelephones.SelectedItems.Count > 0;
            }
        }

            #endregion
        
            #region Close Application logic

        private void miExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_xmlWrapper.IsChanged)
            {
                var culture = Application.CurrentCulture;
                var result = MessageBox.Show(ResourceManagerProvider.GetLocalizedString("MSG_PROGRAM_DATA_CHANGED", culture),
                                    ResourceManagerProvider.GetLocalizedString("MSG_SAVE_TITLE", culture),
                                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (result)
                {
                    case DialogResult.Yes:
                        {
                            _xmlWrapper.SaveXml();
                            break;
                        }
                    case DialogResult.No:
                        {
                            break;
                        }
                    case DialogResult.Cancel:
                        {
                            e.Cancel = true;
                            break;
                        }
                }
            }
        }

            #endregion

        private void listTelephones_Resize(object sender, EventArgs e)
        {
            AdjustLastColumn((ListView)sender);
        }

        private void listTelephones_DoubleClick(object sender, EventArgs e)
        {
            ShowComment();
        }

        private void miComment_Click(object sender, EventArgs e)
        {
            ShowComment();
        }

        private void miEdit_DropDownOpening(object sender, EventArgs e)
        {
            miComment.Enabled = listTelephones.SelectedItems().Any();
        }

        #endregion
    }
}
