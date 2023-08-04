using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Tools.zhong.UtilHelper
{
    //DES
    public class URLUtil
    {
        /// <summary>
        /// URL加密
        /// </summary>
        public static string Encrypt(string EncryptString)
        {
            if (string.IsNullOrEmpty(EncryptString))
            {
                return string.Empty;
            }
            //var result = System.Web.HttpUtility.UrlEncode(EncryptString);
            var result = Uri.EscapeUriString(EncryptString);
            return result?.Replace("%3a", ":/");
        }

        /// <summary>
        /// URL解密
        /// </summary>
        public static string Decrypt(string DecryptString)
        {
            if (string.IsNullOrEmpty(DecryptString))
            {
                return string.Empty;
            }
            return System.Web.HttpUtility.UrlDecode(DecryptString);
        }
    }
}
