using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using MovieCruiserApp.Models;
using Microsoft.AspNetCore.Http;

namespace MovieCruiserApp.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            User Item = new User();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://localhost:44395/api/user/Users", content);
                string apiResponse = await response.Content.ReadAsStringAsync();
                Item = JsonConvert.DeserializeObject<User>(apiResponse);

                StringContent content1 = new StringContent(JsonConvert.SerializeObject(Item), Encoding.UTF8, "application/json");
                using (var response1 = await httpClient.PostAsync("https://localhost:44395/api/auth/", content1))
                {
                    string apiResponse1 = await response1.Content.ReadAsStringAsync();
                    //usr = JsonConvert.DeserializeObject<UserModel>(apiResponse);

                    //string stringData = JsonConvert.SerializeObject(usr);
                    //var contentData = new StringContent(stringData,System.Text.Encoding.UTF8, "application/json");
                    string stringJWT = response1.Content.ReadAsStringAsync().Result;


                    JWT jwt = JsonConvert.DeserializeObject<JWT>(stringJWT);

                    HttpContext.Session.SetString("token", jwt.Token);
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(Item));
                    ViewBag.Message = "User logged in successfully!";
                    ViewBag.User = Item;
                    if (Item.UserId == 1)
                        return RedirectToAction("Index", "Admin");
                    else
                        return RedirectToAction("Index", "Customer");

                }

            }

            //    HttpResponseMessage response = client.PostAsync
            //("/api/security", contentData).Result;
        }
        public ActionResult Logout()
        {
            //HttpContext.Session.SetString("token", null);
            //HttpContext.Session.SetString("user", null);
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("user");

            return View("Login");
        }
    }
}
