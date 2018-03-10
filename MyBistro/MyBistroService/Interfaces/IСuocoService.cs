using MyBistro.BindingModels;
using MyBistro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService.Interfaces
{
    public interface ICuocoService
    {
        List<CuocoViewModels> GetList();

        CuocoViewModels GetElement(int id);

        void AddElement(CuocoBindingModels model);

        void UpdElement(CuocoBindingModels model);

        void DelElement(int id);
    }
}
