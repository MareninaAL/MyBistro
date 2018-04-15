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
      
        VitaAssassina element = source.vita_assassina.FirstOrDefault(rec => rec.Id == id);
        if (element == null)
        {
            throw new Exception("Элемент не найден");
        }
        element.Status = VitaAssassinaStatus.Готов;
    }

        public void PayVitaAssassina(int id)
        {
        
        VitaAssassina element = source.vita_assassina.FirstOrDefault(rec => rec.Id == id);
        if (element == null)
        {
            throw new Exception("Элемент не найден");
        }
        element.Status = VitaAssassinaStatus.Оплачен;
    }

        public void PutConstituentOnRefrigerator(RefrigeratorConstituentBindingModels model)
        {
         
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
