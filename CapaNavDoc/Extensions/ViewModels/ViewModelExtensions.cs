using System;
using System.Linq;
using System.Reflection;

namespace CapaNavDoc.Extensions.ViewModels
{
    public static class ViewModelExtensions
    {
        public static T ToModel<T>(this object model, string editionMode = null) where T : new()
        {
            PropertyInfo[] modelProperties = model.GetType().GetProperties();
            PropertyInfo[] tProperties = typeof(T).GetProperties();
            T t = new T();

            foreach (PropertyInfo tProperty in tProperties)
            {
                PropertyInfo modelProperty = modelProperties.FirstOrDefault(p => p.Name == tProperty.Name);
                if (modelProperty == null) continue;
                tProperty.SetValue(t, ConvertValue(modelProperty.PropertyType, tProperty.PropertyType, modelProperty.GetValue(model)));
            }
            if (string.IsNullOrEmpty(editionMode)) return t;

            PropertyInfo tEmProperty = tProperties.FirstOrDefault(p => p.Name == "EditionMode");
            tEmProperty?.SetValue(t, editionMode);
            return t;
        }



        private static object ConvertValue(Type srcType, Type destType, object src)
        {
            if (srcType == typeof(string) && destType == typeof(string)) return (string)src;
            if (srcType == typeof(string) && destType == typeof(int)) return ((string)src).ToInt32();
            if (srcType == typeof(int) && destType == typeof(string)) return ((int) src).ToString();
            if (srcType == typeof(bool) && destType == typeof(bool)) return (bool) src;
            return null;
        }
    }
}