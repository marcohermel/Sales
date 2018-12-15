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
    [Route("api/Classifications")]
    public class ClassificationsController : Controller
    {
        private readonly SalesContext _context;

        public ClassificationsController(SalesContext context)
        {
            _context = context;
        }

        // GET: api/Classifications
        [HttpGet]
        public IEnumerable<Classification> GetClassification()
        {
            return _context.Classification;
        }

        // GET: api/Classifications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassification([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var classification = await _context.Classification.SingleOrDefaultAsync(m => m.Id == id);

            if (classification == null)
            {
                return NotFound();
            }

            return Ok(classification);
        }

        // PUT: api/Classifications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassification([FromRoute] int id, [FromBody] Classification classification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != classification.Id)
            {
                return BadRequest();
            }

            _context.Entry(classification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassificationExists(id))
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

        // POST: api/Classifications
        [HttpPost]
        public async Task<IActionResult> PostClassification([FromBody] Classification classification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Classification.Add(classification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassification", new { id = classification.Id }, classification);
        }

        // DELETE: api/Classifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassification([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var classification = await _context.Classification.SingleOrDefaultAsync(m => m.Id == id);
            if (classification == null)
            {
                return NotFound();
            }

            _context.Classification.Remove(classification);
            await _context.SaveChangesAsync();

            return Ok(classification);
        }

        private bool ClassificationExists(int id)
        {
            return _context.Classification.Any(e => e.Id == id);
        }
    }
}