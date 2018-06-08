using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService.ViewModels
{
    [DataContract]
    public class RefregiratorsLoadViewModel
    {
        [DataMember]
        public string RefregiratorName { get; set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public IEnumerable<Tuple<string, int>> Constituent { get; set; }
    }
}
