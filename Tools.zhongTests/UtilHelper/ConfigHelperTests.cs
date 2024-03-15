using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.zhong.UtilHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.zhong.UtilHelper.Tests
{
    [TestClass()]
    public class ConfigHelperTests
    {
        [TestMethod()]
        public void GetConfigValueTest()
        {
            var i = ConfigHelper.GetConfigValue<int>("aaa", 20);
            var i2 = ConfigHelper.GetConfigValue<bool>("aaa", false);
            var i3 = ConfigHelper.GetConfigValue<DateTime>("aaa", DateTime.Now);
            var i4 = ConfigHelper.GetConfigValue<string>("aaa", "Y");
            var i5 = ConfigHelper.GetConfigValue<double>("aaa", 1.4);
            Assert.IsTrue(true);
        }
    }
}