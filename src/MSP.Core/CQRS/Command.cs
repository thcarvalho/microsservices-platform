using FluentValidation.Results;
using MediatR;

namespace MSP.Core.CQRS;

public class Command : Message, IRequest<CommandResult>
{
    public DateTime Timestamp { get; private set; }
    public CommandResult CommandResult { get; set; }

    protected Command()
    {
        Timestamp = DateTime.Now;
    }
}