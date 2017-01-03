using System;

namespace CapaNavDoc.Classes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public int Column { get; set; }
    }
}