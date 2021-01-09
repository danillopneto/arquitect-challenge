using System.Collections.Generic;

namespace ArquitectChallenge.Interfaces.Services
{
    public interface IBaseService
    {
        void DeleteById<T>(int id);

        T GetById<T>(int id);

        IList<T> GetList<T>();

        T Save<T>(T model);
    }
}