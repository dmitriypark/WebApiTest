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
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Teacher
        [HttpGet("students/{login}/{password}")]
        public IEnumerable<HandbookStudents> GetUser([FromRoute] string login, [FromRoute] string password)
        {

            if (_context.User.ToList().Where(u => u.login == login && u.password == password).FirstOrDefault().roles == 2)
            {
                var students = from user in _context.User
                               join studentsGrade in _context.StudentsGrade on user.id equals studentsGrade.User into ljStudetnsGrade
                               from studentsGrade in ljStudetnsGrade.DefaultIfEmpty()
                               join grade in _context.Grade on studentsGrade.Grade equals grade.Id into ljGrade
                               from grade in ljGrade.DefaultIfEmpty()
                                   //join subjects in _context.Subjects on grade.Number equals subjects.Grade
                               join tasks in _context.Tasks on user.id equals tasks.User into ljTasks
                               from tasks in ljTasks.DefaultIfEmpty()
                               join tests in _context.Tests on tasks.Test equals tests.Id into ljTests
                               from tests in ljTests.DefaultIfEmpty()
                               join subjects in _context.Subjects on tests.Subject equals subjects.Id into ljSubjects
                               from subjects in ljSubjects.DefaultIfEmpty()
                               select new HandbookStudents
                               {
                                   userId = user.id,
                                   gradeNumberName = grade != null ? grade.Number + grade.Name : " ",
                                   userName = user != null ? user.fullName : " ",
                                   subject = subjects != null ? subjects.Name : " ",
                                   test = tests != null ? tests.Name : " ",
                                   testId =tests!=null? tests.Id : -1,
                                   taskSum = tasks != null ? tasks.Sum : -1,
                                   taskPas = tasks != null ? tasks.Pass : -1,
                                   taskId = tasks != null ? tasks.Id : -1,
                                   taskStart=tasks.Start,
                               };



                return students;
            }
            else
            {
                return null;
            }


            

        }


        // GET: api/Teacher
        [HttpGet("students/{login}/{password}/Test/{userID}/{testId}/{taskId}")]
        public IEnumerable<HandbookSudentTest> GetUserTest([FromRoute] string login, [FromRoute] string password, [FromRoute] int userID,[FromRoute] int testID,[FromRoute] int taskId)
        {
            if (_context.User.ToList().Where(u => u.login == login && u.password == password).FirstOrDefault().roles == 2)
            {
                var test = from task in _context.Tasks where task.Id== taskId
                           where task.User == userID
                           join taskAnswer in _context.TaskAnswers on task.Id equals taskAnswer.Task
                           join question in _context.Questions on taskAnswer.Question equals question.Id where question.Test == testID
                           select new HandbookSudentTest
                           {
                               TaskID = task.Id,
                               QuestionContent = question.Content,
                               StudentAnswer = taskAnswer.StudentAnswer,
                               CorrectAnswer = question.Answer,
                           };
                return test;
            }
            else
            {
                return null;
            }
                          
                      

        }




        // GET: api/Teacher
        [HttpGet ("subject/{login}/{password}")]
        public IEnumerable<HandbookSubjects> GetSubjects([FromRoute] string login, [FromRoute] string password)
        {
            if (_context.User.ToList().Where(u => u.login == login && u.password == password).FirstOrDefault().roles == 2)
            {
                var subjects = from subject in _context.Subjects
                               join grade in _context.Grade on subject.Grade equals grade.Id
                               select new HandbookSubjects
                               {
                                   Id=subject.Id,
                                   Name=subject.Name,
                                   Grade=grade.Id,
                                   GradeNumber=grade.Number,
                                   GradeName=grade.Name,
                               };
                return subjects;
            }
            else
            {
                return null;
            }
        }





        // POST: api/Teacher
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] HandbookSubjects handbookSubjects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Subjects subjects = new Subjects();
            Grade grade = new Grade();

            Grade gradeId = new Grade();
            Subjects subjectId = new Subjects();

            gradeId = _context.Grade.AsNoTracking().Where(g => g.Name == handbookSubjects.GradeName && g.Number == handbookSubjects.GradeNumber).FirstOrDefault();
            subjectId = _context.Subjects.AsNoTracking().Where(s => s.Name == handbookSubjects.Name).FirstOrDefault();


            if (handbookSubjects.Id == 0)
            {

                if (gradeId == null)
                {
                    grade.Name = handbookSubjects.GradeName;
                    grade.Number = handbookSubjects.GradeNumber;
                    _context.Grade.Add(grade);
                    _context.SaveChanges();
                    subjects.Name = handbookSubjects.Name;
                    subjects.Grade = grade.Id;
                    _context.Subjects.Add(subjects);
                }

                else
                {


                    subjects.Name = handbookSubjects.Name;
                    subjects.Grade = gradeId.Id;
                    _context.Subjects.Add(subjects);

                }

            }
            else
            {
                if (gradeId == null)
                {
                    
                    grade.Name = handbookSubjects.GradeName;
                    grade.Number = handbookSubjects.GradeNumber;
                    _context.Grade.Add(grade);
                    _context.SaveChanges();
                    subjects.Name = handbookSubjects.Name;
                    subjects.Grade = grade.Id;
                    subjects.Id = handbookSubjects.Id;
                    _context.Entry(subjects).State = EntityState.Modified;
                }

                else
                {
                    subjects.Name = handbookSubjects.Name;
                    subjects.Grade = handbookSubjects.Grade;
                    subjects.Id = handbookSubjects.Id;
                    try
                    { 
                    _context.Entry(subjects).State = EntityState.Modified;
                    }
                    catch(Exception e)
                    {
                        int ff = 1;
                    }


                    await _context.SaveChangesAsync();
                }





                
            }
            await _context.SaveChangesAsync();

            return Ok(handbookSubjects);
        }

        // DELETE: api/Teacher/5
        [HttpDelete("{login}/{password}/{id}")]
        public async Task<IActionResult> DeleteSubject([FromRoute] string login, [FromRoute] string password,[FromRoute] int id)
        {
            if (_context.User.ToList().Where(u => u.login == login && u.password == password).FirstOrDefault().roles == 2)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var subject = await _context.Subjects.FindAsync(id);
                if (subject == null)
                {
                    return NotFound();
                }

                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();

                return Ok(subject);
            }
            else
            {
                return null;
            }
        }

        // api/teacher
        [HttpGet("Grade/{login}/{password}")]
        public  IEnumerable<Grade> GetGrade([FromRoute] string login, [FromRoute] string password)
        {
            if (_context.User.ToList().Where(u => u.login == login && u.password == password).FirstOrDefault().roles == 2)
            {
                var grades = _context.Grade.ToList() ;
                return grades;
            }
            else
            {
                return null;
            }
        }



        [HttpPost("Grade/{login}/{password}")]
        public async Task<IActionResult> PostGrade([FromBody] Grade grade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (grade.Id == 0)
            {
                _context.Grade.Add(grade);
            }
            else
            {
                _context.Entry(grade).State = EntityState.Modified;
            }


            await _context.SaveChangesAsync();

            return Ok(grade);

        }




        [HttpDelete("Grade/{login}/{password}/{id}")]
        public async Task<IActionResult> DeleteGrade([FromRoute] string login, [FromRoute] string password, [FromRoute] int id)
        {
            if (_context.User.ToList().Where(u => u.login == login && u.password == password).FirstOrDefault().roles == 2)
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
            else
            {
                return null;
            }
        }





        [HttpGet("Test/{login}/{password}")]
        public IEnumerable<HandbookTests> GetTests([FromRoute] string login, [FromRoute] string password)
        {

            if (_context.User.ToList().Where(u => u.login == login && u.password == password).FirstOrDefault().roles == 2)
            {
                var tests = from test in _context.Tests
                            join subject in _context.Subjects on test.Subject equals subject.Id
                            join grade in _context.Grade on subject.Grade equals grade.Id
                            select new HandbookTests
                               {
                                   Id = test.Id,
                                   GradeId=grade.Id,
                                   GradeNumber=grade.Number,
                                   GradeName=grade.Name,
                                   Name = test.Name,
                                   SubjectId=subject.Id,
                                   SubjectName=subject.Name,
                                   Quantity=test.Quantity,
                                   QuantityPass=test.QuantityPass
                               };



                return tests;
            }
            else
            {
                return null;
            }




        }


        [HttpPost("Test/{login}/{password}")]
        public async Task<IActionResult> PostTest([FromBody] Tests test)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (test.Id == 0)
            {
                _context.Tests.Add(test);
            }
            else
            {
                _context.Entry(test).State = EntityState.Modified;
            }


            await _context.SaveChangesAsync();

            return Ok(test);

        }



        [HttpDelete("Test/{login}/{password}/{id}")]
        public async Task<IActionResult> DeleteTest([FromRoute] string login, [FromRoute] string password, [FromRoute] int id)
        {
            if (_context.User.ToList().Where(u => u.login == login && u.password == password).FirstOrDefault().roles == 2)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var test = await _context.Tests.FindAsync(id);
                if (test == null)
                {
                    return NotFound();
                }

                _context.Tests.Remove(test);
                await _context.SaveChangesAsync();

                return Ok(test);
            }
            else
            {
                return null;
            }
        }


        [HttpGet("Question/{login}/{password}")]
        public IEnumerable<HandbookQuestions> GetQuestions([FromRoute] string login, [FromRoute] string password)
        {

            if (_context.User.ToList().Where(u => u.login == login && u.password == password).FirstOrDefault().roles == 2)
            {
                var questions = from question in _context.Questions
                            join test in _context.Tests on question.Test equals test.Id
                            join subject in _context.Subjects on test.Subject equals subject.Id
                            join grade in _context.Grade on subject.Grade equals grade.Id
                            
                            select new HandbookQuestions
                            {
                                Id = question.Id,
                                TestId = test.Id,
                                TestName=test.Name,
                                TestQuantity=test.Quantity,
                                TestQuantityPass=test.QuantityPass,
                                SubjectId=subject.Id,
                                SubjectName=subject.Name,
                                GradeId=grade.Id,
                                GradeName=grade.Name,
                                GradeNumber=grade.Number,
                                Content=question.Content,
                                Answer=question.Answer
                            };



                return questions;
            }
            else
            {
                return null;
            }








        }



        [HttpPost("Question/{login}/{password}")]
        public async Task<IActionResult> PostQuestion([FromBody] Questions question)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (question.Id == 0)
            {
                _context.Questions.Add(question);
            }
            else
            {
                _context.Entry(question).State = EntityState.Modified;
            }


            await _context.SaveChangesAsync();

            return Ok(question);

        }



        [HttpDelete("Question/{login}/{password}/{id}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] string login, [FromRoute] string password, [FromRoute] int id)
        {
            if (_context.User.ToList().Where(u => u.login == login && u.password == password).FirstOrDefault().roles == 2)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var question = await _context.Questions.FindAsync(id);
                if (question == null)
                {
                    return NotFound();
                }

                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();

                return Ok(question);
            }
            else
            {
                return null;
            }
        }






        //    private bool UserExists(int id)
        //    {
        //        return _context.User.Any(e => e.id == id);
        //    }


        //    // PUT: api/User/5
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        if (id != user.id)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(user).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

    }




}

