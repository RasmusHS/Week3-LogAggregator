using LogAggregator.Config;

namespace LogAggregator.Parsing;

public class OpenSSHParser : ILogParser
{
    private readonly WatchTarget _target;

    public OpenSSHParser(WatchTarget target)
    {
        _target = target;
    }

    public async Task<string> Parse(string input)
    {
        throw new NotImplementedException();
    }
}
