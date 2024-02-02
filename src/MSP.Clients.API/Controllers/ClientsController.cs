using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSP.WebAPI.Controller;
using MSP.WebAPI.Services;

namespace MSP.Clients.API.Controllers
{
    [Route("api/clients")]
    public class ClientsController : BaseApiController
    {
        public ClientsController(INotificationCollector notificationCollector) : base(notificationCollector)
        {
        }
    }
}
