using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBistroService.ViewModels;
using MyBistroService.BindingModels;

namespace MyBistroService.Interfaces
{
    public interface IReportService
    {
        void SaveSnackPrice(ReportBindingModel model);

        List<RefregiratorsLoadViewModel> GetRefregiratorsLoad();

        void SaveRefregiratorsLoad(ReportBindingModel model);

        List<AcquirenteVitaAssassinaModel> GetAcquirenteVitaAssassinas(ReportBindingModel model);

        void SaveAcquirenteVitaAssassinas(ReportBindingModel model);
    }
}
