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
        private Regex loopRegex;
        public FileTemplateGenerator(DataTable dt)
        {
            dtData = dt;
            loopRegex = new Regex(@"%LP%(.+)%ELP%");
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
                    fileItem.CopyTo(desFilePath + "\\" + fileName);
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
            //string resultText = string.Empty;
            using (FileStream fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.ReadWrite))
            {
                var rawData = new byte[fileStream.Length];
                fileStream.Read(rawData, 0, rawData.Length);
                string fileText = Encoding.UTF8.GetString(rawData);
                string resultText = ReplaceCodeToName(fileText, templModel);
                if (dtData != null)
                {
                    //处理LOOP指令
                    var match = loopRegex.Match(resultText);
                    while (match.Success)
                    {
                        if (match.Groups == null || match.Groups.Count < 2)
                        {
                            match = match.NextMatch();
                            continue;
                        }
                        var oriText = match.Groups[0]?.Value;
                        var rplText = FormatLoopText(match);
                        resultText = resultText.Replace(oriText, rplText);
                        match = match.NextMatch();
                    }
                }
                var outRawData = Encoding.UTF8.GetBytes(resultText);
                fileStream.Write(outRawData, 0, outRawData.Length);
            }
        }

        private string FormatLoopText(Match curMatch)
        {
            var loopText = curMatch.Groups[1]?.Value;
            StringBuilder sbOutput = new StringBuilder();
            foreach (DataRow item in dtData.Rows)
            {
                var outputLine = loopText;
                int i = 0;
                foreach (DataColumn column in dtData.Columns)
                {
                    outputLine = outputLine.Replace($"${i}", item[column.ColumnName].ToString());
                    i++;
                }
                sbOutput.Append(outputLine);
            }
            var rplText = sbOutput.ToString();
            return rplText;
        }
    }
}
