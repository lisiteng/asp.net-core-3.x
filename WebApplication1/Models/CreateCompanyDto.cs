using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class CreateCompanyDto
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string introduction { get; set; }
        public ICollection<CreateEmployeeDto> employees { get; set; } = new List<CreateEmployeeDto>();
    }
}
