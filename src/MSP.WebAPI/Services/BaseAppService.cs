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
    
    protected IEnumerable<Claim> GetUserClaims()
        => _httpContextAccessor.HttpContext!.User.Claims;

    protected int GetLoggedUserId() 
        => int.Parse(GetUserClaims().SingleOrDefault(x => x.Type == ClaimTypes.Sid)!.Value);

    protected MessageResponse GetMessageResponse() 
        => new (_notificationsCollector.Notifications);
}