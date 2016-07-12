using System;
using Status.Core.Entities.Domain;

namespace Status.Core.Entities
{
    public class ServiceStatus
    {
        public State State { get; set; } 
        public DateTime? MaintenancePlannedDate { get; set; }
    }

    public class ServiceStatusUpdate : ServiceStatus
    {
        public string Description { get; set; }
    }

    public class ServiceStatusLogEntry : ServiceStatusUpdate
    {
        public DateTime CreateDate { get; set; }
    }
}