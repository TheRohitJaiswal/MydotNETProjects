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
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly MovieContext _context;

        public AdminController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Admin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieItem>>> GetMovieItems()
        {
            return await _context.MovieItems.ToListAsync();
        }
        [HttpGet("Genres")]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        // GET: api/Admin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieItem>> GetMovieItem(int id)
        {
            var movieItem = await _context.MovieItems.FindAsync(id);

            if (movieItem == null)
            {
                return NotFound();
            }

            return movieItem;
        }

        // PUT: api/Admin/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieItem(int id, MovieItem movieItem)
        {
            if (id != movieItem.Id)
            {
                return BadRequest();
            }
            await Task.Run(() =>
            {
                _context.Entry(movieItem).State = EntityState.Modified;
            });
            await _context.SaveChangesAsync();
            try
            {
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieItemExists(id))
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

        // POST: api/Admin
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MovieItem>> PostMovieItem(MovieItem movieItem)
        {
            await Task.Run(() =>
            {
                movieItem.Id = _context.MovieItems.Max(m => m.Id) + 1;
            });
            await Task.Run(() =>
            {
                _context.MovieItems.Add(movieItem);
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovieItem", new { id = movieItem.Id }, movieItem);
        }

        // DELETE: api/Admin/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieItem>> DeleteMovieItem(int id)
        {
            var movieItem = await _context.MovieItems.FindAsync(id);
            if (movieItem == null)
            {
                return NotFound();
            }
            await Task.Run(() =>
            {
                _context.MovieItems.Remove(movieItem);
            });
            await _context.SaveChangesAsync();

            return movieItem;
        }

        private bool MovieItemExists(int id)
        {
            return _context.MovieItems.Any(e => e.Id == id);
        }
    }
}
