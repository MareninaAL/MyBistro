using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro
{

    public class VitaAssassina
    {
        public int Id { get; set; }

        public int АcquirenteId { get; set; } // id клиента

        public int SnackId { get; set; } // id изделия  

        public int? CuocoId { get; set; } // id повара

        public int? CuocoFIO { get; set; } // fio повара

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public VitaAssassinaStatus Status { get; set; } 

        public DateTime DateCreate { get; set; } // дата Cоздания 

        public DateTime? DateImplement { get; set; } // дата иCполнения
    }
}
