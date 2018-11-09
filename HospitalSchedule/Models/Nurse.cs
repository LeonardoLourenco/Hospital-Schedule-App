using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Nurse
    {
        //chave primária
        public int NurseID { get; set; }



        [Required(ErrorMessage = "Introduza o seu Nome")]
        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Nome Inválido")]
        public string Name { get; set; }

        [Required]//1->60 anos;2-filhos menores;3-outros;4-Chefe ou não
        public int Type { get; set; }

        [Required(ErrorMessage = "Introduza a sua especialidade")]
        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Nome Inválido")]
        public string Specialties { get; set; }

        [Required(ErrorMessage = "Por favor introduza corretamente o seu email")]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})", ErrorMessage = "Email Inválido")]
        public string Email { get; set; }

        [RegularExpression(@"(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Contacto Inválido")]
        [Required(ErrorMessage = "Por favor indroduza o numero de telefone")]
        public string CellPhoneNumber { get; set; }


        //chave estrangueira do enfermeiro_horário
        /*public Nurse_Schedule Nurse_Schedule { get; set; }
        public int Nurse_ScheduleID { get; set; }
        */
    }
}