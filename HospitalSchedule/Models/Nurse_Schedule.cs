using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Nurse_Schedule
    {
        //chave primária
        [Required]
        public int Nurse_ScheduleID { get; set; }
        public int NurseID { get; set; }
        public int ScheduleID { get; set; }


        public Schedule Schedule { get; set; }
        public Nurse Nurse { get; set; }
    }
}
