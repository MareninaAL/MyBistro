using MyBistro.BindingModels;
using MyBistro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService.Interfaces
{
    public interface IMainService
    {
        List<VitaAssassinaViewModels> GetList();

        void CreateVitaAssassina(VitaAssassinaBindingModels model);

        void TakeVitaAssassinarInWork(VitaAssassinaBindingModels model);

        void FinishVitaAssassina(int id);

        void PayVitaAssassina(int id);

        void PutConstituentOnRefrigerator(RefrigeratorConstituentBindingModels model);
    }
}
