using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowingsController : ControllerBase
    {
        private readonly ShowingContext _context;

        public ShowingsController(ShowingContext context)
        {
            _context = context;
        }

        // GET: api/Showings
        [HttpGet]
        public IEnumerable<Showing> GetShowing()
        {
            return _context.Showing;
        }

        // GET: api/Showings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShowing([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var showing = await _context.Showing.FindAsync(id);

            if (showing == null)
            {
                return NotFound();
            }

            return Ok(showing);
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShowing([FromRoute] string id, [FromBody] Showing showing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != showing.Id)
            {
                return BadRequest();
            }

            _context.Entry(showing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowingExists(id))
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

        // POST: api/Comments
        [HttpPost]
        public async Task<IActionResult> PostShowing([FromBody] Showing showing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Showing.Add(showing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShowing", new { id = showing.Id }, showing);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var showing = await _context.Showing.FindAsync(id);
            if (showing == null)
            {
                return NotFound();
            }

            _context.Showing.Remove(showing);
            await _context.SaveChangesAsync();

            return Ok(showing);
        }

        private bool ShowingExists(string id)
        {
            return _context.Showing.Any(e => e.Id == id);
        }
    }
}