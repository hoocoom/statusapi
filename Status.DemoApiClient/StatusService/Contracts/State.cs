namespace Status.DemoApiClient.StatusService.Contracts
{
    /// <summary>
    /// Состояние сервисов
    /// </summary>
    public enum State
    {
        /// <summary>
        /// Все работает штатно, работы по обновлению не ведутся.
        /// </summary>
        Ok,

        /// <summary>
        /// Сервис недоступен, ведутся технические работы.
        /// </summary>
        Maintenance,

        /// <summary>
        /// Сейчас все работает штатно, но на дд.мм.гггг запланированы работы.
        /// </summary>
        MaintenancePlanned
    }
}