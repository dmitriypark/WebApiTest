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
    public class TestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tests
        
        [HttpGet("{login}/{password}/{id}")]
        public IEnumerable<Tests> GetTests([FromRoute] string login, [FromRoute] string password, [FromRoute] int id)
        {
            var user = _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();
            //var studentsGrade= _context.StudentsGrade.ToList().Where(s => s.User == user.id).FirstOrDefault();
            //var grade = _context.Grade.ToList().Where(g => g.Id == studentsGrade.Grade).FirstOrDefault() ;
            //var subject = _context.Subjects.ToList().Where(s => s.Grade == grade.Id);

            


            //var subject = _context.Subjects.ToList().Where(s => s.Grade == _context.Grade.ToList().Where(g => g.Id == _context.StudentsGrade.ToList().Where(ss => ss.User == _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault().id).FirstOrDefault().Grade).FirstOrDefault().Id);

            var test = _context.Tests.ToList().Where(t => t.Subject == id);



            if (user != null)
            {
                return test;

            }
            else
            {
                return null;
            }

        }
    

        // GET: api/Tests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTests([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tests = await _context.Tests.FindAsync(id);

            if (tests == null)
            {
                return NotFound();
            }

            return Ok(tests);
        }

        // PUT: api/Tests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTests([FromRoute] int id, [FromBody] Tests tests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tests.Id)
            {
                return BadRequest();
            }

            _context.Entry(tests).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestsExists(id))
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

        // POST: api/Tests
        [HttpPost]
        public async Task<IActionResult> PostTests([FromBody] Tests tests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tests.Add(tests);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTests", new { id = tests.Id }, tests);
        }

        // DELETE: api/Tests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTests([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tests = await _context.Tests.FindAsync(id);
            if (tests == null)
            {
                return NotFound();
            }

            _context.Tests.Remove(tests);
            await _context.SaveChangesAsync();

            return Ok(tests);
        }

        private bool TestsExists(int id)
        {
            return _context.Tests.Any(e => e.Id == id);
        }
    }
}