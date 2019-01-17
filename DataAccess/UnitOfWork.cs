using DataAccess.Repositories.DirectorRepository;
using DataAccess.Repositories.MovieRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private readonly DbEntitiesContext context;

        private MovieRepository movieRepository;
        private DirectorRepository directorRepository;

        private bool disposed = false;

        public UnitOfWork()
            :this(new DbEntitiesContext())
        {
        }

        public UnitOfWork(DbEntitiesContext context)
        {
            this.context = context;
        }

        public MovieRepository MovieRepository
        {
            get
            {
                if(movieRepository == null)
                {
                    movieRepository = new MovieRepository(context);
                }
                return movieRepository;
            }
        }

        public DirectorRepository DirectorRepository
        {
            get
            {
                if(directorRepository == null)
                {
                    directorRepository = new DirectorRepository(context);
                }
                return directorRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
