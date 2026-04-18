namespace LogAggregator.Models;

/// <summary>
/// Represents a log entry parsed from an OpenSSH log file, containing details such as timestamp, component, process
/// identifier (PID), and message content.
/// </summary>
/// <remarks>This class is typically used to model individual entries from OpenSSH log files for analysis or
/// processing. The log level may be inferred from the message content using keyword-based heuristics. Inherits from
/// LogEntry.</remarks>
public class OpenSSHEntry : LogEntry
{
    // Structure:
    // Timestamp: Month Day Time e.g. "Jan 12 15:04:05"
    // Component/Source: e.g. "LabSZ"
    // PID: e.g. "12345"
    // Message: e.g. "Accepted password for user from
    // Date,Day,Time,Component,Pid,Content
    // Dec,10,06:55:46,LabSZ,24200,reverse mapping checking getaddrinfo for ns.marryaldkfaczcz.com [173.234.31.186] failed - POSSIBLE BREAK-IN ATTEMPT!

    /// <summary>
    /// Initializes a new instance of the OpenSSHEntry class with the specified timestamp, component, process
    /// identifier, and message content.
    /// </summary>
    /// <remarks>The log level for the entry is inferred from the message content using keyword-based
    /// heuristics. This constructor is typically used when parsing OpenSSH log files.</remarks>
    /// <param name="timestamp">The date and time when the log entry was recorded.</param>
    /// <param name="component">The name of the component or source that generated the log entry. May be null if not specified.</param>
    /// <param name="pid">The process identifier associated with the log entry.</param>
    /// <param name="message">The message content of the log entry.</param>
    public OpenSSHEntry(DateTimeOffset timestamp, string? component, string pid, string message)
    {
        Timestamp = timestamp;
        // LogLevel is determined by inference rules based on the content of the message. Keyword based heuristics.
        // Call a method to determine log level based on message content.
        // Method could look for keywords like "error", "fail", "warning", "info", etc. in the message to assign a log level.
        // The method will be placed either here or in a utility class.
        // A utility class might be better for separation of concerns and reusability across different log entry types.
        // LogLevel = LogLevelInference.InferLogLevel(message);
        LevelInferred = true; // Since log level is inferred from message content, we set this flag to true.
        Source = "OpenSSH";
        Component = component;
        PID = pid;
        Message = message;
    }

    /// <summary>
    /// Gets or sets the process identifier (PID) associated with the current instance.
    /// </summary>
    public string PID { get; set; }
}
