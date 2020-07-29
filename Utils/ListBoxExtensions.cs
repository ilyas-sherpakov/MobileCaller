using System;
using System.Windows.Forms;
using MobileCaller.ComPort;
using MobileCaller.Enums;
using MobileCaller.Localization;

namespace MobileCaller.Utils
{
    public static class ListBoxExtensions
    {
        public static void AddLogItem(this ListBox listView, string message)
        {
            listView.Items.Add(string.Format("{0:HH}:{0:mm}:{0:ss} - {1}", DateTime.Now, message));
        }

        public static void AddLogItem(this ListBox listView, string message, SessionStatistics statistics)
        {
            var parameter = String.Format(ResourceManagerProvider.GetLocalizedString("LOG_STATISTICS", Application.CurrentCulture), 
                statistics.ProcessedCount, statistics.ActivatedCount);
            listView.Items.Add(string.Format("{0:HH}:{0:mm}:{0:ss} - {1} {2}", DateTime.Now, message, parameter));
        }

        public static void AddLogItem(this ListBox listView, ReadPortEvent guiEvent)
        {
            string parameter;
            if (guiEvent.Code == ResponseCode.BLACKLISTED)
            {
                parameter = String.Format(ResourceManagerProvider.GetLocalizedString("LOG_NUMBER_BLACKLISTED", Application.CurrentCulture),
                        guiEvent.Telephone, guiEvent.DialingSeconds, guiEvent.RingingSeconds);
            }
            else if (guiEvent.Code == ResponseCode.NO_ANSWER_MODEM)
            {
                parameter = String.Format(ResourceManagerProvider.GetLocalizedString("LOG_NUMBER_NO_ANSWER_MODEM", Application.CurrentCulture),
                        guiEvent.Telephone, guiEvent.DialingSeconds, guiEvent.RingingSeconds);
            }
            else if (guiEvent.Code == ResponseCode.ERROR)
            {
                parameter = String.Format(ResourceManagerProvider.GetLocalizedString("LOG_NUMBER_ERROR", Application.CurrentCulture),
                        guiEvent.Telephone, guiEvent.DialingSeconds, guiEvent.RingingSeconds);
            }
            else
            {
                parameter = String.Format(ResourceManagerProvider.GetLocalizedString("LOG_NUMBER_PROCESSED", Application.CurrentCulture),
                        guiEvent.Telephone, guiEvent.DialingSeconds, guiEvent.RingingSeconds);
            }
            listView.AddLogItem(parameter);
        }
    }
}
