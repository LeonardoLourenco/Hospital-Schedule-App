using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }//chave primária

        public Shift_Schedule Shift_ScheduleID { get; set; }//chave estrangueira

        public String ShiftName { get; set; } //Manhã,Tarde,Noite

        public int StartingHour { get; set; } //Hora de inicio de cada turno

        public int FinishingHour { get; set; } //Hora de fim de cada turno

        
    }

}
