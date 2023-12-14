using OfficeOpenXml;

namespace songdb;

public interface IExcelPackageWrapper
{
    ExcelWorksheet GetFirstWorksheet();
    int GetTotalRows();
    string GetTitleAtRow(int rowIndex);
    string GetAlbumAtRow(int rowIndex);
    string GetLyricsAtRow(int RowIndex);

}