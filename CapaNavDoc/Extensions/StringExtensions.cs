using System.Collections.Generic;
using System.Linq;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Models.BusinessLayers;

namespace CapaNavDoc.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt32(this string value)
        {
            int result;
            return int.TryParse(value, out result) ? result : 0;
        }


        public static List<T> GetList<T>(this string idList) where T : class
        {
            if(string.IsNullOrEmpty(idList)) return new List<T>();

            BusinessLayer<T> bl = new BusinessLayer<T>(new CapaNavDocDal());
            string[] ids = idList.Split(';');

            return bl.GetList().Where(t => ids.Contains(typeof(T).GetProperty("Id").GetValue(t).ToString())).ToList();
        }




        public static string AddId(this string str, int id)
        {
            if (str == null) str = string.Empty;
            return str + (str.Length == 0 ? id.ToString() : $";{id}");
        }

        public static string AddIdCouple(this string str, int id1, int id2)
        {
            if (str == null) str = string.Empty;
            return str + (str.Length == 0 ? $"{id1},{id2}" : $";{id1},{id2}");
        }
    }
}
