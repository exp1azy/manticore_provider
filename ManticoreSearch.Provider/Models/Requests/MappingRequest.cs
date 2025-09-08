using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a mapping request to define or update the schema of a table.
    /// </summary>
    public class MappingRequest
    {
        /// <summary>
        /// Gets or sets the dictionary of field definitions for the table mapping.
        /// </summary>
        [JsonProperty("properties")]
        public Dictionary<string, MappingField> Properties { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingRequest"/> class.
        /// </summary>
        public MappingRequest()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingRequest"/> class.
        /// </summary>
        /// <param name="properties">The dictionary of field definitions for the table mapping.</param>
        public MappingRequest(Dictionary<string, MappingField> properties)
        {
            Properties = properties;
        }
    }

    /// <summary>
    /// Represents the configuration for a single field in a Manticore Search table mapping.
    /// </summary>
    public class MappingField
    {
        /// <summary>
        /// Gets or sets the data type of the field.
        /// </summary>
        [JsonProperty("type")]
        public MappingFieldType Type { get; set; }
    }

    /// <summary>
    /// Enumerates the supported field types for Manticore Search table mappings.
    /// Each type corresponds to specific data handling and indexing behavior.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MappingFieldType
    {
        /// <summary>Aggregate metric type for numerical aggregations.</summary>
        [EnumMember(Value = "aggregate_metric")]
        AggregateMetric,

        /// <summary>Binary data type for storing binary content.</summary>
        [EnumMember(Value = "binary")]
        Binary,

        /// <summary>Boolean type for true/false values.</summary>
        [EnumMember(Value = "boolean")]
        Boolean,

        /// <summary>8-bit signed integer type.</summary>
        [EnumMember(Value = "byte")]
        Byte,

        /// <summary>Completion type for autocomplete functionality.</summary>
        [EnumMember(Value = "completion")]
        Completion,

        /// <summary>Date type for storing date and time values.</summary>
        [EnumMember(Value = "date")]
        Date,

        /// <summary>High-precision date type with nanosecond resolution.</summary>
        [EnumMember(Value = "date_nanos")]
        DateNanos,

        /// <summary>Date range type for storing date intervals.</summary>
        [EnumMember(Value = "date_range")]
        DateRange,

        /// <summary>Dense vector type for machine learning and similarity search.</summary>
        [EnumMember(Value = "dense_vector")]
        DenseVector,

        /// <summary>Flattened type for indexing entire JSON objects as single fields.</summary>
        [EnumMember(Value = "flattened")]
        Flattened,

        /// <summary>Flat object type for nested object structures.</summary>
        [EnumMember(Value = "flat_object")]
        FlatObject,

        /// <summary>Single-precision floating point number type.</summary>
        [EnumMember(Value = "float")]
        Float,

        /// <summary>Float range type for storing floating point intervals.</summary>
        [EnumMember(Value = "float_range")]
        FloatRange,

        /// <summary>Geographic point type for latitude/longitude coordinates.</summary>
        [EnumMember(Value = "geo_point")]
        GeoPoint,

        /// <summary>Geographic shape type for complex geospatial data.</summary>
        [EnumMember(Value = "geo_shape")]
        GeoShape,

        /// <summary>Half-precision floating point number type (16-bit).</summary>
        [EnumMember(Value = "half_float")]
        HalfFloat,

        /// <summary>Histogram type for statistical data representation.</summary>
        [EnumMember(Value = "histogram")]
        Histogram,

        /// <summary>32-bit signed integer type.</summary>
        [EnumMember(Value = "integer")]
        Integer,

        /// <summary>Integer range type for storing integer intervals.</summary>
        [EnumMember(Value = "integer_range")]
        IntegerRange,

        /// <summary>IP address type for IPv4 and IPv6 addresses.</summary>
        [EnumMember(Value = "ip")]
        Ip,

        /// <summary>IP range type for storing IP address intervals.</summary>
        [EnumMember(Value = "ip_range")]
        IpRange,

        /// <summary>Join type for defining parent-child relationships between documents.</summary>
        [EnumMember(Value = "join")]
        Join,

        /// <summary>Keyword type for exact-match string values (not analyzed).</summary>
        [EnumMember(Value = "keyword")]
        Keyword,

        /// <summary>KNN vector type for nearest neighbor search operations.</summary>
        [EnumMember(Value = "knn_vector")]
        KnnVector,

        /// <summary>64-bit signed integer type.</summary>
        [EnumMember(Value = "long")]
        Long,

        /// <summary>Long range type for storing long integer intervals.</summary>
        [EnumMember(Value = "long_range")]
        LongRange,

        /// <summary>Match-only text type for efficient text matching without scoring.</summary>
        [EnumMember(Value = "match_only_text")]
        MatchOnlyText,

        /// <summary>Object type for nested JSON object structures.</summary>
        [EnumMember(Value = "object")]
        Object,

        /// <summary>Point type for Cartesian coordinate points.</summary>
        [EnumMember(Value = "point")]
        Point,

        /// <summary>Scaled float type for high-precision floating point numbers with scaling factor.</summary>
        [EnumMember(Value = "scaled_float")]
        ScaledFloat,

        /// <summary>Search-as-you-type type for incremental search suggestions.</summary>
        [EnumMember(Value = "search_as_you_type")]
        SearchAsYouType,

        /// <summary>Shape type for geometric shapes and polygons.</summary>
        [EnumMember(Value = "shape")]
        Shape,

        /// <summary>16-bit signed integer type</summary>
        [EnumMember(Value = "short")]
        Short,

        /// <summary>Text type for full-text search with analysis.</summary>
        [EnumMember(Value = "text")]
        Text,

        /// <summary>Unsigned 64-bit integer type.</summary>
        [EnumMember(Value = "unsigned_long")]
        UnsignedLong,

        /// <summary>Version type for software version numbers.</summary>
        [EnumMember(Value = "version")]
        Version
    }
}