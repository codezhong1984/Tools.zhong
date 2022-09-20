using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Tools.zhong.UtilHelper
{
    //DES
    public class DESUtil
    {
        private const string DefaultEncryptKey = "PTS20228";
        /// <summary>
        /// DES加密（数据加密标准，速度较快，适用于加密大量数据的场合）
        /// </summary>
        /// <param name="EncryptString">待加密的密文</param>
        /// <param name="EncryptKey">加密的密钥</param>
        /// <returns>returns</returns>
        public static string DESEncrypt(string EncryptString, string EncryptKey = DefaultEncryptKey)
        {
            if (string.IsNullOrEmpty(EncryptString))
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(EncryptKey))
            {
                throw (new Exception("密钥不得为空"));
            }
            if (EncryptKey.Length != 8)
            {
                throw (new Exception("密钥必须为8位"));
            }
            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            string m_strEncrypt = "";
            DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();
            try
            {
                byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);
                using (MemoryStream m_stream = new MemoryStream())
                {
                    using (CryptoStream m_cstream = new CryptoStream(m_stream,
                        m_DESProvider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV),
                        CryptoStreamMode.Write))
                    {
                        m_cstream.Write(m_btEncryptString, 0, m_btEncryptString.Length);
                        m_cstream.FlushFinalBlock();
                        m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());
                        m_cstream.Close();
                    }
                    m_stream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_DESProvider.Clear();
            }
            return m_strEncrypt;
        }

        /// <summary>
        /// DES解密（数据解标准，速度较快，适用于解密大量数据的场合）
        /// </summary>
        /// <param name="DecryptString">待解密的密文</param>
        /// <param name="DecryptKey">解密的密钥</param>
        /// <returns>returns</returns>
        public static string DESDecrypt(string DecryptString, string DecryptKey = DefaultEncryptKey)
        {
            if (string.IsNullOrEmpty(DecryptString))
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(DecryptKey))
            {
                throw (new Exception("密钥不得为空"));
            }
            if (DecryptKey.Length != 8)
            {
                throw (new Exception("密钥必须为8位"));
            }
            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            string m_strDecrypt = "";
            DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();
            try
            {
                byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);
                using (MemoryStream m_stream = new MemoryStream())
                {
                    using (CryptoStream m_cstream = new CryptoStream(m_stream,
                        m_DESProvider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV),
                        CryptoStreamMode.Write))
                    {
                        m_cstream.Write(m_btDecryptString, 0, m_btDecryptString.Length);
                        m_cstream.FlushFinalBlock();
                        m_strDecrypt = Convert.ToBase64String(m_stream.ToArray());
                        m_cstream.Close();
                    }
                    m_stream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_DESProvider.Clear();
            }
            return Base64Util.DecodeBase64(m_strDecrypt);
        }
    }
}
