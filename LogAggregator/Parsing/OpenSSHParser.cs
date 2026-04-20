using LogAggregator.Config;
using LogAggregator.Models;

namespace LogAggregator.Parsing;

public class OpenSSHParser : ILogParser
{
    private readonly WatchTarget _target;

    public OpenSSHParser(WatchTarget target)
    {
        _target = target;
    }

    public LogEntry Parse(string line)
    {
        throw new NotImplementedException();
    }
}
