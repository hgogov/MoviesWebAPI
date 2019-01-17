using Business.Services.GenreService;
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
    /// Controller providing all actions for genre
    /// </summary>
    [RoutePrefix("api/genres")]
    public class GenreController : ApiController
    {
        private readonly GenreService genreService;
        public GenreController()
        {
            genreService = new GenreService();
        }

        public GenreController(DbEntitiesContext context)
        {
            genreService = new GenreService(context);
        }

        /// <summary>
        /// Gets all genres from the database
        /// </summary>
        /// <returns>All genres</returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpGet]
        [Route]
        public IHttpActionResult Get()
        {
            var genres = genreService.GetAll().Select(g => new GenreDto(g)).ToList();
            if (genres == null)
                return NotFound();
            return Ok(genres);
        }

        /// <summary>
        /// Gets a single genre object from the database
        /// </summary>
        /// <param name="id">The id of the genre object</param>
        /// <returns>The found genre</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response coed="404">NotFound</response>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int? id)
        {
            if (id <= 0 || id == null)
                return BadRequest();
            var genre = genreService.GetByID((int)id);
            if (genre == null)
                return NotFound();
            var genreDto = new GenreDto(genre);
            return Ok(genreDto);
        }


        /// <summary>
        /// Get a genre by name
        /// </summary>
        /// <param name="name">The name of the genre</param>
        /// <returns>The found genre</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpGet]
        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            var genre = genreService.GetGenreByName(name);
            if (genre == null)
                return NotFound();
            var dtoGenre = new GenreDto(genre);
            return Ok(dtoGenre);
        }

        /// <summary>
        /// Creates new genre
        /// </summary>
        /// <param name="genre">The genre object to create</param>
        /// <returns>The created genre</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        [HttpPost]
        [Route]
        public IHttpActionResult Post([FromBody] GenreCreateDto genre)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                Genre newGenre = new Genre()
                {
                    Name = genre.Name
                };

                genreService.Add(newGenre);
                genreService.Save();

                var dtoGenre = new GenreDto(newGenre);
                return Ok(dtoGenre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing genre
        /// </summary>
        /// <param name="id">The id of the genre to be updated</param>
        /// <param name="genre">The genre object containing the update data</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(int? id, [FromBody] GenreDto genre)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (id == null || id <= 0)
                    return NotFound();
                var dbGenre = genreService.GetByID((int)id);
                dbGenre.Name = genre.Name;

                genreService.Update(dbGenre);
                genreService.Save();

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a genre
        /// </summary>
        /// <param name="id">The id of the genre to delete</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int? id)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (id == null || id <= 0)
                    return NotFound();
                var genre = genreService.GetByID((int)id);
                if (genre != null)
                {
                    genreService.Delete(genre);
                    genreService.Save();
                    return Content(HttpStatusCode.NoContent, genre);
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
    }
}
