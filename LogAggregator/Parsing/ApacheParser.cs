using LogAggregator.Config;

namespace LogAggregator.Parsing;

public class ApacheParser : ILogParser
{
    private readonly WatchTarget _target;

    public ApacheParser(WatchTarget target)
    {
        _target = target;
    }

    public async Task<string> Parse(string input)
    {
        throw new NotImplementedException();
    }
}
