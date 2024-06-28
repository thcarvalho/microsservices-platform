using System.Net;
using MSP.WebAPI.Models;
using MSP.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MSP.Core.Models;

namespace MSP.WebAPI.Controller;

[ApiController]
public class BaseApiController : ControllerBase
{
    protected readonly INotificationCollector _notificationCollector;

    public BaseApiController(INotificationCollector notificationCollector)
    {
        _notificationCollector = notificationCollector;
    }

    protected IEnumerable<ErrorResponse> Notifications => _notificationCollector.Notifications.ToList();
    protected ApiBaseResponse<T> BaseResponse<T>(T? data) => ApiResponseFactory.CreateBaseResponse(data, _notificationCollector, HttpContext);
}