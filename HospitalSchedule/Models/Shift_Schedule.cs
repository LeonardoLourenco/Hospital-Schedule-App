using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Shift_Schedule
    {
        public Shift_Schedule Shift_ScheduleID { get; set; }//chave primária

        public Schedule ScheduleID { get; set; } //chave estrangueira

        public Shift ShiftID { get; set;}//chave estrangueira

        public DateTime ShiftDate { get; set; } //Datas dos turnos
    }
}
