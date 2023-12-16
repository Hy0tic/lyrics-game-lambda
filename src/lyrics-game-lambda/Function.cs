using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using songdb;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace lyrics_game_lambda;

public partial class Function
{
    
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<Object> FunctionHandler(ILambdaContext context)
    {
        var excelRepo = ServiceProvider.GetRequiredService<ExcelSongRepository>();
        var result = excelRepo.GetRandomSongTitle();

        var randomSong = excelRepo.GetRandomSong();

        var randomTitles = new List<string>
        {
            excelRepo.GetRandomSongTitle(),
            excelRepo.GetRandomSongTitle(),
            excelRepo.GetRandomSongTitle(),
            randomSong.Name
        }
            .OrderBy(x => Random.Shared.Next())
            .ToList();
        

        return new {
            album = randomSong.Album,
            title = randomSong.Name,
            quote = randomSong.Lyrics,
            choices = randomTitles
        };

    }
}
