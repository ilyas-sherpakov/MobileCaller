using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MobileCaller.Localization;
using MobileCaller.XML;

namespace MobileCaller.Utils
{
    static class LogExtensions
    {
        public static void WriteSettingsLog(XmlCallSettings callSettings, IEnumerable<XmlGroupSettings> groupSettings, string workingDirectory)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            var culture = Application.CurrentCulture;
            var textList = new List<string>();
            textList.Add("====================================================");
            textList.AddRange(new[]
            {
                ResourceManagerProvider.GetLocalizedString("MSG_LOG_SETTINGS", culture),
                String.Format(ResourceManagerProvider.GetLocalizedString("L_PROGRAM_VERSION", culture), fileVersionInfo.ProductVersion),
                String.Format("{0} : {1}", ResourceManagerProvider.GetLocalizedString("L_WAIT_CALL", culture), callSettings.WaitCall),
                String.Format("{0} : {1}", ResourceManagerProvider.GetLocalizedString("L_SEND_NOTIFICATION", culture), callSettings.SendNotification),
                String.Format("{0} : {1}", ResourceManagerProvider.GetLocalizedString("L_REPEATABLE", culture), callSettings.Repeatable),
                String.Format("{0} : {1}", ResourceManagerProvider.GetLocalizedString("L_SHUTDOWN", culture), callSettings.Shutdown),
                String.Format("{0} : {1}", ResourceManagerProvider.GetLocalizedString("L_WORKING_DIRECTORY", culture), workingDirectory)
            });
            foreach (var group in groupSettings)
            {
                textList.Add("----------------------------------------------------");
                textList.AddRange(new[]
                {
                    String.Format("{0} : {1}", ResourceManagerProvider.GetLocalizedString("LST_GROUP_NAME", culture), group.GroupName),
                    String.Format("{0} : {1}", ResourceManagerProvider.GetLocalizedString("LST_WAIT_ANSWER", culture), group.WaitAnswer),
                    String.Format("{0} : {1}", ResourceManagerProvider.GetLocalizedString("LST_DOUBLE_CHECK_ON_TIMEOUT", culture), group.DoubleCheckOnTimeout),
                    String.Format("{0} : {1}", ResourceManagerProvider.GetLocalizedString("LST_SMS_RECIPIENT", culture), group.SmsRecipient),
                    String.Format("{0} : {1}", ResourceManagerProvider.GetLocalizedString("LST_SMS_TEXT", culture), group.SmsText),
                    String.Format("{0} : {1}", ResourceManagerProvider.GetLocalizedString("LST_USSD_TEXT", culture), group.UssdText),
                    String.Format("{0} : {1}", ResourceManagerProvider.GetLocalizedString("LST_NOTIFICATION_TYPE", culture), group.NotificationType)
                });
            }
            textList.Add("====================================================");

            Logger.Write(textList);
        }
    }
}
