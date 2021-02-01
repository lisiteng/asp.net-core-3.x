using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
    public class Company
    {
        public Guid id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string introduction { get; set; }
        public ICollection<Employee> employees { get; set; }
    }
}
