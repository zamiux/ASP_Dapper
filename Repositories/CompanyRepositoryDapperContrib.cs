using ASP_Dapper.Models;
using Dapper;
using Dapper.Contrib.Extensions;
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
    public class CompanyRepositoryDapperContrib : ICompanyRepository
    {
        #region Confige Dapper
        private IDbConnection _db;
        public CompanyRepositoryDapperContrib(IConfiguration configuration)
        {
            this._db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        #endregion
        public Company AddCompany(Company company)
        {
            var id = _db.Insert(company);
            company.CompanyId = (int)id;
            return company;
        }

        public void DeleteCompany(int id)
        {
            _db.Delete(new Company() { CompanyId = id });
        }

        public List<Company> GetAll()
        {
            return _db.GetAll<Company>().ToList();
        }

        public Company GetCompany(int id)
        {

            return _db.Get<Company>(id);    
        }

        public Company UpdateCompany(Company company)
        {
            _db.Update(company);
            return company;
        }
    }
}
