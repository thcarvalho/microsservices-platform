using FluentValidation.Results;
using MSP.Core.Models;
using MSP.WebAPI.Models;

namespace MSP.WebAPI.Services;

public class NotificationCollector : INotificationCollector
{
    private readonly List<ErrorResponse> _notificationsCollector = new();
    public bool HasNotifications => _notificationsCollector.Any();
    public void AddNotification(ValidationFailure notification)
        => _notificationsCollector.Add(new ErrorResponse(notification.PropertyName, notification.ErrorMessage));

    public IEnumerable<ErrorResponse> Notifications => _notificationsCollector;

    public void AddNotification(ErrorResponse notification)
        => _notificationsCollector.Add(notification);

    public void AddNotifications(ValidationResult notifications)
        => _notificationsCollector.AddRange(notifications.Errors.Select(x => new ErrorResponse(x.PropertyName, x.ErrorMessage)));
    
    public void AddNotifications(IEnumerable<ErrorResponse> notifications)
        => _notificationsCollector.AddRange(notifications);

}