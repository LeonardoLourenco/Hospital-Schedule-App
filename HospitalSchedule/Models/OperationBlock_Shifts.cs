using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class OperationBlock_Shifts
    {
        [Required]
        public int OperationBlock_ShiftsId { get; set; }

        //chave estrangueira do horário
        public ICollection<Schedule> Schedules { get; set; }

        //chave estrangueira do turno
        [Required]
        public Shift Shift { get; set; }
        [ForeignKey("ShiftId")]
        public int ShiftId { get; set; }

        //chave estrangueira do bloco
        [Required]
        public OperationBlock OperationBlock { get; set; }
        [ForeignKey("OperationBlockId")]
        public int OperationBlockId { get; set; }
    }
}
