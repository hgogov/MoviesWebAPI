using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesAPI.SOAP.DTOs
{
    public class MovieCreateDto
    {
        public int DirectorID { get; set; }

        public int GenreID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}