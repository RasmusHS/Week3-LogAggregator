using LogAggregator.Config;
using LogAggregator.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LogAggregator.Parsing;

public class HadoopParser : ILogParser
{
    private readonly WatchTarget _target;
    private static readonly Regex _hadoopRegex = new(@"^(?<ts>\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2},\d{3})\s(?<level>\w+)\s\[(?<process>[^\]]+)\]\s(?<component>\S+?):\s(?<message>.+)$", RegexOptions.Compiled);

    public HadoopParser(WatchTarget target)
    {
        _target = target;
    }

    public LogEntry Parse(string line)
    {
        var match = _hadoopRegex.Match(line);
        if (!match.Success)
            return null;

        return new LogEntry
        { 
            Timestamp = DateTimeOffset.ParseExact(match.Groups["ts"].Value, _target.TimeFormat, CultureInfo.InvariantCulture),
            LogLevel = match.Groups["level"].Value switch
            {
                "INFO" => LogLevel.Info,
                "WARN" => LogLevel.Warn,
                "ERROR" => LogLevel.Error,
                _ => null
            },
            LevelInferred = false,
            Source = _target.Source,
            AdditionalProperties = new Dictionary<string, string>
            {
                { "Process", match.Groups["process"].Value }
            },
            Component = match.Groups["component"].Value,
            Message = match.Groups["message"].Value
        };
    }
}
