namespace LogAggregator.Models;

/// <summary>
/// Represents a single log entry containing details about a logging event, such as timestamp, log level, source, and
/// message.
/// </summary>
/// <remarks>This is an abstract base class for log entries. Derived types can extend this class to include
/// additional information relevant to specific logging scenarios. Instances of this class are typically used to record
/// and process log events within logging frameworks.</remarks>
public class LogEntry
{
    public DateTimeOffset Timestamp { get; set; }
    public LogLevel? LogLevel { get; set; }
    public bool LevelInferred { get; set; } // Indicates whether the log level was inferred from the message content.
    public string Source { get; set; }
    public string? Component { get; set; }
    public Dictionary<string, string>? AdditionalProperties { get; set; } = new Dictionary<string, string>(); // For any additional properties that may be relevant to specific log entry types.
    public string Message { get; set; } 
}

/// <summary>
/// Specifies the severity level of a log message.
/// </summary>
/// <remarks>Use this enumeration to indicate the importance or urgency of log entries. Higher severity levels
/// typically represent more critical issues.</remarks>
public enum LogLevel
{
    Info,
    Warn,
    Error,
    Unknown
}
