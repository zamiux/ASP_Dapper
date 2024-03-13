using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace ASP_Dapper.Models
{
    [Table("Companies")]
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        #region Relations

        //dapper contrib, in yani dar query niazi be CRUD nadarad.
        [Write(false)]
        public List<Employee> Employees { get; set; }
        #endregion
    }
}
