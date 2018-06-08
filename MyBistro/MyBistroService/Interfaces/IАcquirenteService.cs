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
    [CustomInterface("Интерфейс для работы с клиентами")]
    public interface IАcquirenteService
    {
        [CustomMethod("Метод получения списка клиентов")]
        List<АcquirenteViewModels> GetList();

        [CustomMethod("Метод получения клиента по id")]
        АcquirenteViewModels GetElement(int id);

        [CustomMethod("Метод добавления клиента")]
        void AddElement(АcquirenteBindingModels model);

        [CustomMethod("Метод изменения данных по клиенту")]
        void UpdElement(АcquirenteBindingModels model);

        [CustomMethod("Метод удаления клиента")]
        void DelElement(int id);
    }
}
