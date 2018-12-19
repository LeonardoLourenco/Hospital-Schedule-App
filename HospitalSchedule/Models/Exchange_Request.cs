using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Models
{
    public class Exchange_Request
    {
        [Required]
        public int Exchange_RequestId { get; set; }

        public Schedule_Exchange1 Schedule_Exchange1 { get; set; }  //Com este id podemos buscar o horário 1
        [Required]
        public int Schedule_Exchange1Id { get; set; }

        public Schedule_Exchange2 Schedule_Exchange2 { get; set; }  //Com este id podemos buscar o horário 2
        [Required]
        public int Schedule_Exchange2Id { get; set; }

        [Required]
        public string RequestState { get; set; } //Estado do Pedido  quando criado tem o valor de "Pending" ,
                                                // as alterações so podem ser "Approved"/"Not Approved"
        [Required]                                     
        public DateTime Date_RequestState { get; set; } //Data da ultima alteração do estado
        [Required]
        public DateTime Date_Exchange_Request { get; set; } //Data da realização do pedido  criado com o valor do tempo do sistema
    }
}
