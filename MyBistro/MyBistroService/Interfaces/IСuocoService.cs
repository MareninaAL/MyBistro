using MyBistro.BindingModels;
using MyBistro.ViewModels;
using MyBistroService.Attributies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService.Interfaces
{
    [CustomInterface("Интерфейс для работы с работниками")]
    public interface ICuocoService
    {
        [CustomMethod("Метод получения списка работников")]
        List<CuocoViewModels> GetList();

        [CustomMethod("Метод получения работника по id")]
        CuocoViewModels GetElement(int id);

        [CustomMethod("Метод добавления работника")]
        void AddElement(CuocoBindingModels model);

        [CustomMethod("Метод изменения данных по работнику")]
        void UpdElement(CuocoBindingModels model);

        [CustomMethod("Метод удаления работника ")]
        void DelElement(int id);
    }
}
