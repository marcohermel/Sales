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
    [Route("api/Genders")]
    public class GendersController : Controller
    {
        private readonly SalesContext _context;

        public GendersController(SalesContext context)
        {
            _context = context;
        }

        // GET: api/Genders
        [HttpGet]
        public IEnumerable<Gender> GetGender()
        {
            return _context.Gender;
        }

        // GET: api/Genders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGender([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gender = await _context.Gender.SingleOrDefaultAsync(m => m.Id == id);

            if (gender == null)
            {
                return NotFound();
            }

            return Ok(gender);
        }

        // PUT: api/Genders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGender([FromRoute] int id, [FromBody] Gender gender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gender.Id)
            {
                return BadRequest();
            }

            _context.Entry(gender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenderExists(id))
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

        // POST: api/Genders
        [HttpPost]
        public async Task<IActionResult> PostGender([FromBody] Gender gender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Gender.Add(gender);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGender", new { id = gender.Id }, gender);
        }

        // DELETE: api/Genders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGender([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gender = await _context.Gender.SingleOrDefaultAsync(m => m.Id == id);
            if (gender == null)
            {
                return NotFound();
            }

            _context.Gender.Remove(gender);
            await _context.SaveChangesAsync();

            return Ok(gender);
        }

        private bool GenderExists(int id)
        {
            return _context.Gender.Any(e => e.Id == id);
        }
    }
}