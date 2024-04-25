using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tools.zhong.Model;

namespace Tools.zhong.UtilHelper
{
    public class FileTemplateGenerator
    {
        private DataTable dtData;
        private Regex _LoopRegex;
        private Regex _LoopSplitRegex;

        //private Regex _HTMLRegex;
        public FileTemplateGenerator(DataTable dt)
        {
            dtData = dt;
            _LoopRegex = new Regex(@"%LP%(\s*.+(%SPT%\s*.*\s*%ESPT%)?\s*)%ELP%");
            _LoopSplitRegex = new Regex(@"%SPT%(\s*.+\s*)%ESPT%");
            //_HTMLRegex = new Regex(@"%HTML_LP%(\s*.+\s*)%HTML_ELP%");
        }

        public string ReplaceCodeToName(string orinalValue, FileTemplateModel templModel)
        {
            if (!string.IsNullOrWhiteSpace(orinalValue))
            {
                return orinalValue.Replace(FileTemplateModel.CLASS_CODE, templModel.ClassName)
                    .Replace(FileTemplateModel.TABLE_CODE, templModel.TableName)
                    .Replace(FileTemplateModel.FUNC_DESC_CODE, templModel.FuncName);
            }
            return string.Empty;
        }

        /// <summary>
        /// 生成文件并按格式生成文件名
        /// </summary>
        /// <param name="templModel"></param>
        public void GenerFiles(FileTemplateModel templModel)
        {
            if (string.IsNullOrWhiteSpace(templModel.OutputDir) || string.IsNullOrWhiteSpace(templModel.TemplDir))
            {
                return;
            }
            var templDirInfo = new DirectoryInfo(templModel.TemplDir);
            string regexPattern = $"^{templModel.TemplDir}".Replace("\\", "\\\\");
            GenerateFilesRecursive(templDirInfo, templModel, regexPattern);
        }

        private void GenerateFilesRecursive(DirectoryInfo dirInfo, FileTemplateModel templModel, string regexPattern)
        {
            var desFilePath = templModel.OutputDir + "\\" + Regex.Replace(dirInfo.FullName, regexPattern, "");
            if (!Directory.Exists(desFilePath))
            {
                Directory.CreateDirectory(desFilePath);
            }
            var files = dirInfo.GetFiles();
            if (files != null)
            {
                foreach (var fileItem in files)
                {
                    string fileName = ReplaceCodeToName(fileItem.Name, templModel);
                    fileItem.CopyTo(desFilePath + "\\" + fileName, true);
                    FormatTemplContent(desFilePath + "\\" + fileName, templModel);
                }
            }
            var dirs = dirInfo.GetDirectories();
            if (dirs != null)
            {
                foreach (DirectoryInfo dirInfoItem in dirs)
                {
                    GenerateFilesRecursive(dirInfoItem, templModel, regexPattern);
                }
            }
        }

        private void FormatTemplContent(string filePath, FileTemplateModel templModel)
        {
            byte[] outRawData = null;
            //读文件
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                var rawData = new byte[fileStream.Length];
                fileStream.Read(rawData, 0, rawData.Length);
                string fileText = Encoding.UTF8.GetString(rawData);
                string resultText = ReplaceCodeToName(fileText, templModel);
                if (dtData != null)
                {
                    //处理LOOP指令
                    var match = _LoopRegex.Match(resultText);
                    while (match.Success)
                    {
                        if (match.Groups == null || match.Groups.Count < 2)
                        {
                            match = match.NextMatch();
                            continue;
                        }
                        var oriText = match.Groups[0]?.Value;
                        var _splitCode = templModel.SPLIT_CODE;
                        var matchValue = match.Groups[1]?.Value;
                        if (match.Groups.Count >= 2 && _LoopSplitRegex.IsMatch(matchValue))
                        {
                            _splitCode = _LoopSplitRegex.Match(matchValue)?.Groups[1].Value;
                            matchValue = _LoopSplitRegex.Replace(matchValue, "");
                        }
                        var rplText = FormatLoopText(matchValue, _splitCode);
                        resultText = resultText.Replace(oriText, rplText);
                        match = match.NextMatch();
                    }

                    //处理Code Section
                    resultText = LoopCodeSections(resultText, templModel.SPLIT_CODE);
                }
                outRawData = Encoding.UTF8.GetBytes(resultText);
            }
            //写文件
            if (outRawData != null)
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    fileStream.Write(outRawData, 0, outRawData.Length);
                }
            }
        }

        /// <summary>
        /// 替换循环指令段
        /// </summary>
        private string FormatLoopText(string matchValue, string splitCode)
        {
            StringBuilder sbOutput = new StringBuilder();
            int rIndex = 0;
            foreach (DataRow item in dtData.Rows)
            {
                if (rIndex++ > 0)
                {
                    sbOutput.Append(splitCode);
                }
                var outputLine = matchValue?.Replace(System.Environment.NewLine, "");//去除回车换行符
                for (int i = dtData.Columns.Count - 1; i >= 0; i--)
                {
                    var column = dtData.Columns[i];
                    outputLine = outputLine.Replace($"${i}", item[column.ColumnName].ToString());
                }
                sbOutput.Append(outputLine);
            }
            var rplText = sbOutput.ToString();
            return rplText;
        }

        /// <summary>
        /// 替换循环代码段
        /// </summary>
        private string LoopCodeSections(string fileText, string splitCode)
        {
            if (string.IsNullOrWhiteSpace(fileText))
            {
                return string.Empty;
            }
            StringBuilder sbOutput = new StringBuilder();
            var splTexts = fileText.Split(new string[] { "%SECTION%" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var splItem in splTexts)
            {
                var subSplitTexts = splItem.Split(new string[] { "%END_SECTION%" }, StringSplitOptions.RemoveEmptyEntries);
                if (subSplitTexts.Length == 1)
                {
                    sbOutput.Append(splItem);
                    continue;
                }
                var templ = subSplitTexts[0];
                StringBuilder sbTemplOutput = new StringBuilder();
                foreach (DataRow item in dtData.Rows)
                {
                    var rowValue = templ;
                    for (int i = dtData.Columns.Count - 1; i >= 0; i--)
                    {
                        var column = dtData.Columns[i];
                        rowValue = rowValue.Replace($"${i}", item[column.ColumnName].ToString());
                    }
                    sbTemplOutput.Append(rowValue);
                }
                subSplitTexts[0] = sbTemplOutput.ToString();
                sbOutput.Append(string.Join("", subSplitTexts));
            }
            var resultText = sbOutput.ToString();
            return resultText;
        }
    }
}
