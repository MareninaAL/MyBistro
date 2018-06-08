using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MyBistro.BindingModels
{
    [DataContract]
    public class SnackBindingModels
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string SnackName { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public List<ConstituentSnackBindingModels> ConstituentSnack { get; set; }
    }
}
