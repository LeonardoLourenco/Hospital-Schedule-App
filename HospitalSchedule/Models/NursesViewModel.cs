﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class NursesViewModel
    {
        public IEnumerable<Nurse> Nurses { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
