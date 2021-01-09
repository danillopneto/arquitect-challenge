using System.Collections.Generic;

namespace ArquitectChallenge.Interfaces.Repositories
{
    public interface IBaseRepository
    {
        void DeleteById(string id);

        T GetById<T>(string id);

        IList<T> GetList<T>();

        T Save<T>(T model);
    }
}