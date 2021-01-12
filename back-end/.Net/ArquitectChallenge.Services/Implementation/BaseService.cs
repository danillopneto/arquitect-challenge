using ArquitectChallenge.Interfaces.Repository;
using ArquitectChallenge.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace ArquitectChallenge.Services.Implementation
{
    public abstract class BaseService<TRepository> : IBaseService where TRepository : IBaseRepository
    {
        protected readonly TRepository _repository;

        public BaseService(TRepository repository)
        {
            _repository = repository;
        }

        public virtual void DeleteById<T>(int id)
        {
            _repository.DeleteById<T>(id);
        }

        public virtual T GetById<T>(int id)
        {
            return _repository.GetById<T>(id);
        }

        public virtual IList<T> GetList<T>()
        {
            return _repository.GetList<T>();
        }

        public virtual T Save<T>(T model)
        {
            return _repository.Save(model);
        }

        protected virtual T ConvertTo<T>(object model)
        {
            if (model == null)
            {
                return default;
            }

            return (T)Convert.ChangeType(model, typeof(T));
        }
    }
}