using OSlBackendWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSlBackendWebService.oslviewmodels
{
    public class EmployeesLogsViewModel
    {
        
        public string StationName { get; set; }
       
        public IEnumerable<Employees> employees { get; set; }
      
    }
}
