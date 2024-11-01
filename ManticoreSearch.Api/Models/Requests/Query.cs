using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    public class Query
    {
        [JsonProperty("match", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object>? Match { get; set; }

        [JsonProperty("match_all", NullValueHandling = NullValueHandling.Ignore)]
        public object? MatchAll { get; set; }

        [JsonProperty("match_phrase", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string>? MatchPhrase { get; set; }

        [JsonProperty("query_string", NullValueHandling = NullValueHandling.Ignore)]
        public string? QueryString { get; set; }

        [JsonProperty("bool", NullValueHandling = NullValueHandling.Ignore)]
        public QueryBool? Bool { get; set; }

        [JsonProperty("equals", NullValueHandling = NullValueHandling.Ignore)]
        public new Dictionary<string, object>? Equals { get; set; }

        [JsonProperty("in", NullValueHandling = NullValueHandling.Ignore)]
        public object? In { get; set; }

        [JsonProperty("range", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, QueryRange>? Range { get; set; }

        [JsonProperty("geo_distance", NullValueHandling = NullValueHandling.Ignore)]
        public QueryGeoDistance? GeoDistance { get; set; }

        [JsonProperty("ql", NullValueHandling = NullValueHandling.Ignore)]
        public string? Ql { get; set; }
    }

    public class QueryRange
    {
        [JsonProperty("gte", NullValueHandling = NullValueHandling.Ignore)]
        public int? Gte { get; set; }

        [JsonProperty("gt", NullValueHandling = NullValueHandling.Ignore)]
        public int? Gt { get; set; }

        [JsonProperty("lte", NullValueHandling = NullValueHandling.Ignore)]
        public int? Lte { get; set; }

        [JsonProperty("lt", NullValueHandling = NullValueHandling.Ignore)]
        public int? Lt { get; set; }
    }

    public class QueryGeoDistance
    {
        [JsonProperty("location_anchor")]
        public GeoLocationAnchor LocationAnchor { get; set; }

        [JsonProperty("location_source")]
        public object LocationSource { get; set; }

        [JsonProperty("distance_type")]
        public string DistanceType { get; set; }

        [JsonProperty("distance")]
        public string Distance { get; set; }
    }

    public class GeoLocationAnchor
    {
        [JsonProperty("lat")]
        public int Lat { get; set; }

        [JsonProperty("lon")]
        public int Lon { get; set; }
    }

    public class QueryBool
    {
        [JsonProperty("must", NullValueHandling = NullValueHandling.Ignore)]
        public List<BoolMust>? Must { get; set; }

        [JsonProperty("must_not", NullValueHandling = NullValueHandling.Ignore)]
        public BoolMust? MustNot { get; set; }

        [JsonProperty("should", NullValueHandling = NullValueHandling.Ignore)]
        public List<object>? Should { get; set; }
    }

    public class BoolMust
    {
        [JsonProperty("match", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object>? Match { get; set; }

        [JsonProperty("range", NullValueHandling = NullValueHandling.Ignore)]
        public object? Range { get; set; }

        [JsonProperty("equals", NullValueHandling = NullValueHandling.Ignore)]
        public new Dictionary<string, object>? Equals { get; set; }

        [JsonProperty("bool", NullValueHandling = NullValueHandling.Ignore)]
        public QueryBool? Bool { get; set; }

        [JsonProperty("query_string", NullValueHandling = NullValueHandling.Ignore)]
        public string? QueryString { get; set; }
    }
}
