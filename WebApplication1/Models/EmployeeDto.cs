using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Models
{
    public class EmployeeDto
    {
        public Guid id { get; set; }
        public Guid companyId { get; set; }
        public string employeeNo { get; set; }
        public string name { get; set; }
        public string genderDisplay { get; set; }
        public int age { get; set; }
    }
}
