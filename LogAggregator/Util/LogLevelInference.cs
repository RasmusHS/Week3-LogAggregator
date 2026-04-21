using LogAggregator.Models;

namespace LogAggregator.Util;

public static class LogLevelInference
{
    // Method to infer log level from message content
    public static LogLevel InferLogLevel(string message, string source)
    {
        return source switch
        {
            "openssh" => InferFromKeywords(message, ErrorKeywordsOpenSSH, WarningKeywordsOpenSSH, InfoKeywordsOpenSSH),
            "linux" => InferFromKeywords(message, ErrorKeywordsLinux, WarningKeywordsLinux, InfoKeywordsLinux),
            _ => InferFromKeywords(message, ErrorKeywordsBase, WarningKeywordsBase, InfoKeywordsBase)
        };
    }

    private static LogLevel InferFromKeywords(string message, List<string> errorKeywords, List<string> warningKeywords, List<string> infoKeywords)
    {
        // Convert message to lower case for case-insensitive matching
        string lowerMessage = message.ToLower();

        if (errorKeywords.Any(k => lowerMessage.Contains(k)))
            return LogLevel.Error;
        if (warningKeywords.Any(k => lowerMessage.Contains(k)))
            return LogLevel.Warn;
        if (infoKeywords.Any(k => lowerMessage.Contains(k)))
            return LogLevel.Info;

        return LogLevel.Unknown;
    }

    // Base keywords for log level inference.
    private static readonly List<string> ErrorKeywordsBase = new List<string>() { "error", "fail", "failure", "critical", "fatal", "exception", "failed" };
    private static readonly List<string> WarningKeywordsBase = new List<string>() { "warn", "warning", "caution" };
    private static readonly List<string> InfoKeywordsBase = new List<string>() { "info", "notice", "debug" };

    // Keywords specific to OpenSSH logs.
    private static readonly List<string> ErrorKeywordsOpenSSH = new List<string>(ErrorKeywordsBase) { "authentication failure", "possible break-in attempt!", "authentication failures", "failed password" };
    private static readonly List<string> WarningKeywordsOpenSSH = new List<string>(WarningKeywordsBase) { "invalid user", "check pass: user unknown" };
    private static readonly List<string> InfoKeywordsOpenSSH = new List<string>(InfoKeywordsBase) { "accepted password", "session opened", "session closed", "received disconnect", "connection closed" };

    // Keywords specific to Linux logs.
    private static readonly List<string> ErrorKeywordsLinux = new List<string>(ErrorKeywordsBase) { "kernel panic", "segmentation fault", "out of memory", "couldn't add command channel" };
    private static readonly List<string> WarningKeywordsLinux = new List<string>(WarningKeywordsBase) { "disk space low", "high memory usage", "cpu temperature high", "is using obsolete" };
    private static readonly List<string> InfoKeywordsLinux = new List<string>(InfoKeywordsBase) { "system boot", "service started", "service stopped", "startup succeeded", "linux version", "console ready", "session closed for user", "session opened for user", "listening on ipv4 interface", "creating device node", "removing device node", "kernel time sync disabled", "kernel time sync enabled", "initializing.", "starting in permissive mode" };
}
