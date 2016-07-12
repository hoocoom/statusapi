using System;

namespace Status.Core.Entities.Domain
{
    /// <summary>
    /// История изменений состояния сервисов
    /// </summary>
    public class StateLog
    {
        public StateLog()
        {
            CreateDate = DateTime.UtcNow;
        }
        
        public State State { get; set; }

        /// <summary>
        /// Дата начала запланированных работ
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Описание работ
        /// </summary>
        public string Description { get; set; }
        

        public DateTime CreateDate { get; set; }
    }
}