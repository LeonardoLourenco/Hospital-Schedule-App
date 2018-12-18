using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class OperationBlock_ShiftView
    {
        public IEnumerable<OperationBlock_Shifts> OperationBlock_Shifts { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
