

using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace nswag_liquid_slim.ViewModels.Statistics
{
    public class GeoJsonFeatureCollection
    {
        public static GeoJsonFeatureCollection<T> Create<T>(IEnumerable<T> items)
            where T : IGeoJsonFeature
        {
            return new GeoJsonFeatureCollection<T>
            {
                Features = items
            };
        }
    }

    public class GeoJsonFeatureCollection<T> : GeoJsonFeatureCollection
        where T : IGeoJsonFeature
    {
        public string Type => "FeatureCollection";

        public IEnumerable<T> Features { get; set; } = null!;

        public object? Properties { get; set; }
    }

    public interface IGeoJsonFeature
    {
        string Type { get; }

        IGeoJsonGeometry Geometry { get; }

        object? Properties { get; }
    }

    public interface IGeoJsonGeometry
    {
        string Type => "Point";

        decimal[] Coordinates { get; }

        object? Properties { get; }
    }

    public struct GeoJsonGeometryPoint : IGeoJsonGeometry
    {
        public string Type { get; }
        
        public decimal[] Coordinates { get; }

        public GeoJsonGeometryPoint(string type, params decimal[] coordinates)
        {
            this.Type        = type;
            this.Coordinates = coordinates;
        }

        [JsonIgnore]
        public object? Properties => null;
    }
}