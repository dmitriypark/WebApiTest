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
    public class SubjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        



        [HttpGet("{login}/{password}")]
        public IEnumerable<Subjects> GetSubjects([FromRoute] string login, [FromRoute] string password)
        {
            //var user = _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();
            //var studentsGrade= _context.StudentsGrade.ToList().Where(s => s.User == user.id).FirstOrDefault();
            //var grade = _context.Grade.ToList().Where(g => g.Id == studentsGrade.Grade).FirstOrDefault() ;
            //var subject = _context.Subjects.ToList().Where(s => s.Grade == grade.Id);


           
            
            var subject = _context.Subjects.ToList().Where(s => s.Grade == _context.Grade.ToList().Where(g => g.Id == _context.StudentsGrade.ToList().Where(ss => ss.User == _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault().id).FirstOrDefault().Grade).FirstOrDefault().Id);




            if (subject != null)
            {
                return subject;
                
            }
            else
            {
                return null;
            }

        }



        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjects([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subjects = await _context.Subjects.FindAsync(id);

            if (subjects == null)
            {
                return NotFound();
            }

            return Ok(subjects);
        }

        // PUT: api/Subjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubjects([FromRoute] int id, [FromBody] Subjects subjects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subjects.Id)
            {
                return BadRequest();
            }

            _context.Entry(subjects).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectsExists(id))
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

        // POST: api/Subjects
        [HttpPost]
        public async Task<IActionResult> PostSubjects([FromBody] Subjects subjects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Subjects.Add(subjects);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubjects", new { id = subjects.Id }, subjects);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubjects([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subjects = await _context.Subjects.FindAsync(id);
            if (subjects == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subjects);
            await _context.SaveChangesAsync();

            return Ok(subjects);
        }

        private bool SubjectsExists(int id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }
    }
}