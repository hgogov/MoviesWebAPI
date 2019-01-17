using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.MovieRepository;
using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.MovieService
{
    public class MovieService : GenericService<Movie, IGenericRepository<Movie>>
    {
        private readonly MovieRepository movieRepository;

        public MovieService()
            : base()
        {
            movieRepository = new MovieRepository();
        }

        public MovieService(DbEntitiesContext context)
        {
            movieRepository = new MovieRepository(context);
        }


        public Movie GetMovieByTitle(string title)
        {
            return movieRepository.GetMovieByTitle(title);
        }

        public List<Movie> GetAllMoviesForDirector(int id)
        {
            return movieRepository.GetAllMoviesForDirector(id);
        }
    }
}
