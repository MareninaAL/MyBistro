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
            List<CuocoViewModels> result = new List<CuocoViewModels>();
            for (int i = 0; i < source.cuoco.Count; ++i)
            {
                result.Add(new CuocoViewModels
                {
                    Id = source.cuoco[i].Id,
                    CuocoFIO = source.cuoco[i].CuocoFIO
                });
            }
            return result;
        }

        public CuocoViewModels GetElement(int id)
        {
            for (int i = 0; i < source.cuoco.Count; ++i)
            {
                if (source.cuoco[i].Id == id)
                {
                    return new CuocoViewModels
                    {
                        Id = source.cuoco[i].Id,
                        CuocoFIO = source.cuoco[i].CuocoFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CuocoBindingModels model)
        {
            int maxId = 0;
            for (int i = 0; i < source.cuoco.Count; ++i)
            {
                if (source.cuoco[i].Id > maxId)
                {
                    maxId = source.cuoco[i].Id;
                }
                if (source.cuoco[i].CuocoFIO == model.CuocoFIO)
                {
                    throw new Exception("Уже еCть Cотрудник C таким ФИО");
                }
            }
            source.cuoco.Add(new Cuoco
            {
                Id = maxId + 1,
                CuocoFIO = model.CuocoFIO
            });
        }

        public void UpdElement(CuocoBindingModels model)
        {
            int index = -1;
            for (int i = 0; i < source.cuoco.Count; ++i)
            {
                if (source.cuoco[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.cuoco[i].CuocoFIO == model.CuocoFIO &&
                    source.cuoco[i].Id != model.Id)
                {
                    throw new Exception("Уже еCть Cотрудник C таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.cuoco[index].CuocoFIO = model.CuocoFIO;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.cuoco.Count; ++i)
            {
                if (source.cuoco[i].Id == id)
                {
                    source.cuoco.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

}
}
