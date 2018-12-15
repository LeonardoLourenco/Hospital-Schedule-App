using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class OperationBlockView
    {
        public IEnumerable<OperationBlock> OperationBlocks { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
