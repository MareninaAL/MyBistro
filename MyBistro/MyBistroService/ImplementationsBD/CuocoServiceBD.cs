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
    public class CuocoServiceBD : ICuocoService
    {
        private BistroDbContext context;

        public CuocoServiceBD(BistroDbContext context)
        {
            this.context = context;
        }

        public List<CuocoViewModels> GetList()
        {
            List<CuocoViewModels> result = context.cuocos
                .Select(rec => new CuocoViewModels
                {
                    Id = rec.Id,
                    CuocoFIO = rec.CuocoFIO
                })
                .ToList();
            return result;
        }

        public CuocoViewModels GetElement(int id)
        {
            Cuoco element = context.cuocos.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CuocoViewModels
                {
                    Id = element.Id,
                    CuocoFIO = element.CuocoFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CuocoBindingModels model)
        {
            Cuoco element = context.cuocos.FirstOrDefault(rec => rec.CuocoFIO == model.CuocoFIO);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            context.cuocos.Add(new Cuoco
            {
                CuocoFIO = model.CuocoFIO
            });
            context.SaveChanges();
        }

        public void UpdElement(CuocoBindingModels model)
        {
            Cuoco element = context.cuocos.FirstOrDefault(rec =>
                                        rec.CuocoFIO == model.CuocoFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            element = context.cuocos.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CuocoFIO = model.CuocoFIO;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Cuoco element = context.cuocos.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.cuocos.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
