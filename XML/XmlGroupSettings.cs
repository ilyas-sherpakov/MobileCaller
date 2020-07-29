using System;
using System.ComponentModel;
using System.Diagnostics;
using MobileCaller.Enums;

namespace MobileCaller.XML
{
    public class XmlGroupSettings : IChangeTracking, ICloneable
    {
        #region Fields

        private string _groupName = String.Empty;
        private int _waitAnswer;
        private bool _doubleCheckOnTimeout;
        private string _smsRecipient = String.Empty;
        private string _smsText = String.Empty;
        private string _ussdText = String.Empty;
        private NotificationType _notificationType = NotificationType.SMS;

        #endregion

        #region Implementing of IChangeTracking

        public bool IsChanged { get; private set; }

        public void AcceptChanges()
        {
            IsChanged = false;
        }

        #endregion

        #region Implementing of IClonable

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        #region Properties

        public string GroupName
        {
            get
            {
                Debug.Assert(_groupName != null);
                return _groupName;
            }
            set
            {
                var val = value ?? String.Empty;
                if (_groupName != val)
                {
                    _groupName = val;
                    IsChanged = true;
                }
            }
        }
        public int WaitAnswer
        {
            get { return _waitAnswer; }
            set
            {
                if (_waitAnswer != value)
                {
                    _waitAnswer = value;
                    IsChanged = true;
                }
            }
        }
        public bool DoubleCheckOnTimeout
        {
            get { return _doubleCheckOnTimeout; }
            set
            {
                if (_doubleCheckOnTimeout != value)
                {
                    _doubleCheckOnTimeout = value;
                    IsChanged = true;
                }
            }
        }
        public string SmsRecipient
        {
            get
            {
                Debug.Assert(_smsRecipient != null);
                return _smsRecipient;
            }
            set
            {
                var val = value ?? String.Empty;
                if (_smsRecipient != val)
                {
                    _smsRecipient = val;
                    IsChanged = true;
                }
            }
        }
        public string SmsText
        {
            get
            {
                Debug.Assert(_smsText != null);
                return _smsText;
            }
            set
            {
                var val = value ?? String.Empty;
                if (_smsText != val)
                {
                    _smsText = val;
                    IsChanged = true;
                }
            }
        }
        public string UssdText
        {
            get
            {
                Debug.Assert(_ussdText != null);
                return _ussdText;
            }
            set
            {
                var val = value ?? String.Empty;
                if (_ussdText != val)
                {
                    _ussdText = val;
                    IsChanged = true;
                }
            }
        }
        public NotificationType NotificationType
        {
            get { return _notificationType; }
            set
            {
                if (_notificationType != value)
                {
                    _notificationType = value;
                    IsChanged = true;
                }
            }
        }
        
        #endregion
    }
}
