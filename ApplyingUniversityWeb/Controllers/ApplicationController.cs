using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplyingUniversityWeb.Services;
using ApplyingUniversityWeb.Models;

namespace ApplyingUniversityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {

        private readonly IUniversityRepository _university;

        public ApplicationController(IUniversityRepository universityRepositoty)
        {
            _university = universityRepositoty;
        }

        // GET: api/Application
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Application>), 200)]
        public async Task<IEnumerable<Application>>GetAllAsyncApplication()
        {
            IEnumerable<Application> application = await _university.GetAllAsyncApplication();
            return application.ToList();
        }

        // GET: api/Application/5
        [HttpGet("{id}", Name = nameof(FindApplication))]
        [ProducesResponseType(typeof(Application), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> FindApplication(int id)
        {
            //Application application = _university.FindAsyncApplication(id);
            //if (application == null)
            //{
            //    return NotFound();
            //}
            //else
            //{
            //    return new ObjectResult(application);
            //}
            Application application = await _university.FindAsyncApplication(id);
            if (application == null)
            {
                return NotFound();
            }
            return new ObjectResult(application);
        }
        // POST: api/Application -create
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Application application)
        {
            if (User == null)
            {
                return BadRequest();
            }
            await _university.AddAsyncApplication(application);
            return CreatedAtRoute(nameof(FindApplication), new { id = application.ApplicationId }, application);
        }

        // PUT: api/Application/5 -update
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(int id, [FromBody] Application application)
        {

            if (application == null || id != application.ApplicationId)
            {
                return BadRequest();
            }
            if (await _university.FindAsyncApplication(id) == null)
            {
                return NotFound();
            }
            await _university.UpdateAsyncApplication(application);
            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id) => await _university.RemoveAsyncApplication(id);

    }
}
