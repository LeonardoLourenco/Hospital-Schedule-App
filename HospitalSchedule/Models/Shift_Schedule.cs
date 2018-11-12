using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Shift_Schedule
    {
        //chave primária
        [Required]
        public Shift_Schedule Shift_ScheduleID { get; set; }

        public DateTime ShiftDate { get; set; } //Datas dos turnos

        //chave estrangueira do horário
        [Required]
        public Schedule Schedule { get; set; } 
        public int ScheduleID { get; set; }

        //chave estrangueira do turno
        [Required]
        public Shift Shift { get; set;}
        public int ShiftID { get; set;}

        
    }
}
