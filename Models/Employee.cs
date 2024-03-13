using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_Dapper.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string JobTitle { get; set; }

        #region Relation
        
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        #endregion
    }
}
