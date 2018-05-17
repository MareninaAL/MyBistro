using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro
{
 public   class Cuoco
    {
        public int Id { get; set; }

        [Required]
        public string CuocoFIO { get; set; }

        [ForeignKey("CuocoId")]
        public virtual List<VitaAssassina> vitaassassina { get; set; }
    }
}
