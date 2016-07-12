using Status.Core.Entities;
using Status.Core.Entities.Domain;

namespace Status.Web.Helpers
{
    public static class ServiceStatusHelper
    {
        public static string GetCss(this ServiceStatus status)
        {
            switch (status.State)
            {
                case State.Ok:
                    return "success";
                case State.Maintenance:
                    return "danger";
                case State.MaintenancePlanned:
                    return "warning";
                default:
                    return string.Empty;
            }
        }

        public static string GetDescription(this ServiceStatus status)
        {
            switch (status.State)
            {
                case State.Ok:
                    return "Все работает штатно, работы по обновлению не ведутся";
                case State.Maintenance:
                    return "Сервис недоступен, ведутся технические работы";
                case State.MaintenancePlanned:
                    var date = status.MaintenancePlannedDate.HasValue ? status.MaintenancePlannedDate.Value.ToString("dd.MM.yyyy") : "неизвестную дату";
                    return string.Format("Сейчас все работает штатно, но на {0} запланированы работы", date);
                default:
                    return string.Empty;
            }
        }

        public static string GetShortDescription(this State state)
        {
            switch (state)
            {
                case State.Ok:
                    return "Сервис работает";
                case State.Maintenance:
                    return "Ведутся работы";
                case State.MaintenancePlanned:
                    return "Запланированы работы";
                default:
                    return string.Empty;
            }
        }
    }
}