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


        [Required(ErrorMessage = "Please enter your email address correctly")]
        //([\w\.\-]+) - this is for the first-level domain (many letters and numbers, also point and hyphen)
        //([\w\-]+) - this is for second-level domain
        //((\.(\w){2,3})+) - and this is for other level domains(from 3 to infinity) which includes a point and 2 or 3 literals
        [RegularExpression(@"(^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$)", ErrorMessage = "Invalid email")]
        public string Email { get; set; } //Email


        [Required(ErrorMessage = "Please insert nurse's type number")]//1 - Chefe ou 0 - não , deixamos assim para uma futura escalabilidade/outros tipos em que a unica forma de inserção seja aqui
        public int Type { get; set; } //Na view apenas aparece um drop list com Enfermeiro ou Enfermeiro Chefe, o valor dos mesmos é 0 ou 1

        [Required(ErrorMessage = "Please insert a phone number")]
        [RegularExpression(@"(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Please insert a valid phone number")]
        public string CellPhoneNumber { get; set; } //Número de telemovel


        [Required(ErrorMessage = "Please insert the nurse's birth date")]
        [RegularExpression(@"([0-9]{8}[A-Z0-9]{4})", ErrorMessage = "Insert the identification")] //Mudaar
        [Required]
        public string IDCard { get; set; } //Cartão de Cidadão/Bilhete de Identidade (CC/BI)

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]

        public DateTime BirthDate { get; set; } //Data de Nascimento

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? YoungestChildBirthDate { get; set; } //Data de Nascimento do filho mais novo

        public ICollection<Schedule> Schedules { get; set; }

        public Specialty Specialty { get; set; }

        public int SpecialtyId { get; set; }



    }
}