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

        public string ShiftName { get; set; } //Manhã,Tarde,Noite

        public DateTime StartingHour { get; set; } //Hora de inicio de cada turno

        public DateTime FinishingHour { get; set; } //Hora de fim de cada turno

        [Required]
        public ICollection<Shift_Schedule> Shift_Schedules { get; set; }
    }
}
