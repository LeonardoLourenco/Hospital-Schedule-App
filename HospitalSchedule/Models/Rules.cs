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

        [Required(ErrorMessage = "Please insert number of weekly hours")]
        public int WeeklyHours { get; set; } //Numero de Horas Semanais

        [Required(ErrorMessage = "Please insert the age in which a nurse won't have to do night shifts")]
        public int NurseAge { get; set; } //Qual a Idade do enfermeiro para fazer n turnos noturnos

        [Required(ErrorMessage = "Please insert the nurse's child's age in which a nurse won't have to do night shifts")]
        public int ChildAge { get; set; } //Qual a Idade do filho mais novo para fazer n turnos noturnos

        [Required(ErrorMessage = "Please insert the resting time a nurse has between shifts")]
        [RegularExpression(@"([0-9]{2})+:+([0-9]{2})", ErrorMessage = "Please insert a number for the resting time a nurse has between shifts in this format 00:00")]
        public string InBetweenShiftTime { get; set; } //Qual a o tempo entre turnos
        //Regras para o algoritmo vão aqui
        //Exemplo (Data_Actual - Nurse.Birthdate) > Age ENTÃO não faz Noites
        //Exemplo (Data_Actual - Nurse.YoungestBirthdate) > ChildAge ENTÃO não faz Noites
    }
}
