using FluentValidation.Results;
using MSP.Core.CQRS;

namespace MSP.Core.Mediator;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T evento) where T : Event;
    Task<CommandResult> SendCommand<T>(T comando) where T : Command;
}