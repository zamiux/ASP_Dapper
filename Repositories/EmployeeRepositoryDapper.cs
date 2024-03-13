using ASP_Dapper.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ASP_Dapper.Repositories
{
    public class EmployeeRepositoryDapper : IEmployeeRepository
    {
        #region Confige Dapper
        private IDbConnection _db;
        public EmployeeRepositoryDapper(IConfiguration configuration)
        {
            this._db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        #endregion
        public Employee AddEmployee(Employee employee)
        {
            string query = "insert into Employees (Name,Phone,Email,JobTitle,CompanyId) values(@Name,@Phone,@Email,@JobTitle,@CompanyId);"
                + "select CAST(SCOPE_IDENTITY() as int)";

            var id = _db.Query<int>(query, new
            {
                employee.Name,
                employee.Phone,
                employee.Email,
                employee.JobTitle,
                employee.CompanyId
            }).Single();

            employee.EmployeeId = id;
            return employee;
        }

        public void DeleteEmployee(int id)
        {
            string query = "delete from Employees where EmployeeId = @Id";
            _db.Execute(query, new { id });
        }

        public List<Employee> GetAll()
        {
            string quety = "select * from Employees;";
            return _db.Query<Employee>(quety).ToList();
        }

        public Employee GetEmployee(int id)
        {
            string query = $"select * from Employees where EmployeeId = @EmployeeId";
            return _db.Query<Employee>(query, new { EmployeeId = id }).Single();
        }

        public Employee UpdateEmployee(Employee employee)
        {
            string query = "update Employees set Name = @Name, Phone = @Phone, Email = @Email, JobTitle = @JobTitle, CompanyId = @CompanyId where EmployeeId = @EmployeeId";
            _db.Execute(query, employee);
            return employee;
        }
    }
}
