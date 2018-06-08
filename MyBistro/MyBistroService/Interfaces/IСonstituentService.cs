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
    [CustomInterface("Интерфейс для работы с компонентами")]
    public interface IConstituentService
    {
        [CustomMethod("Метод получения списка компонент")]
        List<ConstituentViewModels> GetList();

        [CustomMethod("Метод получения компонента по id")]
        ConstituentViewModels GetElement(int id);

        [CustomMethod("Метод добавления компонента")]
        void AddElement(ConstituentBindingModels model);

        [CustomMethod("Метод изменения данных по компоненту")]
        void UpdElement(ConstituentBindingModels model);

        [CustomMethod("Метод удаления компонента")]
        void DelElement(int id);
    }
}
