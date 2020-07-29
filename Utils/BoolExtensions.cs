namespace MobileCaller.Utils
{
    public static class BoolExtensions
    {
        public static string ToXmlString(this bool b)
        {
            return b.ToString().ToLower();
        }
    }
}
