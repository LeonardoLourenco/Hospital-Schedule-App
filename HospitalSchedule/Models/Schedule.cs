using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Schedule
    {
        [Required]
        public int ScheduleId{ set ; get; }//Id do horário

        [Required]
        public DateTime Date { set; get; }//19/18/2018

        [Required]
        public Shift Shift { set; get; }//Chave estrangueira (Manhã,Tarde,Noite)

        [Required]
        public OperationBlock BlockID { set; get; }//Chave estrangueira (Nº do bloco)
        
        [Required]
        public Nurse EnfermeiroID { set; get; } //Chave estrangueira Nome do Enfermeiro)
    }
}
