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
    public class GradeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GradeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Grade
        [HttpGet]
        public IEnumerable<Grade> GetGrade()
        {
            return _context.Grade;
        }




        //[HttpGet("check/{login}/{password}")]
        //public Grade CheckUser([FromRoute] string login, [FromRoute] string password)
        //{
        //   var user = _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();
           
        //   if (user != null)
        //    {
        //        var srudentGrade = _context.StudentsGrade.ToList().Where(s => s.User == user.id);
                
        //    }
        //}


        // GET: api/Grade/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrade([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var grade = await _context.Grade.FindAsync(id);

            if (grade == null)
            {
                return NotFound();
            }

            return Ok(grade);
        }

        // PUT: api/Grade/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrade([FromRoute] int id, [FromBody] Grade grade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grade.Id)
            {
                return BadRequest();
            }

            _context.Entry(grade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(id))
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

        // POST: api/Grade
        [HttpPost]
        public async Task<IActionResult> PostGrade([FromBody] Grade grade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Grade.Add(grade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrade", new { id = grade.Id }, grade);
        }

        // DELETE: api/Grade/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var grade = await _context.Grade.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            _context.Grade.Remove(grade);
            await _context.SaveChangesAsync();

            return Ok(grade);
        }

        private bool GradeExists(int id)
        {
            return _context.Grade.Any(e => e.Id == id);
        }
    }
}