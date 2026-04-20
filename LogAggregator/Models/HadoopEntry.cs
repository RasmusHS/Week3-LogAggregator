namespace LogAggregator.Models;

/// <summary>
/// Represents a log entry specific to Hadoop log formats, including process, component, and message details.
/// </summary>
/// <remarks>This class extends LogEntry to provide additional context relevant to Hadoop logs, such as the
/// originating process and component. It is designed to facilitate parsing and analysis of Hadoop log files in
/// structured logging scenarios.</remarks>
public class HadoopEntry : LogEntry
{
    // Structure:
    // Timestamp: year-month-day hour:minute:second,millisecond example: 2022-12-04 18:00:00,999
    // LogLevel: e.g. "INFO", "ERROR", "WARN"
    // Component/source: e.g. "org.apache.hadoop.yarn.webapp.WebApps"
    // Message: e.g. "Registered webapp guice modules"
    // Date,Time,Level,Process,Component,Content
    // 2015-10-18,"18:01:47,978",INFO,main,org.apache.hadoop.mapreduce.v2.app.MRAppMaster,Created MRAppMaster for application appattempt_1445144423722_0020_000001

    /// <summary>
    /// Initializes a new instance of the HadoopEntry class with the specified timestamp, log level, process, component,
    /// and message.
    /// </summary>
    /// <remarks>The Source property is set to "Hadoop" for all entries created with this
    /// constructor.</remarks>
    /// <param name="timestamp">The date and time when the log entry was created.</param>
    /// <param name="logLevel">The severity level of the log entry, such as INFO, ERROR, or WARN. May be null if the log level is not
    /// specified.</param>
    /// <param name="process">The name of the process or thread that generated the log entry.</param>
    /// <param name="component">The component or source within Hadoop that produced the log entry. May be null if not specified.</param>
    /// <param name="message">The content or message of the log entry.</param>
    public HadoopEntry(DateTimeOffset timestamp, LogLevel logLevel, string process, string? component, string message)
    {
        Timestamp = timestamp;
        LogLevel = logLevel;
        LevelInferred = false;
        Process = process;
        Source = "Hadoop";
        Component = component;
        Message = message;
    }

    /// <summary>
    /// Gets or sets the name of the process associated with this instance.
    /// </summary>
    public string Process { get; set; }
}
