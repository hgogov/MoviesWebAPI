using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.DirectorRepository
{
    public class DirectorRepository : GenericRepository<Director>
    {
        private readonly DbEntitiesContext context;

        public DirectorRepository()
            :this(new DbEntitiesContext())
        {
        }

        public DirectorRepository(DbEntitiesContext context)
            : base(context)
        {
            this.context = context;
        }

        public Director GetDirectorByName(string name)
        {
            return context.Directors.
                Where(d => name.Contains(d.FirstName) && name.Contains(d.LastName))
                .FirstOrDefault();
        }
    }
}
