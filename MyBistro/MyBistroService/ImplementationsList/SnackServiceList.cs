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
            List<SnackViewModels> result = new List<SnackViewModels>();
            for (int i = 0; i < source.snacks.Count; ++i)
            {
                List<ConstituentSnackViewModels> productComponents = new List<ConstituentSnackViewModels>();
                for (int j = 0; j < source.constituent_snack.Count; ++j)
                {
                    if (source.constituent_snack[j].SnackId == source.snacks[i].Id)
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
                        productComponents.Add(new ConstituentSnackViewModels
                        {
                            Id = source.constituent_snack[j].Id,
                            SnackId = source.constituent_snack[j].SnackId,
                            ConstituentId = source.constituent_snack[j].ConstituentId,
                            ConstituentName = componentName,
                            Count = source.constituent_snack[j].Count
                        });
                    }
                }
                result.Add(new SnackViewModels
                {
                    Id = source.snacks[i].Id,
                    SnackName = source.snacks[i].SnackName,
                    Price = source.snacks[i].Price,
                    ConstituentSnack = productComponents
                });
            }
            return result;
        }

        public SnackViewModels GetElement(int id)
        {
            for (int i = 0; i < source.snacks.Count; ++i)
            {
                List<ConstituentSnackViewModels> productComponents = new List<ConstituentSnackViewModels>();
                for (int j = 0; j < source.constituent_snack.Count; ++j)
                {
                    if (source.constituent_snack[j].SnackId == source.snacks[i].Id)
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
                        productComponents.Add(new ConstituentSnackViewModels
                        {
                            Id = source.constituent_snack[j].Id,
                            SnackId = source.constituent_snack[j].SnackId,
                            ConstituentId = source.constituent_snack[j].ConstituentId,
                            ConstituentName = componentName,
                            Count = source.constituent_snack[j].Count
                        });
                    }
                }
                if (source.snacks[i].Id == id)
                {
                    return new SnackViewModels
                    {
                        Id = source.snacks[i].Id,
                        SnackName = source.snacks[i].SnackName,
                        Price = source.snacks[i].Price,
                        ConstituentSnack = productComponents
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(SnackBindingModels model)
        {
            int maxId = 0;
            for (int i = 0; i < source.snacks.Count; ++i)
            {
                if (source.snacks[i].Id > maxId)
                {
                    maxId = source.snacks[i].Id;
                }
                if (source.snacks[i].SnackName == model.SnackName)
                {
                    throw new Exception("Уже еCть изделие C таким названием");
                }
            }
            source.snacks.Add(new Snack
            {
                Id = maxId + 1,
                SnackName = model.SnackName,
                Price = model.Price
            });
            int maxPCId = 0;
            for (int i = 0; i < source.constituent_snack.Count; ++i)
            {
                if (source.constituent_snack[i].Id > maxPCId)
                {
                    maxPCId = source.constituent_snack[i].Id;
                }
            }
            for (int i = 0; i < model.ConstituentSnack.Count; ++i)
            {
                for (int j = 1; j < model.ConstituentSnack.Count; ++j)
                {
                    if (model.ConstituentSnack[i].ConstituentId ==
                        model.ConstituentSnack[j].ConstituentId)
                    {
                        model.ConstituentSnack[i].Count +=
                            model.ConstituentSnack[j].Count;
                        model.ConstituentSnack.RemoveAt(j--);
                    }
                }
            }
            for (int i = 0; i < model.ConstituentSnack.Count; ++i)
            {
                source.constituent_snack.Add(new ConstituentSnack
                {
                    Id = ++maxPCId,
                    SnackId = maxId + 1,
                    ConstituentId = model.ConstituentSnack[i].ConstituentId,
                    Count = model.ConstituentSnack[i].Count
                });
            }
        }

        public void UpdElement(SnackBindingModels model)
        {
            int index = -1;
            for (int i = 0; i < source.snacks.Count; ++i)
            {
                if (source.snacks[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.snacks[i].SnackName == model.SnackName &&
                    source.snacks[i].Id != model.Id)
                {
                    throw new Exception("Уже еCть изделие C таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.snacks[index].SnackName = model.SnackName;
            source.snacks[index].Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.constituent_snack.Count; ++i)
            {
                if (source.constituent_snack[i].Id > maxPCId)
                {
                    maxPCId = source.constituent_snack[i].Id;
                }
            }
            for (int i = 0; i < source.constituent_snack.Count; ++i)
            {
                if (source.constituent_snack[i].SnackId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.ConstituentSnack.Count; ++j)
                    {
                        if (source.constituent_snack[i].Id == model.ConstituentSnack[j].Id)
                        {
                            source.constituent_snack[i].Count = model.ConstituentSnack[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        source.constituent_snack.RemoveAt(i--);
                    }
                }
            }
            for (int i = 0; i < model.ConstituentSnack.Count; ++i)
            {
                if (model.ConstituentSnack[i].Id == 0)
                {
                    for (int j = 0; j < source.constituent_snack.Count; ++j)
                    {
                        if (source.constituent_snack[j].SnackId == model.Id &&
                            source.constituent_snack[j].ConstituentId == model.ConstituentSnack[i].ConstituentId)
                        {
                            source.constituent_snack[j].Count += model.ConstituentSnack[i].Count;
                            model.ConstituentSnack[i].Id = source.constituent_snack[j].Id;
                            break;
                        }
                    }
                    if (model.ConstituentSnack[i].Id == 0)
                    {
                        source.constituent_snack.Add(new ConstituentSnack
                        {
                            Id = ++maxPCId,
                            SnackId = model.Id,
                            ConstituentId = model.ConstituentSnack[i].ConstituentId,
                            Count = model.ConstituentSnack[i].Count
                        });
                    }
                }
            }
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.constituent_snack.Count; ++i)
            {
                if (source.constituent_snack[i].SnackId == id)
                {
                    source.constituent_snack.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.snacks.Count; ++i)
            {
                if (source.snacks[i].Id == id)
                {
                    source.snacks.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

    }
}
