using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Schedule_Exchange1
    {
        [Required]
        public int Schedule_Exchange1Id { get; set; }

        [Required]
        public Schedule Schedule { get; set; }

        [Required]
        public int ScheduleId { get; set; }

        public ICollection<Exchange_Request> Exchange_Requests { get; set; }
    }
}
