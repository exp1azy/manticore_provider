using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ManticoreSearch.Provider
{
    /// <summary>
    /// Represents a base document for tables in Manticore Search. All properties are serialized in the snake case strategy type.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class ManticoreDocument
    {
    }
}