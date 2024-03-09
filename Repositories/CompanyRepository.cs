using ASP_Dapper.Context;
using ASP_Dapper.Models;
using System.Collections.Generic;
using System.Linq;

namespace ASP_Dapper.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Company AddCompany(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
            return company;
        }

        public void DeleteCompany(int id)
        {
            var company_data = _context.Companies.Find(id);
            if (company_data != null)
            {
                _context.Companies.Remove(company_data); 
                _context.SaveChanges();
            }
        }

        public List<Company> GetAll()
        {
            return _context.Companies.ToList(); 
        }

        public Company GetCompany(int id)
        {
            return _context.Companies.Find(id);
        }

        public Company UpdateCompany(Company company)
        {
            _context.Companies.Update(company);
            _context.SaveChanges();
            return company;
        }
    }
}
