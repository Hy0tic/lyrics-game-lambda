using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace songdb;

public class ExcelSongRepository 
{
    private IExcelPackageWrapper excelPackageWrapper { get; set; }

    public ExcelSongRepository(IExcelPackageWrapper excelPackageWrapper)
    {
        this.excelPackageWrapper = excelPackageWrapper;
    }

    public Song GetRandomSong() {
        var totalRows = excelPackageWrapper.GetTotalRows();
        var rowIndex = RandomNumberGenerator.GetInt32(1,totalRows-1);

        var songTitle = RemoveContentInSquareBrackets(RemoveContentInParentheses(excelPackageWrapper.GetTitleAtRow(rowIndex)));
        var songAlbum = RemoveContentInSquareBrackets(RemoveContentInParentheses(excelPackageWrapper.GetAlbumAtRow(rowIndex)));
        var songLyrics = RemoveContentInSquareBrackets(excelPackageWrapper.GetLyricsAtRow(rowIndex));

        var songResult = new Song(songTitle, songAlbum, songLyrics);
        return songResult;
    }

    public string GetRandomSongTitle()
    {
        var totalRows = excelPackageWrapper.GetTotalRows();
        var rowIndex = RandomNumberGenerator.GetInt32(1,totalRows-1);

        var songTitle = RemoveContentInSquareBrackets(excelPackageWrapper.GetTitleAtRow(rowIndex));

        return RemoveContentInParentheses(songTitle);
    }
    
    public string RemoveContentInParentheses(string input)
    {
        return Regex.Replace(input, @"\([^)]*\)", "").Trim();
    }
    
    public string RemoveContentInSquareBrackets(string input)
    {
        return Regex.Replace(input, @"\[[^\]]*\]", "").Trim();
    }

}