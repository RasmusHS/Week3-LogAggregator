using LogAggregator.Config;
using LogAggregator.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LogAggregator.Parsing;

public class ApacheParser : ILogParser
{
    private readonly WatchTarget _target;
    private static readonly Regex _apacheRegex = new(@"^\[(?<ts>.+?)\]\s\[(?<level>\w+)\]\s(?<message>.+)$", RegexOptions.Compiled);

    public ApacheParser(WatchTarget target)
    {
        _target = target;
    }

    public LogEntry Parse(string line)
    {
        var match = _apacheRegex.Match(line);
        if (!match.Success)
            return null;

        return new LogEntry
        {
            Timestamp = DateTimeOffset.ParseExact(match.Groups["ts"].Value, _target.TimeFormat, CultureInfo.InvariantCulture),
            LogLevel = match.Groups["level"].Value switch
            {
                "error" => LogLevel.Error,
                "warn" => LogLevel.Warn,
                "notice" => LogLevel.Info,
                _ => null
            },
            LevelInferred = false,
            Source = _target.Source,
            Component = null,
            Message = match.Groups["message"].Value
        };
    }
}