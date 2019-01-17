using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.GenreRepository;
using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.GenreService
{
    public class GenreService : GenericService<Genre, IGenericRepository<Genre>>
    {
        private readonly GenreRepository genreRepository;
        public GenreService()
            :base()
        {
            genreRepository = new GenreRepository();
        }

        public GenreService(DbEntitiesContext context)
        {
            genreRepository = new GenreRepository(context);
        }

        public Genre GetGenreByName(string name)
        {
            return genreRepository.GetGenreByName(name);
        }
    }
}
