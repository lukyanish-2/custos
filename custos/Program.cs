namespace custos;

internal static class Program
{
    private static void Main(string[] args)
    {
        var logger = new ConsoleLogger();
        
        var custos = new Custos(logger);
        custos.Run();
    }
}