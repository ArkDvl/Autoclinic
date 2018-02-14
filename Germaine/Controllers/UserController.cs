using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Germaine.Data;
using Germaine.Models;
using Germaine.Dtos;

namespace Germaine.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IGermaineRepository _repo;
        private readonly IAuthRepository _auth;
        private readonly IMapper _mapper;
        public UserController(IAuthRepository auth, IGermaineRepository repo, IMapper mapper)
        {
             _mapper = mapper;
             _repo = repo;
             _auth = auth;
        }

        // GET api/values        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDtos>>(users);

            return Ok(usersToReturn);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            var userToReturn = _mapper.Map<UserForDetailDtos>(user);
            return Ok(userToReturn);
        }

        // GET api/moduleauth/5
        // [HttpGet]
        [Route("api/user/{id}/moduleauth")]
        public async Task<IActionResult> ModuleAuth(int id)
        {
            var user = await _repo.GetUser(id);
            var userToReturn = _mapper.Map<UserForDetailDtos>(user);
            return Ok(userToReturn);
        }

        // POST api/values
        [HttpPost]
        public string Post([FromBody]string value)
        {
            return "Hello!";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        // [Route("password")]        
        public async Task<IActionResult>  ResetPassword(int id, [FromBody] UserForPasswordUpdateDtos user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _repo.GetUser(id);

            if (userFromRepo == null)
                return NotFound($"Could not find user with an ID of {id}");

            if (currentUserId != userFromRepo.UserID)
                return Unauthorized(); 

            // var userToUpdate = new User{
            //     UserID = id
            // };        
            
            _auth.Password(userFromRepo, user.password);
            return Ok();
            // return password;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
