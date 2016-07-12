using System;
using System.Linq;
using Maintenance.Web.Tests.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Status.Core;
using Status.Core.Entities;
using Status.Core.Entities.Domain;
using Status.Core.Services;

namespace Maintenance.Web.Tests.Core.Db
{
    [TestClass]
    public class ServiceTests : Setup
    {
        [TestMethod]
        public void GetCurrentStatus_ExpectOk()
        {
            var service = GetService<IStatusService>();
            var currentStatus = service.GetServiceStatus();
            Assert.AreEqual(currentStatus.State, State.Ok);
            Assert.IsFalse(currentStatus.MaintenancePlannedDate.HasValue);
        }

        [TestMethod]
        public void SetServiceStatus_ExpectMaintenancePlanned()
        {
            GetCurrentStatus_ExpectOk();
            var service = GetService<IStatusService>();
            var plannedDate = DateTime.UtcNow.AddHours(2);
            service.SetServiceStatus(new ServiceStatusUpdate()
            {
                State = State.MaintenancePlanned,
                MaintenancePlannedDate = plannedDate
            });

            var currentStatus = service.GetServiceStatus();
            Assert.AreEqual(currentStatus.State, State.MaintenancePlanned);
            Assert.IsNotNull(currentStatus.MaintenancePlannedDate);
            Assert.IsTrue(currentStatus.MaintenancePlannedDate.Value == plannedDate);
        }

        [TestMethod]
        public void SetServiceStatus_ExpectMaintenance()
        {
            var service = GetService<IStatusService>();
            SetServiceStatus_ExpectMaintenancePlanned();

            service.SetServiceStatus(new ServiceStatusUpdate
            {
                State = State.Maintenance
            });

            var currentStatus = service.GetServiceStatus();
            Assert.AreEqual(currentStatus.State, State.Maintenance);
            Assert.IsNull(currentStatus.MaintenancePlannedDate);
        }

        [TestMethod]
        public void SetServiceStatus_Expect2ItemsInHistory()
        {
            var currentDate = DateTime.UtcNow;
            var service = GetService<IStatusService>();

            service.SetServiceStatus(new ServiceStatusUpdate
            {
                State = State.MaintenancePlanned,
                MaintenancePlannedDate = DateTime.UtcNow.AddDays(2),
                Description = "test planned maintenance"
            });

            var currentStatus = service.GetServiceStatus();
            Assert.AreEqual(currentStatus.State, State.MaintenancePlanned);
            Assert.IsNotNull(currentStatus.MaintenancePlannedDate);

            var history = service.GetServiceStatusHistory().ToList();

            Assert.IsTrue(history.Count == 2);
            var last = history.First();
            Assert.AreEqual(last.State, State.MaintenancePlanned);
            Assert.IsNotNull(last.MaintenancePlannedDate);
            Assert.IsNotNull(last.Description);
            Assert.IsTrue(history.All(x => x.CreateDate >= currentDate));
        }

        [TestMethod]
        [ExpectedException(typeof(StatusChangeException))]
        public void SetInvalidServiceStatus_ExpectException()
        {
            GetCurrentStatus_ExpectOk();

            var service = GetService<IStatusService>();

            
            service.SetServiceStatus(new ServiceStatusUpdate
            {
                State = State.MaintenancePlanned
            });
        }

        [TestMethod]
        [ExpectedException(typeof(StatusChangeException))]
        public void SetInvalidServiceStatus_ExpectException2()
        {
            GetCurrentStatus_ExpectOk();

            var service = GetService<IStatusService>();


            service.SetServiceStatus(new ServiceStatusUpdate
            {
                State = State.Maintenance,
                MaintenancePlannedDate = DateTime.UtcNow
            });
        }

        [TestMethod]
        [ExpectedException(typeof(StatusChangeException))]
        public void SetInvalidServiceStatus_ExpectException3()
        {
            GetCurrentStatus_ExpectOk();

            var service = GetService<IStatusService>();

            var plannedDate = DateTime.UtcNow;

            service.SetServiceStatus(new ServiceStatusUpdate
            {
                State = State.MaintenancePlanned,
                MaintenancePlannedDate = plannedDate
            });

            service.SetServiceStatus(new ServiceStatusUpdate
            {
                State = State.MaintenancePlanned,
                MaintenancePlannedDate = plannedDate
            });
        }
    }
}