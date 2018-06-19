using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro.BindingModels
{
    [DataContract]
    public class RefrigeratorConstituentBindingModels
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int RefrigeratorId { get; set; }

        [DataMember]
        public int ConstituentId { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
