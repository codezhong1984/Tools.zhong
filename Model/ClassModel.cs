using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.zhong.Model
{
    public class ClassModel
    {
        public ClassModel()
        {

        }

        public ClassModel(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public ClassModel(string className)
        {
            this.Name = className;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Path { get; set; }

        public List<ClassFieldModel> listFields { get; set; } = new List<ClassFieldModel>();
    }

    public class ClassFieldModel
    {
        public ClassFieldModel(string name, string typeName)
        {
            this.Name = name;
            this.TypeName = typeName;
        }
        public ClassFieldModel(string name, string typeName, bool isArray)
        {
            this.Name = name;
            this.TypeName = typeName;
            this.IsArrary = IsArrary;
        }

        public string Name { get; set; }

        public string TypeName { get; set; }

        public string FieldRemarks { get; set; }

        public bool IsArrary { get; set; }
        public bool IsNullable { get;  set; }
        public string DataType { get; internal set; }
    }
}
