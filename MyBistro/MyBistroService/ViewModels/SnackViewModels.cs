using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro.ViewModels
{
    public class SnackViewModels
    {

        public int Id { get; set; }

        public string SnackName { get; set; }

        public decimal Price { get; set; }

        public List<ConstituentSnackViewModels> ConstituentSnack { get; set; }
    }
}
