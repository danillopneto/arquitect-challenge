using ArquitectChallenge.Domain;
using ArquitectChallenge.Interfaces.Repository;
using System;
using System.Collections.Generic;

namespace ArquitectChallenge.Services.Repository
{
    public abstract class BaseRepository<TDto> : IBaseRepository where TDto : BaseObject
    {
        protected readonly DataContext _dataContext;

        public BaseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public virtual void DeleteById<T>(int id)
        {
            var item = GetById<T>(id);
            if (item == null)
            {
                return;
            }

            _dataContext.Remove(item);
            _dataContext.SaveChanges();
        }

        public abstract T GetById<T>(int id);

        public abstract IList<T> GetList<T>();

        public virtual T Save<T>(T model)
        {
            var modelConverted = model as TDto;

            if (modelConverted != null 
                    && modelConverted.Id > 0)
            {
                var item = GetById<T>(modelConverted.Id) as TDto;
                if (item != null)
                {
                    UpdateItem(item, modelConverted);
                }
                else
                {
                    _dataContext.Add(model);
                }
            }
            else
            {
                _dataContext.Add(model);
            }

            _dataContext.SaveChanges();
            return model;
        }

        protected abstract void UpdateItem(TDto currentItem, TDto updatedItem);
    }
}