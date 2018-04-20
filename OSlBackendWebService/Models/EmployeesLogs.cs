using System;
using System.Collections.Generic;

namespace OSlBackendWebService.Models
{
    public partial class EmployeesLogs
    {
        public int TranszactionId { get; set; }
        public int EmpId { get; set; }
        public DateTime? Lit { get; set; }
        public DateTime? Lot { get; set; }
        public double? Xcoord { get; set; }
        public double? Ycoord { get; set; }
        public DateTime? Qdate { get; set; }
        public bool? CheckedSatus { get; set; }

        public Employees Emp { get; set; }
    }
}
