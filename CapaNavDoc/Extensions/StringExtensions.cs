namespace CapaNavDoc.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt32(this string value)
        {
            int result;
            return int.TryParse(value, out result) ? result : 0;
        }
    }
}
