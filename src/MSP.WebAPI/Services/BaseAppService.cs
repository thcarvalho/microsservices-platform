using System.Security.Claims;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using MSP.Core.Models;

namespace MSP.WebAPI.Services;

public abstract class BaseAppService
{
    protected readonly INotificationCollector _notificationsCollector;
    protected readonly IHttpContextAccessor _httpContextAccessor;

    protected BaseAppService(
        INotificationCollector notifications,
        IHttpContextAccessor httpContextAccessor)
    {
        _notificationsCollector = notifications;
        _httpContextAccessor = httpContextAccessor;
    }

    public void Notify(string propName, string message, object args)
        => _notificationsCollector.AddNotification(new ErrorResponse(propName, message));

    protected IEnumerable<Claim> GetUserClaims()
        => _httpContextAccessor.HttpContext!.User.Claims;

    protected int GetLoggedUserId() 
        => int.Parse(GetUserClaims().SingleOrDefault(x => x.Type == ClaimTypes.Sid)!.Value);

    protected MessageResponse GetMessageResponse(ValidationResult validation) 
        => new MessageResponse(validation.Errors.Select(x => new ErrorResponse(x.PropertyName, x.ErrorMessage)));
}