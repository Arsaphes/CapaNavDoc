using System.Linq;

namespace CapaNavDoc.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt32(this string value)
        {
            int result;
            return int.TryParse(value, out result) ? result : 0;
        }

        public static string AddId(this string str, int id)
        {
            if (str == null) str = "";
            return str + (str.Length == 0 ? id.ToString() : $";{id}");
        }

        public static string RemoveId(this string str, int id)
        {
            if (string.IsNullOrEmpty(str)) return null;
            string[] ids = str.Split(';');
            return ids.Where(theId => theId != theId.ToString()).Aggregate("", (current, i) => current + (current.Length == 0 ? i : $";{i}"));
        }
    }
}
