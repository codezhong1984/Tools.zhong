using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace Tools.zhong.UtilHelper
{
    //MD5
    public class MD5Util
    {
        public static string Encrypt(string inputText)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(inputText))
               .Select(x=>x.ToString("x2")));
        }
    }
}
