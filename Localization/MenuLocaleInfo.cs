using System.Windows.Forms;

namespace MobileCaller.Localization
{
    public class MenuLocaleInfo
    {
        public MenuLocaleInfo(ToolStripMenuItem menuItem, string locale)
        {
            MenuItem = menuItem;
            Locale = locale;
        }

        #region Properties

        public ToolStripMenuItem MenuItem { get; set; }
        public string Locale { get; set; }

        #endregion
    }
}
