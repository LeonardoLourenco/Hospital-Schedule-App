using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Nurse
    {
        //chave primária
        public int NurseId { get; set; }

        [Required(ErrorMessage = "Introduza o seu Nome")]
        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Nome Inválido")]
        public string Name { get; set; } //Nome

        [Required(ErrorMessage = "Por favor introduza corretamente o seu email")]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})", ErrorMessage = "Email Inválido")]
        public string Email { get; set; } //Email


        [Required]//1 - Chefe ou 0 - não , deixamos assim para uma futura escalabilidade/outros tipos em que a unica forma de inserção seja aqui
        public int Type { get; set; } //Na view apenas aparece um drop list com Enfermeiro ou Enfermeiro Chefe, o valor dos mesmos é 0 ou 1

        [RegularExpression(@"(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Contacto Inválido")]
        [Required(ErrorMessage = "Por favor indroduza o numero de telefone")]
        public string CellPhoneNumber { get; set; } //Número de telemovel
        
        [RegularExpression(@"([0-9]{8})", ErrorMessage = "CC/BI Inválido")] //Mudaar
        [Required]
        public string IDCard { get; set; } //Cartão de Cidadão/Bilhete de Identidade (CC/BI)

        [Required]
        public DateTime BirthDate { get; set; } //Data de Nascimento

        public DateTime? YoungestChildBirthDate { get; set; } //Data de Nascimento do filho mais novo

        public ICollection<Schedule> Schedules { get; set; }

        public Specialty Specialty { get; set; }
        [Required(ErrorMessage = "Please introduce a specialty")]
        public int SpecialtyId { get; set; }
    }
}