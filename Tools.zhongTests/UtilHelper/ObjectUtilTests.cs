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
    public class ObjectUtilTests
    {
        [TestMethod()]
        public void IsEmptyCollectionTest()
        {
            List<string> list = null;
            var bl = list.IsEmptyCollection();
            
            list = new List<string>();
            bl = list.IsEmptyCollection();

            list.Add("123");
            bl = list.IsEmptyCollection();


            var obj = "zmb";
            var r = ObjectUtil.isBool(obj);
            obj = "True";
            var r2 = ObjectUtil.isBool(obj); 
            obj = null;
            var r3 = ObjectUtil.isBool(obj);

            Assert.IsTrue(true);
        }
    }
}