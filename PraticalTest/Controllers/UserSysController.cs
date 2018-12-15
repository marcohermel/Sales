using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PraticalTest.Models;

namespace PraticalTest.Controllers
{
    [Produces("application/json")]
    [Route("api/UserSys")]
    public class UserSysController : Controller
    {
        private readonly SalesContext _context;

        public UserSysController(SalesContext context)
        {
            _context = context;
        }

        // GET: api/UserSys
        [HttpGet]
        public IEnumerable<UserSys> GetUserSys()
        {
            return _context.UserSys;
        }

        // GET: api/UserSys/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserSys([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userSys = await _context.UserSys.SingleOrDefaultAsync(m => m.Id == id);

            if (userSys == null)
            {
                return NotFound();
            }

            return Ok(userSys);
        }

        // PUT: api/UserSys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSys([FromRoute] int id, [FromBody] UserSys userSys)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userSys.Id)
            {
                return BadRequest();
            }

            _context.Entry(userSys).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSysExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserSys
        [HttpPost]
        public async Task<IActionResult> PostUserSys([FromBody] UserSys userSys)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserSys.Add(userSys);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserSys", new { id = userSys.Id }, userSys);
        }

        // DELETE: api/UserSys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSys([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userSys = await _context.UserSys.SingleOrDefaultAsync(m => m.Id == id);
            if (userSys == null)
            {
                return NotFound();
            }

            _context.UserSys.Remove(userSys);
            await _context.SaveChangesAsync();

            return Ok(userSys);
        }

        private bool UserSysExists(int id)
        {
            return _context.UserSys.Any(e => e.Id == id);
        }
    }
}