using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro.ViewModels
{
    [DataContract]
    public class RefrigeratorViewModels
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string RefrigeratorName { get; set; }

        [DataMember]
        public List<RefrigeratorConstituentViewModels> RefrigeratorConstituent { get; set; }

    }
}
