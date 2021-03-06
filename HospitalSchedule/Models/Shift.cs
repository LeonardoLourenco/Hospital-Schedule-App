﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Shift
    {
        //chave primária
        [Required]
        public int ShiftId { get; set; }

        [Required(ErrorMessage = "Please insert the shift's name")]
        public string ShiftName { get; set; } //Manhã,Tarde,Noite


        [Required(ErrorMessage = "Please insert the hour in which the shift starts")]
        [RegularExpression(@"([0-1]{1}([0-9]{1})|([2]+([0-3])))+:+([0-5]{1}([0-9]{1}))", ErrorMessage = "Please insert the hour in which the shift starts in the format 00:00, the maximum value is 23:59")]
        public string StartingHour { get; set; } //Hora de inicio do turno

        [Required(ErrorMessage = "Please insert duration of the shift")]
        [RegularExpression(@"([0-1]{1}([0-9]{1})|([2]+([0-3])))+:+([0-5]{1}([0-9]{1}))", ErrorMessage = "Please insert duration of the shift in the format 00:00, the maximum value is 23:59")]
        public string Duration { get; set; } //Duração do turno

        public ICollection<OperationBlock_Shifts> OperationBlock_Shifts { get; set; }
    }
}
