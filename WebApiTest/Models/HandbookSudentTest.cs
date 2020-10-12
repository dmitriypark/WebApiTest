using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Models
{
    public class HandbookSudentTest
    {
        public int TaskID { get; set; }
        public string QuestionContent { get; set; }
        public int StudentAnswer { get; set; }
        public int CorrectAnswer { get; set; }
    }
}
