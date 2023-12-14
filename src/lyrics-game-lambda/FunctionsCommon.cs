using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace lyrics_game_lambda;

public partial class Function
{
    private IServiceProvider _serviceProvider { get; set; }
    private IConfiguration _configuration { get; set; }
    
    public Function()
    {
        var services = new ServiceCollection();
        _serviceProvider = services
            .ConfigureServices()
            .BuildServiceProvider();
    }
}