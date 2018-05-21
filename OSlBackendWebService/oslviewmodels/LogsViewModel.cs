using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSlBackendWebService.oslviewmodels
{
    public class LogsViewModel
    {
        public int TranszactionId { get; set; }
        public int EmpId { get; set; }
        public DateTime Lit { get; set; }
        public DateTime? Lot { get; set; }
        public bool? CheckedSatus { get; set; }
        public string EmployeeName { get; set; }
        
        public string StationName { get; set; }
        public int? SupId { get; set; }

    }
}
