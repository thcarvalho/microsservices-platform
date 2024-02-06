using FluentValidation.Results;
using MSP.Core.Models;

namespace MSP.WebAPI.Services;

public interface INotificationCollector
{
    void AddNotifications(IEnumerable<ValidationFailure> notifications);
    void AddNotifications(IEnumerable<ErrorResponse> notifications);
    void AddNotification(ErrorResponse notification);
    void AddNotification(ValidationFailure notification);
    IEnumerable<ErrorResponse> Notifications { get; }
    bool HasNotifications { get; }
}