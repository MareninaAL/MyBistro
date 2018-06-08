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
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface IMainService
    {
        [CustomMethod("Метод получения списка заказов")]
        List<VitaAssassinaViewModels> GetList();

        [CustomMethod("Метод создания заказа")]
        void CreateVitaAssassina(VitaAssassinaBindingModels model);

        [CustomMethod("Метод передачи заказа в работу")]
        void TakeVitaAssassinarInWork(VitaAssassinaBindingModels model);

        [CustomMethod("Метод передачи заказа на оплату")]
        void FinishVitaAssassina(int id);

        [CustomMethod("Метод фиксирования оплаты по заказу")]
        void PayVitaAssassina(int id);

        [CustomMethod("Метод пополнения компонент на складе")]
        void PutConstituentOnRefrigerator(RefrigeratorConstituentBindingModels model);
    }
}
