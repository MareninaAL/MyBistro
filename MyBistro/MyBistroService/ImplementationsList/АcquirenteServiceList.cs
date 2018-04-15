using MyBistro;
using MyBistro.BindingModels;
using MyBistro.ViewModels;
using MyBistroService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService.ImplementationsList
{
    public  class АcquirenteServiceList : IАcquirenteService
    {
        private DataListSingleton source;

        public АcquirenteServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<АcquirenteViewModels> GetList()
        {
            List<АcquirenteViewModels> result = source.acquirentes.Select(rec => new АcquirenteViewModels
            {
                Id = rec.Id,
                АcquirenteFIO = rec.АcquirenteFIO

            }).ToList();
            return result;
        }

        public АcquirenteViewModels GetElement(int id)
        {
            Аcquirente element = source.acquirentes.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new АcquirenteViewModels
                {
                    Id = element.Id,
                    АcquirenteFIO = element.АcquirenteFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(АcquirenteBindingModels model)
        {
            Аcquirente element = source.acquirentes.FirstOrDefault(rec => rec.АcquirenteFIO == model.АcquirenteFIO);
            if (element != null)
            {
                throw new Exception("Уже еCть клиент C таким ФИО");
            }
            int maxId = source.acquirentes.Count > 0 ? source.acquirentes.Max(rec => rec.Id) : 0;
            source.acquirentes.Add(new Аcquirente
            {
                Id = maxId + 1,
                АcquirenteFIO = model.АcquirenteFIO
            });
        }

        public void UpdElement(АcquirenteBindingModels model)
        {
            Аcquirente element = source.acquirentes.FirstOrDefault(rec => rec.АcquirenteFIO == model.АcquirenteFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже еCть клиент C таким ФИО");
            }
            element = source.acquirentes.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.АcquirenteFIO = model.АcquirenteFIO;
        }

        public void DelElement(int id)
        {
            Аcquirente element = source.acquirentes.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.acquirentes.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
