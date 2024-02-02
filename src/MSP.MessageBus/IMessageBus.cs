using System;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Internals;
using MSP.Core.CQRS;
using MSP.Core.Models;

namespace MSP.MessageBus;

public interface IMessageBus : IDisposable
{
    bool IsConnected { get; }
    IAdvancedBus AdvancedBus { get; }

    void Publish<T>(T message) where T : IntegrationEvent;

    Task PublishAsync<T>(T message) where T : IntegrationEvent;

    void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class;

    AwaitableDisposable<ISubscriptionResult> SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class;

    TResponse Request<TRequest, TResponse>(TRequest request)
        where TRequest : IntegrationEvent
        where TResponse : MessageResponse;

    Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
        where TRequest : IntegrationEvent
        where TResponse : MessageResponse;

    IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
        where TRequest : IntegrationEvent
        where TResponse : MessageResponse;

    AwaitableDisposable<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
        where TRequest : IntegrationEvent
        where TResponse : MessageResponse;
}