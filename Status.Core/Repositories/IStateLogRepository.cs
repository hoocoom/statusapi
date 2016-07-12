using System.Collections.Generic;
using Status.Core.Entities.Domain;

namespace Status.Core.Repositories
{
    public interface IStateLogRepository
    {
        StateLog GetState();

        IEnumerable<StateLog> GetStateHistory();

        void AddState(StateLog stateLog);
    }
}