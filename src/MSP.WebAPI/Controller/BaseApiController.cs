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

    protected ApiBaseResponse<T> BaseResponse<T>(T data)
    {
        var response = _notificationCollector.HasNotifications
            ? new ApiBaseResponse<T>(data, Notifications.ToList())
            : new ApiBaseResponse<T>(data);

        HttpContext.Response.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);

        return response;
    }

    protected ApiBaseResponse<object?> BaseEmptyResponse()
    {
        var response = _notificationCollector.HasNotifications
            ? new ApiBaseResponse<object?>(null, Notifications.ToList())
            : new ApiBaseResponse<object?>(null);

        HttpContext.Response.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);

        return response;
    }
}