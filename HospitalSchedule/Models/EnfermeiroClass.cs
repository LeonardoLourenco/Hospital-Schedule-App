using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class EnfermeiroClass
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string nome { get; set; }

        public string Bloco { get; set; }
        [Required]//1->60 anos;2-filhos menores;3-outros
        public int tipo { get; set; }

        

    }
}
