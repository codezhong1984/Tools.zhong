using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tools.zhong.Component;
using Tools.zhong.Model;
using Tools.zhong.UtilHelper;

namespace Tools.zhong
{
    public partial class JsonToolForm : Form
    {
        public JsonToolForm()
        {
            InitializeComponent();
        }

        private void JsonToolForm_Load(object sender, EventArgs e)
        {
            ComboBoxHelper.BindSplitCharComboBox(cbSplitChar);
            //tabControl1.SelectedIndex = 1;
        }

        private void btnProccess_Click(object sender, EventArgs e)
        {
            if (txtInput2.Text.Trim().Length == 0)
            {
                lblMsg2.Text = "JSON未填写！";
                return;
            }
            if (txtNode.Text.Trim().Length == 0)
            {
                lblMsg2.Text = "提取节点未指定！";
                return;
            }
            try
            {
                var jsonStr = txtInput2.Text.Trim();
                var jObj = JsonConvert.DeserializeObject<JToken>(jsonStr);
                var resultString = new List<Tuple<string, string>>();
                TraverseJObject(jObj, ref resultString);

                var list = resultString.Where(i => Regex.IsMatch(i.Item1, txtNode.Text))?.Select(i => i.Item2)?.ToList();
                txtOutput2.Text = string.Join(cbSplitChar.SelectedValue?.ToString(), list);

            }
            catch (Exception ex)
            {
                lblMsg2.Text = ex.Message;
            }
        }
        public void TraverseJObject(JToken token, ref List<Tuple<string, string>> result)
        {
            if (token is JProperty property && !property.HasValues)
            {
                //Console.WriteLine($"Key: {property.Name}, Value: {property.Value}");
                result.Add(Tuple.Create(property.Name, property.Value?.ToString()));
            }
            else if (token is JValue jval)
            {
                result.Add(Tuple.Create(jval.Path, jval.Value?.ToString()));
            }

            foreach (var child in token.Children())
            {
                TraverseJObject(child, ref result);
            }
        }

        private void lblExample_Click(object sender, EventArgs e)
        {
            txtNode.Text = lblExample.Text.Trim();
        }

        private void btnClear2_Click(object sender, EventArgs e)
        {
            txtInput2.Clear();
        }

        private void txtFormat_Click(object sender, EventArgs e)
        {
            if (txtInput2.Text.Trim().Length == 0)
            {
                lblMsg2.Text = "JSON未填写！";
                return;
            }
            try
            {
                var jsonStr = txtInput2.Text.Trim();
                JObject json = JObject.Parse(jsonStr);
                string formattedJson = json.ToString(Formatting.Indented);
                txtInput2.Text = formattedJson;
                lblMsg2.Text = $"格式化完成！";
            }
            catch (Exception ex)
            {
                lblMsg2.Text = ex.Message;
            }
        }

        private void btnShowNode_Click(object sender, EventArgs e)
        {
            if (txtInput2.Text.Trim().Length == 0)
            {
                lblMsg2.Text = "JSON未填写！";
                return;
            }
            try
            {
                var jsonStr = txtInput2.Text.Trim();
                var jObj = JsonConvert.DeserializeObject<JToken>(jsonStr);
                var resultString = new List<Tuple<string, string>>();
                TraverseJObject(jObj, ref resultString);
                var regex = new Regex(@"\[\d+\]");
                var list = resultString?.OrderBy(i => regex.Replace(i.Item1, ""))?.Select(i => $"{i.Item1}\t ============ {i.Item2}")?.ToList();
                txtOutput2.Text = string.Join(cbSplitChar.SelectedValue?.ToString(), list);
                lblMsg2.Text = $"共{list?.Count ?? 0}个节点";
            }
            catch (Exception ex)
            {
                lblMsg2.Text = ex.Message;
            }
        }

        private void btnClear1_Click(object sender, EventArgs e)
        {
            txtInput1.Clear();
        }

        private void btnShowNode1_Click(object sender, EventArgs e)
        {
            if (txtInput1.Text.Trim().Length == 0)
            {
                lblMsg1.Text = "JSON未填写！";
                return;
            }
            try
            {
                var jsonStr = txtInput1.Text.Trim();
                var jObj = JsonConvert.DeserializeObject<JToken>(jsonStr);
                var resultString = new List<Tuple<string, string>>();
                TraverseJObject(jObj, ref resultString);
                var regex = new Regex(@"\[\d+\]");
                var list = resultString?.OrderBy(i => regex.Replace(i.Item1, ""))?.Select(i => $"{i.Item1}")?.ToList();
                txtOutput1.Text = string.Join(cbSplitChar.SelectedValue?.ToString(), list);
                lblMsg1.Text = $"共{list?.Count ?? 0}个节点";
            }
            catch (Exception ex)
            {
                lblMsg2.Text = ex.Message;
            }
        }

        private void btnFormat1_Click(object sender, EventArgs e)
        {
            if (txtInput1.Text.Trim().Length == 0)
            {
                lblMsg1.Text = "JSON未填写！";
                return;
            }
            try
            {
                var jsonStr = txtInput1.Text.Trim();
                JObject json = JObject.Parse(jsonStr);
                string formattedJson = json.ToString(Formatting.Indented);
                txtInput1.Text = formattedJson;
                lblMsg1.Text = $"格式化完成！";
            }
            catch (Exception ex)
            {
                lblMsg1.Text = ex.Message;
            }
        }

        /// <summary>
        /// JSON生成类
        /// </summary>
        private void btnCreate1_Click(object sender, EventArgs e)
        {
            if (txtInput1.Text.Trim().Length == 0)
            {
                lblMsg1.Text = "JSON未填写！";
                return;
            }
            if (txtClassName.Text.Trim().Length == 0)
            {
                lblMsg1.Text = "类名未填写！";
                return;
            }
            try
            {
                var jsonStr = txtInput1.Text.Trim();
                var jObj = JsonConvert.DeserializeObject<JToken>(jsonStr);
                var resultString = new List<Tuple<string, string>>();
                TraverseJObject(jObj, ref resultString);
                var regex = new Regex(@"\[\d+\]");
                var list = resultString?.OrderBy(i => regex.Replace(i.Item1, ""))?.Select(i => regex.Replace(i.Item1, "[]"))?.Distinct()?.ToList();

                var classList = new List<ClassModel>();
                var rootClass = new ClassModel(txtClassName.Text);
                rootClass.Path = txtClassName.Text;
                classList.Add(rootClass);
                foreach (var item in list)
                {
                    var strVals = item.Split('.');
                    for (int i = 0; i < strVals.Length; i++)
                    {
                        var curVal = strVals[i];
                        if (string.IsNullOrWhiteSpace(curVal))
                        {
                            continue;
                        }
                        if (i < strVals.Length - 1)
                        {
                            string path = GetPath(rootClass, strVals, i);
                            string className = GetClassName(path, curVal);
                            if (!classList.Exists(k => k.Name == className))
                            {
                                var clModel = new ClassModel();
                                clModel.Path = path;
                                clModel.Name = className;
                                classList.Add(clModel);
                            }
                        }

                        if (i == 0)
                        {
                            if (!rootClass.listFields.Exists(k => string.Compare(k.Name, curVal, true) == 0))
                            {
                                AddNewField(rootClass, curVal, i < strVals.Length - 1);
                            }
                        }
                        else
                        {
                            var path = GetPath(rootClass, strVals, i - 1);
                            //string className = GetClassName(path, curVal);
                            var cl = classList.FirstOrDefault(k => k.Path == path);
                            if (cl != null && !cl.listFields.Exists(k => string.Compare(k.Name, curVal?.Replace("[]", ""), true) == 0))
                            {
                                AddNewField(cl, curVal, i < strVals.Length - 1);
                            }
                        }
                    }
                }

                var option = new CodeGenerateOption()
                {
                    AddDisplayName = false,
                    EnumCode = null,
                    FullPropFlag = false,
                    MapperTableName = false,
                    NameSpace = txtNameSpace.Text.Trim(),
                    TrimProp = cbIfTrim.Checked,
                    Underline = false,
                    Required = false,
                };
                var classCode = DbObjectHelper.GenerateCode(classList, option);
                txtOutput1.Text = classCode;
                lblMsg1.Text = $"生成成功。";
            }
            catch (Exception ex)
            {
                lblMsg1.Text = ex.Message;
            }
        }

        private string GetPath(ClassModel rootClass, string[] strVals, int i)
        {
            var list = strVals.ToList().GetRange(0, i + 1);
            //list = list.Select(k => k.Substring(0, 1).ToUpper() + k.Substring(1)).ToList();
            return rootClass.Name + "." + string.Join(".", list);
        }

        private string GetClassName(string path, string curVal)
        {
            var className = curVal.Replace("[]", "");
            className = className.Substring(0, 1).ToUpper() + className.Substring(1);
            className = curVal.EndsWith("[]") ? className + "Item" : className;
            if (!string.IsNullOrWhiteSpace(path))
            {
                var list = path.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(k => k.Substring(0, 1).ToUpper() + k.Substring(1))
                    .Select(k => k.Replace("[]", ""))
                    .ToList();
                list = list.GetRange(0, list.Count - 1);
                className = string.Join("", list) + className;
            }
            return className;
        }

        private void AddNewField(ClassModel classModel, string val, bool isClass)
        {
            //检查是否为数组
            if (val.EndsWith("[]"))
            {
                var fieldNameTemp = val.Replace("[]", "");
                var fieldName = fieldNameTemp.Substring(0, 1).ToUpper() + fieldNameTemp.Substring(1);
                var fieldTypeName = classModel.Name + fieldNameTemp.Substring(0, 1).ToUpper() + fieldNameTemp.Substring(1) + "Item";
                classModel.listFields.Add(new ClassFieldModel(fieldName, $"List<{(isClass ? fieldTypeName : "string")}>", true));
            }
            else if (isClass)
            {
                var fieldTypeName = classModel.Name + val.Substring(0, 1).ToUpper() + val.Substring(1);
                var fieldName = val.Substring(0, 1).ToUpper() + val.Substring(1);
                classModel.listFields.Add(new ClassFieldModel(fieldName, fieldTypeName));
            }
            else
            {
                var fieldName = val;
                fieldName = fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
                classModel.listFields.Add(new ClassFieldModel(fieldName, "string"));
            }
        }

        private void btnClearOutput2_Click(object sender, EventArgs e)
        {
            txtOutput2.Clear();
        }

        private void btnClearOutput1_Click(object sender, EventArgs e)
        {
            txtOutput1.Clear();
        }

        private void btnShowNodeType1_Click(object sender, EventArgs e)
        {
            if (txtInput1.Text.Trim().Length == 0)
            {
                lblMsg1.Text = "JSON未填写！";
                return;
            }
            try
            {
                var jsonStr = txtInput1.Text.Trim();
                var jObj = JsonConvert.DeserializeObject<JToken>(jsonStr);
                var resultString = new List<Tuple<string, string>>();
                TraverseJObject(jObj, ref resultString);
                var regex = new Regex(@"\[\d+\]");
                var list = resultString?.OrderBy(i => regex.Replace(i.Item1, ""))?.Select(i => regex.Replace(i.Item1, "[]"))?.Distinct()?.ToList();
                txtOutput1.Text = string.Join(cbSplitChar.SelectedValue?.ToString(), list);
                lblMsg1.Text = $"共{list?.Count ?? 0}个节点";
            }
            catch (Exception ex)
            {
                lblMsg2.Text = ex.Message;
            }
        }
    }
}
