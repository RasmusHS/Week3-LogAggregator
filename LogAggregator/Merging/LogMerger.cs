using LogAggregator.Models;
using LogAggregator.Parsing;

namespace LogAggregator.Merging;

public class LogMerger
{
    private readonly List<(ILogParser Parser, IEnumerable<string> Files)> _sources;

    public LogMerger(List<(ILogParser Parser, IEnumerable<string> Files)> sources)
    {
        _sources = sources;
    }

    public IEnumerable<LogEntry> Merge()
    {
        var pq = new PriorityQueue<(LogEntry Entry, int Index), DateTimeOffset>();
        var enumerators = new IEnumerator<LogEntry>[_sources.Count];

        // Create a flattened enumerator per source and seed the heap
        for (int i = 0; i < _sources.Count; i++)
        {
            enumerators[i] = FlattenSource(_sources[i].Parser, _sources[i].Files).GetEnumerator();

            if (enumerators[i].MoveNext())
            {
                pq.Enqueue((enumerators[i].Current, i), enumerators[i].Current.Timestamp);
            }
        }

        // Pull smallest timestamp, advance that source, repeat
        while (pq.Count > 0)
        {
            var (entry, idx) = pq.Dequeue();
            yield return entry;

            if (enumerators[idx].MoveNext())
            {
                pq.Enqueue((enumerators[idx].Current, idx), enumerators[idx].Current.Timestamp);
            }
        }

        foreach (var e in enumerators) e.Dispose();
    }

    private static IEnumerable<LogEntry> FlattenSource(ILogParser parser, IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            foreach (var line in File.ReadLines(file))
            {
                var entry = parser.Parse(line);
                if (entry is not null)
                {
                    yield return entry;
                }
            }
        }
    }
}
