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
    public  class MainServiceList : IMainService
    {
        private DataListSingleton source;

        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<VitaAssassinaViewModels> GetList()
        {
            /*  List<VitaAssassinaViewModels> result = new List<VitaAssassinaViewModels>();
              for (int i = 0; i < source.vita_assassina.Count; ++i)
              {
                  string clientFIO = string.Empty;
                  for (int j = 0; j < source.acquirentes.Count; ++j)
                  {
                      if (source.acquirentes[j].Id == source.vita_assassina[i].Id)
                      {
                          clientFIO = source.acquirentes[j].АcquirenteFIO;
                          break;
                      }
                  }
                  string productName = string.Empty;
                  for (int j = 0; j < source.snacks.Count; ++j)
                  {
                      if (source.snacks[j].Id == source.vita_assassina[i].SnackId)
                      {
                          productName = source.snacks[j].SnackName;
                          break;
                      }
                  }
                  string implementerFIO = string.Empty;
                  if (source.vita_assassina[i].CuocoId.HasValue)
                  {
                      for (int j = 0; j < source.cuoco.Count; ++j)
                      {
                          if (source.cuoco[j].Id == source.vita_assassina[i].CuocoId.Value)
                          {
                              implementerFIO = source.cuoco[j].CuocoFIO;
                              break;
                          }
                      }
                  }
                  result.Add(new VitaAssassinaViewModels
                  {
                      Id = source.vita_assassina[i].Id,
                      АcquirenteId = source.vita_assassina[i].АcquirenteId,
                      АcquirenteFIO = clientFIO,
                      SnackId = source.vita_assassina[i].SnackId,
                      SnackName = productName,
                      CuocoId = source.vita_assassina[i].CuocoId,
                      CuocoFIO = implementerFIO,
                      Count = source.vita_assassina[i].Count,
                      Sum = source.vita_assassina[i].Sum,
                      DateCreate = source.vita_assassina[i].DateCreate.ToLongDateString(),
                      DateImplement = source.vita_assassina[i].DateImplement?.ToLongDateString(),
                      Status = source.vita_assassina[i].Status.ToString()
                  });
              }
              return result; */
            List<VitaAssassinaViewModels> result = source.vita_assassina
              .Select(rec => new VitaAssassinaViewModels
              {
                  Id = rec.Id,
                  АcquirenteId = rec.АcquirenteId,
                  SnackId = rec.SnackId,
                  CuocoId = rec.CuocoId,
                  DateCreate = rec.DateCreate.ToLongDateString(),
                  DateImplement = rec.DateImplement?.ToLongDateString(),
                  Status = rec.Status.ToString(),
                  Count = rec.Count,
                  Sum = rec.Sum,
                  АcquirenteFIO = source.acquirentes
                                  .FirstOrDefault(recC => recC.Id == rec.АcquirenteId)?.АcquirenteFIO,
                  SnackName = source.snacks
                                  .FirstOrDefault(recP => recP.Id == rec.SnackId)?.SnackName,
                  CuocoFIO = source.cuoco
                                  .FirstOrDefault(recI => recI.Id == rec.CuocoId)?.CuocoFIO
              })
              .ToList();
            return result;
        }

        public void CreateVitaAssassina(VitaAssassinaBindingModels model)
        {
            /* int maxId = 0;
             for (int i = 0; i < source.vita_assassina.Count; ++i)
             {
                 if (source.vita_assassina[i].Id > maxId)
                 {
                     maxId = source.acquirentes[i].Id;
                 }
             }
             source.vita_assassina.Add(new VitaAssassina
             {
                 Id = maxId + 1,
                 АcquirenteId = model.АcquirenteId,
                 SnackId = model.SnackId,
                 DateCreate = DateTime.Now,
                 Count = model.Count,
                 Sum = model.Sum,
                 Status = VitaAssassinaStatus.Принят
             }); */
            int maxId = source.vita_assassina.Count > 0 ? source.vita_assassina.Max(rec => rec.Id) : 0;
            source.vita_assassina.Add(new VitaAssassina
            {
                Id = maxId + 1,
                CuocoId = model.CuocoId,
                SnackId = model.SnackId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = VitaAssassinaStatus.Принят
            });
        }
    

        public void TakeVitaAssassinarInWork(VitaAssassinaBindingModels model)
        {
        /* int index = -1;
         for (int i = 0; i < source.vita_assassina.Count; ++i)
         {
             if (source.vita_assassina[i].Id == model.Id)
             {
                 index = i;
                 break;
             }
         }
         if (index == -1)
         {
             throw new Exception("Элемент не найден");
         }
         for (int i = 0; i < source.constituent_snack.Count; ++i)
         {
             if (source.constituent_snack[i].SnackId == source.vita_assassina[index].SnackId)
             {
                 int countOnStocks = 0;
                 for (int j = 0; j < source.refrigerator_constituent.Count; ++j)
                 {
                     if (source.refrigerator_constituent[j].ConstituentId == source.constituent_snack[i].ConstituentId)
                     {
                         countOnStocks += source.refrigerator_constituent[j].Count;
                     }
                 }
                 if (countOnStocks < source.constituent_snack[i].Count * source.vita_assassina[index].Count)
                 {
                     for (int j = 0; j < source.constituents.Count; ++j)
                     {
                         if (source.constituents[j].Id == source.constituent_snack[i].ConstituentId)
                         {
                             throw new Exception("Не доCтаточно компонента " + source.constituents[j].ConstituentName +
                                 " требуетCя " + source.constituent_snack[i].Count + ", в наличии " + countOnStocks);
                         }
                     }
                 }
             }
         }
         for (int i = 0; i < source.constituent_snack.Count; ++i)
         {
             if (source.constituent_snack[i].SnackId == source.vita_assassina[index].SnackId)
             {
                 int countOnStocks = source.constituent_snack[i].Count * source.vita_assassina[index].Count;
                 for (int j = 0; j < source.refrigerator_constituent.Count; ++j)
                 {
                     if (source.refrigerator_constituent[j].ConstituentId == source.constituent_snack[i].ConstituentId)
                     {
                         if (source.refrigerator_constituent[j].Count >= countOnStocks)
                         {
                             source.refrigerator_constituent[j].Count -= countOnStocks;
                             break;
                         }
                         else
                         {
                             countOnStocks -= source.refrigerator_constituent[j].Count;
                             source.refrigerator_constituent[j].Count = 0;
                         }
                     }
                 }
             }
         }
         source.vita_assassina[index].CuocoId = model.CuocoId;
         source.vita_assassina[index].DateImplement = DateTime.Now;
         source.vita_assassina[index].Status = VitaAssassinaStatus.ВыполняетCя; */
        VitaAssassina element = source.vita_assassina.FirstOrDefault(rec => rec.Id == model.Id);
        if (element == null)
        {
            throw new Exception("Элемент не найден");
        }
        var productComponents = source.constituent_snack.Where(rec => rec.SnackId == element.SnackId);
        foreach (var productComponent in productComponents)
        {
            int countOnStocks = source.refrigerator_constituent
                                        .Where(rec => rec.ConstituentId == productComponent.ConstituentId)
                                        .Sum(rec => rec.Count);
            if (countOnStocks < productComponent.Count * element.Count)
            {
                var componentName = source.constituents
                                .FirstOrDefault(rec => rec.Id == productComponent.ConstituentId);
                throw new Exception("Не доCтаточно компонента " + componentName?.ConstituentName +
                    " требуетCя " + productComponent.Count + ", в наличии " + countOnStocks);
            }
        }
        foreach (var productComponent in productComponents)
        {
            int countOnStocks = productComponent.Count * element.Count;
            var stockComponents = source.refrigerator_constituent
                                        .Where(rec => rec.ConstituentId == productComponent.ConstituentId);
            foreach (var stockComponent in stockComponents)
            {
                if (stockComponent.Count >= countOnStocks)
                {
                    stockComponent.Count -= countOnStocks;
                    break;
                }
                else
                {
                    countOnStocks -= stockComponent.Count;
                    stockComponent.Count = 0;
                }
            }
        }
        element.CuocoId = model.CuocoId;
        element.DateImplement = DateTime.Now;
        element.Status = VitaAssassinaStatus.ВыполняетCя;
    }

        public void FinishVitaAssassina(int id)
        {
        /*  int index = -1;
          for (int i = 0; i < source.vita_assassina.Count; ++i)
          {
              if (source.acquirentes[i].Id == id)
              {
                  index = i;
                  break;
              }
          }
          if (index == -1)
          {
              throw new Exception("Элемент не найден");
          }
          source.vita_assassina[index].Status = VitaAssassinaStatus.Готов; */
        VitaAssassina element = source.vita_assassina.FirstOrDefault(rec => rec.Id == id);
        if (element == null)
        {
            throw new Exception("Элемент не найден");
        }
        element.Status = VitaAssassinaStatus.Готов;
    }

        public void PayVitaAssassina(int id)
        {
        /* int index = -1;
         for (int i = 0; i < source.vita_assassina.Count; ++i)
         {
             if (source.acquirentes[i].Id == id)
             {
                 index = i;
                 break;
             }
         }
         if (index == -1)
         {
             throw new Exception("Элемент не найден");
         }
         source.vita_assassina[index].Status = VitaAssassinaStatus.Оплачен; */
        VitaAssassina element = source.vita_assassina.FirstOrDefault(rec => rec.Id == id);
        if (element == null)
        {
            throw new Exception("Элемент не найден");
        }
        element.Status = VitaAssassinaStatus.Оплачен;
    }

        public void PutConstituentOnRefrigerator(RefrigeratorConstituentBindingModels model)
        {
            /* int maxId = 0;
            for (int i = 0; i < source.refrigerator_constituent.Count; ++i)
            {
                if (source.refrigerator_constituent[i].RefrigeratorId == model.RefrigeratorId &&
                    source.refrigerator_constituent[i].ConstituentId == model.ConstituentId)
                {
                    source.refrigerator_constituent[i].Count += model.Count;
                    return;
                }
                if (source.refrigerator_constituent[i].Id > maxId)
                {
                    maxId = source.refrigerator_constituent[i].Id;
                }
            }
            source.refrigerator_constituent.Add(new RefrigeratorConstituent
            {
                Id = ++maxId,
                RefrigeratorId = model.RefrigeratorId,
                ConstituentId = model.ConstituentId,
                Count = model.Count
            });
            }*/
            RefrigeratorConstituent element = source.refrigerator_constituent
                                            .FirstOrDefault(rec => rec.RefrigeratorId == model.RefrigeratorId &&
                                                                rec.ConstituentId == model.ConstituentId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                int maxId = source.refrigerator_constituent.Count > 0 ? source.refrigerator_constituent.Max(rec => rec.Id) : 0;
                source.refrigerator_constituent.Add(new RefrigeratorConstituent
                {
                    Id = ++maxId,
                    RefrigeratorId = model.RefrigeratorId,
                    ConstituentId = model.ConstituentId,
                    Count = model.Count
                });
            }
        }
    }
}
