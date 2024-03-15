using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.zhong.UtilHelper
{
    public class ConfigHelper
    {
        public static string GetConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 获取配置参数值，T仅限基础数据类型：int,long,bool,datetime,string,否则统一返回string类型
        /// </summary>
        public static T GetConfigValue<T>(string key, T defalutValue)
        {
            var configVal = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(configVal))
            {
                return defalutValue;
            }
            if (typeof(T) == typeof(int))
            {
                int result;
                return int.TryParse(configVal.ToString(), out result) ? (T)(object)result : defalutValue;
            }
            if (typeof(T) == typeof(double))
            {
                double result;
                return double.TryParse(configVal.ToString(), out result) ? (T)(object)result : defalutValue;
            }
            if (typeof(T) == typeof(bool))
            {
                bool result;
                return bool.TryParse(configVal.ToString(), out result) ? (T)(object)result : defalutValue;
            }
            if (typeof(T) == typeof(decimal))
            {
                decimal result;
                return decimal.TryParse(configVal.ToString(), out result) ? (T)(object)result : defalutValue;
            }
            if (typeof(T) == typeof(DateTime))
            {
                DateTime result;
                return DateTime.TryParse(configVal.ToString(), out result) ? (T)(object)result : defalutValue;
            }
            if (typeof(T) == typeof(string))
            {
                return (T)(object)configVal.ToString();
            }
            return defalutValue;
        }

        public static void SetConfigValue(string key, string value)
        {
            ConfigurationManager.AppSettings.Set(key, value);
        }
        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

    }
}
