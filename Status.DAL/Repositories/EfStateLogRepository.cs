using System;
using System.Collections.Generic;
using System.Linq;
using Status.Core.Repositories;
using CoreState = Status.Core.Entities.Domain.State;
using CoreStateLog = Status.Core.Entities.Domain.StateLog;
using DbState = Status.DAL.Entities.State;
using DbStatelog = Status.DAL.Entities.StateLog;


namespace Status.DAL.Repositories
{
    public sealed class EfStateLogRepository : IStateLogRepository
    {
        private readonly StatusContext _statusContext;

        public EfStateLogRepository(StatusContext statusContext)
        {
            _statusContext = statusContext;
        }

        public CoreStateLog GetState()
        {
            var stateLogDb = _statusContext.StateLogs.OrderByDescending(x => x.StateLogId).First();
            
            var stateLogCore = new CoreStateLog()
            {
                State = (CoreState)Enum.Parse(typeof(CoreState), stateLogDb.StateId),
                Date = stateLogDb.Date,
                Description = stateLogDb.Description,
                CreateDate = stateLogDb.CreateDate,
            };

            return stateLogCore;
        }

        //todo добавить постраничный вывод
        public IEnumerable<CoreStateLog> GetStateHistory()
        {
            var statesLogsDb = _statusContext.StateLogs.OrderByDescending(x => x.StateLogId).ToList();

            var stateLogsCore = statesLogsDb.Select(stateLogDb => new CoreStateLog()
            {
                State = (CoreState) Enum.Parse(typeof (CoreState), stateLogDb.StateId),
                Date = stateLogDb.Date,
                Description = stateLogDb.Description,
                CreateDate = stateLogDb.CreateDate,
            });

            return stateLogsCore;
        }

        public void AddState(CoreStateLog stateLog)
        {
            var stateId = stateLog.State.ToString("G");
            var state = _statusContext.States.First(x => x.StateId == stateId);
            
            var stateLogDb = new DbStatelog()
            {
                State = state,
                StateId = state.StateId,
                Date = stateLog.Date,
                Description = stateLog.Description,
                CreateDate = stateLog.CreateDate
            };
            
            _statusContext.StateLogs.Add(stateLogDb);

            _statusContext.SaveChanges();
        }
    }
}