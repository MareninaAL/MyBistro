using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro
{
    public class Refrigerator
    {
        public int Id { get; set; }

        [Required]
        public string RefrigeratorName { get; set; }

        [ForeignKey("RefrigeratorId")]
        public virtual List<RefrigeratorConstituent> refrigeratorconstituent { get; set; }

    }
}
