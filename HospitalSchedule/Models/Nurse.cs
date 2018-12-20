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

        [Required(ErrorMessage = "Please enter your Name")]
        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Invalid Name")]
        public string Name { get; set; } //Nome

        [Required(ErrorMessage = "Please enter your email address correctly")]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})", ErrorMessage = "Invalid email")]
        public string Email { get; set; } //Email


        [Required]//1 - Chefe ou 0 - não , deixamos assim para uma futura escalabilidade/outros tipos em que a unica forma de inserção seja aqui
        public int Type { get; set; } //Na view apenas aparece um drop list com Enfermeiro ou Enfermeiro Chefe, o valor dos mesmos é 0 ou 1

        [RegularExpression(@"(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Invalid Contact")]
        [Required(ErrorMessage = "Please enter the telephone number")]
        public string CellPhoneNumber { get; set; } //Número de telemovel

        [RegularExpression(@"(([0-9]{8}[A-Z0-9]{4}))", ErrorMessage = "Insert the identification")] //Mudaar
        [Required]
        public string IDCard { get; set; } //Cartão de Cidadão/Bilhete de Identidade (CC/BI)

        [Required]
        public DateTime BirthDate { get; set; } //Data de Nascimento

        public DateTime YoungestChildBirthDate { get; set; } //Data de Nascimento do filho mais novo

        public ICollection<Schedule> Schedules { get; set; }

        public Specialty Specialty { get; set; }

        
        public int SpecialtyId { get; set; }
    }
}