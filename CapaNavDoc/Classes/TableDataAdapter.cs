using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CapaNavDoc.Models;

namespace CapaNavDoc.Classes
{
	public class TableDataAdapter
	{
	    public static List<T> Search<T>(List<T> list, JQueryDataTableParam param)
	    {
            List<T> result = new List<T>();
            List<PropertyInfo> properties = typeof(T).GetProperties().Where(p => p.PropertyType == typeof(string)).ToList();
	        if (properties.Count == 0 || string.IsNullOrEmpty(param.sSearch)) return list;

            foreach (T t in list)
                foreach (PropertyInfo property in properties)
                {
                    string value = ((string) property.GetValue(t));
                    if(string.IsNullOrEmpty(value)) continue;
                    if (!value.ToUpper().Contains(param.sSearch.ToUpper())) continue;
                    result.Add(t);
                    break;
                }
            return result;
	    }

        public static List<T> SortList<T>(List<T> list, JQueryDataTableParam param)
        {
            IEnumerable<PropertyInfo> properties = typeof(T).GetProperties().Where(p => p.GetCustomAttributes(typeof(ColumnAttribute)).Count() == 1);
            PropertyInfo property = properties.FirstOrDefault(p => ((ColumnAttribute[]) p.GetCustomAttributes(typeof(ColumnAttribute)))[0].Column == param.iSortCol_0);
            if (property == null) return list;

            Type t = list[0].GetType();

            return param.sSortDir_0 == "asc" ?
                   list.OrderBy(u => t.InvokeMember(property.Name, BindingFlags.GetProperty, null, u, null)).ToList() :
                   list.OrderByDescending(u => t.InvokeMember(property.Name, BindingFlags.GetProperty, null, u, null)).ToList();
        }

	    public static List<T> PageList<T>(List<T> list, JQueryDataTableParam param)
        {
            return list.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
        }
    }
}