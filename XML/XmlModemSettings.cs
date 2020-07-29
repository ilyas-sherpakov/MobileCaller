using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MobileCaller.XML
{
    public class XmlModemSettings : IChangeTracking
    {
        #region Fields

        private string _comPort = String.Empty;
        private int _baudRate = 115200;

        #endregion

        #region Implementing of IChangeTracking

        public bool IsChanged { get; private set; }

        public void AcceptChanges()
        {
            IsChanged = false;
        }

        #endregion

        #region Properties

        public string ComPort
        {
            get
            {
                Debug.Assert(_comPort != null);
                return _comPort;
            }
            set
            {
                var val = value ?? string.Empty;
                if (_comPort != val)
                {
                    _comPort = val;
                    IsChanged = true;
                }
            }
        }
        public int BaudRate
        {
            get { return _baudRate; }
            set
            {
                if (_baudRate != value)
                {
                    _baudRate = value;
                    IsChanged = true;
                }
            }
        }

        #endregion
    }
}
