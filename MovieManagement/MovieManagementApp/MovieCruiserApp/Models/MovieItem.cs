using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MovieCruiserApp.Models
{
    public class MovieItem
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Display(Name = "Box Ofiice")]
        public bool BoxOffice { get; set; }

        [Display(Name = "Has Teaser")]
        public bool HasTeaser { get; set; }

        [Display(Name = "Is Favorite")]
        public bool IsFavorite { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfLaunch { get; set; }

        public bool Active { get; set; }
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
      
    }
}
