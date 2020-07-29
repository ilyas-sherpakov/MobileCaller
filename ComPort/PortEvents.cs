using System;
using MobileCaller.Enums;

namespace MobileCaller.ComPort
{
    public class ReadPortEvent : EventArgs
    {
        public string Telephone;
        public bool Activated;
        public int DialingSeconds;
        public int RingingSeconds;
        public ResponseCode Code;
    }

    public class FinishPortEvent : EventArgs
    {
        public Exception Exception;
    }
}
