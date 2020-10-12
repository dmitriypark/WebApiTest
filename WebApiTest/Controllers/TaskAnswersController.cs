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
    public class TaskAnswersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskAnswersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskAnswers
        [HttpGet("{login}/{password}/{id}")]
        public IEnumerable<TaskAnswers> GetTaskAnswers([FromRoute] string login, [FromRoute] string password, [FromRoute] int id)
        {
            //var user = _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();
            //var studentsGrade= _context.StudentsGrade.ToList().Where(s => s.User == user.id).FirstOrDefault();
            //var grade = _context.Grade.ToList().Where(g => g.Id == studentsGrade.Grade).FirstOrDefault() ;
            //var subject = _context.Subjects.ToList().Where(s => s.Grade == grade.Id);
            var user = _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();
            var tasks = _context.Tasks.ToList().Where(t => t.User == user.id && t.Id == id).FirstOrDefault();
            var taskAnswers = _context.TaskAnswers.ToList().Where(t => t.Task ==tasks.Id );







            if (user != null)
            {
                return taskAnswers;

            }
            else
            {
                return null;
            }

        }

        [HttpGet("questionContent/{login}/{password}/{id}")]
        public IEnumerable<Questions> GetTaskAnswersQuestion([FromRoute] string login, [FromRoute] string password, [FromRoute] int id)
        {
            //var user = _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();
            //var studentsGrade= _context.StudentsGrade.ToList().Where(s => s.User == user.id).FirstOrDefault();
            //var grade = _context.Grade.ToList().Where(g => g.Id == studentsGrade.Grade).FirstOrDefault() ;
            //var subject = _context.Subjects.ToList().Where(s => s.Grade == grade.Id);
            var user = _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();
            var tasks = _context.Tasks.ToList().Where(t => t.User == user.id && t.Id == id).FirstOrDefault();
            var taskAnswers = _context.TaskAnswers.ToList().Where(t => t.Task == tasks.Id);
            List<Questions> questions = new List<Questions>();
            foreach (var taskAnswer in taskAnswers)
            {
                var question = _context.Questions.ToList().Where(q => q.Id == taskAnswer.Question).FirstOrDefault();
                questions.Add(question);
            }







            if (user != null)
            {
                return questions;

            }
            else
            {
                return null;
            }

        }






        // POST: api/TaskAnswers
        [HttpPost]
        public async Task<IActionResult> PostTaskAnswers([FromBody] TaskAnswers taskAnswers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TaskAnswers.Add(taskAnswers);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetTaskAnswers", new { id = taskAnswers.Id }, taskAnswers);
        }

        // DELETE: api/TaskAnswers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAnswers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taskAnswers = await _context.TaskAnswers.FindAsync(id);
            if (taskAnswers == null)
            {
                return NotFound();
            }

            _context.TaskAnswers.Remove(taskAnswers);
            await _context.SaveChangesAsync();

            return Ok(taskAnswers);
        }

        private bool TaskAnswersExists(int id)
        {
            return _context.TaskAnswers.Any(e => e.Id == id);
        }
    }
}