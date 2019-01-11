using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace HospitalSchedule.Models
{
    public class Schedule
    {
        [Required]
        public int ScheduleId { get; set; }//Id do horário
        
        [Required(ErrorMessage ="Please insert the schedule's date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }//Dia desse horário

        //Após o algoritmo de geração de horário estar feito não será necessário regex.


        public Nurse Nurse { get; set; }
        [Required]
        public int NurseId { get; set; }

        public OperationBlock_Shifts OperationBlock_Shifts { get; set; }
        [Required]
        public int OperationBlock_ShiftsId { get; set; }

        public ICollection<Schedule_Exchange1> Schedule_Exchange1s { get; set; }

        public ICollection<Schedule_Exchange2> Schedule_Exchange2s { get; set; }
    }
}
