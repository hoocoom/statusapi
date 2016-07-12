using System;
using System.Web.Http;
using Status.Core.Entities;
using Status.Core.Entities.Domain;

namespace Status.Web.Controllers
{
    public sealed class DemoController : ApiController
    {
        public ServiceStatus GetOk()
        {
            return new ServiceStatus()
            {
                State = State.Ok,
            };
        }

        public ServiceStatus GetMaintenance()
        {
            return new ServiceStatus()
            {
                State = State.Maintenance,
            };
        }

        public ServiceStatus GetMaintenancePlanned()
        {
            return new ServiceStatus()
            {
                State = State.MaintenancePlanned,
                MaintenancePlannedDate = DateTime.UtcNow.Date.AddDays(10)
            };
        }
    }
}
