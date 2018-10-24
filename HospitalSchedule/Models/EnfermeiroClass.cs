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

        [Required(ErrorMessage = "Introduza o seu Nome")]
        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Nome Inválido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Introduza o seu Nome do bloco")]
        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Nome Inválido")]
        public string Bloco { get; set; }

        [Required]//1->60 anos;2-filhos menores;3-outros
        public int Tipo { get; set; }

        [Required(ErrorMessage = "Introduza a sua especialidade")]
        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Nome Inválido")]
        public string Especialidades { get; set; }

        [Required(ErrorMessage ="Por favor introduza corretamente o seu email")]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})", ErrorMessage = "Email Inválido")]
        public string Email { get; set; }

        [RegularExpression(@"(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Contacto Inválido")]
        [Required(ErrorMessage = "Por favor indroduza o numero de telefone")]
        public string NumeroTelefone { get; set; }

    }
}
