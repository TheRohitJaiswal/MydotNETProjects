using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace MovieCruiserWebApi.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options):base(options)
        {

        }
        public virtual DbSet<MovieItem> MovieItems { get; set; }
       // public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<FavoriteMovie> FavoriteMovies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<FavoriteMovie>()
                  .HasKey(x => new { x.FavoriteId, x.MovieItemId });

            //modelBuilder.Entity<FavoriteMovie>()
            //    .HasOne(x => x.Favorite)
            //    .WithMany(y => y.MovieItems)
            //    .HasForeignKey(y => y.MovieItemId);

            //modelBuilder.Entity<FavoriteMovie>()
            //    .HasOne(x => x.MovieItem)
            //    .WithMany(y => y.Favorites)
            //    .HasForeignKey(y => y.FavoriteId);

        }

    }
}
