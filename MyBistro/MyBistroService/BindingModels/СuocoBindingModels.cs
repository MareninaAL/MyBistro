﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyBistro.BindingModels
{
    [DataContract]
    public class CuocoBindingModels
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string CuocoFIO { get; set; }
    }
}
