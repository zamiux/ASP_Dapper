using ASP_Dapper.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ASP_Dapper.Repositories
{
    public class CompanyRepositoryDapperSP : ICompanyRepository
    {
        #region Confige Dapper
        private IDbConnection _db;
        public CompanyRepositoryDapperSP(IConfiguration configuration)
        {
            this._db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        #endregion
        public Company AddCompany(Company company)
        {
            var parameters = new DynamicParameters();
            //output parameter
            parameters.Add("@CompanyId", 0, DbType.Int32,direction: ParameterDirection.Output);

            // input parameters
            parameters.Add("@Name", company.Name);
            parameters.Add("@Address", company.Address);
            parameters.Add("@City", company.City);
            parameters.Add("@State", company.State);
            parameters.Add("@PostalCode", company.PostalCode);

            _db.Execute("usp_AddCompany",parameters,commandType:CommandType.StoredProcedure);

            company.CompanyId = parameters.Get<int>("CompanyId");
            return company;
        }

        public void DeleteCompany(int id)
        {
            _db.Execute("usp_DeleteCompany", new { CompanyId = id }, commandType: CommandType.StoredProcedure);
        }

        public List<Company> GetAll()
        {
            return _db.Query<Company>("usp_GetAllCompany",commandType:CommandType.StoredProcedure).ToList();
        }

        public Company GetCompany(int id)
        {
            
            return _db.Query<Company>("usp_GetCompany",new {CompanyId = id}, commandType: CommandType.StoredProcedure).Single();
        }

        public Company UpdateCompany(Company company)
        {
            var parameters = new DynamicParameters();
            //output parameter
            parameters.Add("@CompanyId", company.CompanyId, DbType.Int32);

            // input parameters
            parameters.Add("@Name", company.Name);
            parameters.Add("@Address", company.Address);
            parameters.Add("@City", company.City);
            parameters.Add("@State", company.State);
            parameters.Add("@PostalCode", company.PostalCode);

            _db.Execute("usp_UpdateCompany", parameters, commandType: CommandType.StoredProcedure);

            return company;
        }
    }
}
