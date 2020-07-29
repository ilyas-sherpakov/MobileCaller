using System;
using System.Windows.Forms;

namespace MobileCaller.Utils
{
    public static class TextBoxExtensions
    {
        public static bool IsTelephone(this TextBox textBox, char append)
        {
            if (char.IsControl(append))
            {
                // Allow to delete chars by backspace
                return true;
            }
            if (char.IsDigit(append) && textBox.Text.Length < 10)
            {
                return true;
            }
            return false;
        }

        public static bool IsInteger(this TextBox textBox, char append)
        {
            if (char.IsControl(append))
            {
                // Allow to delete chars by backspace
                return true;
            }
            try
            {
                Int32.Parse(textBox.Text + append);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
