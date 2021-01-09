using System.Collections.Generic;

namespace ArquitectChallenge.Interfaces.Repository
{
    public interface IBaseRepository
    {
        void DeleteById<T>(int id);

        T GetById<T>(int id);

        IList<T> GetList<T>();

        T Save<T>(T model);
    }
}