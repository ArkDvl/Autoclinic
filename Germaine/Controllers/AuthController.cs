using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Germaine.Data;
using Germaine.Dtos;
using Germaine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Germaine.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly DataContext _context;

        public AuthController(IAuthRepository repo, IConfiguration config, DataContext context)
        {
            _config = config;
            _repo = repo; 
            _context = context;            
        }
        
        // POST api/values
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDtos userlogin)
        {
            // throw new Exception("Computer says no!");
            if(!string.IsNullOrEmpty(userlogin.Username))
                userlogin.Username = userlogin.Username.ToLower();
            
            var userAwait = await _repo.Login(userlogin.Username, userlogin.Password);

            if(userAwait == null)
                return BadRequest("User does not exist!");

            userlogin.IPAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            _repo.ActivityLog(userAwait.UserID, userlogin.IPAddress);
            
            //profile info
            // var profile = await _repo.Profile(userAwait.PersonID);
            //generate tokens
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userAwait.UserID.ToString()),
                    new Claim(ClaimTypes.Name, userAwait.PasswordReset.ToString() ),

                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok( tokenString );
        }

        // POST api/values
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto user)
        {
            
            if(!string.IsNullOrEmpty(user.Username))
                user.Username = user.Username.ToLower();

            if(await _repo.UserExists(user.Username))
               ModelState.AddModelError("Username","Username already exist!");

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            user.Username = user.Username.ToLower();
                        
            var userToCreate = new User
            {
                Username = user.Username
            };

            var createUser = await _repo.Register(userToCreate,user.Password);

            return StatusCode(201);

        }
    }
}