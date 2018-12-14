using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class ShiftsView
    {
        public IEnumerable<Shift> Shifts { get; set; }

        public PagingInfo PagingInfo { get; set; }

    }
}
