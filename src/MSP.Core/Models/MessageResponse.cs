using FluentValidation.Results;
using MSP.Core.CQRS;

namespace MSP.Core.Models;

public class MessageResponse : Message
{
    public bool IsValid { get; set; }
    public IEnumerable<ErrorResponse>? Errors { get; set; }
    public MessageResponse(IEnumerable<ErrorResponse>? errors = null)
    {
        Errors = errors;
        IsValid = errors is null || !errors.Any();
    }
}