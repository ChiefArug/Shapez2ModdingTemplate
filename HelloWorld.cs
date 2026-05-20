using ILogger = Core.Logging.ILogger;
namespace HelloWorld;


public class Program : IMod {
    private readonly ILogger _logger;
    public Program(ILogger logger)
    {
        _logger = logger;
        logger.Info?.Log("Hello, World!");
    }

    public void Dispose()
    {
        _logger.Info?.Log("Goodbye, World!");
    }
}