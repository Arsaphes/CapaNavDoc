using System.Collections.Generic;
using System.Linq;
using CapaNavDoc.Classes;

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

        public static List<CenterActionCouple> ToCenterActionGroups(this string str)
        {
            List<CenterActionCouple> couples = new List<CenterActionCouple>();
            if (str == null) return couples;
            string[] groups = str.Split(';');

            couples.AddRange(groups.Select(g => g.Split(',')).Select(couple => new CenterActionCouple {CenterId = couple[0], ActionId = couple[1]}));
            return couples;
        }
    }
}
