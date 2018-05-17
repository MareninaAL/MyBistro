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
    public class ConstituentServiceBD : IConstituentService
    {
        private BistroDbContext context;

        public ConstituentServiceBD(BistroDbContext context)
        {
            this.context = context;
        }

        public List<ConstituentViewModels> GetList()
        {
            List<ConstituentViewModels> result = context.constituents
                .Select(rec => new ConstituentViewModels
                {
                    Id = rec.Id,
                    ConstituentName = rec.ConstituentName
                })
                .ToList();
            return result;
        }

        public ConstituentViewModels GetElement(int id)
        {
            Constituent element = context.constituents.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ConstituentViewModels
                {
                    Id = element.Id,
                    ConstituentName = element.ConstituentName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ConstituentBindingModels model)
        {
            Constituent element = context.constituents.FirstOrDefault(rec => rec.ConstituentName == model.ConstituentName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            context.constituents.Add(new Constituent
            {
                ConstituentName = model.ConstituentName
            });
            context.SaveChanges();
        }

        public void UpdElement(ConstituentBindingModels model)
        {
            Constituent element = context.constituents.FirstOrDefault(rec =>
                                        rec.ConstituentName == model.ConstituentName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = context.constituents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ConstituentName = model.ConstituentName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Constituent element = context.constituents.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.constituents.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
