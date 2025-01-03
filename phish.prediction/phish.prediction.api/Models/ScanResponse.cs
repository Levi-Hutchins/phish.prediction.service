namespace phish.prediction.api.Models;

public class Message
{
    public string message { get; set; }
}

public class Result
{
    public string Visibility { get; set; }
    public Guid UUID { get; set; }
    public string URL { get; set; }
}
public class ScanResponse
{
    public bool Success { get; set; }
    public List<Message> Messages { get; set; }
    public Result Result { get; set; }
}