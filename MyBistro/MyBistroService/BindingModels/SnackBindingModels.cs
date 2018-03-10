using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro.BindingModels
{
    public class SnackBindingModels
    {
        public int Id { get; set; }

        public string SnackName { get; set; }

        public decimal Price { get; set; }

        public List<ConstituentSnackBindingModels> ConstituentSnack { get; set; }
    }
}
