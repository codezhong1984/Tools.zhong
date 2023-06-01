using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        private static readonly string DATABASE_TITLE_SUFFIX = "数据库设计文档";
        private static readonly string[] TABLE_FIELDS = new string[] { "字段名称", "字段描述",
            "字段类型", "字段长度", "数据精度", "小数位数", "是否可空" };
        private static readonly string[] TABLE_FIELDS_EXT = new string[] { "字段名称", "字段描述",
            "字段类型", "字段长度", "是否可空" };

        public static DocX CreateDocx(string fileName)
        {
            return DocX.Create(fileName);
        }

        public static DocX LoadDocx(string fileName)
        {
            return DocX.Load(fileName);
        }

        public static void GenerateDocxByTable(string fileName, string dbName, List<TableColumnModel> listData, bool hideNumberCol)
        {
            try
            {
                bool exists = File.Exists(fileName);
                if (exists)
                {
                    using (var docx = LoadDocx(fileName))
                    {
                        WriteDocxSingleTable(docx, listData, hideNumberCol);
                        docx.Save();
                    }
                }
                else
                {
                    using (var docx = CreateDocx(fileName))
                    {
                        WriteDocxTitle(docx, dbName + DATABASE_TITLE_SUFFIX);
                        WriteDocxSingleTable(docx, listData, hideNumberCol);
                        docx.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void GenerateDocxByTables(string fileName, string docTitle, List<List<TableColumnModel>> lists, bool hideNumberCol)
        {
            try
            {
                bool exists = File.Exists(fileName);
                if (exists)
                {
                    using (var docx = LoadDocx(fileName))
                    {
                        WriteDocxTables(docx, lists, hideNumberCol);
                        docx.Save();
                    }
                }
                else
                {
                    using (var docx = CreateDocx(fileName))
                    {
                        WriteDocxTitle(docx, docTitle);
                        WriteDocxTables(docx, lists, hideNumberCol);
                        docx.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void WriteDocxTitle(DocX document, string title)
        {
            var p = document.InsertParagraph();
            p.Append(title)
                .Font(new Xceed.Document.NET.Font("宋体"))
                .FontSize(20)
                .Color(Color.Black)
                .Bold()
                .SpacingAfter(5);
            p.Alignment = Alignment.center;
            //p.InsertPageBreakAfterSelf();
        }

        [Obsolete]
        private static void WriteDocxSingleTable_BackUP(DocX docx, List<TableColumnModel> listData)
        {
            // var docxSample = LoadDocx(@"C:\Users\Administrator\Desktop\sample.docx");
            if (listData == null || listData.Count == 0)
            {
                return;
            }
            var p = docx.InsertParagraph();
            p.Append(listData[0].TableName)
                .Font(new Xceed.Document.NET.Font("宋体"))
                .FontSize(12)
                .Color(Color.Black)
                .Bold()
                .SpacingBefore(10);
            p.Alignment = Alignment.left;

            //docx.AddList(listData[0].TableName, 1, ListItemType.Numbered, null, false, true
            //    , new Formatting() { Size = 20, Bold = true, FontFamily = new Xceed.Document.NET.Font("宋体") });

            if (!string.IsNullOrWhiteSpace(listData[0].TableComment))
            {
                var pComment = docx.InsertParagraph();
                pComment.Append(listData[0].TableComment)
                    .Font(new Xceed.Document.NET.Font("宋体")).Color(Color.Black)
                    .SpacingAfter(0);
                pComment.Alignment = Alignment.left;
            }

            //表字段表格         
            var tableFontSize = 10.5;
            var columnWidths = new float[] { 0.17f, 0.18f, 0.17f, 0.12f, 0.12f, 0.12f, 0.12f };
            var table = docx.InsertTable(1 + listData.Count, TABLE_FIELDS.Length);
            table.SetWidthsPercentage(columnWidths);
            table.AutoFit = AutoFit.Window;

            table.TableCaption = listData[0].TableName;
            table.TableDescription = listData[0].TableComment;
            table.Design = TableDesign.TableGrid;
            table.Alignment = Alignment.left;
            for (int i = 0; i < TABLE_FIELDS.Length; i++)
            {
                table.Rows[0].Cells[i].Paragraphs[0].Append(TABLE_FIELDS[i]).FontSize(tableFontSize);//.Bold(true);
            }

            //添加表格数据
            for (int i = 0; i < listData.Count; i++)
            {
                table.Rows[i + 1].Cells[0].Paragraphs[0].Append(listData[i].FieldName).FontSize(tableFontSize);
                table.Rows[i + 1].Cells[1].Paragraphs[0].Append(listData[i].FieldRemarks).FontSize(tableFontSize);
                table.Rows[i + 1].Cells[2].Paragraphs[0].Append(listData[i].DataType).FontSize(tableFontSize);
                table.Rows[i + 1].Cells[3].Paragraphs[0].Append(listData[i].DataLength?.ToString()).FontSize(tableFontSize);
                table.Rows[i + 1].Cells[4].Paragraphs[0].Append(listData[i].DataPrecision?.ToString()).FontSize(tableFontSize);
                table.Rows[i + 1].Cells[5].Paragraphs[0].Append(listData[i].DataScale?.ToString()).FontSize(tableFontSize);
                table.Rows[i + 1].Cells[6].Paragraphs[0].Append(listData[i].IsNullable ? "Y" : "N").FontSize(tableFontSize);
            }
        }

        private static void WriteDocxSingleTable(DocX docx, List<TableColumnModel> listData, bool hideNumberCol)
        {
            // var docxSample = LoadDocx(@"C:\Users\Administrator\Desktop\sample.docx");
            if (listData == null || listData.Count == 0)
            {
                return;
            }
            var p = docx.Paragraphs.FirstOrDefault(i => i.Text == listData[0].TableName);
            if (p != null)
            {
                if (!string.IsNullOrWhiteSpace(listData[0].TableComment) && p.NextParagraph != null)
                {
                    p.NextParagraph.Remove(false);
                }
                p.Remove(false);
            }
            p = docx.InsertParagraph(listData[0].TableName, false)
                .Font(new Xceed.Document.NET.Font("等线 Light (中文标题)"))
                .FontSize(14)
                .Color(Color.FromArgb(46, 116, 181))
                .Bold()
                .SpacingBefore(10)
                .Heading(HeadingType.Heading1);
            p.Alignment = Alignment.left;
            p.ListItemType = ListItemType.Numbered;

            if (!string.IsNullOrWhiteSpace(listData[0].TableComment))
            {
                var pComment = p.InsertParagraphAfterSelf(listData[0].TableComment)
                    .Font(new Xceed.Document.NET.Font("宋体")).Color(Color.Black)
                    .SpacingAfter(0);
                pComment.Alignment = Alignment.left;
                p = pComment;
            }

            var table = docx.Tables.FirstOrDefault(i => i.TableCaption == listData[0].TableName);
            if (table != null)
            {
                table.Remove();
            }

            //表字段表格         
            var tableFontSize = 10.5;
            var columnWidths = new float[] { 0.17f, 0.18f, 0.17f, 0.12f, 0.12f, 0.12f, 0.12f };
            var tableFields = hideNumberCol ? TABLE_FIELDS_EXT : TABLE_FIELDS;

            if (hideNumberCol)
            {
                columnWidths = new float[] { 0.17f, 0.19f, 0.16f, 0.12f, 0.12f };
            }
            table = p.InsertTableAfterSelf(1 + listData.Count, tableFields.Length);
            //table = listItem.InsertTableAfterSelf(1 + listData.Count, TABLE_FIELDS.Length);
            table.SetWidthsPercentage(columnWidths);
            table.AutoFit = AutoFit.Window;

            table.TableCaption = listData[0].TableName;
            table.TableDescription = listData[0].TableComment;
            table.Design = TableDesign.TableGrid;
            table.Alignment = Alignment.left;

            for (int i = 0; i < tableFields.Length; i++)
            {
                var tRow = table.Rows[0].Cells[i].Paragraphs[0]
                    .Append(tableFields[i]).FontSize(tableFontSize);//.Bold(true);
                //tRow.Alignment = Alignment.center;
            }

            //添加表格数据
            for (int i = 0; i < listData.Count; i++)
            {
                table.Rows[i + 1].Cells[0].Paragraphs[0].Append(listData[i].FieldName).FontSize(tableFontSize);
                table.Rows[i + 1].Cells[1].Paragraphs[0].Append(listData[i].FieldRemarks).FontSize(tableFontSize);

                if (!hideNumberCol)
                {
                    table.Rows[i + 1].Cells[2].Paragraphs[0].Append(listData[i].DataType).FontSize(tableFontSize);
                    table.Rows[i + 1].Cells[3].Paragraphs[0].Append(listData[i].DataLength?.ToString()).FontSize(tableFontSize);
                    table.Rows[i + 1].Cells[4].Paragraphs[0].Append(listData[i].DataPrecision?.ToString()).FontSize(tableFontSize);
                    table.Rows[i + 1].Cells[5].Paragraphs[0].Append(listData[i].DataScale?.ToString()).FontSize(tableFontSize);
                    table.Rows[i + 1].Cells[6].Paragraphs[0].Append(listData[i].IsNullable ? "Y" : "N").FontSize(tableFontSize);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(listData[i].DataPrecision?.ToString()))
                    {
                        var cellVal = string.Format("{0}({1},{2})", listData[i].DataType, listData[i].DataPrecision, listData[i].DataScale);
                        table.Rows[i + 1].Cells[2].Paragraphs[0].Append(cellVal).FontSize(tableFontSize);
                    }
                    else
                    {
                        table.Rows[i + 1].Cells[2].Paragraphs[0].Append(listData[i].DataType?.ToString()).FontSize(tableFontSize);
                        table.Rows[i + 1].Cells[3].Paragraphs[0].Append(listData[i].DataLength?.ToString()).FontSize(tableFontSize);
                    }
                    table.Rows[i + 1].Cells[4].Paragraphs[0].Append(listData[i].IsNullable ? "Y" : "N").FontSize(tableFontSize);
                }
            }

            //var numberedList = docx.AddList("Berries", 0, ListItemType.Numbered, 1);
            //// Add Sub-items(level 1) to the preceding ListItem.
            //docx.AddListItem(numberedList, "Strawberries", 1);
            //docx.AddListItem(numberedList, "Blueberries", 1);
            //docx.AddListItem(numberedList, "Raspberries", 1);
            ////docxn item (level 0)
            //docx.AddListItem(numberedList, "Banana", 0);
            //docx.AddListItem(numberedList, "Apple", 0);

            //docx.AddListItem(numberedList, "Red", 1);
            //docx.AddListItem(numberedList, "Green", 1);
            //docx.AddListItem(numberedList, "Yellow", 1);
            //docx.InsertList(numberedList);
        }

        private static void WriteDocxSingleTableExt(DocX docx, List<TableColumnModel> listData)
        {
            // var docxSample = LoadDocx(@"C:\Users\Administrator\Desktop\sample.docx");
            if (listData == null || listData.Count == 0)
            {
                return;
            }
            var p = docx.Paragraphs.FirstOrDefault(i => i.Text == listData[0].TableName);
            if (p != null)
            {
                if (!string.IsNullOrWhiteSpace(listData[0].TableComment) && p.NextParagraph != null)
                {
                    p.NextParagraph.Remove(false);
                }
                p.Remove(false);
            }
            p = docx.InsertParagraph(listData[0].TableName, false)
                .Font(new Xceed.Document.NET.Font("等线 Light (中文标题)"))
                .FontSize(14)
                .Color(Color.FromArgb(46, 116, 181))
                .Bold()
                .SpacingBefore(10)
                .Heading(HeadingType.Heading1);
            p.Alignment = Alignment.left;
            p.ListItemType = ListItemType.Numbered;

            if (!string.IsNullOrWhiteSpace(listData[0].TableComment))
            {
                var pComment = p.InsertParagraphAfterSelf(listData[0].TableComment)
                    .Font(new Xceed.Document.NET.Font("宋体")).Color(Color.Black)
                    .SpacingAfter(0);
                pComment.Alignment = Alignment.left;
                p = pComment;
            }

            var table = docx.Tables.FirstOrDefault(i => i.TableCaption == listData[0].TableName);
            if (table != null)
            {
                table.Remove();
            }

            //表字段表格         
            var tableFontSize = 10.5;
            var columnWidths = new float[] { 0.17f, 0.18f, 0.17f, 0.12f, 0.12f, 0.12f, 0.12f };
            table = p.InsertTableAfterSelf(1 + listData.Count, TABLE_FIELDS_EXT.Length);
            //table = listItem.InsertTableAfterSelf(1 + listData.Count, TABLE_FIELDS_EXT.Length);
            table.SetWidthsPercentage(columnWidths);
            table.AutoFit = AutoFit.Window;

            table.TableCaption = listData[0].TableName;
            table.TableDescription = listData[0].TableComment;
            table.Design = TableDesign.TableGrid;
            table.Alignment = Alignment.left;
            for (int i = 0; i < TABLE_FIELDS_EXT.Length; i++)
            {
                table.Rows[0].Cells[i].Paragraphs[0].Append(TABLE_FIELDS_EXT[i]).FontSize(tableFontSize);//.Bold(true);
            }

            //添加表格数据
            for (int i = 0; i < listData.Count; i++)
            {
                table.Rows[i + 1].Cells[0].Paragraphs[0].Append(listData[i].FieldName).FontSize(tableFontSize);
                table.Rows[i + 1].Cells[1].Paragraphs[0].Append(listData[i].FieldRemarks).FontSize(tableFontSize);
                table.Rows[i + 1].Cells[2].Paragraphs[0].Append(listData[i].DataType).FontSize(tableFontSize);
                if (listData[i].DataType?.ToUpper().Contains("char") ?? false)
                {
                    table.Rows[i + 1].Cells[3].Paragraphs[0].Append(listData[i].DataLength?.ToString()).FontSize(tableFontSize);
                }
                table.Rows[i + 1].Cells[6].Paragraphs[0].Append(listData[i].IsNullable ? "Y" : "N").FontSize(tableFontSize);
            }

            //var numberedList = docx.AddList("Berries", 0, ListItemType.Numbered, 1);
            //// Add Sub-items(level 1) to the preceding ListItem.
            //docx.AddListItem(numberedList, "Strawberries", 1);
            //docx.AddListItem(numberedList, "Blueberries", 1);
            //docx.AddListItem(numberedList, "Raspberries", 1);
            ////docxn item (level 0)
            //docx.AddListItem(numberedList, "Banana", 0);
            //docx.AddListItem(numberedList, "Apple", 0);

            //docx.AddListItem(numberedList, "Red", 1);
            //docx.AddListItem(numberedList, "Green", 1);
            //docx.AddListItem(numberedList, "Yellow", 1);
            //docx.InsertList(numberedList);
        }


        private static void WriteDocxTables(DocX document, List<List<TableColumnModel>> lists, bool hideNumberCol)
        {
            if (lists == null || lists.Count == 0)
            {
                return;
            }
            foreach (var listItem in lists)
            {
                WriteDocxSingleTable(document, listItem, hideNumberCol);
            }
        }
    }
}
