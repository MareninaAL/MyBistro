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
            List<VitaAssassinaViewModels> result = new List<VitaAssassinaViewModels>();
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
            return result;
        }

        public void CreateVitaAssassina(VitaAssassinaBindingModels model)
        {
            int maxId = 0;
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
            });
        }

        public void TakeVitaAssassinarInWork(VitaAssassinaBindingModels model)
        {
            int index = -1;
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
            source.vita_assassina[index].Status = VitaAssassinaStatus.ВыполняетCя;
        }

        public void FinishVitaAssassina(int id)
        {
            int index = -1;
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
            source.vita_assassina[index].Status = VitaAssassinaStatus.Готов;
        }

        public void PayVitaAssassina(int id)
        {
            int index = -1;
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
            source.vita_assassina[index].Status = VitaAssassinaStatus.Оплачен;
        }

        public void PutConstituentOnRefrigerator(RefrigeratorConstituentBindingModels model)
        {
            int maxId = 0;
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
        }
    }
}
