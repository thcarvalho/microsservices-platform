using FluentValidation.Results;
using MSP.Core.Models;
using MSP.WebAPI.Models;

namespace MSP.WebAPI.Services;

public interface INotificationCollector
{
    void AddNotifications(ValidationResult notifications);
    void AddNotifications(IEnumerable<ErrorResponse> notifications);
    void AddNotification(ErrorResponse notification);
    void AddNotification(ValidationFailure notification);
    IEnumerable<ErrorResponse> Notifications { get; }
    bool HasNotifications { get; }
}