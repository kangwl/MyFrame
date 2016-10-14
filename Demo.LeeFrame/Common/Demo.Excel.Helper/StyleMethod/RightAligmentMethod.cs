using NPOI.SS.UserModel;

namespace Demo.Excel.Helper
{
    /// <summary>
    /// 右对齐
    /// </summary>
    internal class RightAligmentMethod : CellStyleMethod
    {
        internal override ICellStyle SetCell(ICellStyle cellStyle)
        {
            cellStyle.Alignment = HorizontalAlignment.Right;
            return cellStyle;
        }
    }
}