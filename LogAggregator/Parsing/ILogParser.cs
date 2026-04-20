using LogAggregator.Models;

namespace LogAggregator.Parsing;

public interface ILogParser
{
    LogEntry Parse(string line);
}
