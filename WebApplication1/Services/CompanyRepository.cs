using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.DtoParameters;
using WebApplication1.Entities;
using WebApplication1.Helpers;

namespace WebApplication1.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataDbContext _context;

        public CompanyRepository(DataDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            company.id = Guid.NewGuid();
            if (company.employees != null)
            {
                foreach (var employee in company.employees)
                {
                    employee.id = Guid.NewGuid();
                }
            }
            _context.companies.Add(company);
        }

        public void AddEmployee(Guid companyId, Employee employee)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            employee.companyId = companyId;
            employee.id = Guid.NewGuid();
            _context.employees.Add(employee);
        }

        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.companies.AnyAsync(x => x.id == companyId);
        }

        public void DeleteCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            _context.companies.Remove(company);
        }

        public void DeleteEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            _context.employees.Remove(employee);
        }

        public async Task<PagedList<Company>> GetCompaniesAsync(CompanyDtoParameter companyDtoParameter)
        {
            if (companyDtoParameter == null)
            {
                throw new ArgumentNullException(nameof(companyDtoParameter));
            }
            var queryExpression = _context.companies as IQueryable<Company>;
            if (!string.IsNullOrWhiteSpace(companyDtoParameter.companyName))
            {
                companyDtoParameter.companyName = companyDtoParameter.companyName.Trim();
                queryExpression = queryExpression.Where(x => x.name == companyDtoParameter.companyName);
            }
            if (!string.IsNullOrWhiteSpace(companyDtoParameter.searchTerm))
            {
                companyDtoParameter.searchTerm = companyDtoParameter.searchTerm.Trim();
                queryExpression = queryExpression.Where(x => x.name.Contains(companyDtoParameter.searchTerm) || x.introduction.Contains(companyDtoParameter.searchTerm));
            }
            return await PagedList<Company>.CreateAsync(queryExpression, companyDtoParameter.pageNumber, companyDtoParameter.pageSize);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds)
        {
            if (companyIds == null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }
            return await _context.companies.Where(x => companyIds.Contains(x.id)).OrderBy(x => x.name).ToListAsync();
        }

        public async Task<Company> GetCompanyAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.companies.FirstOrDefaultAsync(x => x.id == companyId);
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }
            return await _context.employees.Where(x => x.companyId == companyId && x.id == employeeId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, EmployeeDtoParameter employeeDtoParameter)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            var items = _context.employees.Where(x => x.companyId == companyId);
            if (!string.IsNullOrWhiteSpace(employeeDtoParameter.gender))
            {
                employeeDtoParameter.gender = employeeDtoParameter.gender.Trim();
                var gender = Enum.Parse<Gender>(employeeDtoParameter.gender);
                items = items.Where(x => x.gender == gender);
            }
            if (!string.IsNullOrWhiteSpace(employeeDtoParameter.q))
            {
                employeeDtoParameter.q = employeeDtoParameter.q.Trim();
                items = items.Where(x => x.employeeNo.Contains(employeeDtoParameter.q) || x.firstName.Contains(employeeDtoParameter.q) || x.lastName.Contains(employeeDtoParameter.q));
            }
            if (!string.IsNullOrWhiteSpace(employeeDtoParameter.orderBy))
            {
                if (employeeDtoParameter.orderBy.ToLowerInvariant() == "name")
                {
                    items = items.OrderBy(x => x.firstName).ThenBy(x => x.lastName);
                }
            }
            return await items.ToListAsync();
        }

        public async Task<bool> SaveAysnc()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public void UpdateCompany(Company company)
        {
        }

        public void UpdateEmployee(Employee employee)
        {
        }
    }
}
