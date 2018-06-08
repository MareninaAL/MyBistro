using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBistroService.ViewModels;
using MyBistroService.BindingModels;
using MyBistroService.Attributies;

namespace MyBistroService.Interfaces
{
    [CustomInterface("Интерфейс для работы с отчетами")]
    public interface IReportService
    {
        [CustomMethod("Метод сохранения списка изделий в doc-файл")]
        void SaveSnackPrice(ReportBindingModel model);

        [CustomMethod("Метод получения списка складов с количество компонент на них")]
        List<RefregiratorsLoadViewModel> GetRefregiratorsLoad();

        [CustomMethod("Метод сохранения списка списка складов с количество компонент на них в xls-файл")]
        void SaveRefregiratorsLoad(ReportBindingModel model);

        [CustomMethod("Метод получения списка заказов клиентов")]
        List<AcquirenteVitaAssassinaModel> GetAcquirenteVitaAssassinas(ReportBindingModel model);

        [CustomMethod("Метод сохранения списка заказов клиентов в pdf-файл")]
        void SaveAcquirenteVitaAssassinas(ReportBindingModel model);
    }
}
