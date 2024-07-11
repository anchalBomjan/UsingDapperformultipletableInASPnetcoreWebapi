using Dapper;
using UsingDapperformultipletableInASPnetcoreWebapi.Data;
using UsingDapperformultipletableInASPnetcoreWebapi.Entities;

namespace UsingDapperformultipletableInASPnetcoreWebapi.Repository
{
    public class CompanyRepository:ICompanyRepository
    {
        private readonly DapperDbContext _Context;
        public CompanyRepository(DapperDbContext context)
        {
             _Context= context;
        }



        public async Task<IEnumerable<Company>> GetCompanies()
        {
            //var query = "SELECT * FROM Companies";
            var query = "SELECT Id, Name AS CompanyName, Address, Country FROM Companies";

            using (var connection = _Context.CreateConnection())
            {
                var companies= await connection.QueryAsync<Company>(query);

                return companies.ToList();
            }

        }
    }
}
