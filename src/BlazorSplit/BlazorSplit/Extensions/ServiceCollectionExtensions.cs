using Microsoft.Extensions.DependencyInjection;

namespace BlazorSplit;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the services required to use BlazorSplit
    /// </summary>
    /// <returns>Service Collection</returns>
    public static IServiceCollection AddBlazorSplit(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<SplitInterop>();
    }
}