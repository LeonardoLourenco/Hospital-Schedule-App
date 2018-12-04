using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Specialities
    {
        [Required]
        public int SpecialitiesID { get; set; }

        [Required]//Validações ,ir ver ao nurses
        public string Specialitiename { get; set; }
    }
}
