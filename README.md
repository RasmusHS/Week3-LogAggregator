# LogAggregator

A CLI tool that aggregates log files from multiple sources and formats, merges them by timestamp, and outputs a unified, searchable log stream.

Built as a learning project focused on file I/O, text parsing with regex, data normalization, and priority queue-based merge sorting in C#.

## Supported Log Formats

- **Apache** — `[Thu Jun 09 06:07:04 2005] [notice] message`
- **Hadoop** — `2015-10-17 15:37:56,547 INFO [main] component: message`
- **Linux** — `Jun  9 06:06:20 combo component[pid]: message`
- **OpenSSH** — `Dec 10 06:55:46 LabSZ sshd[24200]: message`

Each format has a dedicated parser implementing the `ILogParser` interface. All log lines are normalized into a common `LogEntry` model with timestamp, source, log level, component, and message.

## How It Works

1. **Discovery** — Finds log files in configured directories
2. **Parsing** — Each parser extracts structured data from raw log lines using regex
3. **Merging** — A priority queue (min-heap) interleaves entries from all sources by timestamp
4. **Output** — Merged entries are printed in a unified format

## Output Format

```
Timestamp | Source | LogLevel, Inferred=true/false | AdditionalProperties | Component | Message
```

## Configuration

Log sources are defined in `appsettings.json`:

```json
{
  "WatchTargets": [
    {
      "Path": "sample-logs/Apache/",
      "Source": "apache",
      "LogFormat": "apache",
      "TimeFormat": "ddd MMM dd HH:mm:ss yyyy"
    },
    {
      "Path": "sample-logs/Hadoop/",
      "Source": "hadoop",
      "LogFormat": "hadoop",
      "TimeFormat": "yyyy-MM-dd HH:mm:ss,fff"
    },
    {
      "Path": "sample-logs/Linux/",
      "Source": "linux",
      "LogFormat": "linux",
      "TimeFormat": "MMM d HH:mm:ss",
      "Year": 2004
    },
    {
      "Path": "sample-logs/SSH/",
      "Source": "openssh",
      "LogFormat": "openssh",
      "TimeFormat": "MMM d HH:mm:ss",
      "Year": 2017
    }
  ]
}
```

Linux and OpenSSH logs lack a year in their timestamps. The `Year` field provides the starting year, and the parsers automatically detect year rollovers (December → January).

## Sample Logs

The `sample-logs/` directory is gitignored due to file size. The logs used for development and testing are from the [LogHub](https://github.com/logpai/loghub) repository:

- [Apache](https://github.com/logpai/loghub/blob/master/Apache)
- [Hadoop](https://github.com/logpai/loghub/blob/master/Hadoop)
- [Linux](https://github.com/logpai/loghub/blob/master/Linux)
- [OpenSSH](https://github.com/logpai/loghub/blob/master/OpenSSH)

## Log Level Inference

Apache and Hadoop logs include explicit severity levels. Linux and OpenSSH logs do not, so log levels are inferred from message content using keyword matching. Inferred levels are flagged with `Inferred=true` in the output. A shared inference utility handles common keywords, with per-format extensions for source-specific phrases.

## Project Structure

```
LogAggregator/
├── LogAggregator.sln
├── LogAggregator/
│   ├── Program.cs
│   ├── appsettings.json
│   ├── Config/
│   │   ├── AppSettings.cs
│   │   └── WatchTarget.cs
│   ├── Parsing/
│   │   ├── ILogParser.cs
│   │   ├── ApacheParser.cs
│   │   ├── HadoopParser.cs
│   │   ├── LinuxParser.cs
│   │   └── OpenSSHParser.cs
│   ├── Models/
│   │   └── LogEntry.cs
│   ├── Merging/
│   │   └── LogMerger.cs
│   └── Util/
│       ├── LogDiscovery.cs
│       └── LogLevelInference.cs
```

## Requirements

- .NET 6+
