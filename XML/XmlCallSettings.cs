using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MobileCaller.XML
{
    public class XmlCallSettings : IChangeTracking
    {
        #region Fields

        private int _waitCall = 5;
        private bool _sendNotification;
        private bool _playSound;
        private bool _repeatable;
        private bool _shutdown;

        #endregion

        #region Implementing of IChangeTracking

        public bool IsChanged { get; private set; }

        public void AcceptChanges()
        {
            IsChanged = false;
        }

        #endregion

        #region Properties

        public int WaitCall
        {
            get { return _waitCall; }
            set
            {
                if (_waitCall != value)
                {
                    _waitCall = value;
                    IsChanged = true;
                }
            }
        }
        public bool SendNotification
        {
            get { return _sendNotification; }
            set
            {
                if (_sendNotification != value)
                {
                    _sendNotification = value;
                    IsChanged = true;
                }
            }
        }
        public bool PlaySound
        {
            get { return _playSound; }
            set
            {
                if (_playSound != value)
                {
                    _playSound = value;
                    IsChanged = true;
                }
            }
        }
        public bool Repeatable
        {
            get { return _repeatable; }
            set
            {
                if (_repeatable != value)
                {
                    _repeatable = value;
                    IsChanged = true;
                }
            }
        }
        public bool Shutdown
        {
            get { return _shutdown; }
            set
            {
                if (_shutdown != value)
                {
                    _shutdown = value;
                    IsChanged = true;
                }
            }
        }

        #endregion
    }
}
