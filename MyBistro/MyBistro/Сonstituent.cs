using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBistro
{
    public class Constituent
    {
        public int Id { get; set; }

        [Required]
        public string ConstituentName { get; set; }

        [ForeignKey("ConstituentId")]
        public virtual List<ConstituentSnack> constituentsnack { get; set; }

        [ForeignKey("ConstituentId")]
        public virtual List<RefrigeratorConstituent> refrigeratorconstituent { get; set; }
    }


}
