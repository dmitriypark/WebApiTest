using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Models
{
    public class HandbookQuestions
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int TestQuantity { get; set; }
        public int TestQuantityPass { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public int GradeNumber { get; set; }
        public string Content { get; set; }
        public int Answer { get; set; }
    }
}
