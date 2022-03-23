namespace PickPoint.RestApi.Service;

internal static class Program
{
    private const string DefaultBaseDir = "/var/pickpoint/backend";
    private const int DefaultPort = 5757;

    public static void Main(
        string baseDir = DefaultBaseDir,
        int port = DefaultPort
    )
    {
        if (string.IsNullOrWhiteSpace(baseDir))
        {
            throw new ArgumentException("Value cannot be null or whitespace", nameof(baseDir));
        }

        if (port <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(port));
        }
    }
}