namespace App.Batches;

public class Batch1Service : IBatch1Service
{
    private readonly ILogger _logger;

    public Batch1Service(ILogger<Batch1Service> logger)
    {
        _logger = logger;
    }

    public void DoBatch()
    {
        _logger.LogInformation("DoBatch");
    }
}
