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

        [Required]
        public DateTime CreationDate { get; set; }//19/18/2018
        [Required]
        public DateTime FinishedDate { get; set; }//19/18/2018

        //Chave estrangueira (Nº do bloco)
        [Required]
        public OperationBlock OperationBlock { get; set; }
        [ForeignKey("OperationBlockFK")]
        public int OperationBlockFK { get; set; }

        //Variável que indica se o horário atual se encontra ativo
        public bool AtiveSchedule { get; set; }
    }
}
