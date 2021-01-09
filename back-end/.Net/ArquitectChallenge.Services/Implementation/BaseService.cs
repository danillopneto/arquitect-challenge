using ArquitectChallenge.Interfaces.Repository;
using ArquitectChallenge.Interfaces.Services;
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

        public void DeleteById<T>(int id)
        {
            _repository.DeleteById<T>(id);
        }

        public T GetById<T>(int id)
        {
            return _repository.GetById<T>(id);
        }

        public IList<T> GetList<T>()
        {
            return _repository.GetList<T>();
        }

        public T Save<T>(T model)
        {
            return _repository.Save(model);
        }
    }
}