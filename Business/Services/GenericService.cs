using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class GenericService<T, E> : IGenericService<T, E>
        where T : class
        where E : IGenericRepository<T>
    {
        private readonly GenericRepository<T> repository;

        public GenericService()
        {
            repository = new GenericRepository<T>(new DbEntitiesContext());
        }

        public GenericService(DbEntitiesContext context)
        {
            repository = new GenericRepository<T>(context);
        }

        public GenericService(GenericRepository<T> _repository)
        {
            repository = _repository;
        }

        public List<T> GetAll()
        {
            return repository.GetAll();
        }

        public T GetByID(int id)
        {
            return repository.GetByID(id);
        }

        public void Add(T obj)
        {
            repository.Add(obj);
        }

        public void Delete(T obj)
        {
            repository.Delete(obj);
        }

        public void Update(T obj)
        {
            repository.Update(obj);
        }

        public void Save()
        {
            repository.Save();
        }
    }
}
