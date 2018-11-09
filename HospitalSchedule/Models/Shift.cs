using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Shift
    {
        //chave primária
        [Required]
        public int ShiftID { get; set; }

        public String ShiftName { get; set; } //Manhã,Tarde,Noite

        public int StartingHour { get; set; } //Hora de inicio de cada turno

        public int FinishingHour { get; set; } //Hora de fim de cada turno
        
        /*//chave estrangueira do turno_horário
        public Shift_Schedule Shift_Schedule { get; set; }
        public int Shift_ScheduleID { get; set; }
        */
    }

}
