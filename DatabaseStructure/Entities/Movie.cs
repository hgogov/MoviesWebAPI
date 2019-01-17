using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseStructure.Entities
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int DirectorID { get; set; }

        public int GenreID { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Title { get; set; }

        [MaxLength(2048)]
        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        [ForeignKey("DirectorID")]
        public virtual Director Director { get; set; }

        [ForeignKey("GenreID")]
        public virtual Genre Genre { get; set; }
    }
}
