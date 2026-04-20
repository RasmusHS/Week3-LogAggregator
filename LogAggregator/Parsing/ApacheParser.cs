using LogAggregator.Config;
using LogAggregator.Models;
using System.Text.RegularExpressions;

namespace LogAggregator.Parsing;

public class ApacheParser : ILogParser
{
    private readonly WatchTarget _target;

    public ApacheParser(WatchTarget target)
    {
        _target = target;
    }

    public LogEntry Parse(string line)
    {
        LogEntry temp = Regex.Matches(line, @"^\[(?<ts>.+?)\]\s\[(?<level>\w+)\]\s(?<message>.+)$").FirstOrDefault() is Match match
            ? new LogEntry
            {
                // Parse the timestamp using the provided format and convert from ddd MMM dd HH:mm:ss yyyy to yyyy-MM-dd HH:mm:ss format
                Timestamp = DateTimeOffset.ParseExact(match.Groups["ts"].Value, "ddd MMM dd HH:mm:ss yyyy", null),
                LogLevel = match.Groups["level"].Value switch
                {
                    "error" => LogLevel.Error,
                    "warn" => LogLevel.Warn,
                    "notice" => LogLevel.Info,
                    _ => null
                },
                LevelInferred = false,
                Component = null,
                Source = _target.Source,
                Message = match.Groups["message"].Value,
            } : throw new FormatException($"Line does not match expected Apache log format: {line}");
    }
}
