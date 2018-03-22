using MyBistro.BindingModels;
using MyBistro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService.Interfaces
{
    public interface IАcquirenteService
    {
        List<АcquirenteViewModels> GetList();

        АcquirenteViewModels GetElement(int id);

        void AddElement(АcquirenteBindingModels model);

        void UpdElement(АcquirenteBindingModels model);

        void DelElement(int id);
    }
}
