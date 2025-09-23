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
        public static void AddManticoreProvider(this IServiceCollection collection, TimeSpan timeout = default)
        {
            collection.AddSingleton<IManticoreProvider, ManticoreProvider>(_ => new ManticoreProvider(timeout));
        }

        /// <summary>
        /// Registers the Manticore Search provider with a specified base URL and optional timeout configuration.
        /// </summary>
        /// <param name="collection">The service collection to add the provider to.</param>
        /// <param name="url">The base URL of the Manticore Search API server.</param>
        /// <param name="timeout">The timeout for HTTP requests. Default is 30 seconds if not specified.</param>
        public static void AddManticoreProvider(this IServiceCollection collection, string url, TimeSpan timeout = default)
        {
            collection.AddSingleton<IManticoreProvider, ManticoreProvider>(_ => new ManticoreProvider(url, timeout));
        }

        /// <summary>
        /// Registers the Manticore Search provider with a specified instance of <see cref="HttpClient"/>.
        /// </summary>
        /// <param name="collection">The service collection to add the provider to.</param>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/>.</param>
        /// <param name="disposeHttpClient"><c>true</c>, if the specified instance of <see cref="HttpClient"/> need to be disposed; otherwise, <c>false</c>.</param>
        public static void AddManticoreProvider(this IServiceCollection collection, HttpClient httpClient, bool disposeHttpClient = false)
        {
            collection.AddSingleton<IManticoreProvider, ManticoreProvider>(_ => new ManticoreProvider(httpClient, disposeHttpClient));
        }
    }
}