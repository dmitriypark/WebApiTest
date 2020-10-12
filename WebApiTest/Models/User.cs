using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Models
{
    public class User
    {
        public int id { get; set; }
        public string login { get; set; }
        public string fullName { get; set; }
        public int roles { get; set; }
        public string password { get; set; }
    }
}
