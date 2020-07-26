using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using MovieCruiserApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MovieCruiserApp.Controllers
{
    public class AnonymousController : Controller
    {
        public async Task<IActionResult> Index()
        {

                List<MovieItem> ItemList = new List<MovieItem>();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);


                    using (var response = await client.GetAsync("https://localhost:44395/api/anonymoususer/"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ItemList = JsonConvert.DeserializeObject<List<MovieItem>>(apiResponse);
                    }
                using (var response = await client.GetAsync("https://localhost:44395/api/anonymoususer/Genres"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Genres = JsonConvert.DeserializeObject<List<Genre>>(apiResponse).ToList();
                }
            }
                return View(ItemList);
            
        }
    }
}
