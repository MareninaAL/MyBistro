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
    public class SnackServiceList : ISnackService
    {
        private DataListSingleton source;

        public SnackServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<SnackViewModels> GetList()
        {
           
            List<SnackViewModels> result = source.snacks
              .Select(rec => new SnackViewModels
              {
                  Id = rec.Id,
                  SnackName = rec.SnackName,
                  Price = rec.Price,
                  ConstituentSnack = source.constituent_snack
                          .Where(recPC => recPC.SnackId == rec.Id)
                          .Select(recPC => new ConstituentSnackViewModels
                          {
                              Id = recPC.Id,
                              SnackId = recPC.SnackId,
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

        public SnackViewModels GetElement(int id)
         {
            
            Snack element = source.snacks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SnackViewModels
                {
                    Id = element.Id,
                    SnackName = element.SnackName,
                    Price = element.Price,
                    ConstituentSnack = source.constituent_snack
                            .Where(recPC => recPC.SnackId == element.Id)
                            .Select(recPC => new ConstituentSnackViewModels
                            {
                                Id = recPC.Id,
                                SnackId = recPC.SnackId,
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

        public void AddElement(SnackBindingModels model)
         {
          
            Snack element = source.snacks.FirstOrDefault(rec => rec.SnackName == model.SnackName);
            if (element != null)
            {
                throw new Exception("Уже еCть изделие C таким названием");
            }
            int maxId = source.snacks.Count > 0 ? source.snacks.Max(rec => rec.Id) : 0;
            source.snacks.Add(new Snack
            {
                Id = maxId + 1,
                SnackName = model.SnackName,
                Price = model.Price
            });
            int maxPCId = source.constituent_snack.Count > 0 ?
                                    source.constituent_snack.Max(rec => rec.Id) : 0;
            
            var groupComponents = model.ConstituentSnack
                                        .GroupBy(rec => rec.ConstituentId)
                                        .Select(rec => new
                                        {
                                            ComponentId = rec.Key,
                                            Count = rec.Sum(r => r.Count)
                                        });
           
            foreach (var groupComponent in groupComponents)
            {
                source.constituent_snack.Add(new ConstituentSnack
                {
                    Id = ++maxPCId,
                    SnackId = maxId + 1,
                    ConstituentId = groupComponent.ComponentId,
                    Count = groupComponent.Count
                });
            }
        }

        public void UpdElement(SnackBindingModels model)
        {
          
            Snack element = source.snacks.FirstOrDefault(rec =>
                                      rec.SnackName == model.SnackName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже еCть изделие C таким названием");
            }
            element = source.snacks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SnackName = model.SnackName;
            element.Price = model.Price;

            int maxPCId = source.constituent_snack.Count > 0 ? source.constituent_snack.Max(rec => rec.Id) : 0;
          
            var compIds = model.ConstituentSnack.Select(rec => rec.ConstituentId).Distinct();
            var updateComponents = source.constituent_snack
                                            .Where(rec => rec.SnackId == model.Id &&
                                           compIds.Contains(rec.ConstituentId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.Count = model.ConstituentSnack
                                                .FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
            }
            source.constituent_snack.RemoveAll(rec => rec.SnackId == model.Id &&
                                       !compIds.Contains(rec.ConstituentId));
        
            var groupComponents = model.ConstituentSnack
                                        .Where(rec => rec.Id == 0)
                                        .GroupBy(rec => rec.ConstituentId)
                                        .Select(rec => new
                                        {
                                            ComponentId = rec.Key,
                                            Count = rec.Sum(r => r.Count)
                                        });
            foreach (var groupComponent in groupComponents)
            {
                ConstituentSnack elementPC = source.constituent_snack
                                        .FirstOrDefault(rec => rec.SnackId == model.Id &&
                                                        rec.ConstituentId == groupComponent.ComponentId);
                if (elementPC != null)
                {
                    elementPC.Count += groupComponent.Count;
                }
                else
                {
                    source.constituent_snack.Add(new ConstituentSnack
                    {
                        Id = ++maxPCId,
                        SnackId = model.Id,
                        ConstituentId = groupComponent.ComponentId,
                        Count = groupComponent.Count
                    });
                }
            }
        }

        public void DelElement(int id)
        {
            Snack element = source.snacks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.constituent_snack.RemoveAll(rec => rec.SnackId == id);
                source.snacks.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

    }
}
