#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularDemoApplication.Data;
using AngularDemoApplication.Models;

namespace AngularDemoApplication.Controllers
{
    [Route("api/Authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthenticateController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Authenticate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Authenticate>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Authenticate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Authenticate>> GetAuthenticate(int id)
        {
            var authenticate = await _context.Users.FindAsync(id);

            if (authenticate == null)
            {
                return NotFound();
            }

            return authenticate;
        }
        [HttpPost("/api/ValidUsers")]
        public async Task<ActionResult<Authenticate>> GetValidUsers(Authenticate user)
        {
            var authenticate = await _context.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefaultAsync();

            if (authenticate == null)
            {
                return NotFound();
            }

            return authenticate;
        }

        // PUT: api/Authenticate/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthenticate(int id, Authenticate authenticate)
        {
            if (id != authenticate.Id)
            {
                return BadRequest();
            }

            _context.Entry(authenticate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthenticateExists(id))
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

        // POST: api/Authenticate
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Authenticate>> PostAuthenticate(Authenticate authenticate)
        {
            _context.Users.Add(authenticate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthenticate", new { id = authenticate.Id }, authenticate);
        }

        // DELETE: api/Authenticate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthenticate(int id)
        {
            var authenticate = await _context.Users.FindAsync(id);
            if (authenticate == null)
            {
                return NotFound();
            }

            _context.Users.Remove(authenticate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthenticateExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
