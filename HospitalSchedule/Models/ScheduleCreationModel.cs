using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class ScheduleCreationModel
    {
        [Required]
        public String BlockName { get; set; }

        //add here the nurse type that will come from a database

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
    }
}
