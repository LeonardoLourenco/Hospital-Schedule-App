using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSchedule.Models
{
    public class Shift_Schedule
    {
        //chave primária
        [Required]
        public int Shift_ScheduleID { get; set; }

        public DateTime ShiftDate { get; set; } //Datas dos turnos

        //chave estrangueira do horário
        [Required]
        public Schedule Schedule { get; set; }
        [ForeignKey("ScheduleFK")]
        public int ScheduleFK { get; set; }

        //chave estrangueira do turno
        [Required]
        public Shift Shift { get; set; }
        [ForeignKey("ShiftFK")]
        public int ShiftFK { get; set;}

        
    }
}
