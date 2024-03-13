using ASP_Dapper.Models;
using System.Collections.Generic;

namespace ASP_Dapper.Repositories
{
    public interface IBonusRepository
    {
        List<Employee> GetEmployeesWithCompany(int id = 0);
    }
}
