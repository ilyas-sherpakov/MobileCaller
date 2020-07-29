using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Linq;
using System.IO.Ports;
using System.ComponentModel;
using System.Threading;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MobileCaller.Enums;
using MobileCaller.Localization;
using MobileCaller.Utils;
using MobileCaller.XML;

namespace MobileCaller.ComPort
{
    public class PortReader : IPortReader
    {
        #region Inner types

        private class ResponseExecCommand
        {
            public ResponseCode Code {get; private set;}
            public string Text { get; private set; }
            public ResponseExecCommand(ResponseCode code, string text)
            {
                Code = code;
                Text = text;
            }
        }

        #endregion

        #region Events implementation

        public event EventHandler<ReadPortEvent> NotificationReadReady;
        public event EventHandler<FinishPortEvent> NotificationFinishedReady;
        public event EventHandler<ReadPortEvent> TelephoneReadReady;
        public event EventHandler<FinishPortEvent> TelephoneFinishedReady;

        #endregion

        #region Fields

        /// <summary>
        /// Specify using of language chosen by user  
        /// </summary>
        /// <remarks>
        /// Background worker gets the operating system locale when starting.
        /// In order to provide user localization we should start worker with locale from user preferences.
        /// </remarks>
        private volatile CultureInfo _culture = Application.CurrentCulture;

        private SerialPort _port;
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();
        private readonly AutoResetEvent _receiveNow = new AutoResetEvent(false);

        private delegate ResponseCode DialUpAction(string telephone, ReadPortEvent guiEvent);
        private DialUpAction _dialUp;

        private delegate ResponseCode HangUpAction();
        private HangUpAction _hangUp;

        // To test regulars: http://derekslager.com/blog/posts/2007/09/a-better-dotnet-regular-expression-tester.ashx
        private readonly Regex _regexClcc = new Regex("CLCC: \\d{1},(?<Dir>\\d{1}),(?<Stat>\\d{1}),\\d{1},\\d{1},\"\\+?[0-9]+\",[0-9]+\r\n\r\nOK\r\n");
        private readonly Regex _regexCend = new Regex("\\^CEND:\\d{1},\\d{1},\\d+,(?<Cause>\\d+)");
        private readonly Regex _regexCpas = new Regex("CPAS: (?<Stat>\\d{1})\r\n(\r\n)?OK\r\n");
        private readonly Regex _regexPhone = new Regex("^\r\n(?<Phone>.+)\r\n\r\nOK\r\n$");
        private readonly Regex _regexImei = new Regex("^\r\n(?<IMEI>\\d+)\r\n(\r\n)?OK\r\n$");
        private readonly Regex _regexSms = new Regex("^\r\n+CMGS: [0-9]+\r\n\r\nOK\r\n$");
        
        private int _introductoryVersionCounter;
        private bool _isIntroductoryVersion = true;

        /// <summary>
        /// Time in milliseconds on waiting for answer from modem of mobile phone on AT-commands sent to it.
        /// </summary>
        // private const int Timeout = 1500;

        /// <summary>
        /// Time in milliseconds on mobile response from SMS commands sent.
        /// </summary>
        private const int TimeoutSms = 15000;

        /// <summary>
        /// Time in milliseconds on mobile response from USSD commands sent.
        /// </summary>
        private const int TimeoutUssd = 60000;

        /// <summary>
        /// Time in milliseconds on waiting for response from modem on AT-commands(CLCC/CPAS) sent to it.
        /// </summary>
        private const int TimeoutCommand = 1000;

        private IList<XmlGroupSettings> _groupSettings;
        private IList<XmlTelephoneItem> _listTelephones;
        /// <summary>
        /// Telephone item which is checked from the telephone list at current moment 
        /// </summary>
        private XmlTelephoneItem _currentPhoneItem;
        
        #endregion

        #region Properties implementation

        public PortReaderOperation Operation { get; set; }
        public string PortName { get; set; }
        public int BaudRate { get; set; }

        public int WaitCall { get; set; }
        public bool SendNotification { get; set; }
        public string WorkingDirectory { get; set; }

        public bool IsBusy
        {
            get { return _backgroundWorker.IsBusy; }
        }

        #endregion 

        #region Properties

        /// <summary>
        /// Время ожидания завершения госового ответа "номер не обслуживается".
        /// Необходим для отделения от таких ответов как "номер вне зоны действия" и др.
        /// </summary>
        private int WaitAnswer 
        { 
            get
            {
                return _groupSettings.First(x => x.GroupName == _currentPhoneItem.GroupName).WaitAnswer;
            }
        }
        /// <summary>
        /// In case line of operator is not stable and inactive call bring out timeout result
        /// this option enforce double check of phone activation when timeout is received.
        /// </summary>
        private bool DoubleCheckOnTimeout
        {
            get
            {
                return _groupSettings.First(x => x.GroupName == _currentPhoneItem.GroupName).DoubleCheckOnTimeout;
            }
        }
        private string SmsRecipient
        {
            get
            {
                return _groupSettings.First(x => x.GroupName == _currentPhoneItem.GroupName).SmsRecipient;
            }
        }
        private string SmsText
        {
            get
            {
                return _groupSettings.First(x => x.GroupName == _currentPhoneItem.GroupName).SmsText;
            }
        }
        private string UssdText
        {
            get
            {
                return _groupSettings.First(x => x.GroupName == _currentPhoneItem.GroupName).UssdText;
            }
        }
        private NotificationType NotificationType
        {
            get
            {
                return _groupSettings.First(x => x.GroupName == _currentPhoneItem.GroupName).NotificationType;
            }
        }

        #endregion

        #region Constructors

        public PortReader()
        {
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;

            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
        }

        #endregion

        #region Methods implementation

        public void OpenPort()
        {
            _port = new SerialPort();
            _port.DataReceived += port_DataReceived;
            _port.Parity = Parity.None;
            _port.DataBits = 8;
            _port.StopBits = StopBits.One;
            _port.Handshake = Handshake.None;
            _port.Encoding = Encoding.ASCII;
            //Время ожидания записи и чтения с порта
            _port.WriteTimeout = TimeoutCommand / 2;
            _port.ReadTimeout = TimeoutCommand / 2;

            _port.PortName = PortName;
            _port.BaudRate = BaudRate;

            _port.Open();
            _port.DtrEnable = true;    // Data Terminal Ready
            _port.RtsEnable = true;    // Ready To Send
        }

        public void ClosePort()
        {
            _port.Close();
            _port = null;
        }

        public void Initialize3GModem()
        {
            // Set to default configuration and disconnect any call
            ExecCommand("ATZ", TimeoutCommand, new[] { ResponseCode.OK, ResponseCode.ERROR });
            // Disable echo
            ExecCommand("ATE0", TimeoutCommand, new[] { ResponseCode.OK, ResponseCode.ERROR });
        }

        private void InitializeHuaweiE1550()
        {
            // Перевод в режим "только модем"
            ExecCommand("AT^U2DIAG=0", TimeoutCommand, new[] { ResponseCode.OK, ResponseCode.ERROR });
            // Активировать голосовые функции модема
            // https://habrahabr.ru/post/192930/
            //ExecCommand("AT^CVOICE=0", TimeoutCommand, new[] { ResponseCode.OK, ResponseCode.ERROR });
            // Turn off periodic status messages. This should control the output of ^BOOT, ^RSSI, ^MODE, ^DSFLOWRPT, ^CONN, ^CEND, ^CONF, ^ORIG
            // Is not working for ^CEND and ^ORIG.
            ExecCommand("AT^CURC=0", TimeoutCommand, new[] { ResponseCode.OK, ResponseCode.ERROR });
            /* The ATD Command overrides this setting when a number is dialed.
                129 Unknown type(IDSN format number)
                161 National number type(IDSN format)
                145 International number type(ISDN format )
                177 Network specific number(ISDN format)
             * 
             * Проверяем: Send:AT+CSTA=?    Recieve: +CSTA: (129,145)
                          Send:AT+CSTA?     Recieve: +CSTA: 129
             * Меняем:    Send:AT+CSTA=145  Recieve: OK
                          Send:AT+CSTA?     Recieve: +CSTA: 145
               Теперь работает и так:  Send:ATDT+7916...;
            */
            ExecCommand("AT+CSTA=145", TimeoutCommand, new[] { ResponseCode.OK, ResponseCode.ERROR });
        }

        public bool Start(IEnumerable<XmlTelephoneItem> listTelephones, IList<XmlGroupSettings> groupSettings)
        {
            if (!_backgroundWorker.IsBusy)
            {
                _culture = Application.CurrentCulture;

                _groupSettings = groupSettings;
                _listTelephones = listTelephones.ToList();

                _backgroundWorker.RunWorkerAsync();

                return true;
            }

            return false;
        }

        public void Stop()
        {
            if (_backgroundWorker.WorkerSupportsCancellation)
            {
                // Cancel the asynchronous operation.
                _backgroundWorker.CancelAsync();
            }
        }

        public string GetIMEI()
        {
            var phoneCgsn = ExecCommand("AT+CGSN", TimeoutCommand, new[] { ResponseCode.OK, ResponseCode.ERROR }).Text;
            var match = _regexImei.Match(phoneCgsn.Replace(" ", ""));
            if (match.Success)
            {
                return match.Groups["IMEI"].Value;
            }

            throw new Exception(ResourceManagerProvider.GetLocalizedString("MSG_IMEI_NOT_SUPPORTED", Application.CurrentCulture));
        }

        #endregion

        #region Methods

        private void AutoMappingCallHandlers()
        {
            // List Current Calls of MT. If command succeeds but no calls are available no information response is sent.
            if (ExecCommand("AT+CLCC=?", TimeoutCommand, new[] { ResponseCode.OK, ResponseCode.ERROR }).Code != ResponseCode.ERROR)
            {
                _dialUp = DialUpCLCC;
            }
            // 	Used to interrogate the ME before requesting action from the phone.
            else if (ExecCommand("AT+CPAS=?", TimeoutCommand, new[] { ResponseCode.OK, ResponseCode.ERROR }).Code != ResponseCode.ERROR)
            {
                _dialUp = DialUpCPAS;
            }
            else
            {
                // Model of mobile is not supportting AT+CLCC и AT+CPAS. Program is using standard mode of modem answer now.
                _dialUp = DialUp;
            }

            if (ExecCommand("AT+CHUP=?", TimeoutCommand, new[] { ResponseCode.OK, ResponseCode.ERROR }).Code != ResponseCode.ERROR)
            {
                _hangUp = HangUpCHUP;
            }
            else
            {
                // Mobile is not support AT+CHUP. Program is using standard mode of call reset now.
                _hangUp = HangUp;
            }
        }

        private bool MappingCallHandlers(string phone)
        {
            bool res = false;
            switch (phone)
            {
                case "Nokia 2700 classic": // в режиме AT+CLCC через каждые примерно 10 звонков телефон попадает в blacklist
                case "Nokia 6300": // c AT+CLCC работает некорректно (присылает Ring, a потом Connect для неактивированного номера)
                case "Nokia X2-00": // RING and then CONNECT for MTC net breakdown and one beep is heard instead of ringing
                case "Nokia 6131": // Twice on day blacklisted phone(when clcc command is used) even in hybrid model
                case "Nokia C2-01": // Timeout when recipient reset call, no Busy code received in CLCC mode
                {
                        _dialUp = DialUpCPAS;
                        _hangUp = HangUpCHUP;
                        res = true;
                        break;
                    }
                case "Nokia 302":
                    {
                        _dialUp = DialUpHibryd;
                        _hangUp = HangUpCHUP;
                        res = true;
                        break;
                    }
                case "Nokia 6233":
                case "Nokia 6500s-1":
                case "Nokia 5800 XpressMusic":
                case "E1550": // CLCC shows error when call is reset by abonent, CPAS didn't show that there is no carrier and continue to keep line.
                {
                        _dialUp = DialUpCLCC;
                        _hangUp = HangUpCHUP;
                        res = true;
                        break;
                    }
                case "LG-T515":
                    {
                        _dialUp = DialUpCLCC;
                        _hangUp = HangUp;
                        res = true;
                        break;
                    }
                case "GT-S5230":
                    {
                        _dialUp = DialUp;
                        _hangUp = HangUp;
                        res = true;
                        break;
                    }
                default:
                    {
                        // "GT-B7722" is silent
                        if (phone.Contains("GT-"))
                        {
                            _dialUp = DialUp;
                            _hangUp = HangUp;
                            res = true;
                        }
                        break;
                    }
            }
            return res;
        }

        /// <summary>
        /// Does all work needed to be done with modem before processing.
        /// </summary>
        private void Initialization()
        {
            OpenPort();
            Initialize3GModem();

            var phone = ExecCommand("AT+GMM", TimeoutCommand, new[] { ResponseCode.OK, ResponseCode.ERROR }).Text;
            var match = _regexPhone.Match(phone);
            if (match.Success)
            {
                phone = match.Groups["Phone"].Value.Trim();
                if (!MappingCallHandlers(phone))
                {
                    AutoMappingCallHandlers();
                }
                else
                {
                    if (phone == "E1550")
                    {
                        InitializeHuaweiE1550();
                    }
                }
            }
            else
            {
                AutoMappingCallHandlers();
            }

            ValidateMobile();
        }

        #region BackgroundWorker event subscribers

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            Thread.CurrentThread.CurrentCulture = _culture;
            Thread.CurrentThread.CurrentUICulture = _culture;

            Initialization();

            // Call only those phone items which are in some named group from settings.
            foreach (var phoneItem in _listTelephones.Where(x => _groupSettings.Any(g => g.GroupName == x.GroupName)))
            {
                if (!worker.CancellationPending)
                {
                    ValidateIntroductoryVersion();

                    _currentPhoneItem = phoneItem;

                    var guiEvent = new ReadPortEvent { Telephone = _currentPhoneItem.Telephone };
                    switch (Operation)
                    {
                        case PortReaderOperation.Call:
                            if (!_currentPhoneItem.IsActivated())
                            {
                                if (ProcessCall(_currentPhoneItem, guiEvent))
                                {
                                    guiEvent.Activated = true;
                                    OnTelephoneReadReady(guiEvent);
                                    Thread.Sleep(WaitCall * 1000);
                                }
                                else
                                {
                                    OnTelephoneReadReady(guiEvent);

                                    if (guiEvent.Code == ResponseCode.NO_ANSWER_MODEM || guiEvent.Code == ResponseCode.BLACKLISTED)
                                    {
                                        Logger.Write(ResourceManagerProvider.GetLocalizedString("MSG_MODEM_PORT_RESET", Application.CurrentCulture));
                                        // pause in 1 minute
                                        Thread.Sleep(60000);

                                        ClosePort();
                                        OpenPort();
                                        Initialize3GModem();
                                    }
                                    else
                                    {
                                        Thread.Sleep(WaitCall * 1000);
                                    }
                                }
                            }
                            break;
                        case PortReaderOperation.Notification:
                            ProcessNotification(_currentPhoneItem.Telephone, guiEvent);
                            OnNotificationReadReady(guiEvent);
                            Thread.Sleep(WaitCall * 1000);
                            break;
                    }
                }
                else
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        /// <summary>
        /// This event handler deals with the results of the background operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var guiEvent = new FinishPortEvent();

            if (e.Cancelled)
            {
                //resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                Logger.Write(ResourceManagerProvider.GetLocalizedString("MSG_ERROR_MODEM_TITLE", Application.CurrentCulture) + " " + e.Error.StackTrace);
                guiEvent.Exception = e.Error;
            }

            try
            {
                ClosePort();
                Logger.Write(ResourceManagerProvider.GetLocalizedString("MSG_MODEM_WORK_COMPLETED", Application.CurrentCulture));
            }
            catch (Exception ex)
            {
                Logger.Write(ResourceManagerProvider.GetLocalizedString("MSG_ERROR_MODEM_TITLE", Application.CurrentCulture) + " " + ex.StackTrace);
                guiEvent.Exception = ex;
            }

            switch (Operation)
            {
                case PortReaderOperation.Call:
                    OnTelephoneFinishedReady(guiEvent);
                    break;
                case PortReaderOperation.Notification:
                    OnNotificationFinishedReady(guiEvent);
                    break;
            }
        }

        #endregion

        #region Event Handlers

        private void OnNotificationReadReady(ReadPortEvent e)
        {
            EventHandler<ReadPortEvent> handler = NotificationReadReady;
            if (handler != null)
                NotificationReadReady(this, e);
        }
        private void OnNotificationFinishedReady(FinishPortEvent e)
        {
            EventHandler<FinishPortEvent> handler = NotificationFinishedReady;
            if (handler != null)
                NotificationFinishedReady(this, e);
        }
        private void OnTelephoneReadReady(ReadPortEvent e)
        {
            EventHandler<ReadPortEvent> handler = TelephoneReadReady;
            if (handler != null)
                TelephoneReadReady(this, e);
        }
        private void OnTelephoneFinishedReady(FinishPortEvent e)
        {
            EventHandler<FinishPortEvent> handler = TelephoneFinishedReady;
            if (handler != null)
                TelephoneFinishedReady(this, e);
        }

        #endregion

        /// <summary>
        /// Function which delimits functional for user without registration 
        /// </summary>
        private void ValidateIntroductoryVersion()
        {
            if (_isIntroductoryVersion)
            {
                if (_introductoryVersionCounter > 5)
                {
                    throw new Exception(ResourceManagerProvider.GetLocalizedString("MSG_INTRODUCTORY_VERSION", Application.CurrentCulture));
                }

                ++_introductoryVersionCounter;
            }
        }

        /// <summary>
        /// Check if registration is valid and check if mobile phone can support required AT commands
        /// </summary>
        private void ValidateMobile()
        {
            if (CPassword.Verify())
            {
                if (GetIMEI() != CPassword.IMEI)
                {
                    throw new Exception(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_WRONG_IMEI_REGISTRATION",
                                                                                 Application.CurrentCulture), CPassword.User, CPassword.IMEI));
                }

                _isIntroductoryVersion = false;
            }
            else
            {
                _isIntroductoryVersion = true;
            }

            // Check support of SMS/USSD by phone
            if (SendNotification || Operation == PortReaderOperation.Notification)
            {
                if (CheckSendSms() != ResponseCode.OK)
                {
                    throw new Exception(ResourceManagerProvider.GetLocalizedString("MSG_SMS_NOT_SUPPORTED", Application.CurrentCulture));
                }
                if (CheckSendUSSD() != ResponseCode.OK)
                {
                    throw new Exception(ResourceManagerProvider.GetLocalizedString("MSG_USSD_NOT_SUPPORTED", Application.CurrentCulture));
                }
            }
        }

        /// <summary>
        /// Provides call on the phone and return the status of call 
        /// </summary>
        /// <param name="phoneItem">Mobile phone item</param>
        /// <param name="guiEvent">Event which will be passed to GUI</param>
        /// <returns>True if number is activated and notification is sent if it is enabled</returns>
        private bool ProcessCall(XmlTelephoneItem phoneItem, ReadPortEvent guiEvent)
        {
            bool retVal = false;

            guiEvent.Code = _dialUp(phoneItem.Telephone, guiEvent);

            if (DoubleCheckOnTimeout && guiEvent.Code == ResponseCode.TIMEOUT)
            {
                guiEvent.Code = _hangUp();
                if (guiEvent.Code == ResponseCode.OK)
                {
                    Thread.Sleep(WaitCall * 1000);

                    // The second attempt to find out the activation in case line of operator is not stable
                    guiEvent.Code = _dialUp(phoneItem.Telephone, guiEvent);
                }
                else if (guiEvent.Code == ResponseCode.TIMEOUT)
                {
                    // in case modem is not responding on hang up
                    return true;
                }
            }

            if (guiEvent.Code == ResponseCode.CONNECT || guiEvent.Code == ResponseCode.RING ||  
                guiEvent.Code == ResponseCode.BUSY || guiEvent.Code == ResponseCode.NO_ANSWER ||
                guiEvent.Code == ResponseCode.TIMEOUT)
            {
                if (_hangUp() == ResponseCode.OK)
                {
                    retVal = true;
                    if (SendNotification)
                    {
                        Thread.Sleep(WaitCall * 1000);

                        retVal = ProcessNotification(phoneItem.Telephone, guiEvent);
                    }
                }
                else if (guiEvent.Code == ResponseCode.TIMEOUT)
                {
                    // in case modem is not responding on hang up
                    return false;
                }
            }
            else 
            {
                // don't hang up phone on ERROR, BLACKLISTED, NO_ANSWER_MODEM, NO_ANSWER, NO_CARRIER, etc.
                return false;
            }
            return retVal;
        }

        /// <summary>
        /// Provides notification of activated number
        /// </summary>
        /// <param name="phone">Activated mobile phone number</param>
        /// <param name="guiEvent">Event which will be passed to GUI</param>
        /// <returns>Result if notification was sent</returns>
        private bool ProcessNotification(string phone, ReadPortEvent guiEvent)
        {
            var retVal = false;

            if (NotificationType == NotificationType.SMS)
            {
                guiEvent.Code = SendSms(phone);
                if (guiEvent.Code == ResponseCode.OK || guiEvent.Code == ResponseCode.SMS)
                {
                    retVal = true;
                }
            }
            else
            {
                guiEvent.Code = SendUSSD(phone);
                if (guiEvent.Code == ResponseCode.OK)
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        private ResponseCode CheckSendSms()
        {
            return ExecCommand("AT+CMGF=?", TimeoutCommand, new[] { ResponseCode.OK, ResponseCode.ERROR }).Code;
        }

        private ResponseCode CheckSendUSSD()
        {
            return ExecCommand("AT+CUSD=?", TimeoutCommand, new[] { ResponseCode.OK, ResponseCode.ERROR }).Code;
        }

        private ResponseCode DialUp(string telephone, ReadPortEvent guiEvent)
        {
            var watch = new Stopwatch();
            watch.Start();

            var res = ExecCommand("ATD" + telephone + ";", WaitAnswer * 1000, new[] { ResponseCode.CONNECT,
                                                                                      ResponseCode.BUSY,
                                                                                      ResponseCode.RING,
                                                                                      ResponseCode.ERROR,
                                                                                      ResponseCode.BLACKLISTED,
                                                                                      ResponseCode.NO_CARRIER,
                                                                                      ResponseCode.NO_ANSWER }).Code;
            
            watch.Stop();
            guiEvent.RingingSeconds = watch.Elapsed.Seconds;

            return res;
        }
        // CPAS - Данная команда показывает статус активности мобильного оборудования.
        private ResponseCode DialUpCPAS(string telephone, ReadPortEvent guiEvent)
        {
            return DialUpHandler(telephone, guiEvent, "AT+CPAS", "AT+CPAS",
                new[] {ResponseCode.CONNECT, ResponseCode.RING, ResponseCode.NO_CARRIER, ResponseCode.NO_ANSWER_MODEM},
                new[] {ResponseCode.CONNECT, ResponseCode.NO_CARRIER});
        }
        // CLCC - Данная команда используется для восстановления списка текущих вызовов
        private ResponseCode DialUpCLCC(string telephone, ReadPortEvent guiEvent)
        {
            var res = DialUpHandler(telephone, guiEvent, "AT+CLCC", "AT+CLCC",
                new[] { ResponseCode.CONNECT, ResponseCode.BUSY, ResponseCode.OK, ResponseCode.RING, ResponseCode.NO_CARRIER },
                new[] { ResponseCode.CONNECT, ResponseCode.BUSY, ResponseCode.OK, ResponseCode.NO_ANSWER, ResponseCode.NO_CARRIER });
            // In case command returns OК that means the connection line is interrupted 
            // Если команда CLCC была выполнена, но соединений нет, то ответ не передается(мы получаем от телефона ответ ОК)
            if (res == ResponseCode.OK)
            {
                res = ResponseCode.NO_CARRIER;
                Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_RESPONSE_CODE_CHANGED", Application.CurrentCulture), res));
            }
            return res;
        }
        private ResponseCode DialUpHibryd(string telephone, ReadPortEvent guiEvent)
        {
            var res = DialUpHandler(telephone, guiEvent, "AT+CPAS", "AT+CLCC",
                new[] { ResponseCode.CONNECT, ResponseCode.RING, ResponseCode.NO_CARRIER, ResponseCode.NO_ANSWER_MODEM },
                new[] { ResponseCode.CONNECT, ResponseCode.BUSY, ResponseCode.OK, ResponseCode.NO_ANSWER, ResponseCode.NO_CARRIER });
            // In case command returns OК that means the connection line is interrupted 
            // Если команда CLCC была выполнена, но соединений нет, то ответ не передается(мы получаем от телефона ответ ОК)
            if (res == ResponseCode.OK)
            {
                res = ResponseCode.NO_CARRIER;
                Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_RESPONSE_CODE_CHANGED", Application.CurrentCulture), res));
            }
            return res;
        }
        private ResponseCode DialUpHandler(string telephone, ReadPortEvent guiEvent, string ringCommand, string command,
            IEnumerable<ResponseCode> commandDialingResponses, IEnumerable<ResponseCode> commandRingingResponses)
        {
            var res = ExecCommand("ATD" + telephone + ";", TimeoutCommand, new[] { ResponseCode.OK,
                                                                                   ResponseCode.NO_CARRIER,
                                                                                   ResponseCode.BLACKLISTED,
                                                                                   ResponseCode.NO_ANSWER_MODEM,
                                                                                   ResponseCode.ERROR}).Code;
            if (res == ResponseCode.OK)
            {
                var watch = new Stopwatch();
                watch.Start();

                var ticker = 0;
                var ringWasFound = false;
                var connectWasFound = false;
                do
                {
                    if (!ringWasFound)
                    {
                        res = ExecCommand(ringCommand, commandDialingResponses).Code;
                        // auto caller doesn't give RING code and answer the phone right away
                        if (res == ResponseCode.RING || res == ResponseCode.CONNECT)
                        {
                            if (!connectWasFound)
                            {
                                watch.Stop();
                                guiEvent.DialingSeconds = watch.Elapsed.Seconds;
                                watch.Reset();
                                watch.Start();
                                ticker = -1;
                            }

                            switch (res)
                            {
                                case ResponseCode.RING:
                                    ringWasFound = true;
                                    break;
                                case ResponseCode.CONNECT:
                                    if (!connectWasFound)
                                    {
                                        connectWasFound = true; 
                                    }
                                    break;
                            }

                            res = ResponseCode.TIMEOUT;
                            Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_RESPONSE_CODE_CHANGED", Application.CurrentCulture), res));
                        }
                    }
                    else
                    {
                        res = ExecCommand(command, commandRingingResponses).Code;
                    }
                    ++ticker;
                }
                while ((res == ResponseCode.TIMEOUT) && (ticker < WaitAnswer));
                watch.Stop();

                if (ringWasFound)
                {
                    guiEvent.RingingSeconds = watch.Elapsed.Seconds;
                }
                else
                {
                    // Operator line is broken so we can't even ring
                    if (connectWasFound && res == ResponseCode.TIMEOUT)
                    {
                        // auto caller was defined
                        res = ResponseCode.CONNECT;
                        Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_RESPONSE_CODE_CHANGED", Application.CurrentCulture), res));

                        guiEvent.RingingSeconds = watch.Elapsed.Seconds;
                    }
                    else
                    {
                        guiEvent.DialingSeconds = watch.Elapsed.Seconds;
                    }
                }
            }
            else if (res == ResponseCode.ERROR)
            {
                res = ResponseCode.NO_ANSWER_MODEM;
                Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_RESPONSE_CODE_CHANGED", Application.CurrentCulture), res));
            }
            return res;
        }

        private ResponseCode HangUpCHUP()
        {
            return ExecCommand("AT+CHUP", TimeoutCommand, new[] { ResponseCode.OK }).Code;
        }

        private ResponseCode HangUp()
        {
            return ExecCommand("ATH0", TimeoutCommand, new[] { ResponseCode.OK }).Code;
        }

        /// <summary>
        /// Sends SMS message
        /// </summary>
        /// <param name="telephone">Telephone number activated</param>
        /// <returns>Response code from modem</returns>
        private ResponseCode SendSms(string telephone)
        {
            //var e = ExecCommand("AT+CSCA?", Timeout, new[] { ResponseCode.OK, ResponseCode.ERROR }).Code;

            Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_SMS_EXECUTING", Application.CurrentCulture), telephone));
            // Select SMS text mode
            var res = ExecCommand("AT+CMGF=1", TimeoutCommand, new[] { ResponseCode.OK, ResponseCode.ERROR }).Code;
            if (res == ResponseCode.OK)
            {
                res = ExecCommand(String.Format("AT+CMGS=\"{0}\"", SmsRecipient), TimeoutCommand, new[] { ResponseCode.SMS_TEXT, ResponseCode.ERROR }).Code;
                if (res == ResponseCode.SMS_TEXT)
                {
                    var smsText = SmsText.Replace("%PHONE%", telephone);
                    //            m_port.WriteLine(m_CurTelephone + System.Environment.NewLine + (char)(26));
                    //return ExecCommand(telephone + char.ConvertFromUtf32(26) + "\r", 10000, new ResponseCode[] { ResponseCode.OK });
                    // Ctrl+z is used on keyboard to send filled up text
                    res = ExecCommand(smsText + char.ConvertFromUtf32(26), TimeoutSms, new[] { ResponseCode.OK, ResponseCode.SMS, ResponseCode.ERROR }).Code;
                    if (res == ResponseCode.TIMEOUT)
                    {
                        res = ResponseCode.OK;
                        Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_RESPONSE_CODE_CHANGED", Application.CurrentCulture), res));
                    }
                }
            }
            else if (res == ResponseCode.TIMEOUT)
            {
                res = ResponseCode.ERROR;
                Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_RESPONSE_CODE_CHANGED", Application.CurrentCulture), res));
            }
            
            //e = ExecCommand("AT+CMEE=305", Timeout, new[] { ResponseCode.OK, ResponseCode.ERROR }).Code;

            return res;
        }

        /// <summary>
        /// Sends Unstructured Supplementary Service Data request
        /// </summary>
        /// <param name="telephone">Telephone number activated</param>
        /// <returns>Response code from modem</returns>
        private ResponseCode SendUSSD(string telephone)
        {
            Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_USSD_EXECUTING", Application.CurrentCulture), telephone));

            var ussdText = UssdText.Replace("%PHONE%", telephone);
            var res = ExecCommand("AT+CUSD=1,\"" + ussdText + "\",15" /*+ (char)13 + (char)26*/, TimeoutUssd, new[] { ResponseCode.OK, ResponseCode.ERROR }).Code;

            return res;
        }

        private ResponseExecCommand ExecCommand(string command, int responseTimeout, IEnumerable<ResponseCode> responses)
        {
            var culture = Application.CurrentCulture;
            _port.DiscardOutBuffer();
            _port.DiscardInBuffer();
            _receiveNow.Reset();
            _port.Write(command + "\r"); // <CR> - should be supplied for the command to be executed
            Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_EXECUTED", culture), command));
            var buffer = String.Empty;
            ResponseExecCommand res;
            for ( ; ; )
            {
                if (_receiveNow.WaitOne(responseTimeout, false))
                {
                    // if the current instance receives a signal
                    string read = _port.ReadExisting();
                    buffer += read;
                    Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_RESPONSE_AT", Application.CurrentCulture), read));
                    res = GetResponse(buffer, responses);
                    if (res != null)
                    {
                        break;
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(buffer))
                    {
                        if (responses.Any(r => r == ResponseCode.NO_ANSWER_MODEM))
                        {
                            res = new ResponseExecCommand(ResponseCode.NO_ANSWER_MODEM, buffer);
                            break;
                        }
                    }

                    res = new ResponseExecCommand(ResponseCode.TIMEOUT, buffer);
                    break;
                }
            }
            Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_RESPONSE_CODE", culture), res.Code));
            return res;
        }

        private ResponseExecCommand ExecCommand(string command, IEnumerable<ResponseCode> responses)
        {
            var culture = Application.CurrentCulture;
            _port.DiscardOutBuffer();
            _port.DiscardInBuffer();
            _receiveNow.Reset();
            _port.Write(command + "\r"); // <CR> - should be supplied for the command to be executed
            Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_EXECUTED", culture), command));

            Thread.Sleep(TimeoutCommand);
            var buffer = _port.ReadExisting();
            Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_RESPONSE_AT", Application.CurrentCulture), buffer));
            var res = GetResponse(buffer, responses);
            if (res == null)
            {
                if (String.IsNullOrEmpty(buffer))
                {
                    if (responses.Any(r => r == ResponseCode.NO_ANSWER_MODEM))
                    {
                        res = new ResponseExecCommand(ResponseCode.NO_ANSWER_MODEM, buffer);
                    }
                }
                res = new ResponseExecCommand(ResponseCode.TIMEOUT, buffer);
            }
            Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_RESPONSE_CODE", culture), res.Code));
            return res;
        }

        /// <summary>
        /// Event handler which set the sign that data coming in modem buffer
        /// </summary>
        /// <param name="sender">SerialPort object</param>
        /// <param name="e">Argument containing data received information</param>
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Chars)
            {
                _receiveNow.Set();
            }
        }

        private ResponseExecCommand GetResponse(string buffer, IEnumerable<ResponseCode> responses)
        {
            if (buffer.Contains("\r\n>\r\n") || buffer.Contains("\r\n> "))
            {
                if (responses.Any(r => r == ResponseCode.SMS_TEXT))
                {
                    return new ResponseExecCommand(ResponseCode.SMS_TEXT, buffer);
                }
            }
            else if (buffer.Contains("\r\nRING\r\n"))
            {
                if (responses.Any(r => r == ResponseCode.RING))
                {
                    return new ResponseExecCommand(ResponseCode.RING, buffer);
                }
            }
            else if (buffer.Contains("\r\nCONNECT\r\n"))
            {
                if (responses.Any(r => r == ResponseCode.CONNECT))
                {
                    return new ResponseExecCommand(ResponseCode.CONNECT, buffer);
                }
            }
            else if (buffer.Contains("\r\nNO CARRIER\r\n"))
            {
                if (responses.Any(r => r == ResponseCode.NO_CARRIER))
                {
                    return new ResponseExecCommand(ResponseCode.NO_CARRIER, buffer);
                }
            }
            else if (buffer.Contains("\r\nBUSY\r\n"))
            {
                if (responses.Any(r => r == ResponseCode.BUSY))
                {
                    return new ResponseExecCommand(ResponseCode.BUSY, buffer);
                }
            }
            else if (buffer.Contains("\r\nNO ANSWER\r\n"))
            {
                if (responses.Any(r => r == ResponseCode.NO_ANSWER))
                {
                    return new ResponseExecCommand(ResponseCode.NO_ANSWER, buffer);
                }
            }
            else if (buffer.Contains("\r\nBLACKLISTED\r\n"))
            {
                if (responses.Any(r => r == ResponseCode.BLACKLISTED))
                {
                    return new ResponseExecCommand(ResponseCode.BLACKLISTED, buffer);
                }
            }
            else if (_regexSms.Match(buffer).Success)
            {
                if (responses.Any(r => r == ResponseCode.SMS))
                {
                    return new ResponseExecCommand(ResponseCode.SMS, buffer);
                }
            }
            else if (_regexCend.Match(buffer).Success)
            {
                var match = _regexCend.Match(buffer);
                Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_RESPONSE_CODE_CEND", Application.CurrentCulture),
                    match.Groups["Cause"].Value));
                var res = ComPortHelper.ConvertCENDResponseCode(Int32.Parse(match.Groups["Cause"].Value));
                if (responses.Any(r => r == res))
                {
                    return new ResponseExecCommand(res, buffer);
                }
            }
            else if (_regexCpas.Match(buffer).Success)
            {
                var match = _regexCpas.Match(buffer);
                Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_RESPONSE_CODE_CPAS", Application.CurrentCulture), 
                    match.Groups["Stat"].Value));
                var res = ComPortHelper.ConvertCPASToResponseCode(Int32.Parse(match.Groups["Stat"].Value));
                if (responses.Any(r => r == res))
                {
                    return new ResponseExecCommand(res, buffer);
                }
            }
            else if (_regexClcc.Match(buffer).Success)
            {
                var match = _regexClcc.Match(buffer);
                Logger.Write(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_COMMAND_RESPONSE_CODE_CLCC", Application.CurrentCulture),
                    match.Groups["Dir"].Value + ";" + match.Groups["Stat"].Value));
                var res = ComPortHelper.ConvertCLCCToResponseCode(Int32.Parse(match.Groups["Dir"].Value),
                                                                  Int32.Parse(match.Groups["Stat"].Value),
                                                                  match.Groups["Cause"].Value.ToNullableInt());
                if (responses.Any(r => r == res))
                {
                    return new ResponseExecCommand(res, buffer);
                }
            }
            else if (buffer.Contains("\r\nERROR\r\n") || buffer.Contains("+CMS ERROR:") || buffer.Contains("+CME ERROR:")) //+CME ERROR: 513
            {
                if (responses.Any(r => r == ResponseCode.ERROR))
                {
                    return new ResponseExecCommand(ResponseCode.ERROR, buffer);
                }
            }
            else if (buffer.Contains("\r\nOK\r\n")) // Should be at last
            {
                if (responses.Any(r => r == ResponseCode.OK))
                {
                    return new ResponseExecCommand(ResponseCode.OK, buffer);
                }
            }
            return null;
        }

        #endregion
    }
}
