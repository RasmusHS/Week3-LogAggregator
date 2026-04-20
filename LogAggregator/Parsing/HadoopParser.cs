using LogAggregator.Config;
using LogAggregator.Models;

namespace LogAggregator.Parsing;

public class HadoopParser : ILogParser
{
    private readonly WatchTarget _target;

    public HadoopParser(WatchTarget target)
    {
        _target = target;
    }

    public LogEntry Parse(string line)
    {
        throw new NotImplementedException();
    }
}
