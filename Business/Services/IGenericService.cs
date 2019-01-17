using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IGenericService<T, E>
        where T : class
        where E : IGenericRepository<T>
    {
        List<T> GetAll();
        T GetByID(int id);
        void Add(T obj);
        void Delete(T obj);
        void Update(T obj);
        void Save();
    }
}
