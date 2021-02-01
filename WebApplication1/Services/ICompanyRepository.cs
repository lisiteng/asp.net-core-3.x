﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DtoParameters;
using WebApplication1.Entities;
using WebApplication1.Helpers;

namespace WebApplication1.Services
{
    public interface ICompanyRepository
    {
        Task<PagedList<Company>> GetCompaniesAsync(CompanyDtoParameter companyDtoParameter);
        Task<Company> GetCompanyAsync(Guid companyId);
        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds);
        void AddCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(Company company);
        Task<bool> CompanyExistsAsync(Guid companyId);

        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, string genderDisplay,string q);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId);
        void AddEmployee(Guid companyId, Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Task<bool> SaveAysnc();
    }
}
