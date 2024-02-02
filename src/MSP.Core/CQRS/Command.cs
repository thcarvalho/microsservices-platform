using FluentValidation.Results;
using MediatR;

namespace MSP.Core.CQRS;

public class Command : Message, IRequest<ValidationResult>
{
    public DateTime Timestamp { get; private set; }
    public ValidationResult ValidationResult { get; set; }

    protected Command()
    {
        Timestamp = DateTime.Now;
    }
}