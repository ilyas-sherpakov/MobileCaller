using System;
using System.Resources;
using System.Reflection;
using System.Globalization;

namespace MobileCaller.Localization
{
    public class ResourceManagerProvider
    {
        #region Private methods

        private static readonly ResourceManager ResourceManagergr = new ResourceManager("MobileCaller.Properties.Resources", Assembly.GetExecutingAssembly());

        private static ResourceManager AppResourcesManager
        {
            get { return ResourceManagergr; }
        }

        #endregion

        public static String GetLocalizedString(String sResourceName, CultureInfo cultureInfo)
        {
            string localizedStrings = AppResourcesManager.GetString(sResourceName, cultureInfo);
            return localizedStrings.Replace(@"\n", "\n");
        }
    }
}
