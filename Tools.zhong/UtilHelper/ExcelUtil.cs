using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Linq;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Text.RegularExpressions;
using System.Text;

namespace Tools.zhong.UtilHelper
{
    public class ExcelUtil
    {
        public static void WriteExcel(DataSet ds, string filepath)
        {
            try
            {
                //string filepath = HttpContext.Current.Server.MapPath("~/download/");              
                StreamWriter sw = new StreamWriter(filepath, false, System.Text.Encoding.UTF8);
                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < ds.Tables[0].Columns.Count; k++)
                {
                    sb.Append(ds.Tables[0].Columns[k].ColumnName.ToString() + "\t");
                }
                sb.Append(Environment.NewLine);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        sb.Append(ds.Tables[0].Rows[i][j].ToString() + "\t");
                    }
                    sb.Append(Environment.NewLine);
                }
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public static void WriteExcel(DataTable dt, string filepath)
        {
            try
            {
                //string filepath = HttpContext.Current.Server.MapPath("~/download/");              
                StreamWriter sw = new StreamWriter(filepath, false, System.Text.Encoding.UTF8);
                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    sb.Append(dt.Columns[k].ColumnName.ToString() + "\t");
                }
                sb.Append(Environment.NewLine);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sb.Append(dt.Rows[i][j].ToString() + "\t");
                    }
                    sb.Append(Environment.NewLine);
                }
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public static void WriteCSV(DataTable dt, string filepath)
        {
            try
            {
                //string filepath = HttpContext.Current.Server.MapPath("~/download/");              
                StreamWriter sw = new StreamWriter(filepath, false, System.Text.Encoding.UTF8);
                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    sb.Append(dt.Columns[k].ColumnName.ToString() + ",");
                }
                sb.Append(Environment.NewLine);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sb.Append(dt.Rows[i][j].ToString() + ",");
                    }
                    sb.Append(Environment.NewLine);
                }
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {

            }
        }

        #region SimpleExportDataTable

        /// <summary>
        /// 导出DataTable到excel中
        /// </summary>
        public static XSSFWorkbook ToExcel(DataTable exportData, string sheetName, XSSFWorkbook workbook = null)
        {
            if (workbook == null)
            {
                workbook = new XSSFWorkbook();
            }
            ISheet sheet = workbook.CreateSheet(sheetName);

            #region 创建表头

            IRow headerRow = sheet.CreateRow(0);

            int columnNums = exportData.Columns.Count;
            for (int i = 0; i < columnNums; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                cell.SetCellValue(exportData.Columns[i].ColumnName);
            }

            #endregion

            #region 填写表格数据
            IRow lastRow = null;
            if (exportData != null)
            {
                int rowsCount = exportData.Rows.Count;
                int columnCount = exportData.Columns.Count;
                for (int k = 0; k < rowsCount; k++)
                {
                    lastRow = sheet.CreateRow(k + 1);
                    var dataRow = exportData.Rows[k];
                    int colIndex = 0;
                    for (int pindex = 0; pindex < columnCount; pindex++, colIndex++)
                    {
                        object obj = dataRow[pindex];
                        if (obj != null && obj is byte[])
                        {
                            obj = System.Text.Encoding.Default.GetString(obj as byte[]);
                            obj = obj.ToString().Length > 32767 ? obj.ToString().Substring(0, 32760) + "..." : obj;
                        }

                        lastRow.CreateCell(colIndex).SetCellValue(obj?.ToString());
                    }
                }
            }
            #endregion

            return workbook;
        }

        #endregion

        /// <summary>
        /// 导入excel到DataTable中，首行当做表头处理
        /// </summary>
        public static DataTable ToDataTable(System.IO.Stream stream)
        {
            DataTable dt = new DataTable();
            using (stream)
            {
                //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
                XSSFWorkbook xssfworkbook = new XSSFWorkbook(stream);
                //获取excel的第一个sheet
                ISheet sheet = xssfworkbook.GetSheetAt(0);
                //获取sheet的首行                
                IRow headerRow = sheet.GetRow(sheet.FirstRowNum);
                //一行最后一个方格的编号 即总的列数
                int cellCount = headerRow.LastCellNum;
                for (int i = 0; i < cellCount; i++)
                {
                    var cellValue = ExcelCellUtil.GetValueType(headerRow.GetCell(i) as XSSFCell, CellType.String);
                    dt.Columns.Add(new DataColumn(cellValue == null ? i.ToString() : cellValue.ToString()));
                }

                //最后一列的标号  即总的行数
                int rowCount = sheet.LastRowNum;

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dr = dt.NewRow();
                    for (int cindex = 0; cindex < cellCount; cindex++)
                    {
                        var cellValue = ExcelCellUtil.GetValueType(row.GetCell(cindex) as XSSFCell, CellType.String);
                        dr[cindex] = cellValue;
                    }
                    dt.Rows.Add(dr);
                }
                xssfworkbook = null;
                sheet = null;
            }
            return dt;
        }
    }

    #region Excel导入导出

    public class ExcelUtil<T> where T : new()
    {
        #region Excel的导入导出

        #region List

        /// <summary>
        /// 导入excel
        /// </summary>
        public static List<T> ToList(System.IO.Stream stream, string[] includeFields = null, string[] excludeFields = null)
        {
            List<T> listData = new List<T>();
            using (stream)
            {
                //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
                XSSFWorkbook xssfworkbook = new XSSFWorkbook(stream);
                //获取excel的第一个sheet
                ISheet sheet = xssfworkbook.GetSheetAt(0);
                //获取sheet的首行                
                IRow headerRow = sheet.GetRow(sheet.FirstRowNum);
                //一行最后一个方格的编号 即总的列数
                int cellCount = headerRow.LastCellNum;
                //最后一列的标号  即总的行数
                int rowCount = sheet.LastRowNum;

                var properties = typeof(T).GetProperties();
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null)
                    {
                        break;
                    }
                    T modelObj = new T();

                    int curColumn = 0;
                    foreach (PropertyInfo pi in properties)
                    {
                        if (!excludeFields.IsEmptyCollection() && excludeFields.Contains(pi.Name))
                        {
                            continue;
                        }
                        if (!includeFields.IsEmptyCollection() && !includeFields.Contains(pi.Name))
                        {
                            continue;
                        }
                        CellType cellType = ExcelCellUtil.GetCellType(pi.PropertyType.FullName);
                        var cellValue = ExcelCellUtil.GetValueType(row.GetCell(curColumn) as XSSFCell, cellType);
                        if (!pi.PropertyType.IsEnum)
                        {
                            ReflectUtil.SetObjectPropertyValue(modelObj, pi, cellValue);
                        }
                        else
                        {
                            object obj = EnumUtil.ToEnumByDisplayName(cellValue?.ToString(), pi.PropertyType);
                            pi.SetValue(modelObj, obj, null);
                        }
                        if (++curColumn >= cellCount) break;
                    }
                    listData.Add(modelObj);
                }
                xssfworkbook = null;
                sheet = null;
            }
            return listData;
        }

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="columnIndexs">需要设置的列宽表</param>
        /// <param name="excludeFields">需要排除的字段列表</param>
        public static XSSFWorkbook ToExcel(IList<T> exportData, string sheetName, string[] includeFields = null, string[] headerTexts = null)
        {
            return ToExcel(exportData, sheetName, headerTexts, null, null);
        }

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="columnWidths">需要设置的列宽表,起始列为0</param>
        /// <param name="excludeFields">需要排除的字段列表</param>
        public static XSSFWorkbook ToExcel(IList<T> exportData, string sheetName, string[] headerTexts, Dictionary<int, int> columnWidths = null,
            string[] includeFields = null, string[] excludeFields = null)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(sheetName);

            if (headerTexts.IsEmptyCollection())
            {
                headerTexts = GetHeaderText();
            }

            if (columnWidths.IsEmptyCollection())
            {
                foreach (var item in columnWidths)
                {
                    sheet.SetColumnWidth(item.Key, (int)((item.Value + 0.72) * 256));
                }
            }
            else
            {
                for (int i = 0; i < headerTexts.Length; i++)
                {
                    sheet.AutoSizeColumn(i + 1);
                }
            }
            ICellStyle headerStyle = ExcelCellUtil.CreateHeaderCellStyle(workbook);
            ICellStyle itemStyle = ExcelCellUtil.CreateItemCellStyle(workbook);
            IDataFormat dataStyle = workbook.CreateDataFormat();

            IRow headerRow = ExcelCellUtil.CreateHeaderRow(sheet, headerStyle, headerTexts);
            IRow lastRow = null;
            var properties = typeof(T).GetProperties();
            for (int i = 0; i < exportData.Count; i++)
            {
                T modelObj = exportData[i];
                lastRow = sheet.CreateRow(i + 1);
                int index = 0;
                for (int pindex = 0; pindex < properties.Length; pindex++)
                {
                    PropertyInfo pi = properties[pindex];
                    if (excludeFields.IsEmptyCollection() && excludeFields.Contains(pi.Name))
                    {
                        continue;
                    }
                    if (includeFields.IsEmptyCollection() && !includeFields.Contains(pi.Name))
                    {
                        continue;
                    }

                    object obj = pi.GetValue(modelObj, null);
                    //处理枚举值
                    if (pi.PropertyType.IsEnum)
                    {
                        obj = ((Enum)obj).GetDisplayName();
                    }

                    CellType cellType = ExcelCellUtil.GetCellType(pi.Name);
                    ICell cell = ExcelCellUtil.CreateCell(lastRow, cellType, index++, obj, itemStyle, dataStyle);
                }
            }
            //if (lastRow != null)
            //{
            //    for (int i = 0; i < lastRow.Cells.Count; i++)
            //    {
            //        sheet.AutoSizeColumn(i);
            //    }
            //}
            return workbook;
        }


        #region 2021.12.1扩展

        /// <summary>
        /// 创建空工作薄
        /// </summary>
        public static XSSFWorkbook CreateEmptryWorkbook()
        {
            return new XSSFWorkbook();
        }

        /// <summary>
        /// 向Workbook中添加 sheet
        /// </summary>
        public static void AppendEmptySheetToWorkbook(ISheet sheet, ref XSSFWorkbook workbook)
        {
            if (sheet != null)
            {
                workbook.Add(sheet);
            }
        }

        /// <summary>
        /// 创建Sheet并填充List集合中的数据
        /// </summary>
        /// <param name="excludeFields">需要排除的字段列表</param>
        public static void AppendSheetToWorkbook(IList<T> exportData, XSSFWorkbook workbook, string sheetName, string[] headerTexts = null,
            string[] includeFields = null, string[] excludeFields = null)
        {
            ISheet sheet = workbook.CreateSheet(sheetName);
            if (headerTexts.IsEmptyCollection())
            {
                headerTexts = GetHeaderText();
            }

            for (int i = 0; i < headerTexts.Length; i++)
            {
                sheet.AutoSizeColumn(i + 1);
            }

            ICellStyle headerStyle = ExcelCellUtil.CreateHeaderCellStyle(workbook);
            ICellStyle itemStyle = ExcelCellUtil.CreateItemCellStyle(workbook);
            IDataFormat dataStyle = workbook.CreateDataFormat();

            IRow headerRow = ExcelCellUtil.CreateHeaderRow(sheet, headerStyle, headerTexts);
            IRow lastRow = null;

            #region 填写Sheet数据            
            var properties = typeof(T).GetProperties();
            for (int i = 0; i < exportData.Count; i++)
            {
                T modelObj = exportData[i];
                lastRow = sheet.CreateRow(i + 1);
                int index = 0;
                for (int pindex = 0; pindex < properties.Length; pindex++)
                {
                    PropertyInfo pi = properties[pindex];
                    if (excludeFields.IsEmptyCollection() && excludeFields.Contains(pi.Name))
                    {
                        continue;
                    }
                    if (includeFields.IsEmptyCollection() && !includeFields.Contains(pi.Name))
                    {
                        continue;
                    }

                    object obj = pi.GetValue(modelObj, null);
                    //处理枚举值
                    if (pi.PropertyType.IsEnum)
                    {
                        obj = ((Enum)obj).GetDisplayName();
                    }

                    CellType cellType = ExcelCellUtil.GetCellType(pi.Name);
                    ICell cell = ExcelCellUtil.CreateCell(lastRow, cellType, index++, obj, itemStyle, dataStyle);
                }
            }
            #endregion            
        }

        /// <summary>
        /// 定制化方法，电子回单》未达账项查询导出“银行余额调节表”
        /// </summary>
        public static void AppendOutStandingAjuestDataSheetToWorkbook(IDictionary<string, string> exportData,
            XSSFWorkbook workbook, string sheetName, string sheetTitle)
        {
            ISheet sheet = workbook.CreateSheet(sheetName);

            //定义样式
            IDataFormat dataFormat = workbook.CreateDataFormat();

            ICellStyle titleCellStyle = ExcelCellUtil.CreateHeaderCellStyle(workbook);
            titleCellStyle.GetFont(workbook).FontHeightInPoints = 16;
            titleCellStyle.BorderBottom = BorderStyle.None;
            titleCellStyle.BorderLeft = BorderStyle.None;
            titleCellStyle.BorderRight = BorderStyle.None;
            titleCellStyle.BorderTop = BorderStyle.None;

            ICellStyle headerCellStyle = ExcelCellUtil.CreateHeaderCellStyle(workbook);

            ICellStyle headerAlignRightCellStyle = ExcelCellUtil.CreateHeaderCellStyle(workbook);
            headerAlignRightCellStyle.Alignment = HorizontalAlignment.Left;

            ICellStyle itemCellStyle = ExcelCellUtil.CreateItemCellStyle(workbook);

            IFont dataFont = ExcelCellUtil.CreateFont(workbook, FontBoldWeight.Bold, System.Drawing.Color.Blue, FontName.SimSun);
            ICellStyle itemDataCellStyle = ExcelCellUtil.CreateCellStyle(workbook, dataFont, HorizontalAlignment.Right);
            itemDataCellStyle.DataFormat = dataFormat.GetFormat("0.00");
            itemDataCellStyle.GetFont(workbook).Color = new XSSFColor(new byte[] { (byte)0, (byte)0, (byte)255 }).Indexed;
            var headRow = ExcelCellUtil.CreateHeaderRow(sheet, titleCellStyle, new string[] { sheetTitle });
            //设置行高
            headRow.Height = 3 * 200;
            ExcelCellUtil.CreateCell(headRow, CellType.Blank, 1, null, titleCellStyle, dataFormat);
            ExcelCellUtil.CreateCell(headRow, CellType.Blank, 2, null, titleCellStyle, dataFormat);
            ExcelCellUtil.CreateCell(headRow, CellType.Blank, 3, null, titleCellStyle, dataFormat);
            //合并单元格
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 3));

            #region 填写数据
            if (exportData != null && exportData.Count > 0)
            {
                int rowIndex = 1;
                int dicIndex = 0;
                IRow curRow = sheet.CreateRow(rowIndex);
                //row:1
                ExcelCellUtil.CreateCell(curRow, CellType.String, 0, $"{exportData.Keys.ElementAt(dicIndex)}{exportData.Values.ElementAt(dicIndex)}", headerAlignRightCellStyle, dataFormat);
                ExcelCellUtil.CreateCell(curRow, CellType.Blank, 1, null, headerCellStyle, dataFormat);
                ExcelCellUtil.CreateCell(curRow, CellType.Blank, 2, null, headerCellStyle, dataFormat);
                ExcelCellUtil.CreateCell(curRow, CellType.Blank, 3, null, headerCellStyle, dataFormat);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowIndex, rowIndex, 0, 3));
                //row:2
                rowIndex++;
                dicIndex++;
                curRow = sheet.CreateRow(rowIndex);
                ExcelCellUtil.CreateCell(curRow, CellType.String, 0, $"{exportData.Keys.ElementAt(dicIndex)}{exportData.Values.ElementAt(dicIndex)}", headerAlignRightCellStyle, dataFormat);
                ExcelCellUtil.CreateCell(curRow, CellType.Blank, 1, null, headerCellStyle, dataFormat);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowIndex, rowIndex, 0, 1));
                dicIndex++;
                ExcelCellUtil.CreateCell(curRow, CellType.String, 2, $"{exportData.Keys.ElementAt(dicIndex)}{exportData.Values.ElementAt(dicIndex)}", headerAlignRightCellStyle, dataFormat);
                ExcelCellUtil.CreateCell(curRow, CellType.Blank, 3, null, headerCellStyle, dataFormat);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowIndex, rowIndex, 2, 3));

                //row:3
                rowIndex++;
                dicIndex++;
                curRow = sheet.CreateRow(rowIndex);
                ExcelCellUtil.CreateCell(curRow, CellType.String, 0, $"{exportData.Keys.ElementAt(dicIndex)}{exportData.Values.ElementAt(dicIndex)}", headerCellStyle, dataFormat);
                ExcelCellUtil.CreateCell(curRow, CellType.Blank, 1, null, headerCellStyle, dataFormat);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowIndex, rowIndex, 0, 1));
                dicIndex++;
                ExcelCellUtil.CreateCell(curRow, CellType.String, 2, $"{exportData.Keys.ElementAt(dicIndex)}{exportData.Values.ElementAt(dicIndex)}", headerCellStyle, dataFormat);
                ExcelCellUtil.CreateCell(curRow, CellType.Blank, 3, null, headerCellStyle, dataFormat);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowIndex, rowIndex, 2, 3));

                //row:4 -  row:8                
                for (int i = 4; i < 8; i++)
                {
                    curRow = sheet.CreateRow(i);
                    dicIndex++;
                    ExcelCellUtil.CreateCell(curRow, CellType.String, 0, exportData.Keys.ElementAt(dicIndex), itemCellStyle, dataFormat);
                    ExcelCellUtil.CreateCell(curRow, CellType.Numeric, 1, exportData.Values.ElementAt(dicIndex), itemDataCellStyle, dataFormat);
                    dicIndex++;
                    ExcelCellUtil.CreateCell(curRow, CellType.String, 2, exportData.Keys.ElementAt(dicIndex), itemCellStyle, dataFormat);
                    ExcelCellUtil.CreateCell(curRow, CellType.Numeric, 3, exportData.Values.ElementAt(dicIndex), itemDataCellStyle, dataFormat);
                }

                dicIndex++;
                curRow = sheet.CreateRow(8);
                ExcelCellUtil.CreateCell(curRow, CellType.String, 0, exportData.Keys.ElementAt(dicIndex), itemCellStyle, dataFormat);
                ExcelCellUtil.CreateCell(curRow, CellType.Numeric, 1, exportData.Values.ElementAt(dicIndex), itemDataCellStyle, dataFormat);
            }

            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);
            sheet.AutoSizeColumn(3);

            #endregion
        }

        #endregion


        #endregion

        #region dataTable

        /// <summary>
        /// 导出DataTable到excel中
        /// </summary>
        public static XSSFWorkbook ToExcel(DataTable exportData, string sheetName, string[] headerTexts, string[] excludeFields = null)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(sheetName);

            ICellStyle headerStyle = ExcelCellUtil.CreateHeaderCellStyle(workbook);
            ICellStyle itemStyle = ExcelCellUtil.CreateItemCellStyle(workbook);
            IDataFormat dataStyle = workbook.CreateDataFormat();

            if (headerTexts == null || headerTexts.Length == 0)
            {
                int columnNums = exportData.Columns.Count;
                headerTexts = new string[columnNums];
                for (int i = 0; i < columnNums; i++)
                {
                    headerTexts[i] = exportData.Columns[i].ColumnName;
                }
            }
            IRow headerRow = ExcelCellUtil.CreateHeaderRow(sheet, headerStyle, headerTexts);
            if (headerRow != null)
            {
                for (int i = 0; i < headerRow.Cells.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
            }
            IRow lastRow = null;
            if (exportData != null)
            {
                for (int i = 0; i < exportData.Rows.Count; i++)
                {
                    lastRow = sheet.CreateRow(i + 1);
                    int index = 0;
                    for (int pindex = 0; pindex < exportData.Columns.Count; pindex++)
                    {
                        if (!excludeFields.IsEmptyCollection()  && excludeFields.Contains(exportData.Columns[pindex].ColumnName))
                        {
                            continue;
                        }

                        object obj = exportData.Rows[i][pindex];
                        if (obj is byte[])
                        {
                            obj = System.Text.Encoding.Default.GetString(obj as byte[]);
                        }
                        CellType cellType = ExcelCellUtil.GetCellType(exportData.Columns[pindex].DataType.Name);
                        ICell cell = ExcelCellUtil.CreateCell(lastRow, cellType, index++, obj, itemStyle, dataStyle);
                    }

                }
            }

            //if (lastRow != null)
            //{
            //    for (int i = 0; i < lastRow.Cells.Count; i++)
            //    {
            //        sheet.AutoSizeColumn(i);
            //    }
            //}

            return workbook;
        }

        /// <summary>
        /// 向Workbook 中添加 sheet
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="exportData"></param>
        /// <param name="sheetName"></param>
        /// <param name="headerTexts"></param>
        /// <param name="excludeFields"></param>
        /// <returns></returns>
        public static XSSFWorkbook AddSheetToWorkbook(XSSFWorkbook workbook, DataTable exportData, string sheetName, string[] headerTexts, string[] excludeFields = null)
        {

            ISheet sheet = workbook.CreateSheet(sheetName);

            ICellStyle headerStyle = ExcelCellUtil.CreateHeaderCellStyle(workbook);
            ICellStyle itemStyle = ExcelCellUtil.CreateItemCellStyle(workbook);
            IDataFormat dataStyle = workbook.CreateDataFormat();

            if (headerTexts == null || headerTexts.Length == 0)
            {
                int columnNums = exportData.Columns.Count;
                headerTexts = new string[columnNums];
                for (int i = 0; i < columnNums; i++)
                {
                    headerTexts[i] = exportData.Columns[i].ColumnName;
                }
            }
            IRow headerRow = ExcelCellUtil.CreateHeaderRow(sheet, headerStyle, headerTexts);
            if (headerRow != null)
            {
                for (int i = 0; i < headerRow.Cells.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
            }
            IRow lastRow = null;
            if (exportData != null)
            {
                for (int i = 0; i < exportData.Rows.Count; i++)
                {
                    lastRow = sheet.CreateRow(i + 1);
                    int index = 0;
                    for (int pindex = 0; pindex < exportData.Columns.Count; pindex++)
                    {
                        if (!excludeFields.IsEmptyCollection() && excludeFields.Contains(exportData.Columns[pindex].ColumnName))
                        {
                            continue;
                        }

                        object obj = exportData.Rows[i][pindex];
                        CellType cellType = ExcelCellUtil.GetCellType(exportData.Columns[pindex].DataType.Name);
                        ICell cell = ExcelCellUtil.CreateCell(lastRow, cellType, index++, obj, itemStyle, dataStyle);
                    }
                }
            }
            return workbook;
        }

        /// <summary>
        /// 导出DataTable到excel中
        /// </summary>
        public static byte[] ExportToExcelWithoutHeaderRow(DataTable exportData, string sheetName)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(sheetName);

            ICellStyle headerStyle = ExcelCellUtil.CreateHeaderCellStyle(workbook);
            ICellStyle itemStyle = ExcelCellUtil.CreateItemCellStyle(workbook);
            IDataFormat dataStyle = workbook.CreateDataFormat();

            int columnNums = exportData.Columns.Count;
            IRow lastRow = null;
            for (int i = 0; i < exportData.Rows.Count; i++)
            {
                lastRow = sheet.CreateRow(i);
                int index = 0;
                for (int pindex = 0; pindex < exportData.Columns.Count; pindex++)
                {
                    object obj = exportData.Rows[i][pindex];
                    CellType cellType = ExcelCellUtil.GetCellType(exportData.Columns[pindex].DataType.Name);
                    ICell cell = ExcelCellUtil.CreateCell(lastRow, cellType, index++, obj, itemStyle, dataStyle);
                }
            }
            return ConvertToBytes(workbook);
        }
        #endregion

        #endregion

        #region 扩展

        /// <summary>
        /// 导出Excel文件,包含批注(用于生成模板文件)
        /// </summary>
        public static byte[] CreateTemplateExcel(string sheetName, List<string> headTextList, List<string> commentList)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(sheetName);

            if (headTextList.IsEmptyCollection())
            {
                return null;
            }

            ICellStyle headerStyle = ExcelCellUtil.CreateHeaderCellStyle(workbook);
            var patr = sheet.CreateDrawingPatriarch();
            var font = workbook.CreateFont();
            font.FontHeightInPoints = 10;

            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < headTextList.Count; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                cell.SetCellValue(headTextList[i]);
                cell.CellStyle = headerStyle;

                if (commentList != null && commentList.Count >= i && !string.IsNullOrWhiteSpace(commentList[i]))
                {
                    var cellComment = patr.CreateCellComment(new XSSFClientAnchor(0, 0, 0, 0, i, 0, i + 3, 1));
                    var rtxt = new XSSFRichTextString(commentList[i]);
                    rtxt.ApplyFont(font);
                    cellComment.String = rtxt;
                    cell.CellComment = cellComment;

                }
            }
            return ConvertToBytes(workbook);
        }

        /// <summary>
        /// 导入excel数据,并将指定列位置的值存入相应的字符串的字段中
        /// </summary>
        public static List<T> ToListHasArrary(System.IO.Stream stream, string[] excludeFields = null, string arrFieldName = null, int fromColIndex = -1, int endColIndex = -1)
        {
            List<T> listData = new List<T>();
            using (stream)
            {
                //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
                XSSFWorkbook xssfworkbook = new XSSFWorkbook(stream);
                //获取excel的第一个sheet
                ISheet sheet = xssfworkbook.GetSheetAt(0);
                //获取sheet的首行                
                IRow headerRow = sheet.GetRow(sheet.FirstRowNum);
                if (headerRow == null)
                {
                    return null;
                }
                //一行最后一个方格的编号 即总的列数
                int cellCount = headerRow.LastCellNum;
                //最后一列的标号  即总的行数
                int rowCount = sheet.LastRowNum;

                var properties = typeof(T).GetProperties();
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null)
                    {
                        continue;
                    }
                    T modelObj = new T();
                    int curColumn = 0;
                    foreach (PropertyInfo pi in properties)
                    {
                        if (!string.IsNullOrWhiteSpace(arrFieldName) && fromColIndex < endColIndex && pi.Name == arrFieldName)
                        {
                            List<string> arrValue = new List<string>();
                            for (int k = fromColIndex; k <= endColIndex && curColumn >= fromColIndex && curColumn < endColIndex; k++)
                            {
                                CellType cellTypeArr = ExcelCellUtil.GetCellType(pi.PropertyType.FullName);
                                var cvItem = ExcelCellUtil.GetValueType(row.GetCell(curColumn) as XSSFCell, cellTypeArr);
                                arrValue.Add(cvItem?.ToString());
                                curColumn++;
                            }

                            try { pi.SetValue(modelObj, arrValue, null); }
                            catch (Exception) { }

                            if (curColumn >= cellCount) break;
                            else continue;
                        }
                        if (!excludeFields.IsEmptyCollection() && excludeFields.Contains(pi.Name))
                        {
                            continue;
                        }

                        CellType cellType = ExcelCellUtil.GetCellType(pi.PropertyType.FullName);
                        var cellValue = ExcelCellUtil.GetValueType(row.GetCell(curColumn) as XSSFCell, cellType);
                        if (!pi.PropertyType.IsEnum)
                        {
                            ReflectUtil.SetObjectPropertyValue(modelObj, pi, cellValue);
                        }
                        else
                        {
                            object obj = EnumUtil.ToEnumByDisplayName(cellValue?.ToString().Trim(), pi.PropertyType);
                            pi.SetValue(modelObj, obj, null);
                        }
                        if (++curColumn >= cellCount) break;
                    }
                    listData.Add(modelObj);
                }
                xssfworkbook = null;
                sheet = null;
            }
            return listData;
        }

        #endregion

        #region 转换成Byte[]

        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <param name="exportData">导出的数据源</param>
        /// <param name="sheetName">表名</param>
        /// <param name="excludeFields">排除的字段属性列表</param>
        /// <returns></returns>
        public static byte[] ExportToExcel(IList<T> exportData, string sheetName, string[] headerTexts, Dictionary<int, int> columnWidths,
            string[] includeFields = null, string[] excludeFields = null)
        {
            var dic = new Dictionary<int, int>();
            XSSFWorkbook book = ToExcel(exportData, sheetName, headerTexts, columnWidths, includeFields, excludeFields);
            return ConvertToBytes(book);
        }

        /// <summary>
        /// 导出Excel文件，根据定义的字段DisplayName属性生成表头
        /// </summary>
        /// <param name="exportData">导出的数据源</param>
        /// <param name="sheetName">表名</param>
        /// <param name="excludeFields">排除的字段属性列表</param>
        /// <returns></returns>
        public static byte[] ExportToExcel(IList<T> exportData, string sheetName, params string[] excludeFields)
        {
            string[] headText = GetHeaderText(excludeFields);
            return ExportToExcel(exportData, sheetName, headText, null, null, excludeFields);
        }
        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <param name="exportData">导出的数据源</param>
        /// <param name="sheetName">表名</param>
        /// <param name="excludeFields">排除的字段属性列表</param>
        /// <returns></returns>
        public static byte[] ExportToExcel(DataTable exportData, string sheetName, string[] headerTexts, string[] excludeFields = null)
        {
            XSSFWorkbook book = ToExcel(exportData, sheetName, headerTexts, excludeFields);
            return ConvertToBytes(book);
        }

        /// <summary>
        /// 导出Excel文件，根据定义的字段DisplayName属性生成表头
        /// </summary>
        /// <param name="exportData">导出的数据源</param>
        /// <param name="sheetName">表名</param>
        /// <param name="excludeFields">排除的字段属性列表</param>
        /// <returns></returns>
        public static byte[] ExportToExcel(DataTable exportData, string sheetName, string[] excludeFields = null)
        {
            string[] headText = GetHeaderText();
            return ExportToExcel(exportData, sheetName, headText, excludeFields);
        }

        /// <summary>
        /// 导出多个sheet
        /// </summary>
        /// <param name="exportDataSet"></param>
        /// <param name="excludeFields"></param>
        /// <returns></returns>
        public static byte[] ExportToExcel(DataSet exportDataSet, string[] excludeFields = null)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            foreach (DataTable dt in exportDataSet.Tables)
            {
                string[] headText = GetHeaderText();
                AddSheetToWorkbook(workbook, dt, dt.TableName, headText, excludeFields);
            }
            return ConvertToBytes(workbook);
        }

        public static byte[] ConvertToBytes(XSSFWorkbook book)
        {
            byte[] buffer = null;
            using (MemoryStream ms = new MemoryStream())
            {
                book.Write(ms);
                buffer = ms.ToArray();
                ms.Flush();
            }
            return buffer;
        }
        public static string[] GetHeaderText(params string[] excludeFields)
        {
            var properties = typeof(T).GetProperties();
            if (properties.IsEmptyCollection())
            {
                return null;
            }
            List<string> list = new List<string>();
            foreach (var item in properties)
            {
                if (!excludeFields.IsEmptyCollection() && excludeFields.Contains(item.Name, StringComparer.OrdinalIgnoreCase))
                {
                    continue;
                }
                var attr = item.GetCustomAttribute(typeof(DisplayNameAttribute));
                list.Add(ObjectUtil.IsNull(attr) ? item.Name : ((DisplayNameAttribute)attr).DisplayName);
            }
            return list.ToArray<string>();
        }


        #endregion
    }

    #endregion

    #region ExcelCellUtil
    class ExcelCellUtil
    {
        #region 常量
        private const string DateTimeFormat = "yyyy/M/d";
        #endregion

        #region Get Cell Type

        /// <summary>
        /// 获取属性类型对应的Excel单元格类型
        /// </summary>
        public static CellType GetCellType(string typeName)
        {
            if (typeName == "System.String")
            {
                return CellType.String;
            }
            else if (typeName == "System.Int16")
            {
                return CellType.Numeric;
            }
            else if (typeName == "System.Int32")
            {
                return CellType.Numeric;
            }
            else if (typeName == "System.Int64")
            {
                return CellType.Numeric;
            }
            else if (typeName == "System.Single")
            {
                return CellType.String;
            }
            else if (typeName == "System.Double")
            {
                return CellType.Numeric;
            }
            else if (typeName == "System.Decimal")
            {
                return CellType.Numeric;
            }
            else if (typeName == "System.Char")
            {
                return CellType.String;
            }
            else if (typeName == "System.Boolean")
            {
                return CellType.Boolean;
            }
            else if (typeName == "System.DateTime")
            {
                return CellType.Numeric;
            }
            //可空类型
            else if (typeName.StartsWith("System.Nullable`1[[System.DateTime"))
            {
                return CellType.Numeric;
            }
            else if (typeName.StartsWith("System.Nullable`1[[System.Int16"))
            {
                return CellType.Numeric;
            }
            else if (typeName.StartsWith("System.Nullable`1[[System.Int32"))
            {
                return CellType.Numeric;
            }
            else if (typeName.StartsWith("System.Nullable`1[[System.Int64"))
            {
                return CellType.Numeric;
            }
            else if (typeName.StartsWith("System.Nullable`1[[System.Decimal"))
            {
                return CellType.Numeric;
            }
            else if (typeName.StartsWith("System.Nullable`1[[System.Boolean"))
            {
                return CellType.Boolean;
            }
            else if (typeName.StartsWith("System.Nullable`1[[System.Double"))
            {
                return CellType.Numeric;
            }
            else if (typeName.StartsWith("System.Nullable`1[[System.Decimal"))
            {
                return CellType.Numeric;
            }
            else
            {
                return CellType.String;
            }
        }

        #endregion

        #region Get Cell Value

        public static object GetValueType(XSSFCell cell, CellType type)
        {
            try
            {
                if (cell == null)
                    return null;
                CellType resultType = cell.CellType;
                if (resultType == CellType.Formula)
                {
                    resultType = type;
                }
                switch (resultType)
                {
                    case CellType.Blank:
                    case CellType.Unknown: //BLANK&Unknown:
                        return null;
                    case CellType.Numeric:
                        return GetCellNumericValue(cell);
                    case CellType.Boolean:
                        return cell.BooleanCellValue;
                    case CellType.String:
                        return GetCellStringValue(cell);
                    case CellType.Formula:
                        return GetCellStringValue(cell);
                    default:
                        return null;
                }
            }
            catch
            {
                string errorMsg = string.Format("单元格{0}{1}数据格式不正确.",
                    (char)(cell.ColumnIndex + 'A'), cell.RowIndex + 1);
                throw new Exception(errorMsg);
            }
        }

        public static object GetCellNumericValue(XSSFCell cell)
        {
            try
            {
                if (DateUtil.IsCellDateFormatted(cell))
                {
                    return cell.DateCellValue;
                }
                else
                {
                    return cell.NumericCellValue;
                }
            }
            catch
            {
                return cell.StringCellValue;
            }
        }

        public static object GetCellStringValue(XSSFCell cell)
        {
            try
            {
                return cell.StringCellValue;
            }
            catch
            {
                return GetCellNumericValue(cell);
            }
        }

        #endregion

        #region Set Excel Cell Value

        public static ICell CreateCell(IRow row, CellType cellType, int index, object obj, ICellStyle cellStyle, IDataFormat dataStyle)
        {
            row.CreateCell(index);
            ICell cell = null;
            try
            {
                switch (cellType)
                {
                    case CellType.Numeric:
                        cell = row.CreateCell(index, CellType.Numeric);
                        if (ObjectUtil.isDateTime(obj))
                        {
                            cell.CellStyle = cellStyle;
                            cell.CellStyle.DataFormat = dataStyle.GetFormat(ExcelCellUtil.DateTimeFormat);
                            cell.SetCellValue(ObjectUtil.ToDateTime(obj, DateTime.MinValue));
                        }
                        else
                        {
                            cell = row.CreateCell(index, CellType.Numeric);
                            cell.CellStyle = cellStyle;
                            cell.SetCellValue(ObjectUtil.ToDouble(obj, 0));
                        }
                        break;
                    case CellType.Boolean:
                        cell = row.CreateCell(index, CellType.Boolean);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(ObjectUtil.ToBool(obj, false));
                        break;
                    case CellType.String:
                    case CellType.Error:
                    case CellType.Blank:
                    case CellType.Formula:
                    default:
                        cell = row.CreateCell(index, CellType.String);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(obj.ToString());
                        break;
                }
            }
            catch
            {
                cell = row.CreateCell(index, CellType.String);
                cell.CellStyle = cellStyle;
                cell.SetCellValue(ObjectUtil.NullToEmpty(obj));
            }
            return cell;
        }

        #endregion

        #region Cell Style   

        public static ICellStyle CreateCellStyle(XSSFWorkbook workbook,
            IFont font,
            HorizontalAlignment textHorizaontalAlign = HorizontalAlignment.Left,
            VerticalAlignment textVerticalAlignment = VerticalAlignment.Center,
            bool Bordered = true,
            bool WrapText = false,
            string dataFormatString = null)
        {
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.Alignment = textHorizaontalAlign;
            cellStyle.VerticalAlignment = textVerticalAlignment;
            cellStyle.SetFont(font);
            #region 设置边框         
            if (Bordered)
            {
                cellStyle.BorderBottom = BorderStyle.Thin;
                cellStyle.BorderLeft = BorderStyle.Thin;
                cellStyle.BorderRight = BorderStyle.Thin;
                cellStyle.BorderTop = BorderStyle.Thin;
                short colorIndex = GetColor(0, 0, 0);
                cellStyle.LeftBorderColor = colorIndex;
                cellStyle.RightBorderColor = colorIndex;
                cellStyle.BottomBorderColor = colorIndex;
                cellStyle.TopBorderColor = colorIndex;
            }
            #endregion
            cellStyle.WrapText = WrapText;
            if (!string.IsNullOrWhiteSpace(dataFormatString))
            {
                IDataFormat datastyle = workbook.CreateDataFormat();
                cellStyle.DataFormat = datastyle.GetFormat("yyyy-MM-dd");
            }
            return cellStyle;
        }

        public static ICellStyle CreateDataFormatCellStyle(XSSFWorkbook workbook, string dataFormatString)
        {
            IFont font = CreateFont(workbook);
            return CreateCellStyle(workbook, font, HorizontalAlignment.Left, VerticalAlignment.Center,
                true, false, dataFormatString);
        }

        public static ICellStyle CreateItemCellStyle(XSSFWorkbook workbook)
        {
            IFont font = CreateFont(workbook);
            return CreateCellStyle(workbook, font);
        }

        public static ICellStyle CreateHeaderCellStyle(XSSFWorkbook workbook)
        {
            IFont font = CreateFont(workbook, FontBoldWeight.Bold);
            return CreateCellStyle(workbook, font, HorizontalAlignment.Center, VerticalAlignment.Center, true, false, null);
        }

        #endregion

        #region 创建文本样式
        public static IFont CreateFont(XSSFWorkbook workbook)
        {
            return CreateFont(workbook, FontBoldWeight.Normal, System.Drawing.Color.Black, FontName.SimSun);
        }

        public static IFont CreateFont(XSSFWorkbook workbook, FontBoldWeight fontBoldWeight)
        {
            return CreateFont(workbook, fontBoldWeight, System.Drawing.Color.Black, FontName.SimSun);
        }

        public static IFont CreateFont(XSSFWorkbook workbook, FontBoldWeight fontBoldWeight, System.Drawing.Color color,
            FontName fontName)
        {
            IFont font = workbook.CreateFont();
            //2.4.1 font Issue solution
            //font.FontHeight = workbook.GetFontAt(0).FontHeight;
            font.Boldweight = (short)fontBoldWeight;
            font.Color = new XSSFColor(color).Indexed;
            font.FontName = ConvertFontName(fontName);
            return font;
        }

        private static string ConvertFontName(FontName fontName)
        {
            var attr = fontName.GetType().GetCustomAttribute(typeof(DescriptionAttribute), false);
            return attr == null ? fontName.ToString() : ((DescriptionAttribute)attr).Description;
        }

        #endregion

        #region 创建颜色
        public static short GetColor(byte r, byte g, byte b)
        {
            XSSFColor color = new XSSFColor(new byte[] { r, g, b });
            return color.Indexed;
        }
        #endregion

        #region 创建标题行
        /// <summary>
        /// 创建标题行
        /// </summary>
        public static IRow CreateHeaderRow(ISheet sheet, string[] headers)
        {
            ICellStyle style = CreateHeaderCellStyle(sheet.Workbook as XSSFWorkbook);
            return CreateHeaderRow(sheet, headers);
        }

        /// <summary>
        /// 创建标题行
        /// </summary>
        public static IRow CreateHeaderRow(ISheet sheet, ICellStyle style, string[] headers)
        {
            IRow headerRow = sheet.CreateRow(0);

            int i = 0;
            foreach (string headerItem in headers)
            {
                ICell cell = headerRow.CreateCell(i++);
                cell.SetCellValue(headerItem);
                cell.CellStyle = style;
                //sheet.AutoSizeColumn(i);
            }
            return headerRow;
        }
        #endregion


    }

    #endregion

    #region ExcleEnums
    public enum FontName
    {
        SimSun,//宋体
        Arial,
        [Description("Times New Roman")]
        TimesNewRoman,
        [Description("Microsoft YaHei")]
        MicrosoftYaHei,//微软雅黑
        SimHei,//黑体
        KaiTi//楷体
    }
    #endregion

    #region Excel 处理
    public class ExcelProcessing
    {
        /// <summary>
        /// 2007exel
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="stream"></param>
        /// <param name="fieldsT1"></param>
        /// <param name="fieldsT2"></param>
        /// <param name="fieldsT3"></param>
        /// <param name="ConditionT1"></param>
        /// <param name="ConditionT2"></param>
        /// <param name="ConditionT3"></param>
        /// <param name="suppler"></param>
        /// <param name="t1l"></param>
        /// <param name="t12"></param>
        /// <param name="t13"></param>
        public static void Excel2007ToList<T1, T2, T3>(System.IO.Stream stream, Dictionary<string, List<KeyValuePair<string, string>>> fieldsT1,
                                                                            Dictionary<string, List<KeyValuePair<string, string>>> fieldsT2,
                                                                            Dictionary<string, List<KeyValuePair<string, string>>> fieldsT3,
                                                                            Func<string, bool> ConditionT1,
                                                                            Func<string, bool> ConditionT2,
                                                                            Func<string, bool> ConditionT3,
                                                                            string suppler,
                                                                            out List<T1> t1l,
                                                                            out List<T2> t12,
                                                                            out List<T3> t13,
                                                                            bool isKey = true
                                                                            ) where T1 : new() where T2 : new() where T3 : new()
        {
            XSSFWorkbook xssfworkbook = new XSSFWorkbook(stream);
            List<T1> temp1 = new List<T1>();
            List<T2> temp2 = new List<T2>();
            List<T3> temp3 = new List<T3>();
            var num = 0;
            num = xssfworkbook.NumberOfSheets;
            for (int i = 0; i < num; ++i)
            {
                XSSFSheet sheet = (XSSFSheet)xssfworkbook.GetSheetAt(i);
                if (ConditionT1(sheet.SheetName) && fieldsT1.ContainsKey(suppler))
                    temp1.AddRange(ToEntityList<T1>((ISheet)sheet, fieldsT1[suppler], isKey));
                if (ConditionT2(sheet.SheetName) && fieldsT2.ContainsKey(suppler))
                    temp2.AddRange(ToEntityList2<T2>((ISheet)sheet, fieldsT2[suppler], isKey));
                if (ConditionT3(sheet.SheetName) && fieldsT3.ContainsKey(suppler))
                    temp3.AddRange(ToEntityList<T3>((ISheet)sheet, fieldsT3[suppler], isKey));
            }
            t1l = temp1;
            t12 = temp2;
            t13 = temp3;
        }

        /// <summary>
        /// 2003exel
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="stream"></param>
        /// <param name="fieldsT1"></param>
        /// <param name="fieldsT2"></param>
        /// <param name="fieldsT3"></param>
        /// <param name="ConditionT1"></param>
        /// <param name="ConditionT2"></param>
        /// <param name="ConditionT3"></param>
        /// <param name="suppler"></param>
        /// <param name="t1l"></param>
        /// <param name="t12"></param>
        /// <param name="t13"></param>
        public static void Excel2003ToList<T1, T2, T3>(System.IO.Stream stream, Dictionary<string, List<KeyValuePair<string, string>>> fieldsT1,
                                                                            Dictionary<string, List<KeyValuePair<string, string>>> fieldsT2,
                                                                            Dictionary<string, List<KeyValuePair<string, string>>> fieldsT3,
                                                                            Func<string, bool> ConditionT1,
                                                                            Func<string, bool> ConditionT2,
                                                                            Func<string, bool> ConditionT3,
                                                                            string suppler,
                                                                            out List<T1> t1l,
                                                                            out List<T2> t12,
                                                                            out List<T3> t13,
                                                                            bool isKey = true
                                                                            ) where T1 : new() where T2 : new() where T3 : new()
        {
            HSSFWorkbook workbook = new HSSFWorkbook(stream);
            List<T1> temp1 = new List<T1>();
            List<T2> temp2 = new List<T2>();
            List<T3> temp3 = new List<T3>();
            var num = 0;
            num = workbook.NumberOfSheets;
            for (int i = 0; i < num; ++i)
            {
                HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(i);
                if (ConditionT1(sheet.SheetName) && fieldsT1.ContainsKey(suppler))
                    temp1.AddRange(ToEntityList<T1>((ISheet)sheet, fieldsT1[suppler], isKey));
                if (ConditionT2(sheet.SheetName) && fieldsT2.ContainsKey(suppler))
                    temp2.AddRange(ToEntityList2<T2>((ISheet)sheet, fieldsT2[suppler], isKey));
                if (ConditionT3(sheet.SheetName) && fieldsT3.ContainsKey(suppler))
                    temp3.AddRange(ToEntityList<T3>((ISheet)sheet, fieldsT3[suppler], isKey));
            }
            t1l = temp1;
            t12 = temp2;
            t13 = temp3;
        }

        /// <summary>
        /// 取sheet页面值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fields"></param>
        /// <param name="stream"></param>
        /// <param name="sheetPage"></param>
        /// <returns></returns>
        public static List<T> SheetToEntityList<T>(List<KeyValuePair<string, string>> fields, System.IO.Stream stream, int sheetPage) where T : new()
        {
            XSSFWorkbook workbook = new XSSFWorkbook(stream);
            XSSFSheet sheet = (XSSFSheet)workbook.GetSheetAt(sheetPage);
            return ToEntityList<T>((ISheet)sheet, fields);
        }

        /// <summary>
        /// Sheet实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheet"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static List<T> ToEntityList<T>(ISheet sheet, List<KeyValuePair<string, string>> fields, bool isKey = true) where T : new()
        {
            if (null == fields)
                return new List<T>();

            int headerRow = 0;
            List<T> enlist = new List<T>();
            bool dataStartRow = false;
            for (int i = 0; i <= sheet.LastRowNum; i++) // 从1开始，第0行为单元头
            {
                if (sheet.GetRow(i) == null)
                    continue;
                if (sheet.GetRow(i).LastCellNum <= 0)
                    continue;
                if (!dataStartRow)
                {
                    if (isKey)
                    {
                        if (sheet.GetRow(i).Cells.Exists(s => (fields.FirstOrDefault().Key.ToLower() == s.ToString().ToLower().TrimEnd())))
                        {
                            headerRow = i;
                            dataStartRow = true;
                            continue;
                        }
                    }
                    else
                    {
                        if (sheet.GetRow(i).Cells.Exists(s => fields.FirstOrDefault().Value == s.ToString().TrimEnd()))
                        {
                            headerRow = i;
                            dataStartRow = true;
                            continue;
                        }
                    }
                }
                else
                {
                    if (isKey)
                    {
                        if (sheet.GetRow(i).Cells.Exists(s => "合计" == s.ToString().TrimEnd() || "总计" == s.ToString().TrimEnd()))
                            continue;
                        T en = new T();
                        string errStr = ""; // 当前行转换时，是否有错误信息，格式为：第1行数据转换异常：XXX列；
                        for (int j = 0; j < fields.Count; j++)
                        {
                            var existField = sheet.GetRow(headerRow).Cells.Find(a => a.ToString().ToLower().TrimEnd() == fields[j].Key.ToLower());
                            if (existField == null)
                                continue;
                            var headerIndex = existField.ColumnIndex;
                            // 3.给指定的属性赋值
                            string propertyName = CheckEntityMemberName(en, fields[j].Key);
                            System.Reflection.PropertyInfo properotyInfo = en.GetType().GetProperty(propertyName);
                            if (properotyInfo != null)
                            {
                                try
                                {
                                    // Excel单元格的值转换为对象属性的值，若类型不对，记录出错信息
                                    properotyInfo.SetValue(en, GetExcelCellToProperty(properotyInfo.PropertyType, sheet.GetRow(i).GetCell(headerIndex)), null);
                                }
                                catch (Exception e)
                                {
                                    if (errStr.Length == 0)
                                    {
                                        errStr = "第" + i + "行数据转换异常：";
                                    }
                                    errStr += fields[j].Key + "列；";
                                }
                            }
                        }
                        // 若有错误信息，就添加到错误信息里
                        if (errStr.Length > 0)
                        {
                            throw new ArgumentException(errStr);
                        }
                        enlist.Add(en);
                    }
                    else
                    {
                        if (sheet.GetRow(i).Cells.Exists(s => "合计" == s.ToString().TrimEnd() || "总计" == s.ToString().TrimEnd()))
                            continue;
                        T en = new T();
                        string errStr = ""; // 当前行转换时，是否有错误信息，格式为：第1行数据转换异常：XXX列；
                        for (int j = 0; j < fields.Count; j++)
                        {
                            var existField = sheet.GetRow(headerRow).Cells.Find(a => !string.IsNullOrEmpty(fields[j].Value) && fields[j].Value == a.ToString().TrimEnd());
                            if (null == existField && fields[j].Value.ToLower().Contains(","))
                            {
                                var t = fields[j].Value.ToLower().Split(',').ToArray();
                                foreach (var e in t)
                                {
                                    existField = sheet.GetRow(headerRow).Cells.Find(a => e == a.ToString().ToLower().TrimEnd());
                                    if (existField != null)
                                        break;
                                }
                            }
                            if (existField == null)
                                continue;
                            var headerIndex = existField.ColumnIndex;
                            // 3.给指定的属性赋值
                            string propertyName = CheckEntityMemberName(en, fields[j].Key);
                            System.Reflection.PropertyInfo properotyInfo = en.GetType().GetProperty(propertyName);
                            if (properotyInfo != null)
                            {
                                try
                                {
                                    // Excel单元格的值转换为对象属性的值，若类型不对，记录出错信息
                                    properotyInfo.SetValue(en, GetExcelCellToProperty(properotyInfo.PropertyType, sheet.GetRow(i).GetCell(headerIndex)), null);
                                }
                                catch (Exception e)
                                {
                                    if (errStr.Length == 0)
                                    {
                                        errStr = "第" + i + "行数据转换异常：";
                                    }
                                    errStr += fields[j].Key + "列；";
                                }
                            }
                        }
                        // 若有错误信息，就添加到错误信息里
                        if (errStr.Length > 0)
                        {
                            throw new ArgumentException(errStr);
                        }
                        enlist.Add(en);
                    }
                }
            }
            return enlist;
        }
        public static List<T> ToEntityList2<T>(ISheet sheet, List<KeyValuePair<string, string>> fields, bool isKey = true) where T : new()
        {
            if (null == fields)
                return new List<T>();


            int headerRow = 0;
            List<T> enlist = new List<T>();
            bool dataStartRow = false;
            for (int i = 0; i <= sheet.LastRowNum; i++) // 从1开始，第0行为单元头
            {
                if (sheet.GetRow(i) == null)
                    continue;
                if (sheet.GetRow(i).LastCellNum <= 0)
                    continue;
                if (!dataStartRow)
                {
                    if (isKey)
                    {
                        if (sheet.GetRow(i).Cells.Exists(s => (fields.FirstOrDefault().Key.ToLower() == s.ToString().ToLower().TrimEnd())))
                        {
                            headerRow = i;
                            dataStartRow = true;
                            continue;
                        }
                    }
                    else
                    {
                        if (sheet.GetRow(i).Cells.Exists(s => fields.FirstOrDefault().Value == s.ToString().TrimEnd()))
                        {
                            headerRow = i;
                            dataStartRow = true;
                            continue;
                        }
                    }
                }
                else
                {
                    if (isKey)
                    {
                        if (sheet.GetRow(i).Cells.Exists(s => "合计" == s.ToString().TrimEnd() || "总计" == s.ToString().TrimEnd()))
                            continue;
                        T en = new T();
                        string errStr = ""; // 当前行转换时，是否有错误信息，格式为：第1行数据转换异常：XXX列；
                        for (int j = 0; j < fields.Count; j++)
                        {
                            var existField = sheet.GetRow(headerRow).Cells.Find(a => a.ToString().ToLower().TrimEnd() == fields[j].Key.ToLower());
                            if (existField == null)
                                continue;
                            var headerIndex = existField.ColumnIndex;
                            // 3.给指定的属性赋值
                            string propertyName = CheckEntityMemberName(en, fields[j].Key);
                            System.Reflection.PropertyInfo properotyInfo = en.GetType().GetProperty(propertyName);
                            if (properotyInfo != null)
                            {
                                try
                                {
                                    // Excel单元格的值转换为对象属性的值，若类型不对，记录出错信息
                                    properotyInfo.SetValue(en, GetExcelCellToProperty(properotyInfo.PropertyType, sheet.GetRow(i).GetCell(headerIndex)), null);
                                }
                                catch (Exception e)
                                {
                                    if (errStr.Length == 0)
                                    {
                                        errStr = "第" + i + "行数据转换异常：";
                                    }
                                    errStr += fields[j].Key + "列；";
                                }
                            }
                        }
                        // 若有错误信息，就添加到错误信息里
                        if (errStr.Length > 0)
                        {
                            throw new ArgumentException(errStr);
                        }
                        if (sheet.SheetName.Contains("现付协议酒店"))
                        {
                            //现付协议酒店只报销服务费，房费不报销
                            var p = en.GetType().GetProperty("Amount");
                            p.SetValue(en, decimal.Zero, null);
                            //var p2 = en.GetType().GetProperty("PaidAmount");
                            //p2.SetValue(en, decimal.Zero, null);
                        }
                        enlist.Add(en);
                    }
                    else
                    {
                        if (sheet.GetRow(i).Cells.Exists(s => "合计" == s.ToString().TrimEnd() || "总计" == s.ToString().TrimEnd()))
                            continue;
                        T en = new T();
                        string errStr = ""; // 当前行转换时，是否有错误信息，格式为：第1行数据转换异常：XXX列；
                        for (int j = 0; j < fields.Count; j++)
                        {
                            var existField = sheet.GetRow(headerRow).Cells.Find(a => !string.IsNullOrEmpty(fields[j].Value) && fields[j].Value == a.ToString().TrimEnd());
                            if (null == existField && fields[j].Value.ToLower().Contains(","))
                            {
                                var t = fields[j].Value.ToLower().Split(',').ToArray();
                                foreach (var e in t)
                                {
                                    existField = sheet.GetRow(headerRow).Cells.Find(a => e == a.ToString().ToLower().TrimEnd());
                                    if (existField != null)
                                        break;
                                }
                            }
                            if (existField == null)
                                continue;
                            var headerIndex = existField.ColumnIndex;
                            // 3.给指定的属性赋值
                            string propertyName = CheckEntityMemberName(en, fields[j].Key);
                            System.Reflection.PropertyInfo properotyInfo = en.GetType().GetProperty(propertyName);
                            if (properotyInfo != null)
                            {
                                try
                                {
                                    // Excel单元格的值转换为对象属性的值，若类型不对，记录出错信息
                                    properotyInfo.SetValue(en, GetExcelCellToProperty(properotyInfo.PropertyType, sheet.GetRow(i).GetCell(headerIndex)), null);
                                }
                                catch (Exception e)
                                {
                                    if (errStr.Length == 0)
                                    {
                                        errStr = "第" + i + "行数据转换异常：";
                                    }
                                    errStr += fields[j].Key + "列；";
                                }
                            }
                        }
                        // 若有错误信息，就添加到错误信息里
                        if (errStr.Length > 0)
                        {
                            throw new ArgumentException(errStr);
                        }

                        if (sheet.SheetName.Contains("现付协议酒店"))
                        {
                            //现付协议酒店只报销服务费，房费不报销
                            var p = en.GetType().GetProperty("Amount");
                            p.SetValue(en, decimal.Zero, null);
                            //var p2 = en.GetType().GetProperty("PaidAmount");
                            //p2.SetValue(en, decimal.Zero, null);
                        }

                        enlist.Add(en);
                    }
                }
            }
            return enlist;
        }
        /// <summary>
        /// 检查实体字段名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public static string CheckEntityMemberName<T>(T entity, string memberName) where T : new()
        {
            foreach (System.Reflection.PropertyInfo p in entity.GetType().GetProperties())
            {
                if (p.Name.ToString().ToLower().Equals(memberName.ToLower()))
                {
                    return p.Name.ToString();
                }
            }
            return memberName;
        }

        /// <summary>
        /// 单元格和实体字段类型不一致判断
        /// </summary>
        /// <param name="distanceType"></param>
        /// <param name="sourceCell"></param>
        /// <returns></returns>
        private static Object GetExcelCellToProperty(Type distanceType, ICell sourceCell)
        {
            object rs = distanceType.IsValueType ? Activator.CreateInstance(distanceType) : null;
            // 1.判断传递的单元格是否为空
            if (sourceCell == null || string.IsNullOrEmpty(sourceCell.ToString()))
                return rs;
            // 2.Excel文本和数字单元格转换，在Excel里文本和数字是不能进行转换，所以这里预先存值
            object sourceValue = null;
            switch (sourceCell.CellType)
            {
                case CellType.Blank:
                    break;
                case CellType.Boolean:
                    break;
                case CellType.Error:
                    break;
                case CellType.Formula:
                    break;
                case CellType.Numeric:
                    if (distanceType.FullName.Contains("System.DateTime"))
                        sourceValue = sourceCell.DateCellValue;
                    else
                        sourceValue = sourceCell.NumericCellValue;
                    break;
                case CellType.String:
                    if (distanceType.FullName.Contains("System.DateTime"))
                    {
                        if (string.IsNullOrEmpty(sourceCell.ToString()) && distanceType.FullName.Contains("nullable`1"))
                            sourceValue = null;
                        else
                            sourceValue = Convert.ToDateTime(sourceCell.ToString());
                    }
                    else if (distanceType.FullName.Contains("System.Decimal"))
                    {
                        string sCell = string.IsNullOrWhiteSpace(sourceCell.ToString()) ? "0" : sourceCell.ToString();
                        if (sCell.IndexOf('%') > 0)
                        {
                            sourceValue = Convert.ToDecimal(sCell.TrimEnd('%')) / 100;
                        }
                        else
                            sourceValue = Convert.ToDecimal(sCell);
                    }
                    else if (distanceType.FullName.Contains("System.Int"))
                    {
                        if (Regex.IsMatch(sourceCell.ToString(), "^\\d{1}星$"))
                            sourceValue = Convert.ToInt32(sourceCell.ToString().Trim('星'));
                        else
                            sourceValue = Convert.ToInt32(sourceCell.ToString());
                    }
                    else
                        sourceValue = sourceCell.ToString();
                    break;
                case CellType.Unknown:
                    break;
                default:
                    break;
            }
            string valueDataType = distanceType.Name;
            if (sourceValue == null)
                return rs;
            // 在这里进行特定类型的处理
            switch (valueDataType.ToLower()) // 以防出错，全部小写
            {
                case "string":
                    rs = (string)Convert.ChangeType(sourceValue, distanceType);
                    break;
                case "int":
                case "int16":
                case "int32":
                    rs = (sourceValue == null) ? 0 : (int)Convert.ChangeType(sourceValue, distanceType);
                    break;
                case "decimal":
                    rs = (sourceValue == null) ? 0 : (decimal)Convert.ChangeType(sourceValue, distanceType);
                    break;
                case "float":
                case "single":
                    rs = (sourceValue == null) ? 0 : (float)Convert.ChangeType(sourceValue, distanceType);
                    break;
                case "datetime":
                    rs = (DateTime)Convert.ChangeType(sourceValue, distanceType);
                    break;
                case "guid":
                    rs = (Guid)Convert.ChangeType(sourceValue, distanceType);
                    break;
                case "nullable`1":
                    rs = sourceValue;
                    break;
            }
            return rs;
        }
    }



    #endregion
}