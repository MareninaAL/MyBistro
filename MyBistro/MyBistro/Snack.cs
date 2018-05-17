using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro
{
    public class Snack
    {
        public int Id { get; set; }

        [Required]
        public string SnackName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("SnackId")]
        public virtual List<VitaAssassina> vitaassassina { get; set; }

        [ForeignKey("SnackId")]
        public virtual List<ConstituentSnack> constituentsnack { get; set; }
    }
}
