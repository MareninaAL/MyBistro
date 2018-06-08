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
    [CustomInterface("Интерфейс для работы с изделиями")]
    public interface ISnackService
    {
        [CustomMethod("Метод получения списка изделий")]
        List<SnackViewModels> GetList();

        [CustomMethod("Метод получения изделия по id")]
        SnackViewModels GetElement(int id);

        [CustomMethod("Метод добавления изделия")]
        void AddElement(SnackBindingModels model);

        [CustomMethod("Метод изменения данных по изделию")]
        void UpdElement(SnackBindingModels model);

        [CustomMethod("Метод удаления изделия")]
        void DelElement(int id);
    }
}
