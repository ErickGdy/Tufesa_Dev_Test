using System;
using System.Collections.Generic;
using System.Text;

namespace Tufesa_Dev_Test.Data.Config
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class EnumHelper : Attribute
    {
        public Type EnumType;
        public EnumHelper(Type enumType)
        {
            EnumType = enumType;
        }
    }
}
