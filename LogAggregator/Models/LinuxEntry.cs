namespace LogAggregator.Models;

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
}
