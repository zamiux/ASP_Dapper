using ASP_Dapper.Models;
using System.Collections.Generic;

namespace ASP_Dapper.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);
        List<Employee> GetAll();
        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
    }
}
