namespace LogAggregator.Models;

public class LogEntry
{

    public DateTime Timestamp { get; set; }
    //public string? LogLevel { get; set; }
    public LogLevel? LogLevel { get; set; }
    public string Message { get; set; } 
    public string Source { get; set; }

    
}

public enum LogLevel
{
    Info,
    Warn,
    Error,
    Debug
}
