using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using songdb;

namespace lyrics_game_lambda;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection)
    {
        var path = "../songDB/songs.xlsx";
        var excelFile = new FileInfo(path);
        var excelPackage = new ExcelPackage(excelFile);
        var excelPackageWrapper = new ExcelPackageWrapper(excelPackage);
        
        serviceCollection
            .AddSingleton<ExcelSongRepository>(new ExcelSongRepository(excelPackageWrapper));
        
        return serviceCollection;
    }
}