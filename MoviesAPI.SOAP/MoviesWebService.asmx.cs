using Business.Services.MovieService;
using DataAccess;
using DatabaseStructure.Entities;
using MoviesAPI.SOAP.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MoviesAPI.SOAP
{
    /// <summary>
    /// Summary description for MoviesWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class MoviesWebService : System.Web.Services.WebService
    {
        private readonly MovieService movieService;

        public MoviesWebService()
        {
            movieService = new MovieService();
        }
        public MoviesWebService(DbEntitiesContext context)
        {
            movieService = new MovieService(context);
        }


        [WebMethod]
        public List<MovieDto> GetAllMovies()
        {
            var movies = movieService.GetAll().Select(m => new MovieDto(m)).ToList();
            return movies;
        }

        [WebMethod]
        public MovieDto GetMovieByID(int id)
        {
            if (id <= 0)
                throw new Exception("Paramater id can't be of negative value");
            var dbMovie = movieService.GetByID(id);
            if (dbMovie == null)
                throw new Exception($"Movie with id: {id} not found.");
            var movie = new MovieDto(dbMovie);
            return movie;
        }

        [WebMethod]
        public string AddMovie(MovieCreateDto movie)
        {
            var newMovie = new Movie
            {
                DirectorID = movie.DirectorID,
                GenreID = movie.GenreID,
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate
            };
            movieService.Add(newMovie);
            movieService.Save();

            return "Book Created!";
        }

        [WebMethod]
        public string EditMovie(MovieDto movie)
        {
            if (movie.ID <= 0)
                return "Id can't be of negative value!";
            var dbMovie = movieService.GetByID(movie.ID);
            if (dbMovie == null)
                return $"Movie with id: {movie.ID} not found!";
            dbMovie.DirectorID = movie.DirectorID;
            dbMovie.GenreID = movie.GenreID;
            dbMovie.Title = movie.Title;
            dbMovie.Description = movie.Description;
            dbMovie.ReleaseDate = movie.ReleaseDate;

            movieService.Update(dbMovie);
            movieService.Save();
            return "Movie updated successfuly!";
        }

        [WebMethod]
        public string DeleteMovie(int id)
        {
            if (id <= 0)
                return "Id can't be of negative value!";
            var movie = movieService.GetByID(id);
            if(movie == null)
                return $"Movie with id: {id} not found!";
            movieService.Delete(movie);
            movieService.Save();

            return "Movie deleted successfuly";
        }
    }
}
