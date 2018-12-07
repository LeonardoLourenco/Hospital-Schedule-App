using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class OperationBlock_Shift
    {
        [Required]
        public int OperationBlock_ShiftsId { get; set; }

        //chave estrangueira do horário
        public ICollection<Schedule> Schedules { get; set; }

        //chave estrangueira do turno
        [Required(ErrorMessage = "Please select a shift that you want the Block to have")]
        public Shift Shift { get; set; }
        [ForeignKey("ShiftId")]
        public int ShiftId { get; set; }

        //chave estrangueira do bloco
        [Required(ErrorMessage = "Please select an Operation Block")]
        public OperationBlock OperationBlock { get; set; }
        [ForeignKey("OperationBlockId")]
        public int OperationBlockId { get; set; }
    }
}
