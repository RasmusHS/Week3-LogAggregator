namespace LogAggregator.Models;

/// <summary>
/// Represents a log entry from an Apache HTTP server log, including timestamp, log level, and message content.
/// </summary>
/// <remarks>Use this class to encapsulate and process individual entries from Apache server logs. The log level
/// may be null if not specified in the log source. This type is typically used for parsing, analyzing, or displaying
/// Apache log data in structured form.</remarks>
public class ApacheEntry : LogEntry
{
    // Structure:
    // Timestamp: day month date time year example: Sun Dec 04 18:00:00 2022
    // LogLevel: e.g. "error", "warn", "notice"
    // Content: e.g. ""
    // Time,Level,Content
    // Sun Dec 04 04:47:44 2005,notice,workerEnv.init() ok /etc/httpd/conf/workers2.properties

    /// <summary>
    /// Initializes a new instance of the ApacheEntry class with the specified timestamp, log level, and message.
    /// </summary>
    /// <param name="timestamp">The date and time when the log entry was recorded.</param>
    /// <param name="logLevel">The severity level of the log entry. May be null if the log level is not specified.</param>
    /// <param name="message">The message content of the log entry.</param>
    public ApacheEntry(DateTimeOffset timestamp, LogLevel logLevel, string message)
    {
        Timestamp = timestamp;
        LogLevel = logLevel;
        LevelInferred = false;
        Source = "Apache";
        Component = "none";
        Message = message;
    }
}
