using LogAggregator.Config;

namespace LogAggregator.Parsing;

public class LinuxParser : ILogParser
{
    private readonly WatchTarget _target;

    public LinuxParser(WatchTarget target)
    {
        _target = target;
    }

    public async Task<string> Parse(string input)
    {
        throw new NotImplementedException();
    }
}
