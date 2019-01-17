using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesAPI.DTOs
{
    public class GenreDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public GenreDto(Genre genre)
        {
            ID = genre.ID;
            Name = genre.Name;
        }
    }
}