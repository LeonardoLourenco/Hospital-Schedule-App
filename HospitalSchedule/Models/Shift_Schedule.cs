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
        public int Shift_ScheduleId { get; set; }

        public DateTime ShiftDate { get; set; } //Datas dos turnos

        //chave estrangueira do horário
        [Required]
        public Schedule Schedule { get; set; }
        [ForeignKey("ScheduleId")]
        public int ScheduleId { get; set; }

        //chave estrangueira do turno
        [Required]
        public Shift Shift { get; set; }
        [ForeignKey("ShiftId")]
        public int ShiftId { get; set;}

        
    }
}
