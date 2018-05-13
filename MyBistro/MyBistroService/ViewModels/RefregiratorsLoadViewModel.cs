using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService.ViewModels
{
    public class RefregiratorsLoadViewModel
    {
        public string RefregiratorName { get; set; }

        public int TotalCount { get; set; }

        public IEnumerable<Tuple<string, int>> Constituent { get; set; }
    }
}
