using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.zhong.Model
{
    public class FileTemplateModel
    {
        /// <summary>
        /// 表名：指令%T%
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 类名：指令%C%
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 功能名：指令%F%
        /// </summary>
        public string FuncName { get; set; }

        /// <summary>
        /// 模板目录
        /// </summary>
        public string TemplDir { get; set; }

        /// <summary>
        /// 输出目标
        /// </summary>
        public string OutputDir { get; set; }

        #region 指令代码
        /// <summary>
        /// 表名指令
        /// </summary>
        public const string TABLE_CODE = "%T%";

        /// <summary>
        /// 类名指令：%C%
        /// </summary>
        public const string CLASS_CODE = "%C%";

        /// <summary>
        /// 功能描述名指令：%FDES%
        /// </summary>
        public const string FUNC_DESC_CODE = "%C%";

        /// <summary>
        /// 循环开始指令：%LP%
        /// </summary>
        public const string LOOP_START_CODE = "%LP%";

        /// <summary>
        /// 循环结束指令：%ELP%
        /// </summary>
        public const string LOOP_END_CODE = "%ELP%";

        #endregion
    }
}
