using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Nurse_Schedule
    {
        [Required]
        public int Nurse_ScheduleID { get; set; }//Chave primária(Guarda os horários,
                                                        //principal e o de prevenção)
        [Required]
        public Schedule ScheduleID {get;set; }//Chave estrangueira do horário

        [Required]
        public Nurse NurseID { get; set; }//Chave estrangueira do enfermeiro
    }
}
