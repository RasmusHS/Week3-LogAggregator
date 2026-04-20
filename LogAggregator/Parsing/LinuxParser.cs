using LogAggregator.Config;
using LogAggregator.Models;

namespace LogAggregator.Parsing;

public class LinuxParser : ILogParser
{
    private readonly WatchTarget _target;

    public LinuxParser(WatchTarget target)
    {
        _target = target;
    }

    public LogEntry Parse(string line)
    {
        throw new NotImplementedException();
    }
}
