using MyBistro.BindingModels;
using MyBistroRestApi.Service;
using MyBistroService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyBistroRestApi.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;

        public MainController(IMainService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void CreateVitaAssassina(VitaAssassinaBindingModels model)
        {
            _service.CreateVitaAssassina(model);
        }

        [HttpPost]
        public void TakeVitaAssassinarInWork(VitaAssassinaBindingModels model)
        {
            _service.TakeVitaAssassinarInWork(model);
        }

        [HttpPost]
        public void FinishVitaAssassina(VitaAssassinaBindingModels model)
        {
            _service.FinishVitaAssassina(model.Id);
        }

        [HttpPost]
        public void PayVitaAssassina(VitaAssassinaBindingModels model)
        {
            _service.PayVitaAssassina(model.Id);
        }

        [HttpPost]
        public void PutConstituentOnRefrigerator(RefrigeratorConstituentBindingModels model)
        {
            _service.PutConstituentOnRefrigerator(model);
        }

        [HttpGet]
        public IHttpActionResult GetInfo()
        {
            ReflectionService service = new ReflectionService();
            var list = service.GetInfoByAssembly();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
    }
}
