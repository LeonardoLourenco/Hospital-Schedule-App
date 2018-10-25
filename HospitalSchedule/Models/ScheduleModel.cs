using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class ScheduleModel
    {
        [Required]
        public int ScheduleId{ set ; get; }//Id do horário

        // [Required]
        //public Enfermeiro EnfermeiroId { set; get; } //Nome do Enfermeiro

        [Required]
        public int Shift { set; get; }//Manhã,Tarde ou Noite

        [Required]
        public DateTime Date { set; get; }//19/18/2018
        /*
        [Required]
        public String Block { set; get; }//Bloco Operatório
        */
        //[Required]
        //public OperationBlock BlockID { set; get; }//Nº do bloco
    }
}
