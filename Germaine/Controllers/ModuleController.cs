using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Germaine.Data;
using Germaine.Models;
using Germaine.Dtos;
using Newtonsoft.Json.Linq;

namespace Germaine.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ModuleController : Controller
    {
        
        private readonly DataContext _context;
        public ModuleController(DataContext context)
        {
            _context = context;
        }

        // GET api/Module        
        [HttpGet]
        public async Task<IActionResult> GetModule()
        {
            var Module = await _context.Modules.ToListAsync();
            return Ok(Module);
        }

        // GET api/Module/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Modules.FirstOrDefaultAsync(x => x.ModulesID == id);

            return Ok(value);
        }

        // POST api/Module
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserForModuleAuthDtos user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

                //run authentication for whether a user can actually has access to the module
            
            var userInfo = await _context.Users.FirstOrDefaultAsync(x => x.UserID == user.UserID);

            // // JObject mID = new JObject();

            // var mars = JArray.Parse(userInfo.Modules);
            // var int red[] = mars;
            return Ok(userInfo);
            
        }
        

        // PUT api/Module/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Module/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
