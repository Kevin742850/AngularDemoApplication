#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StrengthsController : ControllerBase
    {
        private readonly DataContext _context;

        public StrengthsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Strengths
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Strength>>> GetStrength()
        {
            return await _context.Strength.ToListAsync();
        }

        // GET: api/Strengths/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Strength>> GetStrength(int id)
        {
            var strength = await _context.Strength.FindAsync(id);

            if (strength == null)
            {
                return NotFound();
            }

            return strength;
        }

        // PUT: api/Strengths/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStrength(int id, Strength strength)
        {
            if (id != strength.Id)
            {
                return BadRequest();
            }

            _context.Entry(strength).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StrengthExists(id))
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

        // POST: api/Strengths
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Strength>> PostStrength(Strength strength)
        {
            _context.Strength.Add(strength);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStrength", new { id = strength.Id }, strength);
        }

        // DELETE: api/Strengths/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStrength(int id)
        {
            var strength = await _context.Strength.FindAsync(id);
            if (strength == null)
            {
                return NotFound();
            }

            _context.Strength.Remove(strength);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StrengthExists(int id)
        {
            return _context.Strength.Any(e => e.Id == id);
        }
    }
}
