using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.MovieRepository
{
    public class MovieRepository : GenericRepository<Movie>
    {
        private readonly DbEntitiesContext context;

        public MovieRepository()
            : this(new DbEntitiesContext())
        {
        }

        public MovieRepository(DbEntitiesContext context)
            : base(context)
        {
            this.context = context;
        }

        public Movie GetMovieByTitle(string title)
        {
            return context.Movies
                .Where(m => m.Title.Equals(title))
                .FirstOrDefault();
        }

        public List<Movie> GetAllMoviesForDirector(int id)
        {
            return context.Movies
                .Where(m => m.DirectorID.Equals(id))
                .OrderBy(m => m.Title)
                .ToList();
        }
    }
}
