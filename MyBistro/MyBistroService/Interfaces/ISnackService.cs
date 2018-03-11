using MyBistro.BindingModels;
using MyBistro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService.Interfaces
{
    public interface ISnackService
    {
        List<SnackViewModels> GetList();

        SnackViewModels GetElement(int id);

        void AddElement(SnackBindingModels model);

        void UpdElement(SnackBindingModels model);

        void DelElement(int id);
    }
}
