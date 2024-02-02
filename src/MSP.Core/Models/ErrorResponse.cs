namespace MSP.Core.Models;

public class ErrorResponse
{
    public string Key { get; set; }
    public string Message { get; set; }

    public ErrorResponse(string key, string message)
    {
        Key = key;
        Message = message;
    }
}