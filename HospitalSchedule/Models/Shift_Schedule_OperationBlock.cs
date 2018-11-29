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
        [ForeignKey("ScheduleFK")]
        public int ScheduleFK { get; set; }

        //chave estrangueira do turno
        [ForeignKey("ShiftFK")]
        public int ShiftFK { get; set;}

        
    }
}
