using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class HorarioModel
    {
        
        public int HorarioId{ set ; get; }

        [Required]
        public String Dia { set; get; }

        [Required]
        public int Hora_Inicio { set; get; }

        [Required]
        public int Hora_Fim { set; get; }

        [Required]
        public int Turno { set; get; }

        [Required]
        public DateTime Data { set; get; }

        [Required]
        public String Bloco { set; get; }
    }
}
