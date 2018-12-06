﻿using System;
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

        [Required]
        public int WeeklyHours { get; set; } //Numero de Horas Semanais

        [Required]
        public int NurseAge { get; set; } //Qual a Idade do enfermeiro para fazer n turnos noturnos

        [Required]
        public int ChildAge { get; set; } //Qual a Idade do filho mais novo para fazer n turnos noturnos

        [Required]
        public string InBetweenShiftTime { get; set; } //Qual a o tempo entre turnos
        //Regras para o algoritmo vão aqui
        //Exemplo (Data_Actual - Nurse.Birthdate) > Age ENTÃO não faz Noites
        //Exemplo (Data_Actual - Nurse.YoungestBirthdate) > ChildAge ENTÃO não faz Noites
    }
}
