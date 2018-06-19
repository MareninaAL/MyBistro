using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MyBistro.BindingModels
{
    [DataContract]
    public class VitaAssassinaBindingModels
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int АcquirenteId { get; set; }

        [DataMember]
        public int SnackId { get; set; }

        [DataMember]
        public int? CuocoId { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }
    }
}
