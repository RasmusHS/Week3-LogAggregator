namespace LogAggregator.Models;

public class ApacheEntry : LogEntry
{
    // Structure:
    // Timestamp: day month date time year example: Sun Dec 04 18:00:00 2022
    // LogLevel: e.g. "error", "warn", "notice"
    // Content: e.g. ""
    // Time,Level,Content
    // Sun Dec 04 04:47:44 2005,notice,workerEnv.init() ok /etc/httpd/conf/workers2.properties
}
