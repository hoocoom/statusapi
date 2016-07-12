using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Status.DAL.Entities
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

        [Key]
        public int StateLogId { get; set; }

        public string StateId { get; set; }
        public State State { get; set; }

        /// <summary>
        /// Дата начала запланированных работ/
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Описание работ
        /// </summary>
        public string Description { get; set; }

        [Index]
        public DateTime CreateDate { get; set; }
    }
}