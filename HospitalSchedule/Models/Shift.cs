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
        public int Hour { get; set; }//Limite : 0 a 23

        [Required]
        public int Minutes { get; set; }//Limitie : 0 a 59
        //Hora de inicio de cada turno. Vai ser criada pelos inteiros Hour e Minutes

        [Required]
        public int ShiftDuration { get; set; }//Limite: 0 a (limite pela lei)8

        public ICollection<OperationBlock_Shift> OperationBlock_Shifts { get; set; }
    }
}
