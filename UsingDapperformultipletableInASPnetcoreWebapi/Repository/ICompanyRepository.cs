using UsingDapperformultipletableInASPnetcoreWebapi.Entities;

namespace UsingDapperformultipletableInASPnetcoreWebapi.Repository
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<CompanyDto>> GetCompanies();

        public Task<Company> GetCompany(int id);
        public Task<Company> CreateCompany(CompanyForCreationDto company);

        public Task UpdateCompany(int id,CompanyForUpdateDto company);

        public Task DeleteCompany(int id);

        public Task<Company> GetCompanyByEmployeeId(int id);

        public Task<Company> ByCompanyIdGetAllEmployeeEngagged(int id);



        public Task<List<Company>> GetCompaniesEmployeesMultipleMapping();
    }
}
