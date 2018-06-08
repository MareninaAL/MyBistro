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
using System.Net.Mail;
using System.Configuration;
using System.Net;

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
            var vitaAssassinas = new VitaAssassina
            {
                АcquirenteId = model.АcquirenteId,
                SnackId = model.SnackId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = VitaAssassinaStatus.Принят
            };
            context.vitaAssassinas.Add(vitaAssassinas);
            context.SaveChanges();

            var acquirente = context.acquirentes.FirstOrDefault(x => x.Id == model.АcquirenteId);
            SendEmail(acquirente.Mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} создан успешно", vitaAssassinas.Id,
                vitaAssassinas.DateCreate.ToShortDateString()));
        }

        public void TakeVitaAssassinarInWork(VitaAssassinaBindingModels model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    VitaAssassina element = context.vitaAssassinas.Include(rec => rec.Аcquirente).FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    var SnackConstituents = context.constituentSnacks
                                                .Include(rec => rec.Constituent)
                                                .Where(rec => rec.SnackId == element.SnackId);
                    foreach (var SnackConstituent in SnackConstituents)
                    {
                        int countOnRefrigerators = SnackConstituent.Count * element.Count;
                        var RefrigeratorConstituents = context.refrigeratorConstituents
                                                    .Where(rec => rec.ConstituentId == SnackConstituent.ConstituentId);
                        foreach (var RefrigeratorConstituent in RefrigeratorConstituents)
                        {
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
                    SendEmail(element.Аcquirente.Mail, "Оповещение по заказам",
                       string.Format("Заказ №{0} от {1} передеан в работу", element.Id, element.DateCreate.ToShortDateString()));
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
            SendEmail(element.Аcquirente.Mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} передан на оплату", element.Id,
                element.DateCreate.ToShortDateString()));
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
            SendEmail(element.Аcquirente.Mail, "Оповещение по заказам",
            string.Format("Заказ №{0} от {1} оплачен успешно", element.Id, element.DateCreate.ToShortDateString()));
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

        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;

            try
            {
                objMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailLogin"],
                    ConfigurationManager.AppSettings["MailPassword"]);

                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }
    }
}
