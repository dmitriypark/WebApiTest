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
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Admin
        [HttpGet("{login}/{password}")]
        public IEnumerable<User> GetUser([FromRoute] string login,[FromRoute] string password)
        {
            if (_context.User.Where(u=>u.login==login && u.password==password).FirstOrDefault().roles==4)
            {
                return _context.User;
            }
            else
            {
                return null;
            }
        }



        [HttpPost("{login}/{password}")]
        public async Task<IActionResult> PostUser([FromRoute] string login, [FromRoute] string password,[FromBody] User user)
        {

            if (_context.User.Where(u => u.login == login && u.password == password).FirstOrDefault().roles == 4)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                if (user.id == 0)
                {
                    _context.User.Add(user);
                }
                else
                {
                    _context.Entry(user).State = EntityState.Modified;
                }


                await _context.SaveChangesAsync();

                return Ok(user);
            }
            else
            {
                return null;
            }


           

        }



        // DELETE: api/Admin/5
        [HttpDelete("{login}/{password}/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string login, [FromRoute] string password, [FromRoute] int id)
        {
            if (_context.User.Where(u => u.login == login && u.password == password).FirstOrDefault().roles == 4)
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
            else
            {
                return null;
            }
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.id == id);
        }
    }
}