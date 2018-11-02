using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Enfermeiro_Horario
    {
        [Required]
        public int Enfermeiro_horarioID { get; set; }//Chave primária(Guarda os horários,
                                                        //principal e o de prevenção)
        [Required]
        public Schedule ScheduleID {get;set; }//Chave estrangueira do horário

        [Required]
        public Enfermeiro EnfermeiroID { get; set; }//Chave estrangueira do enfermeiro
    }
}
