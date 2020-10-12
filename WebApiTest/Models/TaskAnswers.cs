using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Models
{
    public class TaskAnswers
    {
        public int Id { get; set; }
        public int Task { get; set; }
        public int Question { get; set; }
        public int CorrectAnswer { get; set; }
        public int StudentAnswer { get; set; }
        
    }
}
