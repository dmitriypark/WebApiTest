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
    public class QuestionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet("{login}/{password}/{id}")]
        public IEnumerable<Questions> GetQuestions([FromRoute] string login, [FromRoute] string password, [FromRoute] int id)
        {
            var user = _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();
            //var studentsGrade= _context.StudentsGrade.ToList().Where(s => s.User == user.id).FirstOrDefault();
            //var grade = _context.Grade.ToList().Where(g => g.Id == studentsGrade.Grade).FirstOrDefault() ;
            //var subject = _context.Subjects.ToList().Where(s => s.Grade == grade.Id);




            //var subject = _context.Subjects.ToList().Where(s => s.Grade == _context.Grade.ToList().Where(g => g.Id == _context.StudentsGrade.ToList().Where(ss => ss.User == _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault().id).FirstOrDefault().Grade).FirstOrDefault().Id);

           
            var Questions = _context.Questions.ToList().Where(q => q.Test == id);


            if (user != null)
            {
                return Questions;

            }
            else
            {
                return null;
            }

        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questions = await _context.Questions.FindAsync(id);

            if (questions == null)
            {
                return NotFound();
            }

            return Ok(questions);
        }

        // PUT: api/Questions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestions([FromRoute] int id, [FromBody] Questions questions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != questions.Id)
            {
                return BadRequest();
            }

            _context.Entry(questions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionsExists(id))
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

        // POST: api/Questions
        [HttpPost]
        public async Task<IActionResult> PostQuestions([FromBody] Questions questions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Questions.Add(questions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestions", new { id = questions.Id }, questions);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(questions);
            await _context.SaveChangesAsync();

            return Ok(questions);
        }

        private bool QuestionsExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}