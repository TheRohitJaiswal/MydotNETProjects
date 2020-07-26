using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCruiserWebApi.Models;
using Microsoft.EntityFrameworkCore;


namespace MovieCruiserWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AnonymousUserController : ControllerBase
    {
        private readonly MovieContext _context;


        public AnonymousUserController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet("Genres")]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }

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
    }
}
