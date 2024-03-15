using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tools.zhong.UtilHelper
{
    public static class EnumUtil
    {
        public static string GetDisplayName(this Enum enumVal)
        {
            Type enumType = enumVal.GetType();
            string enumName = Enum.GetName(enumType, enumVal);
            string displayName = enumName;
            try
            {
                MemberInfo member = enumType.GetMember(enumName)[0];
                object attributes = member.GetCustomAttribute(typeof(DisplayAttribute), false);
                DisplayAttribute attribute = (DisplayAttribute)attributes;
                displayName = attribute.Name;
                if (attribute.ResourceType != null)
                {
                    displayName = attribute.GetName();
                }
            }
            catch { }
            return displayName;
        }

        public static object ToEnumByDisplayName(string displayName, Type enumType)
        {
            if (string.IsNullOrWhiteSpace(displayName) || !enumType.IsEnum)
            {
                return null;
            }
            object enumVal = null;
            try
            {
                string[] enumNames = Enum.GetNames(enumType);
                var type = enumType.UnderlyingSystemType;
                foreach (string item in enumNames)
                {
                    MemberInfo member = type.GetMember(item)[0];
                    var dispAttr = member.GetCustomAttribute(typeof(DisplayAttribute), false) as DisplayAttribute;
                    if (dispAttr != null && dispAttr.Name.EqualToIgnoreCase(displayName))
                    {
                        return Enum.Parse(type, item);
                    }
                }
            }
            catch
            {
                try { enumVal = Enum.ToObject(enumType.GetType(), displayName); } catch { }
            };
            return enumVal;
        }

        /// <summary>
        /// 转换为枚举对象
        /// </summary>
        public static T ToEnum<T>(object value, bool ignoreCase = true) where T : struct
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return default(T);
            }
            T returnObj = default(T);
            if (Enum.TryParse(value.ToString(), ignoreCase, out returnObj))
            {
                return returnObj;
            }
            return default(T);
        }
    }
}
