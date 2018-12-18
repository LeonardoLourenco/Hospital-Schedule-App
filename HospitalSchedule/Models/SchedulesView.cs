using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class SchedulesView
    {
        public IEnumerable<Schedule> Schedules { get; set; }

        public PagingInfo PagingInfo { get; set; }

    }
}
