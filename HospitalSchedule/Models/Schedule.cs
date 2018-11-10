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
        public int ScheduleId{ get; set; }//Id do horário

        [Required]
        public DateTime Date { get; set; }//19/18/2018

        //Chave estrangueira (Nº do bloco)
        [Required]

       // public OperationBlock Block { get; set; }

        [ForeignKey("ScheduleFK")]
        public int BlockFK{ get; set; }
        
        //variável que controla se o horário está ativo ou desativo. Quando no delete ou update horário
        //default:ativo
        public Boolean ativeSchedule { get; set; }
    }
}
