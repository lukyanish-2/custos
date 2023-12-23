namespace custos;

public class ConsoleLogger : Logger
{
    public override void Log(string message, LogLevel logLevel)
    {
        Console.WriteLine($"[{logLevel}]: {message}");
    }
}