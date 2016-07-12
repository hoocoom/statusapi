using System;
using System.Runtime.Serialization;

namespace Status.DemoApiClient.StatusService.Contracts
{
    public sealed class ServiceStatus
    {
        public State State { get; set; }
        public DateTime? MaintenancePlannedDate { get; set; }
    }
}