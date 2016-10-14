using NPOI.HSSF.Util;
using NPOI.SS.UserModel;

namespace WebPrint.Office
{
    public static class NpoiExtensions
    {
        public static IRow InsertRow(this ISheet sheet, int num)
        {
            var row = sheet.GetRow(num);
            if (row == null)
            {
                row = sheet.CreateRow(num);
            }

            return row;
        }

        public static IRow Row(this ISheet sheet, int num)
        {
            return sheet.GetRow(num) ?? sheet.CreateRow(num);
        }

        public static ICell Cell(this IRow row, int index)
        {
            return Cell(row, index, row.GetDefaultCellStyle());
        }

        public static ICell OrderCell(this IRow row, int index)
        {
            return Cell(row, index, row.GetInvoiceCellStyle());
        }

        public static ICell Cell(this IRow row, int index, ICellStyle cellStyle)
        {
            var cell = row.GetCell(index);
            if (cell != null)
                return cell;

            cell = row.CreateCell(index);
            cell.CellStyle = cellStyle;

            return cell;
        }

        public static ICellStyle GetInvoiceCellStyle(this IRow row)
        {
            var cellStyle = row.Sheet.Workbook.CreateCellStyle();
            cellStyle.Alignment = HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;

            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;

            cellStyle.BottomBorderColor = HSSFColor.Black.Index;
            cellStyle.LeftBorderColor = HSSFColor.Black.Index;
            cellStyle.RightBorderColor = HSSFColor.Black.Index;
            cellStyle.TopBorderColor = HSSFColor.Black.Index;

            return cellStyle;
        }

        public static ICellStyle GetDefaultCellStyle(this IWorkbook workBook)
        {
            var cellStyle = workBook.CreateCellStyle();
            cellStyle.Alignment = HorizontalAlignment.Left;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;

            return cellStyle;
        }

        public static ICellStyle GetDefaultCellStyle(this ISheet sheet)
        {
            var cellStyle = sheet.Workbook.CreateCellStyle();
            cellStyle.Alignment = HorizontalAlignment.Left;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;

            return cellStyle;
        }

        public static ICellStyle GetDefaultCellStyle(this IRow row)
        {
            var cellStyle = row.Sheet.Workbook.CreateCellStyle();
            cellStyle.Alignment = HorizontalAlignment.Left;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;

            return cellStyle;
        }
    }
}
