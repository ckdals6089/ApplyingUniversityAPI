using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplyingUniversityWebMVC.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ApplyingUniversityWebMVC.Controllers
{
    public class ApplicationController : Controller
    {
        public static HttpClient client = new HttpClient();
        private static IEnumerable<University> university;
        private static IEnumerable<Users> users;

        public ApplicationController()
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://cshin4-eval-prod.apigee.net/apiprojectcomp306/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("apikey", "OAN0dvA7mL2BvKm6EjZvn8kh7kqzl3e9");
            }
        }

        // GET: Application
        public async Task<IActionResult> Index()
        {
            await initDataFromAPIAsync();
            string json;
            HttpResponseMessage response;
            IEnumerable<Application> application;
            try
            {
                response = await client.GetAsync("api/application");

                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                   application = JsonConvert.DeserializeObject<IEnumerable<Application>>(json);
                    return View(application);
                }
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
            return View();
        }

        // GET: Application/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Application/Create
        public IActionResult Create()
        {
            ViewData["University"] = new SelectList(university, "UniversityId", "UniversityName");

            return View();
        }
   
        // POST: Application/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationId, UserId, UniversityId, AppliedDate")]Application application)
        {
            ViewData["UniversityId"] = new SelectList(university, "UniversityId", "UniversityName");
            ViewData["UserId"] = new SelectList(users,"UserId","UserName");
            int userId = (Int32)HttpContext.Session.GetInt32("token");
            application.UserId = userId;
            application.AppliedDate = DateTime.Now;
            HttpResponseMessage response = await client.PostAsJsonAsync("api/application", application);
            return RedirectToAction("Index");
        }

        // GET: Application/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Application application;
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync("api/application/" + id);
                if (response.IsSuccessStatusCode)
                {
                    application = await response.Content.ReadAsAsync<Application>();
                    return View(application);
                }
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
            return View();
        }

        // POST: Application/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationId, UserId, UniversityId, AppliedDate")]Application application)
        {
            HttpResponseMessage response;
            application.ApplicationId = id;
            response = await client.PutAsJsonAsync("api/application/" + application.ApplicationId, application);
            return RedirectToAction("Index");
        }

        // GET: Application/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Application application;
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync("api/application/" + id);
                if (response.IsSuccessStatusCode)
                {
                    application = await response.Content.ReadAsAsync<Application>();
                    return View(application);
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

        // POST: Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            Application application;
            HttpResponseMessage response;
            try
            {
                response = await client.DeleteAsync("api/application/" + id);
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
        private async Task initDataFromAPIAsync()
        {
            string json;
            HttpResponseMessage response;
            response = await client.GetAsync("api/Users");

            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<IEnumerable<Users>>(json);
            }

            response = await client.GetAsync("api/universities");

            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                university = JsonConvert.DeserializeObject<IEnumerable<University>>(json);
            }
           

        }
    }
}