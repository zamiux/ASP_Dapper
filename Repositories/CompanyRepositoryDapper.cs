using ASP_Dapper.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ASP_Dapper.Repositories
{
    public class CompanyRepositoryDapper : ICompanyRepository
    {
        #region Confige Dapper
        private IDbConnection _db;
        public CompanyRepositoryDapper(IConfiguration configuration)
        {
            this._db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        #endregion
        public Company AddCompany(Company company)
        {
            string query = "insert into companies (Name,Address,City,State,PostalCode) values(@Name,@Address,@City,@State,@PostalCode);"
                + "select CAST(SCOPE_IDENTITY() as int)";

            var id = _db.Query<int>(query, new
            {
                company.Name,
                company.Address,
                company.City,
                company.State,
                company.PostalCode
            }).Single();

            company.CompanyId = id;
            return company; 
        }

        public void DeleteCompany(int id)
        {
            string query = "delete from companies where CompanyId = @Id";
            _db.Execute(query, new { id });
        }

        public List<Company> GetAll()
        {
            string quety = "select * from companies;";
            return _db.Query<Company>(quety).ToList();
        }

        public Company GetCompany(int id)
        {
            string query = $"select * from companies where CompanyId = @CompanyId";
            return _db.Query<Company>(query,new { @CompanyId = id }).Single();
        }

        public Company UpdateCompany(Company company)
        {
            string query = "update companies set Name = @Name, Address = @Address, City = @City, State = @State, PostalCode = @PostalCode where CompanyId = @CompanyId";
            _db.Execute(query, company);
            return company;
        }
    }
}
