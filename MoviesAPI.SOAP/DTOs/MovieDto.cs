using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesAPI.SOAP.DTOs
{
    public class MovieDto
    {
        public int ID { get; set; }

        public int DirectorID { get; set; }

        public int GenreID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public MovieDto()
        {
        }

        public MovieDto(Movie movie)
        {
            this.ID = movie.ID;
            this.DirectorID = movie.DirectorID;
            this.GenreID = movie.GenreID;
            this.Title = movie.Title;
            this.Description = movie.Description;
            this.ReleaseDate = movie.ReleaseDate;
        }
    }
}