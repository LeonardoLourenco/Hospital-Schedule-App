using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class HorarioModel
    {
        [Required]
        public int HorarioId{ set ; get; }

        [Required]
        public List<String> Dias_Semana { set; get; }

        [Required]
        public List <int> Horas { set; get; }

        [Required]
        public int Turno { set; get; }

        [Required]
        public String NomeEnfermeiro { set; get; }

        [Required]
        public DateTime Data { set; get; }

        [Required]
        public String Bloco { set; get; }
    }
}
