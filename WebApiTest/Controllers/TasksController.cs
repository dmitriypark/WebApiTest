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
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet("{login}/{password}")]
        public IEnumerable<Tasks> GetTasks([FromRoute] string login, [FromRoute] string password)
        {
            //var user = _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();
            //var studentsGrade= _context.StudentsGrade.ToList().Where(s => s.User == user.id).FirstOrDefault();
            //var grade = _context.Grade.ToList().Where(g => g.Id == studentsGrade.Grade).FirstOrDefault() ;
            //var subject = _context.Subjects.ToList().Where(s => s.Grade == grade.Id);
            var user = _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();
            var tasks = _context.Tasks.ToList().Where(t => t.User == user.id);


            




            if (user != null)
            {
                return tasks;

            }
            else
            {
                return null;
            }

        }





        [HttpGet("{login}/{password}/TaskName")]
        public IEnumerable<Tests> GetTasksName([FromRoute] string login, [FromRoute] string password)
        {
            //var user = _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();
            //var studentsGrade= _context.StudentsGrade.ToList().Where(s => s.User == user.id).FirstOrDefault();
            //var grade = _context.Grade.ToList().Where(g => g.Id == studentsGrade.Grade).FirstOrDefault() ;
            //var subject = _context.Subjects.ToList().Where(s => s.Grade == grade.Id);
            List<Tests> tests=new List<Tests>() ;
            var user = _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();

            
            var tasks = _context.Tasks.ToList().Where(t => t.User == user.id);
            foreach (var task in tasks)
            {
                var test= _context.Tests.ToList().Where(t => t.Id == task.Test).FirstOrDefault();
                tests.Add(test);
            }







            if (user != null)
            {
                return tests;

            }
            else
            {
                return null;
            }

        }



        [HttpGet("{login}/{password}/{id}")]
        public Tasks GetTasksId([FromRoute] string login, [FromRoute] string password, [FromRoute] int id)
        {
           
            var user = _context.User.ToList().Where(l => l.login == login && l.password == password).FirstOrDefault();
            var task = _context.Tasks.ToList().Where(t => t.Id == id && t.User==user.id).FirstOrDefault();

            







            if (user != null)
            {
                return task;

            }
            else
            {
                return null;
            }

        }





        // POST: api/Tasks asdasdsad
        [HttpPost]
        public async Task<Tasks> PostTasks([FromBody] Tasks tasks)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
            }

            _context.Tasks.Add(tasks);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetTasks", new { id = tasks.Id }, tasks);
            return tasks;
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasks([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();

            return Ok(tasks);
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}