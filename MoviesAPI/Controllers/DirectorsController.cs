using Business.Services.DirectorService;
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
    /// Controller providing all actions for director
    /// </summary>
    [RoutePrefix("api/directors")]
    public class DirectorsController : ApiController
    {
        private readonly DirectorService directorService;
        public DirectorsController()
        {
            directorService = new DirectorService(new DbEntitiesContext());
        }

        public DirectorsController(DbEntitiesContext context)
        {
            directorService = new DirectorService(context);
        }

        /// <summary>
        /// Gets all directors from the database
        /// </summary>
        /// <returns>All directors</returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var allDirectors = directorService.GetAll().Select(d => new DirectorDto(d)).ToList();
            if (allDirectors == null)
                return NotFound();
            return Ok(allDirectors);
        }

        /// <summary>
        /// Gets a single director object from the database
        /// </summary>
        /// <param name="id">The id of the director object</param>
        /// <returns>The found director</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response coed="404">NotFound</response>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest();
            var director = directorService.GetByID((int)id);
            if (director == null)
                return NotFound();
            var dtoDirector = new DirectorDto(director);
            return Ok(dtoDirector);
        }

        /// <summary>
        /// Creates new director
        /// </summary>
        /// <param name="director">The director object to create</param>
        /// <returns>The created director</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] DirectorCreateDto director)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                Director newDirector = new Director
                {
                    FirstName = director.FirstName,
                    LastName = director.LastName,
                    City = director.City
                };

                directorService.Add(newDirector);
                directorService.Save();

                var dtoDirector = new DirectorDto(newDirector);
                return Ok(dtoDirector);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing director
        /// </summary>
        /// <param name="id">The id of the director to be updated</param>
        /// <param name="director">The director object containing the update data</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int? id, [FromBody] DirectorDto director)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (id == null || id <= 0)
                    return BadRequest();
                var dbDirector = directorService.GetByID((int)id);
                if (dbDirector == null)
                    return NotFound();
                dbDirector.FirstName = director.FirstName;
                dbDirector.LastName = director.LastName;
                dbDirector.City = director.City;

                directorService.Update(dbDirector);
                directorService.Save();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a director
        /// </summary>
        /// <param name="id">The id of the director to delete</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (id == null || id <= 0)
                    return BadRequest();
                var director = directorService.GetByID((int)id);
                if (director != null)
                {
                    directorService.Delete(director);
                    directorService.Save();
                    return StatusCode(HttpStatusCode.NoContent);
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
        /// Get a director by name
        /// </summary>
        /// <param name="name">The name of the director</param>
        /// <returns>The found director</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpGet]
        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            var director = directorService.GetDirectorByName(name);
            if (director == null)
                return NotFound();
            var dtoDirector = new DirectorDto(director);
            return Ok(dtoDirector);
        }
    }
}
