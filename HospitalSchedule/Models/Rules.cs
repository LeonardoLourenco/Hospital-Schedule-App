using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Rules
    {
        [Required]
        public int RulesId { get; set; }

        //Regras para o algoritmo vão aqui
        //Exemplo (Data_Actual - Nurse.Birthdate) > 60 ENTÃO não faz Noites
        //Exemplo (Data_Actual - Nurse.YoungestBirthdate) > 18 ENTÃO não faz Noites
    }
}
