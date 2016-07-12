using System;
using Maintenance.Web.Tests.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Status.Core.Entities.Domain;
using Status.Core.Repositories;

namespace Maintenance.Web.Tests.Core.Db
{
    [TestClass]
    public class Repository : Setup
    {
        [TestMethod]
        public void GetCurrentState_ExpectOk()
        {
            var repo = GetService<IStateLogRepository>();

            var currentState = repo.GetState();

            Assert.AreEqual(currentState.State, State.Ok);
            Assert.IsTrue(currentState.CreateDate < DateTime.UtcNow);
            Assert.IsNull(currentState.Description);
        }

        [TestMethod]
        public void AddCurrentState_ExpectMaintenance()
        {
            var repo = GetService<IStateLogRepository>();

            var newState = new StateLog {State = State.Maintenance};
            repo.AddState(newState);
            var currentState = repo.GetState();

            Assert.AreEqual(currentState.State, State.Maintenance);
        }
    }
}