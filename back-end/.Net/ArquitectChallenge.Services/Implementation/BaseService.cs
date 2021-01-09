using ArquitectChallenge.Interfaces.Repository;
using ArquitectChallenge.Interfaces.Services;
using System.Collections.Generic;

namespace ArquitectChallenge.Services.Implementation
{
    public abstract class BaseService : IBaseService
    {
        private readonly IBaseRepository _eventRepository;

        public BaseService(IBaseRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void DeleteById<T>(int id)
        {
            _eventRepository.DeleteById<T>(id);
        }

        public T GetById<T>(int id)
        {
            return _eventRepository.GetById<T>(id);
        }

        public IList<T> GetList<T>()
        {
            return _eventRepository.GetList<T>();
        }

        public T Save<T>(T model)
        {
            return _eventRepository.Save(model);
        }
    }
}