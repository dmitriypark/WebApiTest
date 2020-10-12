using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet("{login}/{password}")]
        public IEnumerable<User> GetUser([FromRoute] string login, [FromRoute] string password)
        {
            if (_context.User.ToList().Where(u=>u.login==login && u.password==password).FirstOrDefault().roles==2)
            {
                return _context.User.ToList().Where(u=>u.roles==1);
            }
            else
            {
                return null;
            }
            
        }

        [HttpGet("checkTeacher/{login}/{password}")]
        public User CheckTeacher([FromRoute] string login, [FromRoute] string password)
        {
            if (_context.User.ToList().Where(u => u.login == login && u.password == password).FirstOrDefault().roles == 2)
            {
                return _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }


        [HttpGet("check/{login}/{password}")]
        public User CheckUser([FromRoute] string login,[FromRoute] string password)
        {

            return _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();
        }
        
        


        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/User
        [HttpPost]
        public async Task<User> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetUser", new { id = user.id }, user);
            return user;
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.id == id);
        }
    }
}