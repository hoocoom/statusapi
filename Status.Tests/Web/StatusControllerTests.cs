using System;
using Maintenance.Web.Tests.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Status.Core.Entities;
using Status.Core.Entities.Domain;
using Status.Core.Services;
using Status.Web.Controllers;

namespace Maintenance.Web.Tests.Web
{
    [TestClass]
    public class StatusControllerTests : Setup
    {
        [TestMethod]
        public void GetStatus_ExpectOk()
        {
            var service = GetService<IStatusService>();
            var controller = new StatusController(service);
            Assert.IsNotNull(controller);

            var apiResponse = controller.GetStatus();
            Assert.AreEqual(apiResponse.State, State.Ok);
            Assert.IsNull(apiResponse.MaintenancePlannedDate);
        }

        [TestMethod]
        public void GetStatus_ExpectMaintenancePlanned()
        {
            var service = GetService<IStatusService>();
            var plannedDate = DateTime.UtcNow.AddHours(2);
            service.SetServiceStatus(new ServiceStatusUpdate()
            {
                State = State.MaintenancePlanned,
                MaintenancePlannedDate = plannedDate
            });

            var controller = new StatusController(service);
            Assert.IsNotNull(controller);

            var apiResponse = controller.GetStatus();
            Assert.AreEqual(apiResponse.State, State.MaintenancePlanned);
            Assert.IsNotNull(apiResponse.MaintenancePlannedDate);
        }

        [TestMethod]
        public void GetStatus_ExpectMaintenance()
        {
            var service = GetService<IStatusService>();
            service.SetServiceStatus(new ServiceStatusUpdate()
            {
                State = State.Maintenance,
            });

            var controller = new StatusController(service);
            Assert.IsNotNull(controller);

            var apiResponse = controller.GetStatus();
            Assert.AreEqual(apiResponse.State, State.Maintenance);
            Assert.IsNull(apiResponse.MaintenancePlannedDate);
        }
    }
}