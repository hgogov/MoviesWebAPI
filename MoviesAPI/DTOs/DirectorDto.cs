using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesAPI.DTOs
{
    public class DirectorDto
    {
        public int ID { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public DirectorDto()
        {
        }

        public DirectorDto(Director director)
        {
            this.ID = director.ID;
            this.FirstName = director.FirstName;
            this.LastName = director.LastName;
            this.City = director.City;
        }
    }
}