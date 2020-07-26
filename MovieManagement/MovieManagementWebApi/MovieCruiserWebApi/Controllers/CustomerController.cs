using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCruiserWebApi.Models;

namespace MovieCruiserWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly MovieContext _context;

        public CustomerController(MovieContext context)
        {
            _context = context;
        }
        [HttpGet("Genres")]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<MovieItem>>> GetFavorites(int id)
        {

            IEnumerable<FavoriteMovie> favorites = new List<FavoriteMovie>();
            List<MovieItem> favmovies = new List<MovieItem>();
            List<FavoriteMovie> list = new List<FavoriteMovie>();
            try
            {
                User user= new User();
                await Task.Run(() =>
                {
                    user = _context.Users.FirstOrDefault(u => u.Id == id);
                    if (user.FavId == 0)
                    {
                        Task<ActionResult<User>> action = PostFavorite(user);
                    }
                });
                await Task.Run(() =>
                {
                    list = _context.FavoriteMovies.Where(f => f.FavoriteId == user.FavId).ToList();
                    
                });
                await Task.Run(() => { 
                    foreach (var item in list)
                    {
                        favmovies.Add(_context.MovieItems.FirstOrDefault(m => m.Id == item.MovieItemId));
                    }
                        
                });

                return favmovies.ToList();
            }
            catch(Exception e)
            {
                return NoContent();
            }
        }

        // GET: api/Customer/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieItem>>> GetMovies()
        {
            List<MovieItem> movieItems = new List<MovieItem>();
            List<MovieItem> movieItemsToSend = new List<MovieItem>();
            await Task.Run(() =>
            {
                movieItems = _context.MovieItems.ToList();
                foreach (var item in movieItems)
                {
                    if (item.Active == false || item.DateOfLaunch > DateTime.Now)
                    {
                  //      movieItems.Remove(item);
                    }
                    else
                    {
                        movieItemsToSend.Add(item);
                    }
                }
            });
            return movieItemsToSend;
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, eable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavorite(int id,User user)
        {
            User _user =await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            await Task.Run(() =>
            {
                if (_user.FavId == 0)
                {
                    Task<ActionResult<User>> action = PostFavorite(_user);
                }
            });
            FavoriteMovie fm = new FavoriteMovie();
           await Task.Run(() =>
            {
                fm = new FavoriteMovie() { FavoriteId = _user.FavId, MovieItemId = id };
            });
            await Task.Run(() =>
            {
                _context.FavoriteMovies.Add(fm);
            });
            //_context.Entry(favorite).State = EntityState.Modified;
            // _context.Entry(fm).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            try
            {
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customer
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostFavorite(User user)
        {
            user.FavId = await _context.Users.MaxAsync(f => f.FavId)+1;
            await Task.Run(() => _context.Entry(user).State = EntityState.Modified);
            await _context.SaveChangesAsync();
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        }

        // DELETE: api/Customer/5
        [HttpPost("{id}")]
        public async Task<ActionResult<MovieItem>> DeleteFavorite(int id, User user)
        {
            MovieItem favorite = new MovieItem();
            FavoriteMovie fm = new FavoriteMovie();
            User _user = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            await Task.Run(() =>
            {
                fm = new FavoriteMovie() { FavoriteId = _user.FavId, MovieItemId = id };
            });
           
            favorite =await _context.MovieItems.FirstOrDefaultAsync(m => m.Id == id);
            await Task.Run(() =>
            {
               
                _context.FavoriteMovies.Remove(fm);

            });
             if (favorite == null)
            {
                return NotFound();
            }
           // _context.Favorites.FindAsync(1).Result.MovieItems.Remove(favorite);
            await _context.SaveChangesAsync();

            return favorite;
        }

        private bool FavoriteExists(int id)
        {
            return _context.MovieItems.Any(e => e.Id == id);
        }
    }
}
