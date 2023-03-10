using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.zhong.Model
{
    public class CodeGenerateOption
    {
        /// <summary>
        /// 是否处理下划线
        /// </summary>
        public bool Underline { get; set; }

        /// <summary>
        /// 是否生成DisplayName
        /// </summary>
        public bool AddDisplayName { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; set; }

        /// <summary>
        /// 枚举代码值，仅适用于MySQL
        /// </summary>
        public string EnumCode { get; set; }

        /// <summary>
        /// cbCreateTbName
        /// </summary>
        public bool MapperTableName { get; set; }

        /// <summary>
        /// 是否生成完整GET|SET方法
        /// </summary>
        public bool FullPropFlag { get; set; }

        /// <summary>
        /// 是否对SET方法去空格
        /// </summary>
        public bool TrimProp { get; set; }
    }
}
