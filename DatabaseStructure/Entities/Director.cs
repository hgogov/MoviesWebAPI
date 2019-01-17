using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseStructure.Entities
{
    public class Director
    {
        public Director()
        {
            this.Movies = new HashSet<Movie>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string City { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
