using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Collections;
using System.Linq.Expressions;

namespace Tools.zhong.UtilHelper
{
    public static class ReflectUtil //<T> where T : new() 表示T对象必须有个无参构造函数
    {
        /// <summary>
        /// convert to enum , ignoreCase
        /// </summary>
        public static T ToEnum<T>(this string enumStr)
        {
            try
            {
                if (ObjectUtil.IsNullOrEmpty(enumStr))
                {
                    return default(T);
                }
                return (T)Enum.Parse(typeof(T), enumStr, true);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// convert to enum ,do not ignoreCase
        /// </summary>
        public static T ToEnum<T>(this object obj)
        {
            try
            {
                return (T)Enum.ToObject(typeof(T), obj);
            }
            catch
            {
                return default(T);
            }
        }

        public static string EnumToString<T>(T enumValue)
        {
            try
            {
                return Enum.GetName(typeof(T), enumValue);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// validate value is between range
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <param name="includeLowerBound"></param>
        /// <param name="includeUpperBound"></param>
        /// <returns></returns>
        public static bool IsBetween<T>(this T t, T lowerBound, T upperBound,
            bool includeLowerBound = false, bool includeUpperBound = false) where T : IComparable<T>
        {
            var lowerCommpareResult = t.CompareTo(lowerBound);
            var upperCommpareResult = t.CompareTo(upperBound);
            return (includeLowerBound && lowerCommpareResult == 0)
                || (includeUpperBound && upperCommpareResult == 0)
                || (lowerCommpareResult > 0 && upperCommpareResult < 0);
        }

        /// <summary>
        /// validate value is between range
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <param name="compareer">cannot be null</param>
        /// <param name="includeLowerBound"></param>
        /// <param name="includeUpperBound"></param>
        /// <returns></returns>
        public static bool IsBetween<T>(this T t, T lowerBound, T upperBound, IComparer<T> compareer,
            bool includeLowerBound = false, bool includeUpperBound = false) where T : IComparable<T>
        {
            if (compareer == null)
            {
                throw new ArgumentNullException("compareer cannot be null!");
            }
            var lowerCommpareResult = compareer.Compare(t, lowerBound);
            var upperCommpareResult = compareer.Compare(t, upperBound);
            return (includeLowerBound && lowerCommpareResult == 0)
                || (includeUpperBound && upperCommpareResult == 0)
                || (lowerCommpareResult > 0 && upperCommpareResult < 0);
        }

        public static IList<T> ToModelList<T>(this DataTable dt) where T : new()
        {
            IList<T> returnList = new List<T>();
            if (dt == null && dt.Rows.Count == 0)
            {
                return returnList;
            }
            var type = typeof(T);
            string propertyName = string.Empty;

            foreach (DataRow dtRow in dt.Rows)
            {
                T modelObj = new T();
                PropertyInfo[] properties = modelObj.GetType().GetProperties();
                foreach (var propertyItem in properties)
                {
                    propertyName = propertyItem.Name;
                    if (dt.Columns.Contains(propertyName))
                    {
                        var drVal = dtRow[propertyName];
                        SetObjectPropertyValue<T>(modelObj, propertyItem, drVal);
                    }
                }
                returnList.Add(modelObj);
            }
            return returnList;
        }

        public static DataTable ToDataTable<T>(this IList<T> varlist)
        {
            var dtReturn = new DataTable();
            // column names 
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;
            foreach (var rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = rec.GetType().GetProperties();
                    foreach (var pi in oProps)
                    {
                        var colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                var dr = dtReturn.NewRow();
                foreach (var pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value;
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        /// <summary>
        /// 设置对象的属性值 
        /// </summary>
        public static void SetObjectPropertyValue<T>(T modelObj, PropertyInfo propertyInfo, object value) where T : new()
        {
            if (propertyInfo == null || !propertyInfo.CanWrite)
            {
                return;
            }
            try
            {
                string propertyTypeName = propertyInfo.PropertyType.FullName;
                if (!ObjectUtil.IsNullOrEmpty(value))
                {
                    #region SetValue

                    if (propertyTypeName.ContainsIgnoreCase("System.String"))
                    {
                        propertyInfo.SetValue(modelObj, Convert.ToString(value).Trim(), null);
                    }
                    else if (propertyTypeName.ContainsIgnoreCase("System.Int16"))
                    {
                        propertyInfo.SetValue(modelObj, Convert.ToInt16(value), null);
                    }
                    else if (propertyTypeName.ContainsIgnoreCase("System.Int32"))
                    {
                        propertyInfo.SetValue(modelObj, Convert.ToInt32(value), null);
                    }
                    else if (propertyTypeName.ContainsIgnoreCase("System.Int64"))
                    {
                        propertyInfo.SetValue(modelObj, Convert.ToInt64(value), null);
                    }
                    else if (propertyTypeName.ContainsIgnoreCase("System.UInt16"))
                    {
                        propertyInfo.SetValue(modelObj, Convert.ToUInt16(value), null);
                    }
                    else if (propertyTypeName.ContainsIgnoreCase("System.UInt32"))
                    {
                        propertyInfo.SetValue(modelObj, Convert.ToUInt32(value), null);
                    }
                    else if (propertyTypeName.ContainsIgnoreCase("System.UInt64"))
                    {
                        propertyInfo.SetValue(modelObj, Convert.ToUInt64(value), null);
                    }
                    else if (propertyTypeName.ContainsIgnoreCase("System.Double"))
                    {
                        propertyInfo.SetValue(modelObj, Convert.ToDouble(value), null);
                    }
                    else if (propertyTypeName.ContainsIgnoreCase("System.Decimal"))
                    {
                        propertyInfo.SetValue(modelObj, Convert.ToDecimal(value), null);
                    }
                    else if (propertyTypeName.ContainsIgnoreCase("System.Char"))
                    {
                        propertyInfo.SetValue(modelObj, Convert.ToChar(value), null);
                    }
                    else if (propertyTypeName.ContainsIgnoreCase("System.Boolean"))
                    {
                        propertyInfo.SetValue(modelObj, Convert.ToBoolean(value), null);
                    }
                    else if (propertyTypeName.ContainsIgnoreCase("System.DateTime"))
                    {
                        propertyInfo.SetValue(modelObj, Convert.ToDateTime(value), null);
                    }
                    else if (propertyTypeName.ContainsIgnoreCase("System.Single"))
                    {
                        propertyInfo.SetValue(modelObj, Convert.ToSingle(value), null);
                    }
                    else if (propertyTypeName.ContainsIgnoreCase("System.Byte"))
                    {
                        propertyInfo.SetValue(modelObj, Convert.ToByte(value), null);
                    }
                    else if (propertyTypeName.ContainsIgnoreCase("System.SByte"))
                    {
                        propertyInfo.SetValue(modelObj, Convert.ToSByte(value), null);
                    }
                    else if (propertyTypeName.ContainsIgnoreCase("System.Guid"))
                    {
                        string valString = value.ToString().Trim();
                        propertyInfo.SetValue(modelObj, new Guid(valString), null);
                    }
                    else if (propertyInfo.PropertyType.IsEnum)
                    {
                        object enumName = Enum.ToObject(propertyInfo.PropertyType, value);
                        propertyInfo.SetValue(modelObj, enumName, null); //获取枚举值，设置属性值
                    }
                    #endregion
                }
                else
                {
                    #region Null值时不做赋值处理
                    //if (propertyTypeName.StartsWith("System.Nullable"))
                    //{
                    //    propertyInfo.SetValue(modelObj, null, null);
                    //}
                    //else if (propertyTypeName == "System.Int16"
                    //    || propertyTypeName == "System.Int32"
                    //    || propertyTypeName == "System.Int64"
                    //    || propertyTypeName == "System.UInt16"
                    //    || propertyTypeName == "System.UInt32"
                    //    || propertyTypeName == "System.UInt64"
                    //    || propertyTypeName == "System.Decimal"
                    //    || propertyTypeName == "System.Double"
                    //    || propertyTypeName == "System.Single")
                    //{
                    //    propertyInfo.SetValue(modelObj, 0, null);
                    //}                   
                    //else if (propertyTypeName == "System.Boolean")
                    //{
                    //    propertyInfo.SetValue(modelObj, false, null);
                    //}
                    //else if (propertyTypeName == "System.DateTime")
                    //{
                    //    //若是不能为空的DateTime类型，则默认为最小日期
                    //    propertyInfo.SetValue(modelObj, DateTime.MinValue, null);
                    //}
                    ////可空类型
                    //else if (propertyTypeName.StartsWith("System.Nullable`1[["))
                    //{
                    //    propertyInfo.SetValue(modelObj, null, null);
                    //}
                    //else
                    //{
                    //    propertyInfo.SetValue(modelObj, null, null);
                    //}
                    #endregion
                }
            }
            catch
            {
                //propertyInfo.SetValue(modelObj, null, null);
            }
        }

        /// <summary>
        /// 设置对象的属性值 
        /// </summary>
        public static void SetObjectPropertyToNull<T>(T modelObj, string propertyName) where T : new()
        {
            PropertyInfo propertyInfo = GetPropertyInfo<T>(modelObj, propertyName);
            propertyInfo.SetValue(modelObj, null);
        }

        /// <summary>
        /// 设置对象的属性值 
        /// </summary>
        public static void SetObjectPropertyValue<T>(T modelObj, string propertyName, object value) where T : new()
        {
            PropertyInfo propertyInfo = GetPropertyInfo<T>(modelObj, propertyName);
            SetObjectPropertyValue<T>(modelObj, propertyInfo, value);
        }

        /// <summary>
        /// 根据属性名称取得对象的属性值
        /// </summary>
        public static object GetValueByPropertyName<T>(T modelObj, string propertyName)
        {
            var piItem = GetPropertyInfo<T>(modelObj, propertyName);
            if (piItem != null)
            {
                return piItem.GetValue(modelObj, null);
            }
            return null;
        }

        /// <summary>
        /// 获取对象属性
        /// </summary>
        public static PropertyInfo GetPropertyInfo<T>(T modelObj, string propertyName)
        {
            PropertyInfo[] properties = modelObj.GetType().GetProperties();
            var piItem = properties.FirstOrDefault<PropertyInfo>(i => i.Name.EqualToIgnoreCase(propertyName));
            return piItem;
        }

        /// <summary>
        /// 通用（调用对象方法前先new一遍对象，故对象的状态无法保留；无用有无参构造函数，并调用无参方法），
        /// </summary>
        public static void InvokeMethod<T>(string methodName, object[] param = null) where T : new()
        {
            T instance = new T();
            MethodInfo method = typeof(T).GetMethod(methodName);
            method.Invoke(instance, param);
        }

        /// <summary>
        /// 调用一个具体实例对象的方法，会保留对象状态
        /// </summary>
        public static void InvokeMethod(object o, string methodName, object[] param = null)
        {
            o.GetType().GetMethod(methodName).Invoke(o, param);
        }

        /// <summary>
        /// 根据对象名返回类实例
        /// </summary>
        /// <param name="parObjectName">对象名称</param>
        /// <returns>对象实例（可强制转换为对象实例）</returns>
        public static object GetObjectByObjectName(string parObjectName)
        {
            Type t = Type.GetType(parObjectName); //找到对象
            return System.Activator.CreateInstance(t); //实例化对象
        }

        #region 枚举转list对象

        public static List<T> EnumToList<T>()
        {
            var enumType = typeof(T);

            // Can't use type constraints on value types, so have to do check like this
            if (enumType.BaseType != typeof(Enum))
            {
                return null;
                //throw new ArgumentException("T must be of type System.Enum");
            }

            var enumValArray = Enum.GetValues(enumType);

            var enumValList = new List<T>(enumValArray.Length);
            enumValList.AddRange(from int val in enumValArray select (T)Enum.Parse(enumType, val.ToString()));
            return enumValList;
        }

        #endregion

        #region 枚举转字典对象

        public static IDictionary<string, int> EnumToDictionary(this Type t)
        {
            if (t == null) return null;
            if (!t.IsEnum) return null;

            var names = Enum.GetNames(t);
            var values = Enum.GetValues(t);

            return (from i in Enumerable.Range(0, names.Length)
                    select new { Key = names[i], Value = (int)values.GetValue(i) })
                .ToDictionary(k => k.Key, k => k.Value);
        }

        #endregion

        #region 序列化反序列化xml

        /// <summary>
        /// 序列化成UTF8 格式的无命名空间的xml
        /// <param name="prefix"></param>
        /// <param name="ns">namespace</param>
        /// <param name="xmlElement">Attribute must be defined with [XmlElement]</param>
        /// </summary>
        public static string ToXml<T>(this T xmlElement, string prefix = "", string xmlns = "") where T : new()
        {
            var str = string.Empty;
            using (var stream = new MemoryStream())
            {
                var xml = new XmlSerializer(typeof(T));
                var xwriter = new XmlTextWriter(stream, Encoding.UTF8);
                //Create our own namespaces for the output
                var ns = new XmlSerializerNamespaces();
                //Add an empty namespace and empty value
                ns.Add(prefix, xmlns);
                try
                {
                    //序列化对象
                    xml.Serialize(xwriter, xmlElement, ns);
                }
                catch (InvalidOperationException ex)
                {
                    throw ex;
                }
                stream.Position = 0;
                using (var sr = new StreamReader(stream))
                {
                    str = sr.ReadToEnd();
                }
            }
            return str;
        }

        /// <summary>
        /// 反序列成对象
        /// <param name="T">Attribute must be defined with [XmlElement]</param>
        /// </summary>
        public static T FromXml<T>(this string xml) where T : new()
        {
            using (var sr = new StringReader(xml))
            {
                var xmldes = new XmlSerializer(typeof(T));
                return (T)xmldes.Deserialize(sr);
            }
        }

        public static T Deserialize<T>(this XDocument xmlDocument)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var reader = xmlDocument.CreateReader())
                return (T)xmlSerializer.Deserialize(reader);
        }

        #endregion 序列化反序列化xml

        #region 其他常用集合方法
        /// <summary>
        /// for eache action collection
        /// </summary>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static void ForEach(this IEnumerable source, Action action)
        {
            foreach (var item in source)
            {
                action();
            }
        }

        /// <summary>
        /// t if in a collection
        /// </summary>
        public static bool In<T>(this T t, params T[] c)
        {
            return c.Contains(t);
        }

        /// <summary>
        /// Alternates the specified first.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns></returns>
        public static IEnumerable<TSource> Alternate<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            using (IEnumerator<TSource> e1 = first.GetEnumerator())
            {
                using (IEnumerator<TSource> e2 = second.GetEnumerator())
                {
                    while (e1.MoveNext() && e2.MoveNext())
                    {
                        yield return e1.Current;
                        yield return e2.Current;
                    }
                }
            }
        }

        /// <summary>
        /// Appends the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="element">The element.</param>
        /// <returns>IEnumerable<TSource></returns>
        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource element)
        {
            using (IEnumerator<TSource> e1 = source.GetEnumerator())
            {
                while (e1.MoveNext())
                {
                    yield return e1.Current;
                }
            }
            yield return element;
        }

        /// <summary>
        /// Determines whether [contains] [the specified source].
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="value">The value.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>
        ///     <c>true</c> if [contains] [the specified source]; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains<TSource, TResult>(
            this IEnumerable<TSource> source, TResult value, Func<TSource, TResult> selector)
        {
            foreach (TSource sourceItem in source)
            {
                TResult sourceValue = selector(sourceItem);
                if (sourceValue.Equals(value))
                    return true;
            }
            return false;
        }

        //   /// <summary>
        //   /// Distincts the specified source.
        //   /// </summary>
        //   /// <typeparam name="TSource">The type of the source.</typeparam>
        //   /// <typeparam name="TResult">The type of the result.</typeparam>
        //   /// <param name="source">The source.</param>
        //   /// <param name="comparer">The comparer.</param>
        //   /// <returns>IEnumerable<TSource></returns>
        //   public static IEnumerable<TSource> Distinct<TSource, TResult>(
        //this IEnumerable<TSource> source, Func<TSource, TResult> comparer)
        //   {
        //       return source.Distinct(new DynamicComparer<TSource, TResult>(comparer));
        //   }

        #endregion

        /// <summary>
        /// 转换成指定类型的对象
        /// </summary>
        public static T To<T>(IConvertible value)
        {
            try
            {
                var t = typeof(T);
                var u = Nullable.GetUnderlyingType(t);

                if (u != null)
                {
                    if (value == null || value.Equals(""))
                        return default(T);

                    return (T)Convert.ChangeType(value, u);
                }
                if (value == null || value.Equals(""))
                    return default(T);

                return (T)Convert.ChangeType(value, t);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 判断是否在范围内
        /// </summary>
        public static bool Between<T>(T me, T lower, T upper) where T : IComparable<T>
        {
            return me.CompareTo(lower) >= 0 && me.CompareTo(upper) < 0;
        }


        #region 集合排序
        /// <summary>
        /// 集合排序
        /// </summary>
        public static IList<T> OrderByFieldName<T>(this IList<T> source, string propertyName, bool isDescending)
        {
            var list = new List<T>();
            if (isDescending)
            {
                list = source.OrderByDescending(c => c.GetType().GetProperty(propertyName).GetValue(c, null))?.ToList();
            }
            else
            {
                list = source.OrderBy(c => c.GetType().GetProperty(propertyName).GetValue(c, null))?.ToList();
            }
            return list;
        }
        #endregion

    }
}
