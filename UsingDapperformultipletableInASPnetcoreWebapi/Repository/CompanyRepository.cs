using Dapper;
using Microsoft.Identity.Client;
using UsingDapperformultipletableInASPnetcoreWebapi.Data;
using UsingDapperformultipletableInASPnetcoreWebapi.Entities;

namespace UsingDapperformultipletableInASPnetcoreWebapi.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperDbContext _Context;
        public CompanyRepository(DapperDbContext context)
        {
            _Context = context;
        }



        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT * FROM Companies";
            //var query = "SELECT Id, Name AS CompanyName, Address, Country FROM Companies";

            using (var connection = _Context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);

                return companies.ToList();
            }


        

        }




        public async Task<Company>GetCompany(int id)
        {
            var query = "SELECT * FROM Companies WHERE Id =@Id ";

            using (var connection=_Context.CreateConnection())
            { 
            
                  var company= await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });
                return company;
            }
        }
    }
}
