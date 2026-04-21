using LogAggregator.Config;
using LogAggregator.Parsing;
using LogAggregator.Util;
using System.Text.Json;

namespace LogAggregator;

public class Program
{
    public static void Main(string[] args)
    {
        var config = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText("appsettings.json"));

        foreach (var target in config.WatchTargets)
        {
            ILogParser parser = target.LogFormat switch
            {
                "apache" => new ApacheParser(target),
                "hadoop" => throw new NotImplementedException(),
                "linux" => throw new NotImplementedException(),
                "openssh" => new OpenSSHParser(target),
                _ => throw new ArgumentException($"Unknown log format: {target.LogFormat}")
            };

            var files = LogDiscovery.DiscoverLogFiles(target.Path);

            foreach (var file in files)
            {
                foreach (var line in File.ReadLines(file))
                {
                    var entry = parser.Parse(line);
                    if (entry is not null)
                    {
                        Console.WriteLine($"{entry.Timestamp:yyyy-MM-dd HH:mm:ss} | {entry.LogLevel} | {entry.Source} | {entry.Message}");
                    }
                    //else
                    //{
                    //    Console.WriteLine($"SKIPPED: {line}");
                    //}
                }
            }
        }
    }
}