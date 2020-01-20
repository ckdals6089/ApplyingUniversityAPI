using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplyingUniversityWebMVC.Models;
using Newtonsoft.Json;

namespace ApplyingUniversityWebMVC.Controllers
{
    public class UniversityController : Controller
    {
        public static HttpClient client = new HttpClient();
        private static IEnumerable<University> university;
        private static IEnumerable<Application> application;

        public UniversityController()
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://cshin4-eval-prod.apigee.net/apiprojectcomp306/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("apikey", "OAN0dvA7mL2BvKm6EjZvn8kh7kqzl3e9");
            }

        }
        // GET: University
        public async Task<IActionResult> Index()
        {
            string json;
            HttpResponseMessage response;
            IEnumerable<University> universities;
            try
            {
                response = await client.GetAsync("api/universities");

                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    universities = JsonConvert.DeserializeObject<IEnumerable<University>>(json);
                    return View(universities);
                }
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
            return View();
        }

        // GET: University/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: University/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: University/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: University/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: University/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: University/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: University/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}