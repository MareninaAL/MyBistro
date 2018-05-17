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
    public class RefrigeratorServiceBD : IRefrigeratorService
    {
        private BistroDbContext context;

        public RefrigeratorServiceBD(BistroDbContext context)
        {
            this.context = context;
        }

        public List<RefrigeratorViewModels> GetList()
        {
            List<RefrigeratorViewModels> result = context.refrigerators
                .Select(rec => new RefrigeratorViewModels
                {
                    Id = rec.Id,
                    RefrigeratorName = rec.RefrigeratorName,
                    RefrigeratorConstituent = context.refrigeratorConstituents
                            .Where(recPC => recPC.RefrigeratorId == rec.Id)
                            .Select(recPC => new RefrigeratorConstituentViewModels
                            {
                                Id = recPC.Id,
                                RefrigeratorId = recPC.RefrigeratorId,
                                ConstituentId = recPC.ConstituentId,
                                ConstituentName = recPC.Constituent.ConstituentName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public RefrigeratorViewModels GetElement(int id)
        {
            Refrigerator element = context.refrigerators.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new RefrigeratorViewModels
                {
                    Id = element.Id,
                    RefrigeratorName = element.RefrigeratorName,
                    RefrigeratorConstituent = context.refrigeratorConstituents
                            .Where(recPC => recPC.RefrigeratorId == element.Id)
                            .Select(recPC => new RefrigeratorConstituentViewModels
                            {
                                Id = recPC.Id,
                                RefrigeratorId = recPC.RefrigeratorId,
                                ConstituentId = recPC.ConstituentId,
                                ConstituentName = recPC.Constituent.ConstituentName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(RefrigeratorBindingModels model)
        {
            Refrigerator element = context.refrigerators.FirstOrDefault(rec => rec.RefrigeratorName == model.RefrigeratorName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            context.refrigerators.Add(new Refrigerator
            {
                RefrigeratorName = model.RefrigeratorName
            });
            context.SaveChanges();
        }

        public void UpdElement(RefrigeratorBindingModels model)
        {
            Refrigerator element = context.refrigerators.FirstOrDefault(rec =>
                                        rec.RefrigeratorName == model.RefrigeratorName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = context.refrigerators.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.RefrigeratorName = model.RefrigeratorName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Refrigerator element = context.refrigerators.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        context.refrigeratorConstituents.RemoveRange(
                                            context.refrigeratorConstituents.Where(rec => rec.RefrigeratorId == id));
                        context.refrigerators.Remove(element);
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
