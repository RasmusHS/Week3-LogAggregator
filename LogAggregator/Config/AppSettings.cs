using System.Text.Json.Serialization;

namespace LogAggregator.Config;

public class AppSettings
{
    [JsonPropertyName("WatchTargets")]
    public List<WatchTarget> WatchTargets { get; set; }
}
