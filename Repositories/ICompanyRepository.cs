using ASP_Dapper.Models;
using System.Collections.Generic;

namespace ASP_Dapper.Repositories
{
    public interface ICompanyRepository
    {
        Company GetCompany(int id);
        List<Company> GetAll();
        Company AddCompany(Company company);
        Company UpdateCompany(Company company);
        void DeleteCompany(int id);
    }
}
