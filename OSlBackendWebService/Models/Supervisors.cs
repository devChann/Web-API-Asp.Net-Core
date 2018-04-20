using System;
using System.Collections.Generic;

namespace OSlBackendWebService.Models
{
    public partial class Supervisors
    {
        public Supervisors()
        {
            Checkings = new HashSet<Checkings>();
            Employees = new HashSet<Employees>();
            Stations = new HashSet<Stations>();
        }

        public int SupId { get; set; }
        public int? EmpId { get; set; }
        public string Phone { get; set; }
        public int? Password { get; set; }

        public ICollection<Checkings> Checkings { get; set; }
        public ICollection<Employees> Employees { get; set; }
        public ICollection<Stations> Stations { get; set; }
    }
}
