using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro.BindingModels
{

    public class VitaAssassinaBindingModels
    {
        public int Id { get; set; }

        public int АcquirenteId { get; set; } 

        public int SnackId { get; set; }

        public int? CuocoId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
