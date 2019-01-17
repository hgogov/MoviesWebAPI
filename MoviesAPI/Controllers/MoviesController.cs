using Business.Services.MovieService;
using DataAccess;
using DatabaseStructure.Entities;
using MoviesAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoviesAPI.Controllers
{
    /// <summary>
    /// Controller providing all actions for movie
    /// </summary>
    [RoutePrefix("api/movies")]
    public class MoviesController : ApiController
    {
        private readonly MovieService movieService;

        public MoviesController()
        {
            movieService = new MovieService();
        }

        public MoviesController(DbEntitiesContext context)
        {
            movieService = new MovieService(context);
        }

        /// <summary>
        /// Gets all movies from the database
        /// </summary>
        /// <returns>All movies</returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var allMovies = movieService.GetAll().Select(m => new MovieDto(m)).ToList();
            if (allMovies == null)
                return NotFound();
            return Ok(allMovies);
        }

        /// <summary>
        /// Gets a single movie object from the database
        /// </summary>
        /// <param name="id">The id of the movie object</param>
        /// <returns>The found movie</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response coed="404">NotFound</response>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest();
            var movie = movieService.GetByID((int)id);
            if (movie == null)
                return NotFound();
            var dtoMovie = new MovieDto(movie);
            return Ok(dtoMovie);
        }

        /// <summary>
        /// Creates new movie
        /// </summary>
        /// <param name="movie">The movie object to create</param>
        /// <returns>The created movie</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]MovieCreateDto movie)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                Movie newMovie = new Movie
                {
                    DirectorID = movie.DirectorID,
                    Title = movie.Title,
                    GenreID = movie.GenreID,
                    Description = movie.Description,
                    ReleaseDate = movie.ReleaseDate
                };

                movieService.Add(newMovie);
                movieService.Save();

                var dtoMovie = new MovieDto(newMovie);
                return Ok(dtoMovie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing movie
        /// </summary>
        /// <param name="id">The id of the movie to be updated</param>
        /// <param name="movie">The movie object containing the update data</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int? id, [FromBody]MovieDto movie)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (id == null || id <= 0)
                    return BadRequest();
                var dbMovie = movieService.GetByID((int)id);
                if (dbMovie == null)
                    return NotFound();
                dbMovie.DirectorID = movie.DirectorID;
                dbMovie.Title = movie.Title;
                dbMovie.GenreID = movie.GenreID;
                dbMovie.Description = movie.Description;
                dbMovie.ReleaseDate = movie.ReleaseDate;
                movieService.Update(dbMovie);
                movieService.Save();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a movie
        /// </summary>
        /// <param name="id">The id of the movie to delete</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();
                var movie = movieService.GetByID(id);
                if (movie != null)
                {
                    movieService.Delete(movie);
                    movieService.Save();
                    return Content(HttpStatusCode.NoContent, movie);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get a movie by title
        /// </summary>
        /// <param name="title">The title of the movie</param>
        /// <returns>The found movie</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpGet]
        [Route("{title}")]
        public IHttpActionResult Get(string title)
        {
            if (string.IsNullOrEmpty(title))
                return BadRequest();
            var movie = movieService.GetMovieByTitle(title);
            if (movie == null)
                return NotFound();
            var dtoMovie = new MovieDto(movie);
            return Ok(dtoMovie);
        }

        /// <summary>
        /// Gets a movie by the id of a director
        /// </summary>
        /// <param name="id">The id of the director</param>
        /// <returns>The found movie</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpGet]
        [Route("~/api/directors/{id:int}/movies")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
                return BadRequest();
            var movies = movieService.GetAllMoviesForDirector(id)
                .Select(m => new MovieDto(m)).ToList();
            if (movies.Count == 0)
                return NotFound();
            return Ok(movies);
        }
    }
}
