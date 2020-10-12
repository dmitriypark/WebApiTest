using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Models
{
    public class Tests
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Subject { get; set; }
        public int Quantity { get; set; }
        public int QuantityPass { get; set; }
    }
}
