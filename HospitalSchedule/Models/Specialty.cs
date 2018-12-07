using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Specialty
    {
        [Required]
        public int SpecialtyId { get; set; }

        [Required(ErrorMessage = "Please insert the name of the specialty")]
        public string Name { get; set; }

        public ICollection<Nurse> Nurses { get; set; }
    }
}
