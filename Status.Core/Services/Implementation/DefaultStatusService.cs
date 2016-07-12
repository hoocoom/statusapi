using System.Collections.Generic;
using System.Linq;
using Status.Core.Entities;
using Status.Core.Entities.Domain;
using Status.Core.Repositories;

namespace Status.Core.Services.Implementation
{
    public sealed class DefaultStatusService : IStatusService
    {
        private readonly IStateLogRepository _stateLogRepository;

        public DefaultStatusService(IStateLogRepository stateLogRepository)
        {
            _stateLogRepository = stateLogRepository;
        }
        
        public ServiceStatus GetServiceStatus()
        {
            var currentStatus = _stateLogRepository.GetState();

            return new ServiceStatus()
            {
                State = currentStatus.State,
                MaintenancePlannedDate = currentStatus.Date
            };
        }

        public IReadOnlyCollection<ServiceStatusLogEntry> GetServiceStatusHistory()
        {
            var statuses = _stateLogRepository.GetStateHistory();

            return statuses.Select(x => new ServiceStatusLogEntry()
            {
                State = x.State,
                MaintenancePlannedDate = x.Date,
                CreateDate = x.CreateDate,
                Description = x.Description
            }).ToList().AsReadOnly();
        }

        public void SetServiceStatus(ServiceStatusUpdate status)
        {
            if (status.State == State.MaintenancePlanned && !status.MaintenancePlannedDate.HasValue)
                throw new StatusChangeException("для состояния 'запланированы работы' должна быть задана дата работ");
            
            if (status.State != State.MaintenancePlanned && status.MaintenancePlannedDate.HasValue)
                throw new StatusChangeException("Дата работ может быть задана только для состояния 'запланированы работы'");
            
            var currentStatus = _stateLogRepository.GetState();

            if (status.State == State.MaintenancePlanned && 
                currentStatus.State == State.MaintenancePlanned &&
                status.MaintenancePlannedDate.HasValue &&
                currentStatus.Date.HasValue &&
                status.MaintenancePlannedDate.Value == currentStatus.Date.Value)
                throw new StatusChangeException("для состояния 'запланированы работы' новая дата работ должна отличаться от текущей");


            if (status.State != currentStatus.State || currentStatus.State == State.MaintenancePlanned)
            {
                _stateLogRepository.AddState(new StateLog
                {
                    State = status.State,
                    Date = status.MaintenancePlannedDate,
                    Description = status.Description
                });
            }
        }
    }
}