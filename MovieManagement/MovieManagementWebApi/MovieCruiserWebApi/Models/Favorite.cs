using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MovieCruiserWebApi.Models
{
    public class Favorite
    {
        public Favorite()
        {
            this.MovieItems = new HashSet<FavoriteMovie>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }

        public virtual ICollection<FavoriteMovie> MovieItems { get; set; }
        public User User { get; set; }

    }
}
