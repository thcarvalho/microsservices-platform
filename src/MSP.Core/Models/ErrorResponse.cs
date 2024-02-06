namespace MSP.Core.Models;

public class ErrorResponse
{
    public string Key { get; set; }
    public string Message { get; set; }
    public ErrorTypeEnum Type { get; set; }

    public ErrorResponse(string key, string message, ErrorTypeEnum type = ErrorTypeEnum.BadRequest)
    {
        Key = key;
        Message = message;
        Type = type;
    }
}