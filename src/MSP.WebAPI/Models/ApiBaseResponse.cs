using MSP.Core.Models;

namespace MSP.WebAPI.Models;

public class ApiBaseResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public List<ErrorResponse> Errors { get; set; }

    public ApiBaseResponse() { }

    public ApiBaseResponse(T? data, List<ErrorResponse> errors = null)
    {
        Data = data;
        Errors = errors ?? new List<ErrorResponse>();
        Success = Errors.Count == 0;
    }
}