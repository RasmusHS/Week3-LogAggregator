namespace LogAggregator.Models;

/// <summary>
/// Represents a log entry from a Linux system log, including process identifier (PID) and related metadata.
/// </summary>
/// <remarks>This class is typically used to parse and model individual entries from Linux log files, such as
/// syslog or auth.log. The log level is inferred from the message content using keyword-based heuristics. Inherits
/// common log entry properties from the LogEntry base class.</remarks>
public class LinuxEntry : LogEntry
{
    // Structure:
    // Timestamp: Month Day Time e.g. "Jan 12 15:04:05"
    // LogLevel: e.g. "combo"
    // Source: e.g. "su", "ftpd", "sshd(12345)"
    // PID: e.g. "12345"
    // Message: e.g. ""
    // Month,Date,Time,Level,Component,PID,Content
    // Jun,14,15:16:01,combo,sshd(pam_unix),19939,authentication failure; logname= uid=0 euid=0 tty=NODEVssh ruser= rhost=218.188.2.4

    // For loglevel: Use inference rules to determine the log level based on the content of the message. Keyword based heuristics.

    /// <summary>
    /// Initializes a new instance of the LinuxEntry class with the specified timestamp, component, process identifier,
    /// and message content.
    /// </summary>
    /// <remarks>The log level for the entry is inferred from the message content using keyword-based
    /// heuristics. This allows for flexible classification of log severity based on common terms such as "error",
    /// "fail", "warning", or "info".</remarks>
    /// <param name="timestamp">The date and time when the log entry was recorded.</param>
    /// <param name="component">The name of the component or service that generated the log entry, or null if not specified.</param>
    /// <param name="pid">The process identifier associated with the log entry.</param>
    /// <param name="message">The message content of the log entry.</param>
    public LinuxEntry(DateTimeOffset timestamp, string? component, string pid, string message)
    {
        Timestamp = timestamp;
        // LogLevel is determined by inference rules based on the content of the message. Keyword based heuristics.
        // Call a method to determine log level based on message content.
        // Method could look for keywords like "error", "fail", "warning", "info", etc. in the message to assign a log level.
        // The method will be placed either here or in a utility class.
        // A utility class might be better for separation of concerns and reusability across different log entry types.
        // LogLevel = LogLevelInference.InferLogLevel(message);
        LevelInferred = true; // Since log level is inferred from message content, we set this flag to true.
        Source = "Linux"; 
        Component = component;
        PID = pid;
        Message = message;
    }

    /// <summary>
    /// Gets or sets the process identifier (PID) associated with the current instance.
    /// </summary>
    public string PID { get; set; }
}
