using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace Tools.zhong.UtilHelper
{
    public static class StringUtil
    {
        /// <summary>
        /// 判断是否为null、空、去空格后为空。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool isBlank(this string value)
        {
            return string.IsNullOrEmpty(value) || value.ToString().Trim().Length == 0;
        }

        /// <summary>
        /// 是否是数字格式的字符串，可包含正负号(+-)、数字、小数点构成
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static bool isNumeric(this string stringValue)
        {
            Regex regex = new Regex(@"^[+-]?\d+[.]?\d*$");
            return regex.IsMatch(stringValue);
        }

        /// <summary>
        /// 是否为纯数字字符串
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static bool isNumericOnly(this string stringValue)
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(stringValue);
        }

        /// <summary>
        /// 是否由数字或字母构成的字符串
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static bool isNumericOrLetters(this string stringValue)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]+$");
            return regex.IsMatch(stringValue);
        }

        /// <summary>
        /// 比较两个字符串在忽略大小写的情况下是否相等
        /// </summary>
        public static bool EqualToIgnoreCase(this string value, string compareTo)
        {
            return string.Compare(value, compareTo, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        /// 忽略大小写的情况下,检查字符串是否某个字符串值
        /// </summary>
        public static bool ContainsIgnoreCase(this string value, string containString)
        {
            if (ObjectUtil.IsNull(value) || ObjectUtil.IsNull(value))
            {
                return false;
            }
            Regex regex = new Regex(containString, RegexOptions.IgnoreCase);
            return regex.IsMatch(value);
        }

        /// <summary>
        /// value做为拼接符拼接字符串数组
        /// </summary>
        public static string ConcatArrayString(this string value, params string[] arrString)
        {
            if (string.IsNullOrEmpty(value) || arrString == null || arrString.Length == 0)
            {
                return string.Empty;
            }
            return string.Join(value, arrString);
        }

        /// <summary>
        /// 用于截取字符串,长度不够返回本身,起始索引超出长度则返回空字符串.
        /// </summary>
        public static string Substring(this string value, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(value) || startIndex > value.Length - 1)
            {
                return string.Empty;
            }
            startIndex = startIndex < 0 ? 0 : startIndex;
            return length >= value.Length ? value.Substring(startIndex) : value.Substring(startIndex, length);
        }

        /// <summary>
        /// 用于截取到索引字符串的字符串,举例如下：
        /// SubstringIndex("1001#1002#1003#1004","#",2)返回"1002,1003"
        /// SustringIndex("str1;#str2;#str3;#str4",";#",3)返回str1;#str2;#str3
        /// SustringIndex("str1;#str2;#str3;#str4",";#",-3)返回str2;#str3;#str4
        /// </summary>
        /// <param name="indexStr">索引字符串返回值中不包含</param>
        /// <param name="indexAt">索引出现的第几次，0：第1次，-1：表示倒数第1次</param>
        /// <returns></returns>
        public static string SubstringIndex(this string value, string indexStr, int indexAt)
        {
            if (String.IsNullOrEmpty(value) || String.IsNullOrEmpty(indexStr))
            {
                return string.Empty;
            }
            string[] valueArray = value.Split(new string[] { indexStr }, StringSplitOptions.RemoveEmptyEntries);
            int arrLen = valueArray.Length;
            if (arrLen == 1 || indexAt == 0 || Math.Abs(indexAt) > arrLen)
            {
                return value;
            }
            return indexAt > 0
                ? string.Join(indexStr, valueArray, 0, indexAt)
                : string.Join(indexStr, valueArray, arrLen - Math.Abs(indexAt), Math.Abs(indexAt));
        }

        /// <summary>
        /// 用于截取到索引字符串的字符串
        /// </summary>
        public static string SubstringIndex(this string value, string indexStr)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(indexStr))
            {
                return string.Empty;
            }
            int index = value.IndexOf(indexStr);
            return index == -1 ? value : value.Substring(0, index);
        }

        /// <summary>
        /// 用于逆向截取到索引字符串的字符串
        /// </summary>
        public static string SubstringLastIndex(this string value, string indexStr)
        {
            if (ObjectUtil.IsNullOrEmpty(value) || ObjectUtil.IsNullOrEmpty(indexStr))
            {
                return string.Empty;
            }
            int index = value.LastIndexOf(indexStr);
            return index == -1 ? value : value.Substring(index + indexStr.Length, value.Length - index - indexStr.Length);
        }

        /// <summary>
        /// HTML按字节数截取字符串，多余部分以省略号结尾
        /// </summary>
        public static string GetHtmlSubString(this string origStr, int endIndex)
        {
            if (origStr == null || origStr.Length == 0 || endIndex < 0)
                return "";
            int bytesCount = System.Text.Encoding.Default.GetByteCount(origStr);
            if (bytesCount > endIndex)
            {
                int readyLength = 0;
                int byteLength;
                for (int i = 0; i < origStr.Length; i++)
                {
                    byteLength = System.Text.Encoding.Default.GetByteCount(new char[] { origStr[i] });
                    readyLength += byteLength;
                    if (readyLength == endIndex)
                    {
                        origStr = origStr.Substring(0, i + 1) + "...";
                        break;
                    }
                    else if (readyLength > endIndex)
                    {
                        origStr = origStr.Substring(0, i) + "...";
                        break;
                    }
                }
            }
            return origStr;
        }

        /// <summary>
        /// 按指定字符分隔，去除空元素
        /// </summary>
        public static string[] SplitNoEmpty(this string origStr, string splitChar)
        {
            if (string.IsNullOrEmpty(origStr) || origStr.Trim().Length == 0)
            {
                return null;
            }
            string[] strArr = origStr.Split(new string[] { splitChar }, StringSplitOptions.RemoveEmptyEntries);
            //string.Join(";", strArr);
            return strArr;
        }

        /// <summary>
        /// 按指定字符分隔，包含空元素
        /// </summary>
        public static string[] SplitIncludeEmptry(this string origStr, string splitChar)
        {
            if (string.IsNullOrEmpty(origStr) || origStr.Trim().Length == 0)
            {
                return null;
            }
            string[] strArr = origStr.Split(new string[] { splitChar }, StringSplitOptions.None);
            //string.Join(";", strArr);
            return strArr;
        }

        /// <summary>
        /// 校验邮箱地址是否合法
        /// </summary>
        public static bool IsEmail(this string emailAddress)
        {
            //return Regex.IsMatch(emailAddress.ToLower(), @"[_a-z\d\-\./]+@[_a-z\d\-]+(\.[_a-z\d\-]+)*(\.(info|biz|com|edu|gov|net|am|bz|cn|cx|hk|jp|tw|vc|vn))$");
            return Regex.IsMatch(emailAddress, "^[a-z0-9A-Z]+[-|a-z0-9A-Z._]+@([a-z0-9A-Z]+(-[a-z0-9A-Z]+)?\\.)+[a-z]{2,}$");
        }

        /// <summary>
        /// 一个中文字符按两位长度计算
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <param name="endwith">截取后需尾部补充字符串，如...</param>
        /// <returns></returns>
        public static string SubstringByte(this string str, int len, string endWith)
        {
            if (str.isBlank())
            {
                return string.Empty;
            }
            int l = str.Length;
            int clen = 0;
            while (clen < len && clen < l)
            {
                if ((int)str[clen] > 128)
                {
                    len--;
                }
                clen++;
            }

            if (clen < l)
            {
                return str.Substring(0, clen)
                    + (endWith.isBlank() ? string.Empty : endWith);
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 一个中文字符计算两位长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int getStringByteLen(this string str)
        {
            if (str.isBlank())
            {
                return 0;
            }
            return Encoding.Default.GetByteCount(str);
        }

        /// <summary>
        /// 将去掉字符串中的HTML标记,忽略大小写
        /// </summary>
        public static string HtmlFilter(this string original)
        {
            if (string.IsNullOrEmpty(original))
            {
                return string.Empty;
            }
            Regex reg = new Regex("(<\\s*[a-zA-Z][^>]*>)|(</\\s*[a-zA-Z][^>]*>)|(\\s)", RegexOptions.IgnoreCase);
            original = reg.Replace(original, "");
            return original;
        }

        /// <summary>
        /// 将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        /// 被包含于，如BeQuot("value","\"")返回"value"
        /// </summary>
        /// <param name="input"></param>
        /// <param name="quotStr"></param>
        /// <returns></returns>
        public static string QuotaBy(this string input, string quotStr)
        {
            return string.Format("{1}{0}{1}", ObjectUtil.NullToEmpty(input), ObjectUtil.NullToEmpty(quotStr));
        }

        /// <summary>
        /// 被包含于，如BeQuot("value","div")返回<div>value</div>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="quotStr"></param>
        /// <returns></returns>
        public static string QuotaByTag(this string input, string tagName)
        {
            return string.Format("<{1}>{0}</{1}>", ObjectUtil.NullToEmpty(input), ObjectUtil.NullToEmpty(tagName));
        }

        #region Regex
        /// <summary>
        /// example:bool b = "12345".IsMatch(@"\d+");
        /// </summary>
        public static bool IsMatch(this string str, string pattern)
        {
            if (str == null) return false;
            else return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// 正则匹配，如:"example11" pattern："[a-zA-Z]+" ,结果为：example
        /// </summary>
        public static string RegexMatch(this string str, string pattern)
        {
            if (str == null) return string.Empty;
            return Regex.Match(str, pattern).Value;
        }

        /// <summary>
        /// 用于正则匹配，如："2013DY1#2011DY1"用\d{4}匹配得到2013，2011 
        /// </summary>
        public static List<string> RegexMatches(this string str, string pattern)
        {
            if (str == null)
            {
                return null;
            }
            List<string> listResult = new List<string>();
            Regex regex = new Regex(pattern);
            MatchCollection matchCol = regex.Matches(str);
            foreach (Match item in matchCol)
            {
                listResult.Add(item.Value);
            }
            return listResult;
        }

        /// <summary>
        /// 正则替换，
        /// 如：Example2013DY1#2011DY1#2012DY1#2014DY1#
        /// pattern: (\w+)?\d{4}(\w+\d#?) 
        /// replacement: $1????$2
        /// 结果为：Example????DY1#????DY1#????DY1#????DY1#
        /// 其中replacement中的$1表示正则中第一个匹配组，替换后会保留。
        /// </summary>
        public static string RegexReplace(this string str, string pattern, string replacement)
        {
            if (str == null || pattern == null)
            {
                return string.Empty;
            }
            Regex regex = new Regex(pattern);
            return regex.Replace(str, replacement);
        }

        #endregion

        /// <summary>
        /// 以小写字母打头，骆驼格式
        /// </summary>
        public static string ToCamel(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return s[0].ToString().ToLower() + s.Substring(1);
        }

        /// <summary>
        /// 以大写字母打头，帕斯卡格式
        /// </summary>
        public static string ToPascal(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return s[0].ToString().ToUpper() + s.Substring(1);
        }

        /// <summary>
        /// 根据
        /// </summary>
        /// <param name="budgetCode"></param>
        /// <returns></returns>
        public static string GetBudgetName(string budgetCode)
        {
            string budgetName = "";

            switch (budgetCode)
            {
                case "BMYS":
                    budgetName = "部门费用预算";
                    break;
                case "GKYS":
                    budgetName = "归口管理费用预算";
                    break;
                case "ZXYS":
                    budgetName = "专项费用预算";
                    break;
                case "XMYS":
                    budgetName = "项目费用预算";
                    break;
                case "":
                    break;
            }

            return budgetName;
        }

        /// <summary>
        /// 去除开始字符串
        /// </summary>
        public static string TrimStartString(this string s, string startString)
        {
            if (string.IsNullOrEmpty(s)) return s;
            if (string.IsNullOrEmpty(startString)) return s;
            while (s.StartsWith(startString))
            {
                s = s.Substring(startString.Length);
            }
            return s;
        }

        /// <summary>
        /// 去除结尾字符串
        /// </summary>
        public static string TrimEndString(this string s, string endString)
        {
            if (string.IsNullOrEmpty(s)) return s;
            if (string.IsNullOrEmpty(endString)) return s;
            while (s.EndsWith(endString))
            {
                s = s.Substring(0, s.Length - endString.Length);
            }
            return s;
        }  
        
        /// <summary>
        /// 去除字符串
        /// </summary>
        public static string TrimString(this string s, string trimString)
        {
            if (string.IsNullOrEmpty(s)) return s;
            if (string.IsNullOrEmpty(trimString)) return s;
            while (s.EndsWith(trimString))
            {
                s = s.Substring(0, s.Length - trimString.Length);
            }
            while (s.StartsWith(trimString))
            {
                s = s.Substring(trimString.Length);
            }
            return s;
        }

        /// <summary>
        /// 判断字段串指定位置字符是不是字母
        /// </summary>
        public static bool IsLetter(this string s, int index)
        {
            if (string.IsNullOrEmpty(s)) return false;
            if (index < 0 || s.Length - 1 < index)
            {
                return false;
            }
            return Char.IsLetter(s.ToArray()[index]);
        }

        /// <summary>
        /// 判断字段串指定位置字符是不是字母或数字
        /// </summary>
        public static bool IsLetterOrDigit(this string s, int index)
        {
            if (string.IsNullOrEmpty(s)) return false;
            if (index < 0 || s.Length - 1 < index)
            {
                return false;
            }
            return Char.IsLetterOrDigit(s.ToArray()[index]);
        }

        /// <summary>
        /// 判断字段串指定位置字符是不是数字
        /// </summary>
        public static bool IsDigit(this string s, int index)
        {
            if (string.IsNullOrEmpty(s)) return false;
            if (index < 0 || s.Length - 1 < index)
            {
                return false;
            }
            return Char.IsDigit(s.ToArray()[index]);
        }
    }
}
