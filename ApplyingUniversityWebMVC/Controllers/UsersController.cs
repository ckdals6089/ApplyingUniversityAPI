using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplyingUniversityWebMVC.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace ApplyingUniversityWebMVC.Controllers
{
    public class UsersController : Controller
    {
        public static HttpClient client = new HttpClient();

        public UsersController()
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://cshin4-eval-prod.apigee.net/apiprojectcomp306/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("apikey", "OAN0dvA7mL2BvKm6EjZvn8kh7kqzl3e9");
            }

        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            string json;
            HttpResponseMessage response;
            IEnumerable<Users> users;
            try
            {
                response = await client.GetAsync("api/users");

                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<IEnumerable<Users>>(json);
                    return View(users);
                }
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
            return View();
        }
        //GET Users/Login
        public IActionResult Login()
        {
            return View();
        }

        //GET Users/Login
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();

            return View("Login");
        }
        //POST Uers/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync([Bind("Username, Password")]Users user)
        {
            string json;
            HttpResponseMessage response;
            if (ModelState.IsValid)
            {
                response = await client.GetAsync("api/users/find/" + user.Username);

                if (response != null)
                {
                    json = await response.Content.ReadAsStringAsync();
                    Users loginuser = JsonConvert.DeserializeObject<Users>(json);
                    if (loginuser.Password.Equals(user.Password))
                    {
                        HttpContext.Session.SetInt32("token", loginuser.UserId);
                        HttpContext.Session.SetString("user", loginuser.Username);
                        HttpContext.Session.SetString("userRole", loginuser.Roll);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ViewData["Message"] = "Username or Password doesn't exist";
            return View();
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId, Username, Password, Roll, FirstName, LastName, Email")]Users users)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/users", users);
            return RedirectToAction("Index");
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Users users;
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync("api/users/" + id);
                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content.ReadAsAsync<Users>();
                    return View(users);
                }
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId, Username, Password, Roll, FirstName, LastName, Email")]Users users)
        {
            //HttpResponseMessage response;
            //response = await client.PutAsJsonAsync("api/users/" + id, users);
            //return RedirectToAction("Details", new { id });

            HttpResponseMessage response;
            users.UserId = id;
            response = await client.PutAsJsonAsync("api/users/" + users.UserId, users);
            //response.EnsureSuccessStatusCode();
            // Deserialize the updated product from the response body.
            return RedirectToAction("Index");
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Users users;
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync("api/users/" + id);
                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content.ReadAsAsync<Users>();
                    return View(users);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            Users users;
            HttpResponseMessage response;
            try
            {
                response = await client.DeleteAsync("api/users/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                return NotFound();
            }
            return View();
        }
    }
}