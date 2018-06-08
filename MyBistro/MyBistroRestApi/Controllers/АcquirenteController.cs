using MyBistro.BindingModels;
using MyBistroService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyBistroRestApi.Controllers
{
    public class АcquirenteController : ApiController
    {
        private readonly IАcquirenteService _service;
        public АcquirenteController(IАcquirenteService service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("нет данных"));
            }
            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("нет данных"));
            }
            return Ok(element);
        }

        [HttpPost]
        public void AddElement(АcquirenteBindingModels model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(АcquirenteBindingModels model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(АcquirenteBindingModels model)
        {
            _service.DelElement(model.Id);
        }
    }
}
