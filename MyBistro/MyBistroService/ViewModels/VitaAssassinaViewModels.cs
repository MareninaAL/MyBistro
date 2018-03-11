using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro.ViewModels
{
    public class VitaAssassinaViewModels
    {
        public int Id { get; set; }
        
        public int АcquirenteId { get; set; } 

        public string АcquirenteFIO { get; set; }

        public int SnackId { get; set; }   

        public string SnackName { get; set; }

        public int? CuocoId { get; set; } 

        public string CuocoFIO { get; set; } 

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public string Status { get; set; }

        public string DateCreate { get; set; }

        public string DateImplement { get; set; }
    }
}
