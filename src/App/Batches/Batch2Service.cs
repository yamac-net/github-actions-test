namespace App.Batches;

public class Batch2Service : IBatch2Service
{
    private readonly ILogger _logger;

    public Batch2Service(ILogger<Batch2Service> logger)
    {
        _logger = logger;
    }

    public void DoBatch()
    {
        _logger.LogInformation("DoBatch");
    }
}
