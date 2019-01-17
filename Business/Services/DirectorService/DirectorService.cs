using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.DirectorRepository;
using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.DirectorService
{
    public class DirectorService : GenericService<Director, IGenericRepository<Director>>
    {
        private readonly DirectorRepository directorRepository;

        public DirectorService()
            : base(new DbEntitiesContext())
        {
            directorRepository = new DirectorRepository();
        }

        public DirectorService(DbEntitiesContext context)
        {
            directorRepository = new DirectorRepository(context);
        }

        public Director GetDirectorByName(string name)
        {
            return directorRepository.GetDirectorByName(name);
        }
    }
}
