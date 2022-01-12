#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace PharmacyManagementSystem.Controllers
{
    [Authorize]
    [Route("api/Authenticate")]
    [ApiController]

    public class AuthenticateController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthenticateController(DataContext context)
        {
            _context = context;
        }
        //[AllowAnonymous]
        //[HttpPost("/api/token")]
        //public ActionResult GetToken()
        //{
        //    string securityKey = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
        //    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

        //    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        //    var claims = new List<Claim>();
        //    //claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
        //    //claims.Add(new Claim(ClaimTypes.Role, "Reader"));
        //    claims.Add(new Claim("Our_Custom_Claim", "Our custom value"));
        //    claims.Add(new Claim("Id", "120"));


        //    var token = new JwtSecurityToken(
        //            issuer: "ali",
        //            audience: "readers",
        //            expires: DateTime.Now.AddHours(1),
        //            signingCredentials: signingCredentials
        //            , claims: claims
        //        );

        //    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        //}

        [AllowAnonymous]
        [HttpPost("/api/token")]
        public ActionResult<string> CreateToken()
        {
            return GetToken();
        }

        private string GetToken()
        {
            var tokenResponse = new TokenResponse();
            string jwtToken = string.Empty;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
                IConfiguration configuration = builder.Build();
                string JWTTokenKey = configuration.GetValue<string>("AppSettings:JwtSecret");

                var key = Encoding.ASCII.GetBytes(JWTTokenKey);
                var encKey = Encoding.ASCII.GetBytes(JWTTokenKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                         {
                                   new Claim(ClaimTypes.NameIdentifier, "1"),
                                   new Claim(ClaimTypes.Name, "1"),
                                   new Claim("id", "1540"),
                         })
                     ,
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                    EncryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encKey), JwtConstants.DirectKeyUseAlg, SecurityAlgorithms.Aes256CbcHmacSha512),

                };


                var token = tokenHandler.CreateToken(tokenDescriptor);
                jwtToken = tokenHandler.WriteToken(token);
                tokenResponse.Token = jwtToken;


            }
            catch (AggregateException aggExc)
            {
                foreach (Exception exc in aggExc.Flatten().InnerExceptions)
                {
                }

                return null;
            }
            catch (Exception exc)
            {
                return null;
            }
            finally
            {
                jwtToken = null;
            }
            var t = JsonConvert.SerializeObject(tokenResponse.Token);
            return t;
        }


        [HttpGet("/api/get-my-id")]
        public ActionResult<string> GetMyId()
        {


            string authHeader = HttpContext.Request.Headers["Authorization"];

            if (!string.IsNullOrWhiteSpace(authHeader))
            {
                // Get the token from the Authorization header 
                var token = authHeader.Replace("Bearer ", "");

                //var token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJPdXJfQ3VzdG9tX0NsYWltIjoiT3VyIGN1c3RvbSB2YWx1ZSIsIklkIjoiMTEwIiwiZXhwIjoxNjQxMzc4NzQ4LCJpc3MiOiJhbGkiLCJhdWQiOiJyZWFkZXJzIn0.FuReOAKspGYmGadFZ1SS-0W592DYdmCd_kfbK0vB9GQ";
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var tokenS = handler.ReadToken(token) as JwtSecurityToken;


                var key = Encoding.ASCII.GetBytes("401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1");
                var tokenSecure = handler.ReadToken(token) as SecurityToken;
                var validations = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    TokenDecryptionKey = new SymmetricSecurityKey(key),
                };
                var claims = handler.ValidateToken(token, validations, out tokenSecure);

                var id = claims.Claims.FirstOrDefault(x => x.Type == "id")?.Value ?? "";

                //if (principal is ClaimsPrincipal claims)
                //{
                //    return new ApplicationDTO
                //    {
                //        Id = claims.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? "",
                //        UserName = claims.Claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value ?? "",
                //        Email = claims.Claims.FirstOrDefault(x => x.Type == "email")?.Value ?? ""
                //    };
                //}




                if (id != null)
                {
                    return Ok($"This is your Id: {id}");
                }
            }
            return BadRequest("No claim");
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

        [AllowAnonymous]
        [HttpPost("/api/ValidUsers")]
        public async Task<ActionResult<string>> GetValidUsers(Authenticate user)
        {
            var authenticate = await _context.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefaultAsync();

            if (authenticate == null)
            {
                return NotFound();
            }
            return GetToken();
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

    public class TokenResponse
    {
        public string ErrorDescription { get; set; }
        public string Token { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
