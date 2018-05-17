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
    public class SnackServiceBD : ISnackService
    {
        private BistroDbContext context;

        public SnackServiceBD(BistroDbContext context)
        {
            this.context = context;
        }

        public List<SnackViewModels> GetList()
        {
            List<SnackViewModels> result = context.snacks
                .Select(rec => new SnackViewModels
                {
                    Id = rec.Id,
                    SnackName = rec.SnackName,
                    Price = rec.Price,
                    ConstituentSnack = context.constituentSnacks
                            .Where(recPC => recPC.SnackId == rec.Id)
                            .Select(recPC => new ConstituentSnackViewModels
                            {
                                Id = recPC.Id,
                                SnackId = recPC.SnackId,
                                ConstituentId = recPC.ConstituentId,
                                ConstituentName = recPC.Constituent.ConstituentName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public SnackViewModels GetElement(int id)
        {
            Snack element = context.snacks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SnackViewModels
                {
                    Id = element.Id,
                    SnackName = element.SnackName,
                    Price = element.Price,
                    ConstituentSnack = context.constituentSnacks
                            .Where(recPC => recPC.SnackId == element.Id)
                            .Select(recPC => new ConstituentSnackViewModels
                            {
                                Id = recPC.Id,
                                SnackId = recPC.SnackId,
                                ConstituentId = recPC.ConstituentId,
                                ConstituentName = recPC.Constituent.ConstituentName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(SnackBindingModels model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Snack element = context.snacks.FirstOrDefault(rec => rec.SnackName == model.SnackName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Snack
                    {
                        SnackName = model.SnackName,
                        Price = model.Price
                    };
                    context.snacks.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupComponents = model.ConstituentSnack
                                                .GroupBy(rec => rec.ConstituentId)
                                                .Select(rec => new
                                                {
                                                    ConstituentId = rec.Key,
                                                    Count = rec.Sum(r => r.Count)
                                                });
                    // добавляем компоненты
                    foreach (var groupComponent in groupComponents)
                    {
                        context.constituentSnacks.Add(new ConstituentSnack
                        {
                            SnackId = element.Id,
                            ConstituentId = groupComponent.ConstituentId,
                            Count = groupComponent.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdElement(SnackBindingModels model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Snack element = context.snacks.FirstOrDefault(rec =>
                                        rec.SnackName == model.SnackName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.snacks.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.SnackName = model.SnackName;
                    element.Price = model.Price;
                    context.SaveChanges();

                    // обновляем существуюущие компоненты
                    var compIds = model.ConstituentSnack.Select(rec => rec.ConstituentId).Distinct();
                    var updateComponents = context.constituentSnacks
                                                    .Where(rec => rec.SnackId == model.Id &&
                                                        compIds.Contains(rec.ConstituentId));
                    foreach (var updateComponent in updateComponents)
                    {
                        updateComponent.Count = model.ConstituentSnack
                                                        .FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
                    }
                    context.SaveChanges();
                    context.constituentSnacks.RemoveRange(
                                        context.constituentSnacks.Where(rec => rec.SnackId == model.Id &&
                                                                            !compIds.Contains(rec.ConstituentId)));
                    context.SaveChanges();
                    // новые записи
                    var groupComponents = model.ConstituentSnack
                                                .Where(rec => rec.Id == 0)
                                                .GroupBy(rec => rec.ConstituentId)
                                                .Select(rec => new
                                                {
                                                    ConstituentId = rec.Key,
                                                    Count = rec.Sum(r => r.Count)
                                                });
                    foreach (var groupComponent in groupComponents)
                    {
                        ConstituentSnack elementPC = context.constituentSnacks
                                                .FirstOrDefault(rec => rec.SnackId == model.Id &&
                                                                rec.ConstituentId == groupComponent.ConstituentId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupComponent.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.constituentSnacks.Add(new ConstituentSnack
                            {
                                SnackId = model.Id,
                                ConstituentId = groupComponent.ConstituentId,
                                Count = groupComponent.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Snack element = context.snacks.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.constituentSnacks.RemoveRange(
                                            context.constituentSnacks.Where(rec => rec.SnackId == id));
                        context.snacks.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
