using MSP.Core.Models;

namespace MSP.Core.CQRS;

public class CommandResult
{
    public bool Success { get; set; }
    public IEnumerable<ErrorResponse> Errors { get; set; }
}