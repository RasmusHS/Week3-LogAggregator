using LogAggregator.Config;

namespace LogAggregator.Parsing;

public class HadoopParser : ILogParser
{
    private readonly WatchTarget _target;

    public HadoopParser(WatchTarget target)
    {
        _target = target;
    }

    public async Task<string> Parse(string input)
    {
        throw new NotImplementedException();
    }
}
