using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro.ViewModels
{
    public class RefrigeratorConstituentViewModels
    {
        public int Id { get; set; }

        public int RefrigeratorId { get; set; }

        public int ConstituentId { get; set; }

        public string ConstituentName { get; set; }

        public int Count { get; set; }
    }
}
