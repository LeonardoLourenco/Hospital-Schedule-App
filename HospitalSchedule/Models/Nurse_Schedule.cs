using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Nurse_Schedule
    {
        //Chave primária(Guarda os horários,principal e o de prevenção)
        [Required]
        public int Nurse_ScheduleId { get; set; }
        
        //Chave estrangueira do horário
        [Required]
        public Schedule Schedule {get;set; }
        [ForeignKey("ScheduleId")]
        public int ScheduleId { get; set; }

         //Chave estrangueira do enfermeiro
        [Required]
        public Nurse Nurse { get; set; }
        [ForeignKey("NurseId")]
        public int NurseId { get; set; }
    }
}
