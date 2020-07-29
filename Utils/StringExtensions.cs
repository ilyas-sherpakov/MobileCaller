using System;

namespace MobileCaller.Utils
{
    public static class StringExtensions
    {
        public static int? ToNullableInt(this string s)
        {
            int i;
            if (Int32.TryParse(s, out i)) return i;
            return null;
        }
    }
}
