namespace LogAggregator.Parsing;

public interface ILogParser
{
    Task<string> Parse(string input);
}
