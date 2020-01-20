using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApplyingUniversityWeb.Models;
using ApplyingUniversityWeb.Services;

namespace ApplyingUniversityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private readonly IUniversityRepository _university;

        public UniversitiesController(IUniversityRepository universityRepositoty)
        {
            _university = universityRepositoty;
        }

        // GET: api/Universities
        [HttpGet]
        public async Task<IEnumerable<University>> GetUniversity()
        {
            IEnumerable<University> university = await _university.GetAllAsyncUniversity();
            return university.ToList();
        }

        // GET: api/Universities/5
        [HttpGet("{id}")]
        public IActionResult FindAsyncUniversity(int id)
        {
            var university =  _university.FindAsyncUniversity(id);

            if (university == null)
            {
                return NotFound();
            }

            return new ObjectResult(university);
        }

    }
}
