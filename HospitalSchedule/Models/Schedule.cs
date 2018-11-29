using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace HospitalSchedule.Models
{
    public class Schedule
    {
        [Required]
        public int ScheduleId { get; set; }//Id do horário
        
        [Required]
        public DateTime Date { get; set; }//Dia desse horário

        //Nome do enfermerio,Nome do Bloco operatin Ex: Maternidade Prevençao e Tipo de turno, M,T ou N
        //São buscados através das chaves estrangeiras

        [Required]
        public Nurse Nurse { get; set; }
        [ForeignKey("NurseId")]
        public int NurseId { get; set; }

        [Required]
        public ICollection<Shift_Schedule_OperationBlock> Shift_Schedule_OperationBlock { get; set; }
    }
}
