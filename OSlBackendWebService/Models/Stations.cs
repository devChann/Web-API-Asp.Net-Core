using System;
using System.Collections.Generic;

namespace OSlBackendWebService.Models
{
    public partial class Stations
    {
        public Stations()
        {
            Employees = new HashSet<Employees>();
        }

        public int Stid { get; set; }
        public string Region { get; set; }
        public string StationName { get; set; }
        public int? SupId { get; set; }

        public Supervisors Sup { get; set; }
        public ICollection<Employees> Employees { get; set; }
    }
}
