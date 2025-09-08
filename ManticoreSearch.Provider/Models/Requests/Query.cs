using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a query object used in search operations.
    /// </summary>
    public class Query
    {
        /// <summary>
        /// Gets or sets a dictionary for matching documents based on specific fields and values.
        /// </summary>
        [JsonProperty("match", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object>? Match { get; set; }

        /// <summary>
        /// Gets or sets an object that matches all documents in the index.
        /// </summary>
        [JsonProperty("match_all", NullValueHandling = NullValueHandling.Ignore)]
        public object? MatchAll { get; set; }

        /// <summary>
        /// Gets or sets a dictionary for matching documents based on exact phrase matches.
        /// </summary>
        [JsonProperty("match_phrase", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string>? MatchPhrase { get; set; }

        /// <summary>
        /// Gets or sets the query string used for full-text search.
        /// </summary>
        [JsonProperty("query_string", NullValueHandling = NullValueHandling.Ignore)]
        public string? QueryString { get; set; }

        /// <summary>
        /// Gets or sets the boolean query that combines multiple query conditions.
        /// </summary>
        [JsonProperty("bool", NullValueHandling = NullValueHandling.Ignore)]
        public Bool? Bool { get; set; }

        /// <summary>
        /// Gets or sets a dictionary for equality checks on fields.
        /// This property overrides the base class property.
        /// </summary>
        [JsonProperty("equals", NullValueHandling = NullValueHandling.Ignore)]
        public new Dictionary<string, object>? Equals { get; set; }

        /// <summary>
        /// Gets or sets an object for checking membership in a set of values.
        /// </summary>
        [JsonProperty("in", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object>? In { get; set; }

        /// <summary>
        /// Gets or sets a dictionary defining range queries on numeric or date fields.
        /// </summary>
        [JsonProperty("range", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, RangeFilter>? Range { get; set; }

        /// <summary>
        /// Gets or sets the geographic distance query for spatial filtering.
        /// </summary>
        [JsonProperty("geo_distance", NullValueHandling = NullValueHandling.Ignore)]
        public GeoDistance? GeoDistance { get; set; }

        /// <summary>
        /// Gets or sets the query language for the query, if applicable.
        /// </summary>
        [JsonProperty("ql", NullValueHandling = NullValueHandling.Ignore)]
        public string? Ql { get; set; }
    }

    /// <summary>
    /// Represents a range query with minimum and maximum value constraints.
    /// This class can be used to specify inclusive or exclusive bounds.
    /// </summary>
    public class RangeFilter
    {
        /// <summary>
        /// Gets or sets the lower bound for the range, inclusive.
        /// </summary>
        /// <remarks>Greater than or equal to.</remarks>
        [JsonProperty("gte", NullValueHandling = NullValueHandling.Ignore)]
        public int? Gte { get; set; }

        /// <summary>
        /// Gets or sets the lower bound for the range, exclusive.
        /// </summary>
        /// <remarks>Greater than.</remarks>
        [JsonProperty("gt", NullValueHandling = NullValueHandling.Ignore)]
        public int? Gt { get; set; }

        /// <summary>
        /// Gets or sets the upper bound for the range, inclusive.
        /// </summary>
        /// <remarks>Less than or equal to.</remarks>
        [JsonProperty("lte", NullValueHandling = NullValueHandling.Ignore)]
        public int? Lte { get; set; }

        /// <summary>
        /// Gets or sets the upper bound for the range, exclusive.
        /// </summary>
        /// <remarks>Less than.</remarks>
        [JsonProperty("lt", NullValueHandling = NullValueHandling.Ignore)]
        public int? Lt { get; set; }
    }

    /// <summary>
    /// Represents a geographic distance query.
    /// </summary>
    public class GeoDistance
    {
        /// <summary>
        /// Gets or sets the geographic anchor location for the distance query.
        /// </summary>
        [JsonProperty("location_anchor")]
        public GeoLocationAnchor LocationAnchor { get; set; }

        /// <summary>
        /// Gets or sets the source location for the query.
        /// </summary>
        [JsonProperty("location_source")]
        public object LocationSource { get; set; }

        /// <summary>
        /// Gets or sets the type of distance measurement (e.g., "kilometers", "miles").
        /// </summary>
        [JsonProperty("distance_type")]
        public string DistanceType { get; set; }

        /// <summary>
        /// Gets or sets the distance value to filter the query results.
        /// </summary>
        [JsonProperty("distance")]
        public string Distance { get; set; }
    }

    /// <summary>
    /// Represents a geographic location defined by latitude and longitude.
    /// </summary>
    public class GeoLocationAnchor
    {
        /// <summary>
        /// Gets or sets the latitude of the geographic location.
        /// </summary>
        [JsonProperty("lat")]
        public int Lat { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the geographic location.
        /// </summary>
        [JsonProperty("lon")]
        public int Lon { get; set; }
    }

    /// <summary>
    /// Represents a boolean query structure for constructing complex queries using logical operators in a query language.
    /// </summary>
    public class Bool
    {
        /// <summary>
        /// Gets or sets the list of conditions that must be satisfied 
        /// for the query to match. Each condition in this list must 
        /// evaluate to true for the overall query to be considered a match.
        /// </summary>
        [JsonProperty("must", NullValueHandling = NullValueHandling.Ignore)]
        public List<Must>? Must { get; set; }

        /// <summary>
        /// Gets or sets the list of condition that must not be satisfied for the query to match.
        /// </summary>
        [JsonProperty("must_not", NullValueHandling = NullValueHandling.Ignore)]
        public List<Must>? MustNot { get; set; }

        /// <summary>
        /// Gets or sets the list of conditions that must not be met for the query to be considered valid.
        /// </summary>
        [JsonProperty("should", NullValueHandling = NullValueHandling.Ignore)]
        public List<object>? Should { get; set; }
    }

    /// <summary>
    /// Represents a condition that must be satisfied for a boolean query to match.
    /// </summary>
    public class Must
    {
        /// <summary>
        /// Gets or sets a dictionary defining the match condition.
        /// </summary>
        [JsonProperty("match", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object>? Match { get; set; }

        /// <summary>
        /// Gets or sets a range condition that must be satisfied.
        /// </summary>
        [JsonProperty("range", NullValueHandling = NullValueHandling.Ignore)]
        public object? Range { get; set; }

        /// <summary>
        /// Gets or sets a dictionary defining equality conditions.
        /// The key is the field name and the value is the expected value.
        /// </summary>
        [JsonProperty("equals", NullValueHandling = NullValueHandling.Ignore)]
        public new Dictionary<string, object>? Equals { get; set; }

        /// <summary>
        /// Gets or sets another boolean query that must be satisfied.
        /// </summary>
        [JsonProperty("bool", NullValueHandling = NullValueHandling.Ignore)]
        public Bool? Bool { get; set; }

        /// <summary>
        /// Gets or sets a query string condition that must be satisfied.
        /// </summary>
        [JsonProperty("query_string", NullValueHandling = NullValueHandling.Ignore)]
        public string? QueryString { get; set; }

        /// <summary>
        /// Gets or sets a dictionary defining attribute values.
        /// Filter to match a given set of attribute values.
        /// </summary>
        [JsonProperty("in", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object>? In { get; set; }
    }
}
