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
    public  class АcquirenteServiceList : IАcquirenteService
    {
        private DataListSingleton source;

        public АcquirenteServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<АcquirenteViewModels> GetList()
        {
            List<АcquirenteViewModels> result = new List<АcquirenteViewModels>();
            for (int i = 0; i < source.acquirentes.Count; ++i)
            {
                result.Add(new АcquirenteViewModels
                {
                    Id = source.acquirentes[i].Id,
                    АcquirenteFIO = source.acquirentes[i].АcquirenteFIO
                });
            }
            return result;
        }

        public АcquirenteViewModels GetElement(int id)
        {
            for (int i = 0; i < source.acquirentes.Count; ++i)
            {
                if (source.acquirentes[i].Id == id)
                {
                    return new АcquirenteViewModels
                    {
                        Id = source.acquirentes[i].Id,
                        АcquirenteFIO = source.acquirentes[i].АcquirenteFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(АcquirenteBindingModels model)
        {
            int maxId = 0;
            for (int i = 0; i < source.acquirentes.Count; ++i)
            {
                if (source.acquirentes[i].Id > maxId)
                {
                    maxId = source.acquirentes[i].Id;
                }
                if (source.acquirentes[i].АcquirenteFIO == model.АcquirenteFIO)
                {
                    throw new Exception("Уже еCть клиент C таким ФИО");
                }
            }
            source.acquirentes.Add(new Аcquirente
            {
                Id = maxId + 1,
                АcquirenteFIO = model.АcquirenteFIO
            });
        }

        public void UpdElement(АcquirenteBindingModels model)
        {
            int index = -1;
            for (int i = 0; i < source.acquirentes.Count; ++i)
            {
                if (source.acquirentes[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.acquirentes[i].АcquirenteFIO == model.АcquirenteFIO &&
                    source.acquirentes[i].Id != model.Id)
                {
                    throw new Exception("Уже еCть клиент C таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.acquirentes[index].АcquirenteFIO = model.АcquirenteFIO;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.acquirentes.Count; ++i)
            {
                if (source.acquirentes[i].Id == id)
                {
                    source.acquirentes.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
