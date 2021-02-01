using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DtoParameters
{
    public class CompanyDtoParameter
    {
        public string companyName { get; set; }
        public string searchTerm { get; set; }

        public int pageNumber { get; set; } = 1;
        private int _pageSize = 5;

        public int pageSize
        {
            get => _pageSize;
            set => _pageSize = (value > 20) ? 20 : _pageSize;
        }

    }
}
