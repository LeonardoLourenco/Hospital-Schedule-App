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
        public int OperationBlockId { get; set; }            //Block Id

        [Required]
        public string BlockName { get; set; }       //Nome do bloco       também é aqui que é indicado se é de prevenção ou não             

        [Required]
        public string TypeOfShift { get; set; }     //Os tipos de turnos presentes neste bloco M - manha T - Tarde N - Noite
                                                    //Ex M:T:N funciona de manha,tarde e noite                
        [Required]
        public ICollection<Shift_Schedule_OperationBlock> Shift_Schedule_OperationBlock { get; set; }

    }
}