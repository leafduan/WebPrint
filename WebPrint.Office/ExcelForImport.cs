using System;
using System.Collections;
using System.Linq;
using System.IO;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace WebPrint.Office
{
    public class ExcelForImport : IImport
    {
        protected static ExcelForImport _instance;
        private static object lock_instance = new object();

        protected ExcelForImport()
        {

        }

        #region IImport 成员

        public System.Data.DataTable Import(string filepath)
        {
            IWorkbook workBook = this.InitializeWorkbook(filepath);
            IFormulaEvaluator evaluator = new HSSFFormulaEvaluator(workBook);
            ISheet sheet = workBook.GetSheetAt(0);
            IEnumerator rows = sheet.GetEnumerator();
            DataTable dt = new DataTable();

            string[] headerNames = new string[sheet.GetRow(0).PhysicalNumberOfCells];
            for (int j = 0; j < headerNames.Length; j++)
            {
                headerNames[j] = Convert.ToChar(((int)'A') + j % 26).ToString() + ((j / 26) > 0 ? (j / 26).ToString() : string.Empty); // A-Z A1-Z1 An-Zn
            }

            this.AddColumn(dt, headerNames);

            while (rows.MoveNext())
            {
                IRow row = rows.Current as HSSFRow;
                this.AddRow(dt, row, headerNames,evaluator);
            }

            return dt;
        }

        public System.Data.DataTable Import(string filepath, string[] headerNames)
        {
            DataTable dt = new DataTable();
            this.AddColumn(dt, headerNames);

            IWorkbook wb = InitializeWorkbook(filepath);
            IFormulaEvaluator evaluator = new HSSFFormulaEvaluator(wb);
            ISheet sht = wb.GetSheetAt(0);
            IEnumerator rows = sht.GetRowEnumerator();

            //默认第一行为头部列名
            if (rows.MoveNext())
            {
                while (rows.MoveNext())
                {
                    IRow row = rows.Current as HSSFRow;
                    //M by Duanqh 2012-7-27
                    //if (row == null) continue;
                    this.AddRow(dt, row, headerNames, evaluator);
                }
            }
            return dt;
        }

        #endregion

        protected void AddRow(DataTable dt, IRow row, string[] headerNames,IFormulaEvaluator evaluator)
        {
            System.Data.DataRow newRow = dt.NewRow();

            for (int i = 0; i < headerNames.Count(); i++)
            {
                newRow[headerNames[i]] = GetHSSFCellValue(evaluator.EvaluateInCell(row.GetCell(i)));
            }

            dt.Rows.Add(newRow);
        }

        protected object GetHSSFCellValue(ICell cell)
        {
            if (cell == null) return string.Empty;

            object rValue = string.Empty;
            switch (cell.CellType)
            {
                case  CellType.Numeric:
                    /*
                    if (NPOI.HSSF.UserModel.HSSFDateUtil.IsCellDateFormatted(cell))
                        rValue = cell.DateCellValue.ToString("yyyy-MM-dd HH:mm:ss");
                    else
                        rValue = cell.NumericCellValue.ToString();
                     * */
                    rValue = cell.ToString();
                    break;
                case CellType.String:
                    rValue = cell.StringCellValue;
                    break;
                case CellType.Boolean:
                    rValue = cell.BooleanCellValue;
                    break;
                case CellType.Formula: //if HSSFFormulaEvaluator.EvaluateInCell(ICell) CellType.FORMULA will never happen
                    rValue = "=" + cell.CellFormula;
                    break;
                case CellType.Blank:
                default:
                    break;
            }

            return rValue;
        }

        protected void AddColumn(System.Data.DataTable dt, string[] headerNames)
        {
            foreach (string h in headerNames)
            {
                dt.Columns.Add(h);
            }
        }

        protected IWorkbook InitializeWorkbook(string path)
        {
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    return new XSSFWorkbook(file);
                }
                catch (Exception ex)
                {
                    return new HSSFWorkbook(file);
                }
            }
        }

        public static ExcelForImport CreateInstance()
        {
            if (_instance == null)
            {
                lock (lock_instance)
                {

                    if (_instance == null)
                    {
                        _instance = new ExcelForImport();
                    }
                }
            }
            return _instance;
        }
    }
}
