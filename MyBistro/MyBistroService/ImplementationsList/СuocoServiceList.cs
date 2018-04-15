using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBistro.BindingModels;
using MyBistroService.Interfaces;
using MyBistro.ViewModels;
using MyBistro;

namespace MyBistroService.ImplementationsList
{
    public class CuocoServiceList : ICuocoService
    {
        private DataListSingleton source;

        public CuocoServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<CuocoViewModels> GetList()
        {
            List<CuocoViewModels> result = source.cuoco.Select(rec => new CuocoViewModels
            {
                Id = rec.Id,
                CuocoFIO = rec.CuocoFIO
            })
              .ToList();
            return result;
        }

        public CuocoViewModels GetElement(int id)
        {
            Cuoco element = source.cuoco.FirstOrDefault(rec => rec.Id == id);
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
            Cuoco element = source.cuoco.FirstOrDefault(rec => rec.CuocoFIO == model.CuocoFIO);
            if (element != null)
            {
                throw new Exception("Уже еCть Cотрудник C таким ФИО");
            }
            int maxId = source.cuoco.Count > 0 ? source.cuoco.Max(rec => rec.Id) : 0;
            source.cuoco.Add(new Cuoco
            {
                Id = maxId + 1,
                CuocoFIO = model.CuocoFIO
            });
        }

        public void UpdElement(CuocoBindingModels model)
        {
            Cuoco element = source.cuoco.FirstOrDefault(rec =>
                                     rec.CuocoFIO == model.CuocoFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже еCть Cотрудник C таким ФИО");
            }
            element = source.cuoco.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CuocoFIO = model.CuocoFIO;
        }

        public void DelElement(int id)
        {
            Cuoco element = source.cuoco.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.cuoco.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

}
}
