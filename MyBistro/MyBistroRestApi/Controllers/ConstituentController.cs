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
    public class ConstituentController : ApiController
    {
        private readonly IConstituentService _service;
        public ConstituentController(IConstituentService service)
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
        public void AddElement(ConstituentBindingModels model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(ConstituentBindingModels model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(ConstituentBindingModels model)
        {
            _service.DelElement(model.Id);
        }
    }

}
