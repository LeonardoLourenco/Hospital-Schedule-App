using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class RulesView
    {
        public IEnumerable<Rules> Rules { get; set; }

        public PagingInfo PagingInfo { get; set; }

    }
}
