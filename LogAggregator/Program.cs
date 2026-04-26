using LogAggregator.Config;
using LogAggregator.Merging;
using LogAggregator.Parsing;
using LogAggregator.Util;
using System.Diagnostics;
using System.Text.Json;

namespace LogAggregator;

public class Program
{
    public static void Main(string[] args)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        var config = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText("appsettings.json"));

        var sources = new List<(ILogParser Parser, IEnumerable<string> Files)>();

        foreach (var target in config.WatchTargets)
        {
            ILogParser parser = target.LogFormat switch
            {
                "apache" => new ApacheParser(target),
                "hadoop" => new HadoopParser(target),
                "linux" => new LinuxParser(target),
                "openssh" => new OpenSSHParser(target),
                _ => throw new ArgumentException($"Unknown log format: {target.LogFormat}")
            };

            var files = LogDiscovery.DiscoverLogFiles(target.Path);
            sources.Add((parser, files));
        }

        var merger = new LogMerger(sources);

        foreach (var entry in merger.Merge())
        {
            Console.WriteLine($"{entry.Timestamp:yyyy-MM-dd HH:mm:ss} | {entry.Source} | {entry.LogLevel}, Inferred={entry.LevelInferred} | {string.Join(", ", entry.AdditionalProperties.Select(kv => $"{kv.Key}={kv.Value}"))} | {entry.Component} | {entry.Message}");
        }

        stopwatch.Stop();

        // Print out the total time taken to parse all logs in minutes, seconds and milliseconds.
        Console.WriteLine($"Total time taken: {stopwatch.Elapsed.Minutes}m {stopwatch.Elapsed.Seconds}s {stopwatch.Elapsed.Milliseconds}ms");
    }
}