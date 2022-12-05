using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.zhong.Model;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Tools.zhong.UtilHelper
{
    public class DocxHelper
    {
        public static void GenerateDocx(string fileName, string dataBaseFileName, List<TableColumnModel> listData)
        {
            string filePath = @"C:\Users\Administrator\Desktop\11.docx";
            DocX doc = DocX.Load(filePath);

            if (listData == null || listData.Count == 0)
            {
                return;
            }
            try
            {
                using (var document = DocX.Create(fileName))
                {
                    var p = document.InsertParagraph();
                    p.Append(listData[0].TableName)
                        .Font(new Xceed.Document.NET.Font("宋体"))
                        .FontSize(14)
                        .Color(Color.Black)
                        .Bold()
                        .Spacing(18);

                    p = document.InsertParagraph();
                    p.Append(listData[0].TableComment)
                        .Font(new Xceed.Document.NET.Font("宋体")).Color(Color.Black)//.Italic()
                        .SpacingAfter(0);

                    //表字段表格
                    string[] headTexts = new string[] { "序号", "字段名称", "字段描述", "字段类型", "字段长度", "数据精度", "小数位数", "是否必填" };
                    //var table = document.AddTable(1 + listData.Count, headTexts.Length);
                    var columnWidths = new float[] { 50f, 150f, 150f, 100f, 50f, 50f, 50f, 50f };
                    var table = document.InsertTable(1 + listData.Count, headTexts.Length);
                    table.SetWidths(columnWidths);
                    table.AutoFit = AutoFit.Window;

                    table.TableCaption = listData[0].TableName;
                    table.TableDescription = listData[0].TableComment;
                    table.Design = TableDesign.TableGrid;
                    table.Alignment = Alignment.center;
                    for (int i = 0; i < headTexts.Length; i++)
                    {
                        table.Rows[0].Cells[i].Paragraphs[0].Append(headTexts[i]);
                        if (i == 0)
                        {
                            table.Rows[0].Cells[i].Width = 3;
                        }
                        if (i == 2)
                        {
                            table.Rows[0].Cells[i].Width = 10;
                        }
                    }

                    //添加表格数据
                    for (int i = 0; i < listData.Count; i++)
                    {
                        table.Rows[i + 1].Cells[0].Paragraphs[0].Append((i + 1).ToString());
                        table.Rows[i + 1].Cells[1].Paragraphs[0].Append(listData[i].FieldName);
                        table.Rows[i + 1].Cells[2].Paragraphs[0].Append(listData[i].FieldRemarks);
                        table.Rows[i + 1].Cells[3].Paragraphs[0].Append(listData[i].DataType);
                        table.Rows[i + 1].Cells[4].Paragraphs[0].Append(listData[i].DataLength?.ToString());
                        table.Rows[i + 1].Cells[5].Paragraphs[0].Append(listData[i].DataPrecision?.ToString());
                        table.Rows[i + 1].Cells[6].Paragraphs[0].Append(listData[i].DataScale?.ToString());
                        table.Rows[i + 1].Cells[7].Paragraphs[0].Append(listData[i].IsNullable ? "Y" : "N");
                    }
                
                    document.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
