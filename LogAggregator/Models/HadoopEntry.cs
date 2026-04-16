namespace LogAggregator.Models;

public class HadoopEntry : LogEntry
{
    // Structure:
    // Timestamp: year-month-day hour:minute:second,PID example: 2022-12-04 18:00:00,999
    // LogLevel: e.g. "INFO", "ERROR", "WARN"
    // Component/source: e.g. "org.apache.hadoop.yarn.webapp.WebApps"
    // Message: e.g. "Registered webapp guice modules"
    // Date,Time,Level,Process,Component,Content
    // 2015-10-18,"18:01:47,978",INFO,main,org.apache.hadoop.mapreduce.v2.app.MRAppMaster,Created MRAppMaster for application appattempt_1445144423722_0020_000001
}
