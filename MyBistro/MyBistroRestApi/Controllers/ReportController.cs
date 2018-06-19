using MyBistroService.BindingModels;
using MyBistroService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyBistroRestApi.Controllers
{
    public class ReportController : ApiController
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetRefregiratorsLoad()
        {
            var list = _service.GetRefregiratorsLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public IHttpActionResult GetAcquirenteVitaAssassinas(ReportBindingModel model)
        {
            var list = _service.GetAcquirenteVitaAssassinas(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void SaveSnackPrice(ReportBindingModel model)
        {
            _service.SaveSnackPrice(model);
        }

        [HttpPost]
        public void SaveRefregiratorsLoad(ReportBindingModel model)
        {
            _service.SaveRefregiratorsLoad(model);
        }

        [HttpPost]
        public void SaveAcquirenteVitaAssassinas(ReportBindingModel model)
        {
            _service.SaveAcquirenteVitaAssassinas(model);
        }
    }
}
