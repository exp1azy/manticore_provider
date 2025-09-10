using ManticoreSearch.Provider.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ManticoreSearch.Provider.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the Manticore Search provider with the default base address (http://localhost:9308) and optional timeout configuration.
        /// </summary>
        /// <param name="collection">The service collection to add the provider to.</param>
        /// <param name="timeout">The timeout for HTTP requests. Default is 30 seconds if not specified.</param>
        public static void AddManticoreSearchProvider(this IServiceCollection collection, TimeSpan timeout = default)
        {
            RegisterManticoreSearch(collection, null, timeout);
        }

        /// <summary>
        /// Registers the Manticore Search provider with a specified base URL and optional timeout configuration.
        /// </summary>
        /// <param name="collection">The service collection to add the provider to.</param>
        /// <param name="url">The base URL of the Manticore Search API server.</param>
        /// <param name="timeout">The timeout for HTTP requests. Default is 30 seconds if not specified.</param>
        public static void AddManticoreSearchProvider(this IServiceCollection collection, string url, TimeSpan timeout = default)
        {
            RegisterManticoreSearch(collection, url, timeout);
        }

        private static void RegisterManticoreSearch(IServiceCollection serviceCollection, string? url = null, TimeSpan timeout = default)
        {
            if (string.IsNullOrEmpty(url))
                serviceCollection.AddSingleton<IManticoreProvider, ManticoreProvider>(_ => new ManticoreProvider(timeout));
            else
                serviceCollection.AddSingleton<IManticoreProvider, ManticoreProvider>(_ => new ManticoreProvider(url, timeout));
        }
    }
}