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
    public string FunctionHandler(ILambdaContext context)
    {
        var excelRepo = _serviceProvider.GetRequiredService<ExcelSongRepository>();
        var result = excelRepo.GetRandomSongTitle();
        return new {result}.ToString();

    }
}
