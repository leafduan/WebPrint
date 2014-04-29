using System;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;

namespace WebPrint.Office
{
    public class ExcelForExport : IExport
    {
        protected static ExcelForExport _instance;
        private static object lock_instance = new object();

        protected ExcelForExport()
        {

        }

        #region IExport 成员

        public void Export(string filepath, System.Data.DataTable dt)
        {
            if (dt == null)
            {
                throw new ArgumentNullException("The argument of dt is null: ExcelForExport.Export(string filepath, System.Data.DataTable dt)");
            }

            string[] headers = new string[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                headers[i] = dt.Columns[i].ColumnName;
            }

            this.Export(filepath, dt, headers);
        }

        public void Export(string filepath, System.Data.DataTable dt, string[] headers)
        {
            IWorkbook newWB = new HSSFWorkbook();
            //ISheet newSht = newWB.CreateSheet("Sheet1");

            this.AddSheet(newWB, dt, headers);
            this.Save(filepath, newWB);
        }

        #endregion

        protected void Save(string filepath, IWorkbook wb)
        {
            using (System.IO.FileStream fs = System.IO.File.Create(filepath))
            {
                wb.Write(fs);
                fs.Close();
            }
        }

        protected void AddSheet(IWorkbook workBook, DataTable dt, string[] headers)
        {
            //头部样式
            ICellStyle hStyle = workBook.CreateCellStyle();
            hStyle.FillForegroundColor = HSSFColor.YELLOW.index;
            hStyle.FillPattern = FillPatternType.BIG_SPOTS;
            hStyle.FillBackgroundColor = HSSFColor.YELLOW.index;
            hStyle.Alignment = HorizontalAlignment.CENTER;
            hStyle.VerticalAlignment = VerticalAlignment.CENTER;
            hStyle.BorderBottom = hStyle.BorderLeft = hStyle.BorderRight = hStyle.BorderTop = BorderStyle.MEDIUM;
            hStyle.BottomBorderColor = hStyle.LeftBorderColor = hStyle.RightBorderColor = hStyle.TopBorderColor = HSSFColor.BLACK.index;
            IFont hFont = workBook.CreateFont();
            hFont.Boldweight = (short)FontBoldWeight.BOLD;
            hStyle.SetFont(hFont);

            int RecordCounts = dt.Rows.Count;
            int PageSize = 65501;
            int TotalPages = (RecordCounts + PageSize - 1) / PageSize;
            for (int i = 1; i <= TotalPages; i++)
            {
                ISheet sheet = workBook.CreateSheet(string.Format("Sheet{0}", i));
                this.AddHeader(sheet, hStyle, headers);
                if (i == TotalPages)
                {
                    this.FillSheet(sheet, dt, PageSize * (i - 1), RecordCounts);
                }
                else
                {
                    this.FillSheet(sheet, dt, PageSize * (i - 1), PageSize * i);
                }
            }
        }

        protected void FillSheet(ISheet sheet, DataTable dt, int rowIndexStart, int rowIndexEnd)
        {
            for (int i = rowIndexStart; i < rowIndexEnd; i++)
            {
                object[] columns = dt.Rows[i].ItemArray;
                this.AddRow(sheet, columns);
            }

            #region
            /*
             * 自动调整列的宽度 只支持数字和英文 不支持中文
             * 中文解决方案 遍历获取列中最大宽度 
             * http://blog.csdn.net/jerry_cool/article/details/7000085
             * */
            #endregion
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sheet.AutoSizeColumn(i);
            }
        }

        protected void AddRow(ISheet sht, object[] columns)
        {
            this.AddRow(sht, columns, sht.LastRowNum + 1);
        }

        protected void AddRow(ISheet sht, object[] columns, int rowindex)
        {
            IRow hRow = sht.CreateRow(rowindex);
            hRow.Height = 100 * 4;

            int cellIndex = 0;
            foreach (object c in columns)
            {
                ICell hCell = hRow.CreateCell(cellIndex++);
                hCell.SetCellValue(c.ToString());
            }
        }

        protected void AddHeader(ISheet sht, ICellStyle hStyle, string[] headers)
        {
            this.AddHeader(sht, hStyle, headers, 0);
        }

        protected void AddHeader(ISheet sht, ICellStyle hStyle, string[] headers, int rowindex)
        {
            IRow hRow = sht.CreateRow(rowindex);
            hRow.Height = 200 * 3;
            int cellIndex = 0;
            foreach (string h in headers)
            {
                ICell hCell = hRow.CreateCell(cellIndex++);
                hCell.CellStyle = hStyle;
                hCell.SetCellValue(h);
            }
        }

        public static ExcelForExport CreateInstance()
        {
            if (_instance == null)
            {
                lock (lock_instance)
                {

                    if (_instance == null)
                    {
                        _instance = new ExcelForExport();
                    }

                }
            }
            return _instance;
        }

    }
}
