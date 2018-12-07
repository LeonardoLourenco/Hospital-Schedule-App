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
        public Shift Shift { get; set; }
        [Required(ErrorMessage = "Please select a shift that you want the Block to have")]
        public int ShiftId { get; set; }

        //chave estrangueira do bloco
        public OperationBlock OperationBlock { get; set; }
        [Required(ErrorMessage = "Please select an Operation Block")]
        public int OperationBlockId { get; set; }
    }
}
