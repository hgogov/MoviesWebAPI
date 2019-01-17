using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        T GetByID(int id);
        void Add(T obj);
        void Delete(T obj);
        void Update(T obj);
        void Save();
    }
}
