using System.Windows.Forms;

namespace MobileCaller.Utils
{
    public static class ToolTipExtensions
    {
        /// <summary>
        /// Add visual hint to control when user move cursor on it
        /// </summary>
        /// <param name="ctrl">The object which should show hint</param>
        /// <param name="text">Hint text</param>
        public static void SetHint(this Control ctrl, string text)
        {
            var toolTip = new ToolTip
                              {
                                  IsBalloon = true,
                                  AutoPopDelay = 10000,
                                  InitialDelay = 500,
                                  ReshowDelay = 100,
                                  ShowAlways = true
                              };
            toolTip.SetToolTip(ctrl, text);
        }
    }
}
