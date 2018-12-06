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
        public int ShiftId { get; set; }

        [Required]
        public string ShiftName { get; set; } //Manhã,Tarde,Noite

        [Required]
        public string StartingHour { get; set; } //Hora de inicio do turno

        [Required]
        public int Duration { get; set; } //Duração do turno

        public ICollection<OperationBlock_Shifts> OperationBlock_Shifts { get; set; }
    }
}
