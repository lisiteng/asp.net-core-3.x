using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DtoParameters;
using WebApplication1.Entities;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public CompaniesController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies(CompanyDtoParameter companyDtoParameter)
        {
            var companies = await _companyRepository.GetCompaniesAsync(companyDtoParameter);
            var companydtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return Ok(companydtos);
        }

        [HttpGet("{companyId}", Name = nameof(GetCompany))]
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid companyId)
        {
            var company = await _companyRepository.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CompanyDto>(company));
        }
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompany(CreateCompanyDto createCompanyDto)
        {
            var entity = _mapper.Map<Company>(createCompanyDto);
            _companyRepository.AddCompany(entity);
            await _companyRepository.SaveAysnc();

            var returnDto = _mapper.Map<CompanyDto>(entity);
            return CreatedAtRoute(nameof(GetCompany), new { companyId = returnDto.id }, returnDto);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            var company = await _companyRepository.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }
            await _companyRepository.GetEmployeesAsync(companyId, null, null);
            _companyRepository.DeleteCompany(company);
            await _companyRepository.SaveAysnc();
            return NoContent();
        }
    }
}
