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
    public class SignupController : Controller
    {
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(User user)
        {
            using (var httpClient = new HttpClient())
            {


                StringContent content1 = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response1 = await httpClient.PostAsync("https://localhost:44395/api/User/", content1))
                {
                    string apiResponse1 = await response1.Content.ReadAsStringAsync();
                    //usr = JsonConvert.DeserializeObject<UserModel>(apiResponse);

                    //string stringData = JsonConvert.SerializeObject(usr);
                    //var contentData = new StringContent(stringData,System.Text.Encoding.UTF8, "application/json");
                    string stringJWT = response1.Content.ReadAsStringAsync().Result;

                }
            }
            return RedirectToAction("Login", "Login");
        }
    }
}
