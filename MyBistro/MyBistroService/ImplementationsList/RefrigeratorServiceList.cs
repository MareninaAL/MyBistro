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
    public class RefrigeratorServiceList : IRefrigeratorService
    {
        private DataListSingleton source;

        public RefrigeratorServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<RefrigeratorViewModels> GetList()
        {
           
            List<RefrigeratorViewModels> result = source.refrigerators
              .Select(rec => new RefrigeratorViewModels
              {
                  Id = rec.Id,
                  RefrigeratorName = rec.RefrigeratorName,
                  RefrigeratorConstituent = source.refrigerator_constituent
                          .Where(recPC => recPC.RefrigeratorId == rec.Id)
                          .Select(recPC => new RefrigeratorConstituentViewModels
                          {
                              Id = recPC.Id,
                              RefrigeratorId = recPC.RefrigeratorId,
                              ConstituentId = recPC.ConstituentId,
                              ConstituentName = source.constituents
                                  .FirstOrDefault(recC => recC.Id == recPC.ConstituentId)?.ConstituentName,
                              Count = recPC.Count
                          })
                          .ToList()
              })
              .ToList();
            return result;
        }

        public RefrigeratorViewModels GetElement(int id)
        {
           
            Refrigerator element = source.refrigerators.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new RefrigeratorViewModels
                {
                    Id = element.Id,
                    RefrigeratorName = element.RefrigeratorName,
                    RefrigeratorConstituent = source.refrigerator_constituent
                            .Where(recPC => recPC.RefrigeratorId == element.Id)
                            .Select(recPC => new RefrigeratorConstituentViewModels
                            {
                                Id = recPC.Id,
                                RefrigeratorId = recPC.RefrigeratorId,
                                ConstituentId = recPC.ConstituentId,
                                ConstituentName = source.constituents
                                    .FirstOrDefault(recC => recC.Id == recPC.ConstituentId)?.ConstituentName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(RefrigeratorBindingModels model)
        {
         
            Refrigerator element = source.refrigerators.FirstOrDefault(rec => rec.RefrigeratorName == model.RefrigeratorName);
            if (element != null)
            {
                throw new Exception("Уже еCть Cклад C таким названием");
            }
            int maxId = source.refrigerators.Count > 0 ? source.refrigerators.Max(rec => rec.Id) : 0;
            source.refrigerators.Add(new Refrigerator
            {
                Id = maxId + 1,
                RefrigeratorName = model.RefrigeratorName
            });
        }

        public void UpdElement(RefrigeratorBindingModels model)
        {
            Refrigerator element = source.refrigerators.FirstOrDefault(rec =>
                                                  rec.RefrigeratorName == model.RefrigeratorName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже еCть Cклад C таким названием");
            }
            element = source.refrigerators.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.RefrigeratorName = model.RefrigeratorName;
        }

        public void DelElement(int id)
        {
          
            Refrigerator element = source.refrigerators.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.refrigerator_constituent.RemoveAll(rec => rec.RefrigeratorId == id);
                source.refrigerators.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

    }
}
