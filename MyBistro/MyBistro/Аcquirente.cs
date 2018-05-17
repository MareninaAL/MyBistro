using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBistro
{
 
    public class Аcquirente
    {
        public int Id { get; set; }

        [Required]
        public string АcquirenteFIO { get; set; }

        [ForeignKey("АcquirenteId")]
        public virtual List<VitaAssassina> vitaAssassina { get; set; }
    }
}
