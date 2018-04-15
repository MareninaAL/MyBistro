﻿using System;
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
    public class ConstituentServiceList : IConstituentService
    {
        private DataListSingleton source;

        public ConstituentServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ConstituentViewModels> GetList()
        {
            /* List<ConstituentViewModels> result = new List<ConstituentViewModels>();
             for (int i = 0; i < source.constituents.Count; ++i)
             {
                 result.Add(new ConstituentViewModels
                 {
                     Id = source.constituents[i].Id,
                     ConstituentName = source.constituents[i].ConstituentName
                 });
             }
             return result; */
            List<ConstituentViewModels> result = source.constituents
              .Select(rec => new ConstituentViewModels
              {
                  Id = rec.Id,
                  ConstituentName = rec.ConstituentName
              })
              .ToList();
            return result;
        }

        public ConstituentViewModels GetElement(int id)
        {
            /* for (int i = 0; i < source.constituents.Count; ++i)
             {
                 if (source.constituents[i].Id == id)
                 {
                     return new ConstituentViewModels
                     {
                         Id = source.constituents[i].Id,
                         ConstituentName = source.constituents[i].ConstituentName
                     };
                 }
             }
             throw new Exception("Элемент не найден"); */
            Constituent element = source.constituents.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ConstituentViewModels
                {
                    Id = element.Id,
                    ConstituentName = element.ConstituentName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ConstituentBindingModels model)
        {
            /* int maxId = 0;
             for (int i = 0; i < source.constituents.Count; ++i)
             {
                 if (source.constituents[i].Id > maxId)
                 {
                     maxId = source.constituents[i].Id;
                 }
                 if (source.constituents[i].ConstituentName == model.ConstituentName)
                 {
                     throw new Exception("Уже еCть компонент C таким названием");
                 }
             }
             source.constituents.Add(new Constituent
             {
                 Id = maxId + 1,
                 ConstituentName = model.ConstituentName
             });  */
            Constituent element = source.constituents.FirstOrDefault(rec => rec.ConstituentName == model.ConstituentName);
            if (element != null)
            {
                throw new Exception("Уже еCть компонент C таким названием");
            }
            int maxId = source.constituents.Count > 0 ? source.constituents.Max(rec => rec.Id) : 0;
            source.constituents.Add(new Constituent
            {
                Id = maxId + 1,
                ConstituentName = model.ConstituentName
            });
        }

        public void UpdElement(ConstituentBindingModels model)
        {
            /* int index = -1;
             for (int i = 0; i < source.constituents.Count; ++i)
             {
                 if (source.constituents[i].Id == model.Id)
                 {
                     index = i;
                 }
                 if (source.constituents[i].ConstituentName == model.ConstituentName &&
                     source.constituents[i].Id != model.Id)
                 {
                     throw new Exception("Уже еCть компонент C таким названием");
                 }
             }
             if (index == -1)
             {
                 throw new Exception("Элемент не найден");
             }
             source.constituents[index].ConstituentName = model.ConstituentName; */
            Constituent element = source.constituents.FirstOrDefault(rec =>
                                      rec.ConstituentName == model.ConstituentName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже еCть компонент C таким названием");
            }
            element = source.constituents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ConstituentName = model.ConstituentName;
        }

        public void DelElement(int id)
        {
            /*   for (int i = 0; i < source.constituents.Count; ++i)
               {
                   if (source.constituents[i].Id == id)
                   {
                       source.constituents.RemoveAt(i);
                       return;
                   }
               }
               throw new Exception("Элемент не найден"); */
            Constituent element = source.constituents.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.constituents.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
