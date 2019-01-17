using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.GenreRepository
{
    public class GenreRepository : GenericRepository<Genre>
    {
        private readonly DbEntitiesContext context;

        public GenreRepository()
            : this(new DbEntitiesContext())
        {
        }

        public GenreRepository(DbEntitiesContext context)
            : base(context)
        {
            this.context = context;
        }

        public Genre GetGenreByName(string name)
        {
            return context.Genres
                 .Where(g => g.Name.ToLower().Equals(name))
                 .FirstOrDefault();
        }
    }
}
