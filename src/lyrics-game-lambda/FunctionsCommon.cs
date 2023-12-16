using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace lyrics_game_lambda;

public partial class Function
{
    private IServiceProvider ServiceProvider { get; set; }
    private IConfiguration Configuration { get; set; }
    
    public Function()
    {
        var services = new ServiceCollection();
        ServiceProvider = services
            .ConfigureServices()
            .BuildServiceProvider();
    }
}