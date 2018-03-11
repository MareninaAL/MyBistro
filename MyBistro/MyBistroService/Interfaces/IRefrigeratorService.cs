using MyBistro.BindingModels;
using MyBistro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService.Interfaces
{
    public interface IRefrigeratorService
    {
        List<RefrigeratorViewModels> GetList();

        RefrigeratorViewModels GetElement(int id);

        void AddElement(RefrigeratorBindingModels model);

        void UpdElement(RefrigeratorBindingModels model);

        void DelElement(int id);
    }
}
