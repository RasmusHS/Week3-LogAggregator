using System.Text.Json.Serialization;

namespace LogAggregator.Config;

public class WatchTarget
{
    [JsonPropertyName("Path")]
    public string Path { get; set; }

    [JsonPropertyName("Source")]
    public string Source { get; set; }

    [JsonPropertyName("LogFormat")]
    public string LogFormat { get; set; }

    [JsonPropertyName("TimeFormat")]
    public string TimeFormat { get; set; }

    [JsonPropertyName("Year")]
    public int Year { get; set; }
}
