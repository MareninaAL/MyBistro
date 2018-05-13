using MyBistro;
using MyBistro.BindingModels;
using MyBistro.ViewModels;
using MyBistroService.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService.ImplementationsBD
{
    public class MainServiceBD : IMainService
    {
        private BistroDbContext context;

        public MainServiceBD(BistroDbContext context)
        {
            this.context = context;
        }

        public List<VitaAssassinaViewModels> GetList()
        {
            List<VitaAssassinaViewModels> result = context.vitaAssassinas
                .Select(rec => new VitaAssassinaViewModels
                {
                    Id = rec.Id,
                    АcquirenteId = rec.АcquirenteId,
                    SnackId = rec.SnackId,
                    CuocoId = rec.CuocoId,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    DateImplement = rec.DateImplement == null ? "" :
                                        SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    АcquirenteFIO = rec.Аcquirente.АcquirenteFIO,
                    SnackName = rec.Snack.SnackName,
                    CuocoFIO = rec.Cuoco.CuocoFIO
                })
                .ToList();
            return result;
        }

        public void CreateVitaAssassina(VitaAssassinaBindingModels model)
        {
            context.vitaAssassinas.Add(new VitaAssassina
            {
                АcquirenteId = model.АcquirenteId,
                SnackId = model.SnackId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = VitaAssassinaStatus.Принят
            });
            context.SaveChanges();
        }

        public void TakeVitaAssassinarInWork(VitaAssassinaBindingModels model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    VitaAssassina element = context.vitaAssassinas.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    var SnackConstituents = context.constituentSnacks
                                                .Include(rec => rec.Constituent)
                                                .Where(rec => rec.SnackId == element.SnackId);
                    // списываем
                    foreach (var SnackConstituent in SnackConstituents)
                    {
                        int countOnRefrigerators = SnackConstituent.Count * element.Count;
                        var RefrigeratorConstituents = context.refrigeratorConstituents
                                                    .Where(rec => rec.ConstituentId == SnackConstituent.ConstituentId);
                        foreach (var RefrigeratorConstituent in RefrigeratorConstituents)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (RefrigeratorConstituent.Count >= countOnRefrigerators)
                            {
                                RefrigeratorConstituent.Count -= countOnRefrigerators;
                                countOnRefrigerators = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnRefrigerators -= RefrigeratorConstituent.Count;
                                RefrigeratorConstituent.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnRefrigerators > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                                SnackConstituent.Constituent.ConstituentName + " требуется " +
                                SnackConstituent.Count + ", не хватает " + countOnRefrigerators);
                        }
                    }
                    element.CuocoId = model.CuocoId;
                    element.DateImplement = DateTime.Now;
                    element.Status = VitaAssassinaStatus.ВыполняетCя;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void FinishVitaAssassina(int id)
        {
            VitaAssassina element = context.vitaAssassinas.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = VitaAssassinaStatus.Готов;
            context.SaveChanges();
        }

        public void PayVitaAssassina(int id)
        {
            VitaAssassina element = context.vitaAssassinas.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = VitaAssassinaStatus.Оплачен;
            context.SaveChanges();
        }

        public void PutConstituentOnRefrigerator(RefrigeratorConstituentBindingModels model)
        {
            RefrigeratorConstituent element = context.refrigeratorConstituents
                                                .FirstOrDefault(rec => rec.RefrigeratorId == model.RefrigeratorId &&
                                                                    rec.ConstituentId == model.ConstituentId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.refrigeratorConstituents.Add(new RefrigeratorConstituent
                {
                    RefrigeratorId = model.RefrigeratorId,
                    ConstituentId = model.ConstituentId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }
    }
}
