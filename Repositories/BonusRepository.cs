using ASP_Dapper.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ASP_Dapper.Repositories
{
    public class BonusRepository : IBonusRepository
    {
        #region Confige Dapper
        private IDbConnection _db;
        public BonusRepository(IConfiguration configuration)
        {
            this._db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        #endregion

        public List<Employee> GetEmployeesWithCompany(int id = 0)
        {
            var query = "SELECT E.*,C.* From Companies AS E INNER JOIN Employees AS C WHERE E.CompanyId = C.CompanyId";
            if (id != 0)
            {
                query += query + " AND E.CompanyId = @Id";
            }

            var employees = _db.Query<Employee, Company, Employee>(sql: query, (e, c) =>
            {
                e.Company = c;
                return e;
            }, new { id }, splitOn: "CompanyId");


            return employees.ToList();
        }
    }
}
