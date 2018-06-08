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
    [CustomInterface("Интерфейс для работы со складами")]
    public interface IRefrigeratorService
    {
        [CustomMethod("Метод получения списка складов")]
        List<RefrigeratorViewModels> GetList();

        [CustomMethod("Метод получения склада по id")]
        RefrigeratorViewModels GetElement(int id);

        [CustomMethod("Метод добавления склада")]
        void AddElement(RefrigeratorBindingModels model);

        [CustomMethod("Метод обновления данных по складу")]
        void UpdElement(RefrigeratorBindingModels model);

        [CustomMethod("Метод удаления склада")]
        void DelElement(int id);
    }
}
