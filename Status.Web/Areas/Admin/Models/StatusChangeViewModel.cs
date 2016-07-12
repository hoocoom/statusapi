using System.Collections.Generic;
using Status.Core.Entities;

namespace Status.Web.Areas.Admin.Models
{
    public class StatusChangeModel : ServiceStatusUpdate
    {
    }

    public class StatusChangeModel<TView> : StatusChangeModel
    {
        public TView ViewData { get; set; }
    }

    public class StatusChangeViewModel
    {
        public IReadOnlyCollection<ServiceStatusLogEntry> StatusHistory { get; set; }
    }
}