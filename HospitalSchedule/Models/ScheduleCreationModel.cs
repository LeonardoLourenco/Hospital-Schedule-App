using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class ScheduleCreationModel
    {
        [Required]
        //Ir a buscar o BlockName da classe operationBlock
        public OperationBlock OperationBlock { get; set; }
        [Required]
        //Ir a buscar dados dos enfermeiros a partir da classe dos enfermeiros
        public Nurse Nurse { get; set; }

        [Required]
        //Ir a buscar dados dos horário a partir da classe do horário
        /*Exemplo:Gerar uma data de criação
        */

        public Schedules Schedule { get; set; }
    }
}
