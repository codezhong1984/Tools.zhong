﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.zhong.UtilHelper
{
    public class ConfigHelper
    {
        public string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
