using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService.ViewModels
{
    [DataContract]
    public class AcquirenteVitaAssassinaModel
    {
        [DataMember]
        public string AcquirenteName { get; set; }

        [DataMember]
        public string DateCreate { get; set; }

        [DataMember]
        public string SnackName { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }

        [DataMember]
        public string Status { get; set; }
    }
}
