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
        /*
        [Required]
        public DateTime CreationDate { get; set; }//Eliminar
        //Colocar no controlador
        [Required]
        public DateTime FinishedDate { get; set; }//Eliminar
        */
        
        //Chave estrangueira (Nº do bloco)
        [Required]
        public OperationBlock OperationBlock { get; set; }
        [ForeignKey("OperationBlockFK")]
        public int OperationBlockFK { get; set; }

        [Required]
        public DateTime Date { get; set; }//Dia desse horário

        [Required]
        public string NurseName { get; set; }//Nome do enfermerio

        [Required]
        public string OperationBlockName { get; set; }//Nome do Bloco operatin Ex: Maternidade Prevençao

        [Required]
        public string ShiftType { get; set; }//Tipo de turno, M,T ou N

        
    }
}
