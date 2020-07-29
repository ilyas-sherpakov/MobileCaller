using System;
using System.Windows.Forms;
using MobileCaller.ComPort;
using MobileCaller.Localization;
using MobileCaller.Utils;

namespace MobileCaller.Forms
{
    public partial class ModemSettingsForm : Form, ILocalizable
    {
        #region Fields

        private string _comPort;
        private int _baudRate;

        private readonly IPortReader _portReader;

        #endregion

        #region Properties

        public string ComPort 
        {
            get { return _comPort; }
            set 
            {
                _comPort = value;
                cbComPort.Text = _comPort; 
            }
        }
        public int BaudRate
        {
            get { return _baudRate; }
            set
            {
                _baudRate = value; 
                cbPortRate.Text = _baudRate.ToString();
            }
        }

        #endregion

        public ModemSettingsForm(IPortReader portReader)
        {
            InitializeComponent();

            _portReader = portReader;

            // Initialize port list here so to have list of ports filled before settings are applied
            foreach (var port in System.IO.Ports.SerialPort.GetPortNames())
            {
                cbComPort.Items.Add(port);
            }
        }

        public void PerformTranslation()
        {
            var culture = Application.CurrentCulture;

            Text = ResourceManagerProvider.GetLocalizedString("WND_MODEM_SETTINGS_FORM_TITLE", culture);

            lPort.Text = String.Format("{0}:", ResourceManagerProvider.GetLocalizedString("L_PORT", culture));
            pbPort.SetHint(ResourceManagerProvider.GetLocalizedString("L_PORT_HINT", culture));

            lPortRate.Text = String.Format("{0}:", ResourceManagerProvider.GetLocalizedString("L_PORT_RATE", culture));
            pbPortRate.SetHint(ResourceManagerProvider.GetLocalizedString("L_PORT_RATE_HINT", culture));

            btnOk.Text = ResourceManagerProvider.GetLocalizedString("BTN_OK", culture);
            btnCancel.Text = ResourceManagerProvider.GetLocalizedString("BTN_CANCEL", culture);
        }

        private void cbComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combobox = sender as ComboBox;
            _comPort = combobox.Text;

            CheckModemStatus();
        }

        private void cbPortSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combobox = sender as ComboBox;
            _baudRate = Int32.Parse(combobox.Text);

            CheckModemStatus();
        }

        private void ModemSettingsForm_Load(object sender, EventArgs e)
        {
            PerformTranslation();
            CheckModemStatus();
        }

        /// <summary>
        /// Check if modem is responding on AT commands and show IMEI of mobile phone
        /// </summary>
        private void CheckModemStatus()
        {
            var culture = Application.CurrentCulture;
            if (cbComPort.SelectedIndex == -1)
            {
                lModemConnectionStatus.Text = ResourceManagerProvider.GetLocalizedString("L_PORT_INFORMATION_MODEM_NOT_SELECTED", culture);
            }
            else
            {
                try
                {
                    _portReader.PortName = _comPort;
                    _portReader.BaudRate = _baudRate;

                    _portReader.OpenPort();
                    _portReader.Initialize3GModem();
                    tbIEMI.Text = _portReader.GetIMEI();
                    _portReader.ClosePort();

                    lModemConnectionStatus.Text = ResourceManagerProvider.GetLocalizedString("L_PORT_INFORMATION_MODEM_SELECTED", culture);
                }
                catch (Exception ex)
                {
                    _portReader.ClosePort();

                    tbIEMI.Text = String.Empty;
                    lModemConnectionStatus.Text = ResourceManagerProvider.GetLocalizedString("L_PORT_INFORMATION_MODEM_NOT_ANSWERED", culture);
                    Logger.Write(ResourceManagerProvider.GetLocalizedString("L_PORT_INFORMATION_MODEM_NOT_ANSWERED", culture) + " " + ex.Message);
                }
            }
        }
    }
}
