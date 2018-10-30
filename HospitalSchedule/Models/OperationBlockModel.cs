using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class OperationBlockModel
    {
        [Required]
        public int BlockId { get; set; }            //Block Id
        [Required]
        public string BlockName { get; set; }       //Nome do bloco
        [Required]
        public int MaxNumOfNurses { get; set; }     //Numero máximo de enfermeiros
        [Required]
        public int CurrentNurses { get; set; }      //Numero actual de enfermeiros

    }
}
