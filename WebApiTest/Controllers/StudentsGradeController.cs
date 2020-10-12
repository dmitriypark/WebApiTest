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
    public class StudentsGradeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsGradeController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        



        [HttpGet("{login}/{password}")]
        public StudentsGrade GetStudentsGrade([FromRoute] string login, [FromRoute] string password)
        {
            var user = _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();

            if (user != null)
            {
                return _context.StudentsGrade.ToList().Where(s => s.User == user.id).FirstOrDefault();

            }
            else
            {
                return null;
            }
            
        }



        // GET: api/StudentsGrade/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentsGrade([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studentsGrade = await _context.StudentsGrade.FindAsync(id);

            if (studentsGrade == null)
            {
                return NotFound();
            }

            return Ok(studentsGrade);
        }

        // PUT: api/StudentsGrade/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentsGrade([FromRoute] int id, [FromBody] StudentsGrade studentsGrade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentsGrade.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentsGrade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentsGradeExists(id))
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

        // POST: api/StudentsGrade
        [HttpPost]
        public async Task<IActionResult> PostStudentsGrade([FromBody] StudentsGrade studentsGrade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.StudentsGrade.Add(studentsGrade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentsGrade", new { id = studentsGrade.Id }, studentsGrade);
        }

        // DELETE: api/StudentsGrade/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentsGrade([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studentsGrade = await _context.StudentsGrade.FindAsync(id);
            if (studentsGrade == null)
            {
                return NotFound();
            }

            _context.StudentsGrade.Remove(studentsGrade);
            await _context.SaveChangesAsync();

            return Ok(studentsGrade);
        }

        private bool StudentsGradeExists(int id)
        {
            return _context.StudentsGrade.Any(e => e.Id == id);
        }
    }
}