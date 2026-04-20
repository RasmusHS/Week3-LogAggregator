namespace LogAggregator.Util;

/// <summary>
/// Provides methods for discovering log files within a specified directory or for returning a single file if a file
/// path is provided.
/// </summary>
public static class LogDiscovery
{
    /// <summary>
    /// Discovers all log files within the specified directory or returns the file if a file path is provided.
    /// </summary>
    /// <param name="path">The path to a directory to search for log files, or the path to a single file. The path must refer to an
    /// existing directory or file.</param>
    /// <returns>An enumerable collection of file paths. If a directory is specified, returns all files within the directory and
    /// its subdirectories. If a file is specified, returns a collection containing only that file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the specified path does not exist as either a file or a directory.</exception>
    public static IEnumerable<string> DiscoverLogFiles(string path)
    {
        if (Directory.Exists(path))
        {
            return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
        }
        else if (File.Exists(path))
        {
            return new[] { path };
        }
        else
        {
            throw new FileNotFoundException($"The specified path '{path}' does not exist.");
        }
    }
}
