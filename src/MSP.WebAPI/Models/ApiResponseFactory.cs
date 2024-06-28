using System.Net;
using Microsoft.AspNetCore.Http;
using MSP.WebAPI.Services;

namespace MSP.WebAPI.Models;

public class ApiResponseFactory
{
    public static ApiBaseResponse<T> CreateBaseResponse<T>(T? data, INotificationCollector notificationCollector, HttpContext context)
    {
        var response = notificationCollector.HasNotifications
            ? new ApiBaseResponse<T>(data, notificationCollector.Notifications.ToList())
            : new ApiBaseResponse<T>(data);

        context.Response.StatusCode = GetStatusCode(notificationCollector);

        return response;
    }

    private static int GetStatusCode(INotificationCollector notificationCollector)
    {
        if (!notificationCollector.HasNotifications)
            return (int)HttpStatusCode.OK;

        var notification = notificationCollector.Notifications.FirstOrDefault();
        return (int)notification.Type;
    }
}