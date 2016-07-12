using System;
using System.Linq;
using System.Web.Mvc;
using Maintenance.Web.Tests.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Status.Core.Entities.Domain;
using Status.Core.Services;
using Status.Web.Areas.Admin.Controllers;
using Status.Web.Areas.Admin.Models;

namespace Maintenance.Web.Tests.Web
{
    [TestClass]
    public class AdminHomeControllerTests : Setup
    {
        [TestMethod]
        public void GetAdminMainView_ExpectValidView()
        {
            var service = GetService<IStatusService>();
            var controller = new HomeController(service);

            var result = controller.Index();
            Assert.IsNotNull(result);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as StatusChangeModel<StatusChangeViewModel>;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.ViewData.StatusHistory.Count == 1);
            Assert.IsTrue(model.ViewData.StatusHistory.First().State == State.Ok);
        }

        [TestMethod]
        public void ChangeServiceState_ExpectStatusAndHistoryChange()
        {
            var service = GetService<IStatusService>();
            var controller = new HomeController(service);

            var result = controller.Index(new StatusChangeModel()
            {
                State = State.MaintenancePlanned,
                MaintenancePlannedDate = DateTime.UtcNow.AddDays(2),
                Description = "test maintenance"
            });

            Assert.IsNotNull(result);

            var resultAfterChange = controller.Index();
            Assert.IsNotNull(resultAfterChange);
            var viewResult = resultAfterChange as ViewResult;

            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as StatusChangeModel<StatusChangeViewModel>;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.ViewData.StatusHistory.Count == 2);
            Assert.IsTrue(model.State == State.MaintenancePlanned);
        }

    }
}