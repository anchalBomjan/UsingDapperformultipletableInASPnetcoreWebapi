using Dapper;
using Microsoft.Identity.Client;
using System.Data;
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



        public async Task<IEnumerable<CompanyDto>> GetCompanies()
        {
            var query = "SELECT * FROM Companies";
            //var query = "SELECT Id, Name AS CompanyName, Address, Country FROM Companies";

            using (var connection = _Context.CreateConnection())
            {
                var companies = await connection.QueryAsync<CompanyDto>(query);

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


        public async Task<Company> CreateCompany(CompanyForCreationDto company)
        {
            var query = "INSERT INTO Companies(Name,Address,Country)VALUES(@Name,@Adress,@Country)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Adress",company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);



            using (var connection = _Context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdCompany = new Company
                {
                    Id = id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country
                };
                return createdCompany;
            }
        }


        public async Task UpdateCompany(int id, CompanyForUpdateDto company)
        {
            var query = "UPDATE Companies SET Name=@Name,Address=@Address,Country=@Country WHERE Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);
            using (var connection = _Context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }


        public async Task DeleteCompany(int id)
        {
            var query = "DELETE FROM Companies WHERE Id=@Id";

            using(var connection = _Context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }


        public async Task <Company>GetCompanyByEmployeeId(int id)
        {
            var procedureName = "ShowCompanyForProvidedEmployeeId";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            using (var connection = _Context.CreateConnection())
            {
                var company = await connection.QueryFirstOrDefaultAsync<Company>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return company;
            }
        }

        //Executing Multiple SQL Statements with a Single Query
        public async Task<Company> ByCompanyIdGetAllEmployeeEngagged(int id)
        {
            var query = "SELECT * FROM Companies WHERE Id = @Id;" +
              "SELECT * FROM Employees WHERE CompanyId = @Id";

            using(var connection= _Context.CreateConnection())
                using(var multi=await connection.QueryMultipleAsync(query, new { id }))
            {
                var company = await multi.ReadSingleOrDefaultAsync<Company>();
                if (company != null)
                    company.Employees = (await multi.ReadAsync<Employee>()).ToList();



                return company;
                
            }
        }


        public async Task<List<Company>> GetCompaniesEmployeesMultipleMapping()
        {
            var query = "SELECT * FROM Companies c JOIN Employees e ON c.Id = e.CompanyId";
            using (var connection = _Context.CreateConnection())
            {
                var companyDict = new Dictionary<int, Company>();
                var companies = await connection.QueryAsync<Company, Employee, Company>(
                    query, (company, employee) =>
                    {
                        if (!companyDict.TryGetValue(company.Id, out var currentCompany))
                        {
                            currentCompany = company;
                            companyDict.Add(currentCompany.Id, currentCompany);
                        }
                        currentCompany.Employees.Add(employee);
                        return currentCompany;
                    }
                );
                return companies.Distinct().ToList();
            }
        }

        
    }
}
