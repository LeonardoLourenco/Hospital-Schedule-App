using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class SpecialitiesView
    {
        public IEnumerable<Specialty> Specialties { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
