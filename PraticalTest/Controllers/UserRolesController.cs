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
    [Route("api/UserRoles")]
    public class UserRolesController : Controller
    {
        private readonly SalesContext _context;

        public UserRolesController(SalesContext context)
        {
            _context = context;
        }

        // GET: api/UserRoles
        [HttpGet]
        public IEnumerable<UserRole> GetUserRole()
        {
            return _context.UserRole;
        }

        // GET: api/UserRoles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userRole = await _context.UserRole.SingleOrDefaultAsync(m => m.Id == id);

            if (userRole == null)
            {
                return NotFound();
            }

            return Ok(userRole);
        }

        // PUT: api/UserRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRole([FromRoute] int id, [FromBody] UserRole userRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(userRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRoleExists(id))
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

        // POST: api/UserRoles
        [HttpPost]
        public async Task<IActionResult> PostUserRole([FromBody] UserRole userRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserRole.Add(userRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserRole", new { id = userRole.Id }, userRole);
        }

        // DELETE: api/UserRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userRole = await _context.UserRole.SingleOrDefaultAsync(m => m.Id == id);
            if (userRole == null)
            {
                return NotFound();
            }

            _context.UserRole.Remove(userRole);
            await _context.SaveChangesAsync();

            return Ok(userRole);
        }

        private bool UserRoleExists(int id)
        {
            return _context.UserRole.Any(e => e.Id == id);
        }
    }
}