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

        [Required(ErrorMessage = "Please insert the nurse's name")]
        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Please insert a valid name")]
        public string Name { get; set; } //Nome

        [Required(ErrorMessage = "Please insert your email address")]  //incluir - hifens no email
        [RegularExpression(@"([\w-]+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})", ErrorMessage = "Please insert a valid email address")]
        public string Email { get; set; } //Email


        [Required(ErrorMessage = "Please insert nurse's type number")]//1 - Chefe ou 0 - não , deixamos assim para uma futura escalabilidade/outros tipos em que a unica forma de inserção seja aqui
        public int Type { get; set; } //Na view apenas aparece um drop list com Enfermeiro ou Enfermeiro Chefe, o valor dos mesmos é 0 ou 1

        [Required(ErrorMessage = "Please insert a phone number")]
        [RegularExpression(@"(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Please insert a valid phone number")]
        public string CellPhoneNumber { get; set; } //Número de telemovel

        [Required(ErrorMessage = "Please insert the id card number")]
        [RegularExpression(@"(([0-9]{8}[A-Z0-9]{4}))", ErrorMessage = "Please insert a valid id card number")]
        public string IDCard { get; set; } //Cartão de Cidadão/Bilhete de Identidade (CC/BI)

        [Required(ErrorMessage = "Please insert the nurse's birth date")]
        public DateTime BirthDate { get; set; } //Data de Nascimento

        public DateTime? YoungestChildBirthDate { get; set; } //Data de Nascimento do filho mais novo

        public ICollection<Schedule> Schedules { get; set; }

        public Specialty Specialty { get; set; }

        public int SpecialtyId { get; set; }
    }
}