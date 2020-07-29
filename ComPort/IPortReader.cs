using System;
using System.Collections.Generic;
using MobileCaller.Enums;
using MobileCaller.XML;

namespace MobileCaller.ComPort
{
    public interface IPortReader
    {
        #region Events

        event EventHandler<ReadPortEvent> NotificationReadReady;
        event EventHandler<FinishPortEvent> NotificationFinishedReady;
        event EventHandler<ReadPortEvent> TelephoneReadReady;
        event EventHandler<FinishPortEvent> TelephoneFinishedReady;

        #endregion

        #region Properties

        PortReaderOperation Operation { get; set; }
        string PortName { get; set; }
        int BaudRate { get; set; }

        /// <summary>
        /// Время задержки между звонками в секундах.
        /// Используется, если оператор блокирует карточку, определяя номер как подозрительный.
        /// </summary>
        int WaitCall { get; set; }
        bool SendNotification { get; set; }
        string WorkingDirectory { get; set; }

        /// <summary>
        /// Provide information if processing is continued at current moment.
        /// </summary>
        bool IsBusy { get; }

        #endregion

        #region Methods

        void OpenPort();
        void ClosePort();

        /// <summary>
        /// Initialize modem by set of AT commands before start working.
        /// </summary>
        /// <remarks>Should be executed after port for modem is successfully opened.</remarks>
        void Initialize3GModem();

        /// <summary>
        /// Start the process of call or sending notification.
        /// </summary>
        /// <param name="listTelephones">The list of telephones which are needs to be processed.</param>
        /// <param name="groupSettings">The collection of telephone groups.</param>
        /// <returns>True - in case background process is started. False - if it is still working.</returns>
        bool Start(IEnumerable<XmlTelephoneItem> listTelephones, IList<XmlGroupSettings> groupSettings);
        void Stop();
        
        /// <summary>
        /// Returns IMEI code of mobile phone synchronously.
        /// </summary>
        /// <returns>Exception if IMEI can't be got or IMEI code otherwise.</returns>
        string GetIMEI();

        #endregion
    }
}
