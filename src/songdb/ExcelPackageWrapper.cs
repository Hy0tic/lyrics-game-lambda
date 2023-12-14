using OfficeOpenXml;

namespace songdb;

public class ExcelPackageWrapper : IExcelPackageWrapper
{
    private readonly ExcelPackage _excelPackage;
    public ExcelPackageWrapper(ExcelPackage excelPackage)
    {
        _excelPackage = excelPackage;
    }
    public string GetAlbum()
    {
        throw new NotImplementedException();
    }
    public ExcelWorksheet GetFirstWorksheet()
    {
        return _excelPackage.Workbook.Worksheets[0];
    }
    public string GetTitleAtRow(int rowIndex)
    {
        return _excelPackage.Workbook.Worksheets[0].Cells[rowIndex, 1].Value.ToString();
    }
    public string GetAlbumAtRow(int rowIndex)
    {
        return _excelPackage.Workbook.Worksheets[0].Cells[rowIndex, 2].Value.ToString();
    }


    public string GetLyricsAtRow(int rowIndex)
    {
        return _excelPackage.Workbook.Worksheets[0].Cells[rowIndex, 3].Value.ToString();
    }

    public int GetTotalRows()
    {
        return _excelPackage.Workbook.Worksheets[0].Dimension.Rows;
    }
}