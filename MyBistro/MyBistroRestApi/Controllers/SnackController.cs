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
    public class SnackController : ApiController
    {
        private readonly ISnackService _service;
        public SnackController(ISnackService service)
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
        public void AddElement(SnackBindingModels model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(SnackBindingModels model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(SnackBindingModels model)
        {
            _service.DelElement(model.Id);
        }
    }
}
