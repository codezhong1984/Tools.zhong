using System;

namespace Tools.zhong
{
    /// <summary>
    /// 通知消息
    /// </summary>
    public class ArgInvalidException : System.Exception
    {
        //public string ReturnCode = EXCECommon.ARGUMENTINVALIDCODE;

        /// <summary>
        /// 全局资源文件名称
        /// </summary>
        public string ResourceMessageName { get; set; }

        /// <summary>
        /// 定义参数名称
        /// </summary>
        public string ArgumentName { get; set; }

        /// <summary>
        /// 定义参数值
        /// </summary>
        public object[] ArgumentValues { get; set; }

        public ArgInvalidException()
        { }

        public ArgInvalidException(string message) : base(message)
        { }

        public ArgInvalidException(string resourceMessageName, string argumentName)
        {
            this.ResourceMessageName = resourceMessageName;
            this.ArgumentName = argumentName;
        }

        public ArgInvalidException(string resourceMessageName, params object[] argumentValues)
        {
            this.ResourceMessageName = resourceMessageName;
            this.ArgumentValues = argumentValues;
        }

        public ArgInvalidException(string message, Exception ex) : base(message, ex)
        { }

        public ArgInvalidException(string message, Exception ex, string messageCode) : base(message, ex)
        { }
    }
}
