using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace CapaNavDoc.Extensions
{
    public static class ModelExtensions
    {
        public static object ToModel(this object modelView, object model, string editionMode = null)
        {
            PropertyInfo[] modelViewProperties = modelView.GetType().GetProperties();
            PropertyInfo[] modelProperties = model.GetType().GetProperties();
            
            foreach (PropertyInfo modelProperty in modelProperties)
            {
                // Get the property from the model view matching the property from the model.
                PropertyInfo modelViewProperty = modelViewProperties.FirstOrDefault(p => p.Name == modelProperty.Name);

                // The model property doesn't exists in the model view.
                if (modelViewProperty == null)
                {
                    // Check if the model property is a DateTime struct.
                    if (modelProperty.PropertyType != typeof(DateTime))
                    {
                        Debug.WriteLine($"Model property [{modelProperty.Name}] of Model [{model.GetType().Name}] not found in Model View.");
                        continue;
                    }
                    // The model property is a DateTime struct. Checking if the property is null to initialize it if so.
                    DateTime date = modelProperty.GetValue(model) == null ? DateTime.MinValue : (DateTime) modelProperty.GetValue(model);
                    if (date == DateTime.MinValue) modelProperty.SetValue(model, SqlDateTime.MinValue.Value);
                }
                // The model property exists in the model view.
                else
                {
                    modelProperty.SetValue(model, ConvertValue(modelViewProperty.PropertyType, modelProperty.PropertyType, modelViewProperty.GetValue(modelView)));
                }
            }
            // If the model is not used for Edition end here.
            if (string.IsNullOrEmpty(editionMode)) return model;

            // If the model is used for edition, set the EditMode property.
            PropertyInfo editionModeProperty = modelProperties.FirstOrDefault(p => p.Name == "EditionMode");
            editionModeProperty?.SetValue(model, editionMode);
            return model;
        }

        private static object ConvertValue(Type srcType, Type destType, object src)
        {
            if (srcType == typeof(string) && destType == typeof(string)) return (string)src;
            if (srcType == typeof(bool) && destType == typeof(bool)) return (bool)src;
            if (srcType == typeof(int) && destType == typeof(int)) return (int)src;

            if (srcType == typeof(string) && destType == typeof(int)) return ((string)src).ToInt32();
            if (srcType == typeof(int) && destType == typeof(string)) return ((int) src).ToString();

            if (srcType == typeof(DateTime) && destType == typeof(string)) return ((DateTime) src).ToString("dd-mm-yyyy");
            if (srcType == typeof(string) && destType == typeof(DateTime)) return string.IsNullOrEmpty((string) src) ? SqlDateTime.MinValue.Value : DateTime.ParseExact((string) src, "dd-mm-yyyy", CultureInfo.InvariantCulture);

            if (srcType == typeof(string) && destType == typeof(bool)) return ((string) src) == "Oui";
            if (srcType == typeof(bool) && destType == typeof(string)) return ((bool) src) ? "Oui" : "Non";

            Debug.WriteLine($"Can't convert {srcType.Name} to {destType.Name}");

            return null;
        }
    }
}