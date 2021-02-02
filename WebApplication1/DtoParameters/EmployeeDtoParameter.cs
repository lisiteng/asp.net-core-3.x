using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DtoParameters
{
    public class EmployeeDtoParameter
    {
        public string gender { get; set; }
        public string q { get; set; }
        public int pageNumber { get; set; } = 1;
        private int _pageSize;

        public int pageSize
        {
            get => _pageSize;
            set => _pageSize = (value > 20) ? 20 : value;
        }
        public string orderBy { get; set; } = "name";
    }
}
