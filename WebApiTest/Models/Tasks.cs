using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public int User { get; set; }
        public int Test { get; set; }
        public int Sum { get; set; }
        public int Pass { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
    }
}
