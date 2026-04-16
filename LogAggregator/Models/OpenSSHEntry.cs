namespace LogAggregator.Models;

public class OpenSSHEntry : LogEntry
{
    // Structure:
    // Timestamp: Month Day Time e.g. "Jan 12 15:04:05"
    // Component/Source: e.g. "LabSZ"
    // PID: e.g. "12345"
    // Message: e.g. "Accepted password for user from
    // Date,Day,Time,Component,Pid,Content
    // Dec,10,06:55:46,LabSZ,24200,reverse mapping checking getaddrinfo for ns.marryaldkfaczcz.com [173.234.31.186] failed - POSSIBLE BREAK-IN ATTEMPT!
}
