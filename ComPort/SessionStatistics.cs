using System.Collections.Generic;
using MobileCaller.XML;

namespace MobileCaller.ComPort
{
    public struct SessionStatistics
    {
        /// <summary>
        /// The count of phone numbers which were already called in bounds of current call.
        /// </summary>
        public int ProcessedCount;

        /// <summary>
        /// The count of phone numbers which are need to be called in bounds of current call.
        /// </summary>
        public int RemainedCount;

        /// <summary>
        /// The count of phone numbers which have been activated in bounds of current call.
        /// </summary>
        public int ActivatedCount;

        /// <summary>
        /// The phone numbers which have been called.
        /// </summary>
        public IList<XmlTelephoneItem> Telephones;
    }
}
