using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models.AccountViewModel
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Por favor indroduza o nome")]
        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Nome Inválido")]
        public string Name { get; set; }

        [RegularExpression(@"\d{8}(\s\d{1})?", ErrorMessage = "Cartão de Cidadão Inválido")]
        public string IDCard { get; set; }

       


        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+)", ErrorMessage = "Profissão Inválida")]
        public Specialty Specialty { get; set; }

        [Required(ErrorMessage = "Por favor indroduza o email")]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})", ErrorMessage = "Email Inválido")]
        public string Email { get; set; }

        [RegularExpression(@"(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Contacto Inválido")]
        [Required(ErrorMessage = "Por favor indroduza o contacto")]
        public string CellPhoneNumber { get; set; }

      

      

        [Required(ErrorMessage = "Por favor indroduza a data de nascimento do turista")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
