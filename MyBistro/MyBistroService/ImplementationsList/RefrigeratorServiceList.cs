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
            /* List<RefrigeratorViewModels> result = new List<RefrigeratorViewModels>();
             for (int i = 0; i < source.refrigerators.Count; ++i)
             {
                 List<RefrigeratorConstituentViewModels> StockComponents = new List<RefrigeratorConstituentViewModels>();
                 for (int j = 0; j < source.refrigerator_constituent.Count; ++j)
                 {
                     if (source.refrigerator_constituent[j].RefrigeratorId == source.refrigerators[i].Id)
                     {
                         string componentName = string.Empty;
                         for (int k = 0; k < source.constituents.Count; ++k)
                         {
                             if (source.constituent_snack[j].ConstituentId == source.constituents[k].Id)
                             {
                                 componentName = source.constituents[k].ConstituentName;
                                 break;
                             }
                         }
                         StockComponents.Add(new RefrigeratorConstituentViewModels
                         {
                             Id = source.refrigerator_constituent[j].Id,
                             RefrigeratorId = source.refrigerator_constituent[j].RefrigeratorId,
                             ConstituentId = source.refrigerator_constituent[j].ConstituentId,
                             ConstituentName = componentName,
                             Count = source.refrigerator_constituent[j].Count
                         });
                     }
                 }
                 result.Add(new RefrigeratorViewModels
                 {
                     Id = source.refrigerators[i].Id,
                     RefrigeratorName = source.refrigerators[i].RefrigeratorName,
                     RefrigeratorConstituent = StockComponents
                 });
             }
             return result; */
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
            /* for (int i = 0; i < source.refrigerators.Count; ++i)
             {
                 List<RefrigeratorConstituentViewModels> StockComponents = new List<RefrigeratorConstituentViewModels>();
                 for (int j = 0; j < source.refrigerator_constituent.Count; ++j)
                 {
                     if (source.refrigerator_constituent[j].RefrigeratorId == source.refrigerators[i].Id)
                     {
                         string componentName = string.Empty;
                         for (int k = 0; k < source.constituents.Count; ++k)
                         {
                             if (source.constituent_snack[j].ConstituentId == source.constituents[k].Id)
                             {
                                 componentName = source.constituents[k].ConstituentName;
                                 break;
                             }
                         }
                         StockComponents.Add(new RefrigeratorConstituentViewModels
                         {
                             Id = source.constituent_snack[j].Id,
                             RefrigeratorId = source.constituent_snack[j].SnackId,
                             ConstituentId = source.constituent_snack[j].ConstituentId,
                             ConstituentName = componentName,
                             Count = source.constituent_snack[j].Count
                         });
                     }
                 }
                 if (source.refrigerators[i].Id == id)
                 {
                     return new RefrigeratorViewModels
                     {
                         Id = source.refrigerators[i].Id,
                         RefrigeratorName = source.refrigerators[i].RefrigeratorName,
                         RefrigeratorConstituent = StockComponents
                     };
                 }
             }
             throw new Exception("Элемент не найден"); */
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
            /*  int maxId = 0;
              for (int i = 0; i < source.refrigerators.Count; ++i)
              {
                  if (source.refrigerators[i].Id > maxId)
                  {
                      maxId = source.refrigerators[i].Id;
                  }
                  if (source.refrigerators[i].RefrigeratorName == model.RefrigeratorName)
                  {
                      throw new Exception("Уже еCть Cклад C таким названием");
                  }
              }
              source.refrigerators.Add(new Refrigerator
              {
                  Id = maxId + 1,
                  RefrigeratorName = model.RefrigeratorName
              }); */
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
            /* int index = -1;
             for (int i = 0; i < source.refrigerators.Count; ++i)
             {
                 if (source.refrigerators[i].Id == model.Id)
                 {
                     index = i;
                 }
                 if (source.refrigerators[i].RefrigeratorName == model.RefrigeratorName &&
                     source.refrigerators[i].Id != model.Id)
                 {
                     throw new Exception("Уже еCть Cклад C таким названием");
                 }
             }
             if (index == -1)
             {
                 throw new Exception("Элемент не найден");
             }
             source.refrigerators[index].RefrigeratorName = model.RefrigeratorName; */
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
            /* for (int i = 0; i < source.refrigerator_constituent.Count; ++i)
             {
                 if (source.refrigerator_constituent[i].RefrigeratorId == id)
                 {
                     source.refrigerator_constituent.RemoveAt(i--);
                 }
             }
             for (int i = 0; i < source.refrigerators.Count; ++i)
             {
                 if (source.refrigerators[i].Id == id)
                 {
                     source.refrigerators.RemoveAt(i);
                     return;
                 }
             }
             throw new Exception("Элемент не найден"); */
            Refrigerator element = source.refrigerators.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // при удалении удаляем вCе запиCи о компонентах на удаляемом Cкладе
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
