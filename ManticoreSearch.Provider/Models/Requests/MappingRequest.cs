using Newtonsoft.Json;

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
        public MappingRequest() { }

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
        public string Type { get; set; }
    }

    /// <summary>
    /// Provides a static class containing constants for various field types 
    /// that can be used in the mapping of data indices.
    /// </summary>
    public class FieldType
    {
        public static string AggregateMetric => "aggregate_metric";
        public static string Binary => "binary";
        public static string Boolean => "boolean";
        public static string Byte => "byte";
        public static string Completion => "completion";
        public static string Date => "date";
        public static string DateNanos => "date_nanos";
        public static string DateRange => "date_range";
        public static string DenseVector => "dense_vector";
        public static string Flattened => "flattened";
        public static string FlatObject => "flat_object";
        public static string Float => "float";
        public static string FloatRange => "float_range";
        public static string GeoPoint => "geo_point";
        public static string GeoShape => "geo_shape";
        public static string HalfFloat => "half_float";
        public static string Histogram => "histogram";
        public static string Integer => "integer";
        public static string IntegerRange => "integer_range";
        public static string Ip => "ip";
        public static string IpRange => "ip_range";
        public static string Keyword => "keyword";
        public static string KnnVector => "knn_vector";
        public static string Long => "long";
        public static string LongRange => "long_range";
        public static string MatchOnlyText => "match_only_text";
        public static string Object => "object";
        public static string Point => "point";
        public static string ScaledFloat => "scaled_float";
        public static string SearchAsYouType => "search_as_you_type";
        public static string Shape => "shape";
        public static string Short => "short";
        public static string Text => "text";
        public static string UnsignedLong => "unsigned_long";
        public static string Version => "version";
    }
}
