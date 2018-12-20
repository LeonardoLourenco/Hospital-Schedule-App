using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class OperationBlock
    {
        [Required]
        public int OperationBlockId { get; set; }//Block Id

        [Required(ErrorMessage ="Please insert the name of the Block")]

        [RegularExpression(@"([A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s0-9]+)", ErrorMessage = "Invalid Name")]
        public string BlockName { get; set; }       //Nome do bloco       também é aqui que é indicado se é de prevenção ou não                  


        public ICollection<OperationBlock_Shifts> OperationBlock_Shifts { get; set; }

    }
}