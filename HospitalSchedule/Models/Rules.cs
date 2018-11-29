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
        [Required]
        public int InBeetweenHoursShift { get; set; }

        [Required]
        public int WeeklyHours { get; set; }

        [Required]
        public int ShiftDuration { get; set; }

        [Required]
        public int NurseAge { get; set; }

        [Required]
        public int YoungestChildAge { get; set; }

        [Required]
        public bool HasChild { get; set; }
    }
}
