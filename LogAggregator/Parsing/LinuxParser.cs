using LogAggregator.Config;
using LogAggregator.Models;
using LogAggregator.Util;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LogAggregator.Parsing;

public class LinuxParser : ILogParser
{
    private readonly WatchTarget _target;
    private static readonly Regex _linuxRegex = new(@"^(?<ts>\w{3}\s+\d{1,2}\s\d{2}:\d{2}:\d{2})\s\S+\s(?<component>.+?)(?:\[(?<pid>\d+)\])?:\s+(?<message>.+)$", RegexOptions.Compiled);
    private int _currentYear;
    private int _lastMonth = 0;

    public LinuxParser(WatchTarget target)
    {
        _target = target;
        _currentYear = _target.Year;
    }

    public LogEntry Parse(string line)
    {
        var match = _linuxRegex.Match(line);
        if (!match.Success)
            return null;

        // Normalize double spaces to single (space-padded days like "Jun  9")
        var rawTs = Regex.Replace(match.Groups["ts"].Value, @"\s+", " ");
        var tsWithYear = $"{rawTs} {_currentYear}";

        var timestamp = DateTimeOffset.ParseExact(tsWithYear, $"{_target.TimeFormat} yyyy", CultureInfo.InvariantCulture);

        // Detect year rollover
        if (_lastMonth == 12 && timestamp.Month == 1)
        {
            _currentYear++;
            timestamp = timestamp.AddYears(1);
        }
        _lastMonth = timestamp.Month;

        return new LogEntry
        {
            Timestamp = timestamp,
            LogLevel = LogLevelInference.InferLogLevel(match.Groups["message"].Value, _target.Source),
            LevelInferred = true,
            Source = _target.Source,
            Component = match.Groups["component"].Value,
            AdditionalProperties = new Dictionary<string, string>
            {
                { "PID", match.Groups["pid"].Success ? match.Groups["pid"].Value : "NoPID" }
            },
            Message = match.Groups["message"].Value
        };
    }
}
