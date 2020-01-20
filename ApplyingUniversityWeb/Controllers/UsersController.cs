using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApplyingUniversityWeb.Models;
using ApplyingUniversityWeb.Services;
using AutoMapper;

namespace ApplyingUniversityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUniversityRepository _university;
        private readonly IMapper _mapper;

        public UsersController(IUniversityRepository universityRepositoty, IMapper mapper)
        {
            _university = universityRepositoty;
            _mapper = mapper;
        }
        // GET:api/Users/username
        [HttpGet("find/{Username}", Name = nameof(GetUserByNameAsync))]
        public async Task<IActionResult> GetUserByNameAsync(string Username)
        {
            Users user = await _university.FindAsyncUsername(Username);
            if (user == null)
            {
                return NotFound();
            }   
            return new ObjectResult(user);
        }
        // GET: api/Users/5
        [HttpGet("{id}", Name = nameof(GetUserByIdAsync))]
        [ProducesResponseType(typeof(Users), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            Users user = await _university.FindAsyncUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }
        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Users>), 200)]
        public async Task<IEnumerable<Users>> GetAllAsyncUser()
        {
            var user = await _university.GetAllAsyncUser();
            var result =  _mapper.Map<IEnumerable<Users>>(user);
            return result.ToList();
        }


        // POST: api/Users
        [HttpPost]
        [ProducesResponseType(typeof(Users), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostUserAsync([FromBody]Users users)
        {
            if (User == null)
            {
                return BadRequest();
            }
            await _university.AddasyncUser(users);
            return CreatedAtRoute(nameof(GetUserByIdAsync), new { id = users.UserId }, users);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutUserAsync(int id, Users users)
        {
            if (users == null || id != users.UserId)
            {
                return BadRequest();
            }
            if (await _university.FindAsyncUser(id) == null)
            {
                return NotFound();
            }
            await _university.UpdateAsyncUser(users);
            return new NoContentResult();
        }
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id) => await _university.RemoveAsyncUser(id);
    }
}
