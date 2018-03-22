using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro.ViewModels
{
    public class RefrigeratorViewModels
    {
        public int Id { get; set; }

        public string RefrigeratorName { get; set; }

        public List<RefrigeratorConstituentViewModels> RefrigeratorConstituent { get; set; }

    }
}
