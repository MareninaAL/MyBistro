using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro.ViewModels
{
    [DataContract]
    public class ConstituentViewModels
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ConstituentName { get; set; }
    }
}
