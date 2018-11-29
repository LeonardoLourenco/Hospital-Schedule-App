using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Shift_Schedule_OperationBlock
    {
        //chave primária
        [Required]
        public int Shift_Schedule_OperationBlockId { get; set; }

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
        public int ShiftId { get; set; }

        //chave estrangueira do bloco
        [Required]
        public OperationBlock OperationBlock { get; set; }
        [ForeignKey("OperationBlockId")]
        public int OperationBlockId { get; set; }
    }
}
