using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a request to define the mapping for fields in a data index.
    /// This class allows the specification of field properties and their types.
    /// </summary>
    public class MappingRequest
    {
        /// <summary>
        /// Gets or sets the dictionary of field properties where the key is the field name
        /// and the value is the corresponding mapping field definition.
        /// </summary>
        [JsonProperty("properties")]
        public Dictionary<string, MappingField> Properties { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingRequest"/> class.
        /// </summary>
        public MappingRequest()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingRequest"/> class with
        /// the specified field properties.
        /// </summary>
        /// <param name="properties">The dictionary of field properties.</param>
        public MappingRequest(Dictionary<string, MappingField> properties)
        {
            Properties = properties;
        }
    }

    /// <summary>
    /// Represents a mapping field definition, which includes the field type.
    /// </summary>
    public class MappingField
    {
        /// <summary>
        /// Gets or sets the type of the mapping field.
        /// This indicates how the field should be indexed and queried.
        /// </summary>
        [JsonProperty("type")]
        public MappingFieldType Type { get; set; }
    }

    /// <summary>
    /// Provides a enumeration containing field types
    /// that can be used in the mapping of data indices.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MappingFieldType
    {
        [EnumMember(Value = "aggregate_metric")]
        AggregateMetric,

        [EnumMember(Value = "binary")]
        Binary,

        [EnumMember(Value = "boolean")]
        Boolean,

        [EnumMember(Value = "byte")]
        Byte,

        [EnumMember(Value = "completion")]
        Completion,

        [EnumMember(Value = "date")]
        Date,

        [EnumMember(Value = "date_nanos")]
        DateNanos,

        [EnumMember(Value = "date_range")]
        DateRange,

        [EnumMember(Value = "dense_vector")]
        DenseVector,

        [EnumMember(Value = "flattened")]
        Flattened,

        [EnumMember(Value = "flat_object")]
        FlatObject,

        [EnumMember(Value = "float")]
        Float,

        [EnumMember(Value = "float_range")]
        FloatRange,

        [EnumMember(Value = "geo_point")]
        GeoPoint,

        [EnumMember(Value = "geo_shape")]
        GeoShape,

        [EnumMember(Value = "half_float")]
        HalfFloat,

        [EnumMember(Value = "histogram")]
        Histogram,

        [EnumMember(Value = "integer")]
        Integer,

        [EnumMember(Value = "integer_range")]
        IntegerRange,

        [EnumMember(Value = "ip")]
        Ip,

        [EnumMember(Value = "ip_range")]
        IpRange,

        [EnumMember(Value = "join")]
        Join,

        [EnumMember(Value = "keyword")]
        Keyword,

        [EnumMember(Value = "knn_vector")]
        KnnVector,

        [EnumMember(Value = "long")]
        Long,

        [EnumMember(Value = "long_range")]
        LongRange,

        [EnumMember(Value = "match_only_text")]
        MatchOnlyText,

        [EnumMember(Value = "object")]
        Object,

        [EnumMember(Value = "point")]
        Point,

        [EnumMember(Value = "scaled_float")]
        ScaledFloat,

        [EnumMember(Value = "search_as_you_type")]
        SearchAsYouType,

        [EnumMember(Value = "shape")]
        Shape,

        [EnumMember(Value = "short")]
        Short,

        [EnumMember(Value = "text")]
        Text,

        [EnumMember(Value = "unsigned_long")]
        UnsignedLong,

        [EnumMember(Value = "version")]
        Version
    }
}