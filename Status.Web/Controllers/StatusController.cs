using System.Web.Http;
using Status.Core.Entities;
using Status.Core.Services;

namespace Status.Web.Controllers
{
    public sealed class StatusController : ApiController
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        public ServiceStatus GetStatus()
        {
            var currentStatus = _statusService.GetServiceStatus();
            
            return currentStatus;
        }
    }
}
