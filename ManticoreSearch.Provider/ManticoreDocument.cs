using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ManticoreSearch.Provider
{
    /// <summary>
    /// Represents a base document for tables in Manticore Search.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class ManticoreDocument
    {
    }
}