using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCruiserWebApi.Models
{
    public class FavoriteMovie
    {
        public int MovieItemId { get; set; }
        public int FavoriteId { get; set; }
        public virtual MovieItem MovieItem { get; set; }
    }
}
