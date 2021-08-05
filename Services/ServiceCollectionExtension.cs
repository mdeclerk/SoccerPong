using Microsoft.Extensions.DependencyInjection;

namespace SoccerPong.Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSoccerPongServices(this IServiceCollection @this) =>
            @this.AddTransient<GameService>().AddTransient<ConfigService>();
    }
}