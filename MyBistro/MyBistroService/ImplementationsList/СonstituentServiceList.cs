using System;
using System.Collections.Generic;
using System.Linq;
using MyBistro;
using MyBistro.BindingModels;
using MyBistro.ViewModels;
using MyBistroService.Interfaces;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService.ImplementationsList
{
    public class ConstituentServiceList : IConstituentService
    {
        private DataListSingleton source;

        public ConstituentServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ConstituentViewModels> GetList()
        {
            List<ConstituentViewModels> result = source.constituents
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
            Constituent element = source.constituents.FirstOrDefault(rec => rec.Id == id);
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
            Constituent element = source.constituents.FirstOrDefault(rec => rec.ConstituentName == model.ConstituentName);
            if (element != null)
            {
                throw new Exception("Уже еCть компонент C таким названием");
            }
            int maxId = source.constituents.Count > 0 ? source.constituents.Max(rec => rec.Id) : 0;
            source.constituents.Add(new Constituent
            {
                Id = maxId + 1,
                ConstituentName = model.ConstituentName
            });
        }

        public void UpdElement(ConstituentBindingModels model)
        {
            Constituent element = source.constituents.FirstOrDefault(rec =>
                                      rec.ConstituentName == model.ConstituentName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже еCть компонент C таким названием");
            }
            element = source.constituents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ConstituentName = model.ConstituentName;
        }

        public void DelElement(int id)
        {
            Constituent element = source.constituents.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.constituents.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
