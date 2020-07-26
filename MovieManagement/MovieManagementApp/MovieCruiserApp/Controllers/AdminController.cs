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
    public class AdminController : Controller
    {
        // GET: AdminController
        public async Task<IActionResult> Index()
        {
            //HttpContext.Session.Remove("token");
            //ViewBag.Message = "User logged out successfully!";
            //return View("Index");

            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                List<MovieItem> ItemList = new List<MovieItem>();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44395/api/admin/"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ItemList = JsonConvert.DeserializeObject<List<MovieItem>>(apiResponse);
                    }
                    using (var response = await client.GetAsync("https://localhost:44395/api/admin/Genres"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Genres = JsonConvert.DeserializeObject<List<Genre>>(apiResponse).ToList();
                    }
                }
                return View(ItemList);
            }
        }

        //[HttpGet]
        //public ActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(User user)
        //{
        //    User Item = new User();
        //    using (var httpClient = new HttpClient())
        //    {
        //        StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
        //        var response = await httpClient.PostAsync("https://localhost:44395/api/user/Users", content);
        //        string apiResponse = await response.Content.ReadAsStringAsync();
        //        Item = JsonConvert.DeserializeObject<User>(apiResponse);

        //        StringContent content1 = new StringContent(JsonConvert.SerializeObject(Item), Encoding.UTF8, "application/json");
        //        using (var response1 = await httpClient.PostAsync("https://localhost:44395/api/auth/", content1))
        //        {
        //            string apiResponse1 = await response1.Content.ReadAsStringAsync();
        //            //usr = JsonConvert.DeserializeObject<UserModel>(apiResponse);

        //            //string stringData = JsonConvert.SerializeObject(usr);
        //            //var contentData = new StringContent(stringData,System.Text.Encoding.UTF8, "application/json");
        //            string stringJWT = response1.Content.ReadAsStringAsync().Result;


        //            JWT jwt = JsonConvert.DeserializeObject<JWT>(stringJWT);

        //            HttpContext.Session.SetString("token", jwt.Token);

        //            ViewBag.Message = "User logged in successfully!";
        //            return RedirectToAction("Index");

        //        }

        //    }

        //    //    HttpResponseMessage response = client.PostAsync
        //    //("/api/security", contentData).Result;
        //}


        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                 //   StringContent content = new StringContent(JsonConvert.SerializeObject(movieItem), Encoding.UTF8, "application/json");

                    using (var response = await client.GetAsync("https://localhost:44395/api/admin/Genres"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Genres = JsonConvert.DeserializeObject<List<Genre>>(apiResponse).ToList();
                        //var obj = JsonConvert.DeserializeObject<List<Genre>>(apiResponse);       //No Response
                        //List<SelectListItem> list = new List<SelectListItem>();
                        //foreach (var item in obj)
                        //{
                        //    list.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                        //}
                        //ViewBag.Genres = new SelectList(list,"");
                    }

                }
                return View();
            }
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieItem movieItem)
        {
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                MovieItem Item = new MovieItem();
               
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    StringContent content = new StringContent(JsonConvert.SerializeObject(movieItem), Encoding.UTF8, "application/json");

                    using (var response = await client.PostAsync("https://localhost:44395/api/admin/", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Item = JsonConvert.DeserializeObject<MovieItem>(apiResponse);       //No Response
                    }
                    
                }
                return RedirectToAction("Index");
            }
        }

      
      

        // GET: AdminController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                MovieItem Item = new MovieItem();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44395/api/admin/"+id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Item = JsonConvert.DeserializeObject<MovieItem>(apiResponse);
                    }
                }
                return View(Item);
            }
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieItem movieItem)
        {
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                MovieItem Item = new MovieItem();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    StringContent content = new StringContent(JsonConvert.SerializeObject(movieItem), Encoding.UTF8, "application/json");

                    using (var response = await client.PutAsync("https://localhost:44395/api/admin/"+id,content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Item = JsonConvert.DeserializeObject<MovieItem>(apiResponse);       //No Response
                    }
                }
                return RedirectToAction("Index");
            }
        }
        // GET: AdminController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                MovieItem Item = new MovieItem();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    //    StringContent content = new StringContent(JsonConvert.SerializeObject(movieItem), Encoding.UTF8, "application/json");

                    using (var response = await client.GetAsync("https://localhost:44395/api/admin/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Item = JsonConvert.DeserializeObject<MovieItem>(apiResponse);       //No Response
                    }

                }
                return View(Item);
            }
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                MovieItem Item = new MovieItem();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                //    StringContent content = new StringContent(JsonConvert.SerializeObject(movieItem), Encoding.UTF8, "application/json");

                    using (var response = await client.DeleteAsync("https://localhost:44395/api/admin/"+id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Item = JsonConvert.DeserializeObject<MovieItem>(apiResponse);
                    }
                             //No Response
                }
                return RedirectToAction("Index");
            }
        }
    }
}
