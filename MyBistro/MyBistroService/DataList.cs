using MyBistro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService
{
     class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Аcquirente> acquirentes { get; set; }

        public List<Constituent> constituents { get; set; }

        public List<Cuoco> cuoco { get; set; }

        public List<VitaAssassina> vita_assassina { get; set; }

        public List<Snack> snacks { get; set; }

        public List<ConstituentSnack> constituent_snack { get; set; }

        public List<Refrigerator> refrigerators { get; set; }

        public List<RefrigeratorConstituent> refrigerator_constituent { get; set; }

        private DataListSingleton()
        {
            acquirentes = new List<Аcquirente>();
            constituents = new List<Constituent>();
            cuoco = new List<Cuoco>();
            vita_assassina = new List<VitaAssassina>();
            snacks = new List<Snack>();
            constituent_snack = new List<ConstituentSnack>();
            refrigerators = new List<Refrigerator>();
            refrigerator_constituent = new List<RefrigeratorConstituent>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }

            return instance;
        }
    }
}
