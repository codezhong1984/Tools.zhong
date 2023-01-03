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
        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static void SetValue(string key,string value)
        {
            ConfigurationManager.AppSettings.Set(key, value);
        }
}
}
