using System.Web.Mvc;
using Status.Core.Entities;
using Status.Core.Services;
using Status.Web.Areas.Admin.Models;

namespace Status.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatusService _statusService;

        public HomeController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = GetData();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ServiceStatusUpdate model)
        {
            _statusService.SetServiceStatus(model);

            return RedirectToAction("Index");
        }

        private StatusChangeModel<StatusChangeViewModel> GetData()
        {
            var current = _statusService.GetServiceStatus();
            var history = _statusService.GetServiceStatusHistory();

            var model = new StatusChangeModel<StatusChangeViewModel>
            {
                ViewData = new StatusChangeViewModel
                {
                    StatusHistory = history
                },
                MaintenancePlannedDate = current.MaintenancePlannedDate,
                State = current.State
            };

            return model;
        }
    }
}