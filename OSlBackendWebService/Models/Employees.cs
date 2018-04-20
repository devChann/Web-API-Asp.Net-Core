using System;
using System.Collections.Generic;

namespace OSlBackendWebService.Models
{
    public partial class Employees
    {
        public Employees()
        {
            EmployeesLogs = new HashSet<EmployeesLogs>();
        }

        public int EmpId { get; set; }
        public int? Telephone { get; set; }
        public int? Password { get; set; }
        public string Name { get; set; }
        public int? StationId { get; set; }

        public Stations Station { get; set; }
        public ICollection<EmployeesLogs> EmployeesLogs { get; set; }
    }
}
