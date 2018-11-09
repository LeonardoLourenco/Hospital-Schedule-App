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
        public int ScheduleId{ get; set; }//Id do horário

        [Required]
        public DateTime Date { get; set; }//19/18/2018

        //Chave estrangueira (Manhã,Tarde,Noite)
        [Required]
        public Shift Shift { get; set; }
        public int ShiftId { get; set; }

        //Chave estrangueira (Nº do bloco)
        [Required]
        public OperationBlock Block { get; set; }
        public int BlockID { get; set; }
        
        //Chave estrangueira Nome do Enfermeiro
        [Required]
        public Nurse Enfermeiro { get; set; }
        public int EnfermeiroID { get; set; }

    }
}
