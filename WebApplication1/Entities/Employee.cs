using System;

namespace WebApplication1.Entities
{
    public class Employee
    {
        public Guid id { get; set; }
        public Guid companyId { get; set; }
        public string employeeNo { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Gender gender { get; set; }
        public DateTime birthday { get; set; }
        public Company company { get; set; }
    }
}