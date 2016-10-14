using NPOI.HSSF.Util;
using NPOI.SS.UserModel;

namespace Demo.Excel.Helper
{
    /// <summary>
    /// 标题操作
    /// </summary>
    internal class TitleBackgroundMethod : CellStyleMethod
    {
        internal override ICellStyle SetCell(ICellStyle cellStyle)
        {
            cellStyle.FillForegroundColor = HSSFColor.Grey50Percent.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            return cellStyle;
        }
    }
}