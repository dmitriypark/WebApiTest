using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Models
{
    public class HandbookTests
    {
        public int Id { get; set; }
        public int GradeId { get; set; }
        public int GradeNumber { get; set; }
        public string GradeName { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int Quantity { get; set; }
        public int QuantityPass { get; set; }
    }
}
