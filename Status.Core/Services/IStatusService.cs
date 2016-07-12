using System.Collections.Generic;
using Status.Core.Entities;

namespace Status.Core.Services
{
    /// <summary>
    /// Сервис для работы со статусом
    /// </summary>
    public interface IStatusService
    {
        ServiceStatus GetServiceStatus();

        IReadOnlyCollection<ServiceStatusLogEntry> GetServiceStatusHistory();

        void SetServiceStatus(ServiceStatusUpdate status);
    }
}