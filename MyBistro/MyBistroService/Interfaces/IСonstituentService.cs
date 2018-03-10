using MyBistro.BindingModels;
using MyBistro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService.Interfaces
{
    public interface IConstituentService
    {
        List<ConstituentViewModels> GetList();

        ConstituentViewModels GetElement(int id);

        void AddElement(ConstituentBindingModels model);

        void UpdElement(ConstituentBindingModels model);

        void DelElement(int id);
    }
}
