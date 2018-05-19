using MyBistro;
using MyBistro.BindingModels;
using MyBistro.ViewModels;
using MyBistroService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService.ImplementationsBD
{
    public class AcquirenteServiceBD : IАcquirenteService
    {
        private BistroDbContext context;

        public AcquirenteServiceBD(BistroDbContext context)
        {
            this.context = context;
        }

        public List<АcquirenteViewModels> GetList()
        {
            List<АcquirenteViewModels> result = context.acquirentes
                .Select(rec => new АcquirenteViewModels
                {
                    Id = rec.Id,
                    АcquirenteFIO = rec.АcquirenteFIO
                })
                .ToList();
            return result;
        }

        public АcquirenteViewModels GetElement(int id)
        {
            Аcquirente element = context.acquirentes.FirstOrDefault(rec => rec.Id == id);
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
            Аcquirente element = context.acquirentes.FirstOrDefault(rec => rec.АcquirenteFIO == model.АcquirenteFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.acquirentes.Add(new Аcquirente
            {
                АcquirenteFIO = model.АcquirenteFIO
            });
            context.SaveChanges();
        }

        public void UpdElement(АcquirenteBindingModels model)
        {
            Аcquirente element = context.acquirentes.FirstOrDefault(rec =>
                                    rec.АcquirenteFIO == model.АcquirenteFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.acquirentes.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.АcquirenteFIO = model.АcquirenteFIO;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Аcquirente element = context.acquirentes.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.acquirentes.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
