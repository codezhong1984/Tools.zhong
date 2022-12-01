using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Tools.zhong.UtilHelper
{
    public enum BaseDataType
    {
        String,
        Int16,
        Int32,
        Int64,
        UInt16,
        UInt32,
        UInt64,
        Float,
        Double,
        Decimal,
        Bool,
        DateTime,
        Byte,
        Char,
        Guid,
        Single,
        SByte,
        TimeSpan
    }

    public class ObjectUtil
    {
        #region Object Convert Util

        /// <summary>
        /// 转换为字符串，NULL则转换成String.Empty
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string NullToEmpty(object obj)
        {
            if (IsNull(obj))
            {
                return string.Empty;
            }
            return obj.ToString();
        }

        #region 判断类型
        public static bool isDateTime(object obj)
        {
            DateTime returnValue;
            return !IsNullOrEmpty(obj) && DateTime.TryParse(obj.ToString(), out returnValue);
        }
        public static bool isBool(object obj)
        {
            bool returnValue;
            return !IsNullOrEmpty(obj) && bool.TryParse(obj.ToString(), out returnValue);
        }

        public static bool isNumberic(object obj)
        {
            double dtReturn;
            return !IsNullOrEmpty(obj) && double.TryParse(obj.ToString(), out dtReturn);
        }

        /// <summary>
        /// Check Is Specify DataType
        /// </summary>
        /// <returns></returns>
        public static bool IsTypeOf(object obj, BaseDataType dataType)
        {
            if (obj == null)
            {
                return false;
            }
            try
            {
                string objVal = obj.ToString().Trim();
                switch (dataType)
                {
                    #region Parse

                    case BaseDataType.Int16:
                        Int16.Parse(objVal);
                        break;
                    case BaseDataType.Int32:
                        Int32.Parse(objVal);
                        break;
                    case BaseDataType.Int64:
                        Int64.Parse(objVal);
                        break;
                    case BaseDataType.UInt16:
                        UInt16.Parse(objVal);
                        break;
                    case BaseDataType.UInt32:
                        UInt32.Parse(objVal);
                        break;
                    case BaseDataType.UInt64:
                        UInt64.Parse(objVal);
                        break;
                    case BaseDataType.Float:
                        float.Parse(objVal);
                        break;
                    case BaseDataType.Double:
                        double.Parse(objVal);
                        break;
                    case BaseDataType.Decimal:
                        decimal.Parse(objVal);
                        break;
                    case BaseDataType.Bool:
                        bool.Parse(objVal);
                        break;
                    case BaseDataType.DateTime:
                        DateTime.Parse(objVal);
                        break;
                    //case BaseDataType.String:
                    //    break;
                    case BaseDataType.Byte:
                        byte.Parse(objVal);
                        break;
                    case BaseDataType.Char:
                        char.Parse(objVal);
                        break;
                    //case BaseDataType.Guid:                        
                    //    break;
                    case BaseDataType.Single:
                        Single.Parse(objVal);
                        break;
                    case BaseDataType.SByte:
                        SByte.Parse(objVal);
                        break;
                    case BaseDataType.TimeSpan:
                        TimeSpan.Parse(objVal);
                        break;
                    default:
                        break;

                        #endregion
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<long> ConvertToLongBatch(List<string> idList)
        {
            if (idList == null)
            {
                return null;
            }
            List<long> list = new List<long>();
            foreach (string item in idList)
            {
                long? val = ToLong(item);
                if (val.HasValue)
                {
                    list.Add(val.Value);
                }
            }
            return list;
        }


        #endregion

        #region 类型转换

        /// <summary>
        /// 转换对象为日期
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            try
            {
                if (obj == null)
                {
                    return defaultValue;
                }
                else
                {
                    DateTime dt = defaultValue;
                    DateTime.TryParse(obj.ToString().Trim(), out dt);
                    return dt;
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 转换对象为日期；如果无法转换，则返回最小日期值(MinValue)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(object obj)
        {
            DateTime result = DateTime.MinValue;
            if (obj == null) return null;

            if (DateTime.TryParse(obj.ToString().Trim(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static int ToInt(object obj, int defaultValue)
        {
            try
            {
                if (obj == null)
                {
                    return defaultValue;
                }
                else
                {
                    int.TryParse(obj.ToString().Trim(), out defaultValue);
                    return defaultValue;
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public static int? ToInt(object obj)
        {
            int result = 0;
            if (obj == null) return null;

            if (int.TryParse(obj.ToString().Trim(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static object ToDBValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }

        public static int ToShort(object obj, short defaultValue)
        {
            try
            {
                if (obj == null)
                {
                    return defaultValue;
                }
                else
                {
                    short.TryParse(obj.ToString().Trim(), out defaultValue);
                    return defaultValue;
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public static int? ToShort(object obj)
        {
            short result = 0;
            if (obj == null) return null;

            if (short.TryParse(obj.ToString().Trim(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static long ToLong(object obj, long defaultValue)
        {
            try
            {
                if (obj == null)
                {
                    return defaultValue;
                }
                else
                {
                    long.TryParse(obj.ToString().Trim(), out defaultValue);
                    return defaultValue;
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public static long? ToLong(object obj)
        {
            long result = 0;
            if (obj == null) return null;

            if (long.TryParse(obj.ToString().Trim(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static byte ToByte(object obj, byte defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            else
            {
                byte.TryParse(obj.ToString().Trim(), out defaultValue);
                return defaultValue;
            }
        }

        public static byte? ToByte(object obj)
        {
            byte result = 0;
            if (obj == null) return null;

            if (byte.TryParse(obj.ToString().Trim(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static double ToDouble(object obj, double defaultValue)
        {
            try
            {
                if (obj == null)
                {
                    return defaultValue;
                }
                else
                {
                    double.TryParse(obj.ToString().Trim(), out defaultValue);
                    return defaultValue;
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public static double? ToDouble(object obj)
        {
            double result = 0;
            if (obj == null) return null;

            if (double.TryParse(obj.ToString().Trim(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static decimal ToDecimal(object obj, decimal defaultValue)
        {
            try
            {
                if (obj == null)
                {
                    return defaultValue;
                }
                else
                {
                    decimal.TryParse(obj.ToString().Trim(), out defaultValue);
                    return defaultValue;
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public static decimal? ToDecimal(object obj)
        {
            decimal result = 0;
            if (obj == null) return null;

            if (decimal.TryParse(obj.ToString().Trim(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static bool ToBool(object obj, bool defaultValue)
        {
            try
            {
                if (obj == null)
                {
                    return defaultValue;
                }
                else
                {
                    bool.TryParse(obj.ToString().Trim(), out defaultValue);
                    return defaultValue;
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool? ToBool(object obj)
        {
            bool result = false;
            if (obj == null) return null;

            if (bool.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region 判断对象是否为空

        /// <summary>
        ///判断是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// 判断是否为DBNull
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsDbNull(object obj)
        {
            return obj == DBNull.Value;
        }

        public static bool IsNullOrDBNull(object obj)
        {
            return IsNull(obj) || IsDbNull(obj);
        }

        /// <summary>
        /// 增加去空格处理判断是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(object obj)
        {
            return IsNull(obj) || obj.ToString().Trim().Length == 0;
        }

        #region 判断集合或数组    

        /// <summary>
        /// 判断集合是否为空,适用于List,HashTable,HashSet,IDictionary
        /// 集合类型包括IList,ArrayList,Hashtable,HashSet,LinkedList,IDictionary
        /// </summary>
        public static bool IsEmpty(ICollection ienum)
        {
            return ienum == null || ienum.Count == 0;
        }

        /// <summary>
        /// 判断集合是否为空,如果不是集合也返回true
        /// 集合类型包括IList,ArrayList,Hashtable,HashSet,LinkedList,IDictionary
        /// </summary>
        public static bool IsEmpty(Array obj)
        {
            return obj == null || (obj is Array && ((Array)obj).Length == 0);
        }
        #endregion

        #endregion


        /// <summary>
        /// 转换日期格式为指定格式的字符
        /// </summary>
        public static string FormatDateTime(object obj, string formatString)
        {
            DateTime dt;
            if (obj == null || !DateTime.TryParse(obj.ToString(), out dt))
            {
                return string.Empty;
            }
            else
            {
                return dt.ToString(formatString);
            }
        }

        #endregion
        /// <summary>
        /// 把字符串批量转为int数组，其中有非数字字符串转换失败
        /// </summary>
        /// <param name="strVals"></param>
        /// <param name="intValues"></param>
        /// <returns></returns>
        public static bool ConvertToIntBatch(string value, out List<int> intValues, string splictString = ",")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                intValues = new List<int>();
                return true;
            }
            string[] listVal = value.SplitNoEmpty(splictString);
            if (IsEmpty(listVal))
            {
                intValues = new List<int>();
                return true;
            }
            List<int> list = new List<int>();
            bool flag = true;
            foreach (string item in listVal)
            {
                int i = 0;
                if (!int.TryParse(item, out i))
                {
                    flag = false;
                }
                else
                {
                    list.Add(i);
                }
            }
            intValues = list;
            return flag;
        }

        /// <summary>
        /// 把字符串批量转为int数组，其中有非数字字符串转换失败
        /// </summary>
        /// <param name="strVals"></param>
        /// <param name="intValues"></param>
        /// <returns></returns>
        public static bool ConvertToIntBatch(List<string> strVals, out List<int> intValues)
        {
            if (IsEmpty(strVals))
            {
                intValues = null;
                return true;
            }
            List<int> list = new List<int>();
            foreach (string item in strVals)
            {
                int i = 0;
                if (!int.TryParse(item, out i))
                {
                    intValues = null;
                    return false;
                }
                list.Add(i);
            }
            intValues = list;
            return true;
        }

        /// <summary>
        /// 把字符串批量转为int数组，其中有非数字字符串转换失败
        /// </summary>
        /// <param name="strVals"></param>
        /// <param name="intValues"></param>
        /// <returns></returns>
        public static bool ConvertToIntBatch(string[] strVals, out int[] intValues)
        {
            List<int> intList = new List<int>();
            bool fl = ConvertToIntBatch(strVals?.ToList(), out intList);
            intValues = intList.ToArray();
            return fl;
        }

        /// <summary>
        /// 把字符串批量转为int数组，其中有非数字字符串转换失败
        /// </summary>
        /// <param name="strVals"></param>
        /// <param name="intValues"></param>
        /// <returns></returns>
        public static bool ConvertToIntBatch(List<string> strVals, out int[] intValues)
        {
            List<int> intList = new List<int>();
            bool fl = ConvertToIntBatch(strVals, out intList);
            intValues = intList.ToArray();
            return fl;
        }
        /// <summary>
        /// 把字符串批量转为int数组，其中有非数字字符串转换失败
        /// </summary>
        /// <param name="strVals"></param>
        /// <param name="intValues"></param>
        /// <returns></returns>
        public static bool ConvertToIntBatch(string[] strVals, out List<int> intValues)
        {
            return ConvertToIntBatch(strVals?.ToList(), out intValues);
        }

        /// <summary>
        /// 从动态对象中获取属性值 
        /// </summary>
        public static T2 GetObjectFromDymtic<T1, T2>(T1 dynObj, string propertyName)
        {
            return (T2)dynObj.GetType().GetProperty(propertyName)?.GetValue(dynObj);
        }

        /// <summary>
        /// 通用类型转换方法
        /// </summary>
        public static object ChangeType(object value, Type type)
        {
            if (value != null && value.ToString() == "" && type.Name != "String" )
            {
                value = null;
            }
            if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
            if (value == null) return null;
            if (type == value.GetType()) return value;
            if (type.IsEnum)
            {
                if (value is string)
                    return Enum.Parse(type, value as string);
                else
                    return Enum.ToObject(type, value);
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                Type innerType = type.GetGenericArguments()[0];
                object innerValue = ChangeType(value, innerType);
                return Activator.CreateInstance(type, new object[] { innerValue });
            }
            if (value is string && type == typeof(Guid)) return new Guid(value as string);
            if (value is string && type == typeof(Version)) return new Version(value as string);
            if (!(value is IConvertible)) return value;
            return Convert.ChangeType(value, type);
        }
    }
}
