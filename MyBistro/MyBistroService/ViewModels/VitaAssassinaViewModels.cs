using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro.ViewModels
{
    [DataContract]
    public class VitaAssassinaViewModels
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int АcquirenteId { get; set; }

        [DataMember]
        public string АcquirenteFIO { get; set; }

        [DataMember]
        public int SnackId { get; set; }

        [DataMember]
        public string SnackName { get; set; }

        [DataMember]
        public int? CuocoId { get; set; }

        [DataMember]
        public string CuocoFIO { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string DateCreate { get; set; }

        [DataMember]
        public string DateImplement { get; set; }
    }
}
