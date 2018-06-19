using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro.ViewModels
{
    [DataContract]
    public class АcquirenteViewModels
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string АcquirenteFIO { get; set; }
    }
}
