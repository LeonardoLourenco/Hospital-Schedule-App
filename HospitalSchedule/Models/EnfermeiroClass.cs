using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class EnfermeiroClass
    {
       
        public int EnfermeiroID { get; set; }
        [Required]
        public string nome { get; set; }

        public string Bloco { get; set; }

        [Required]//1->60 anos;2-filhos menores;3-outros
        public int tipo { get; set; }

        [Required]
        public string especialidades { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }



    }
}
