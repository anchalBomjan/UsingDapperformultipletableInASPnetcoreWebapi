﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsingDapperformultipletableInASPnetcoreWebapi.Repository;

namespace UsingDapperformultipletableInASPnetcoreWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {

        private readonly ICompanyRepository _companyRepo;

        public CompaniesController(ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
        }


        [HttpGet]

        public async Task<IActionResult> GetCompanies()
        {

            try
            {
                var companies = await _companyRepo.GetCompanies();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

        }
    }
}
