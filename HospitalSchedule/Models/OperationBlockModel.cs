using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class OperationBlock
    {
        [Required]
        public int BlockId { get; set; }            //Block Id

        [Required]
        [RegularExpression(@"[A-Z]+(_Reserva)?", ErrorMessage = "Invalid Block Name.")] //[A-Z]+(_Reserva)? Significa letra maiuscula de A a Z
        public string BlockName { get; set; }       //Nome do bloco                     // com _Reserva presente ou não ex: A e A_Reserva

        [Required]
        [RegularExpression(@"[0-9]", ErrorMessage = "Invalid Max Number Of Nurses.")]
        public int MaxNumOfNurses { get; set; }     //Numero máximo de enfermeiros

        [Required]
        [RegularExpression(@"[0-9]", ErrorMessage = "Invalid Current Nurses number.")]
        public int CurrentNurses { get; set; }      //Numero actual de enfermeiros


        //chave estrangueira do horário
        public Schedule Schedule { get;set; }
        public int ScheduleID { get; set; }

    }
}