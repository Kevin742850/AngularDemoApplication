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
    public class DistributionsController : ControllerBase
    {
        private readonly DataContext _context;

        public DistributionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Distributions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Distribution>>> GetDistributions()
        {
            return await _context.Distributions.ToListAsync();
        }

        // GET: api/Distributions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Distribution>> GetDistribution(int id)
        {
            var distribution = await _context.Distributions.FindAsync(id);

            if (distribution == null)
            {
                return NotFound();
            }

            return distribution;
        }

        // PUT: api/Distributions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistribution(int id, Distribution distribution)
        {
            if (id != distribution.Id)
            {
                return BadRequest();
            }

            _context.Entry(distribution).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistributionExists(id))
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

        // POST: api/Distributions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Distribution>> PostDistribution(Distribution distribution)
        {
            _context.Distributions.Add(distribution);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDistribution", new { id = distribution.Id }, distribution);
        }

        // DELETE: api/Distributions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistribution(int id)
        {
            var distribution = await _context.Distributions.FindAsync(id);
            if (distribution == null)
            {
                return NotFound();
            }

            _context.Distributions.Remove(distribution);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DistributionExists(int id)
        {
            return _context.Distributions.Any(e => e.Id == id);
        }
    }
}
