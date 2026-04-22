using LogAggregator.Config;
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
        int linesParsedCounter = 0;
        int skippedLineCounter = 0;

        var config = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText("appsettings.json"));

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

            foreach (var file in files)
            {
                foreach (var line in File.ReadLines(file))
                {
                    var entry = parser.Parse(line);
                    if (entry is not null)
                    {
                        Console.WriteLine($"{entry.Timestamp:yyyy-MM-dd HH:mm:ss} | {entry.Source} | {entry.LogLevel}, Inferred={entry.LevelInferred.ToString()} | {string.Join(", ", entry.AdditionalProperties.Select(kv => $"{kv.Key}={kv.Value}"))} | {entry.Component} | {entry.Message}");
                        linesParsedCounter++;
                    }
                    else
                    {
                        skippedLineCounter++;
                    }
                }
            }
        }
        stopwatch.Stop();

        // Print out the total time taken to parse all logs in minutes, seconds and milliseconds, as well as the total number of lines parsed and skipped.
        Console.WriteLine($"Total time taken: {stopwatch.Elapsed.Minutes}m {stopwatch.Elapsed.Seconds}s {stopwatch.Elapsed.Milliseconds}ms");
        Console.WriteLine($"Total lines parsed: {linesParsedCounter}");
        Console.WriteLine($"Total lines skipped: {skippedLineCounter}");
    }
}